using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Telerik.WinControls.Zip.Extensions;

namespace WindowsFormsApp1
{
    public partial class Main_Page : Form
    {
        private string selectedFolder;
        private string extractFolder;
        private Dictionary<string, List<string>> packageDependencies = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        private bool suppressItemCheckEvent = false; // to avoid recursion on check/uncheck all

        public Main_Page()
        {
            InitializeComponent();
            checkBoxSelectAll.Visible = false;
        }

        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select folder containing .nupkg files";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = dlg.SelectedPath;
                    extractFolder = Path.Combine(selectedFolder, "Extracted");

                    if (Directory.Exists(extractFolder))
                        Directory.Delete(extractFolder, true);

                    LoadPackagesAndDependencies(selectedFolder);
                    checkBoxSelectAll.Checked = false;
                }
            }
        }

        private void LoadPackagesAndDependencies(string folder)
        {
            checkedListBoxPackages.Items.Clear();
            textBoxDependencies.Clear();
            packageDependencies.Clear();

            //var nupkgFiles = Directory.GetFiles(folder, "*.nupkg");
            string[] nupkgFiles;
            try
            {
                nupkgFiles = Directory.GetFiles(folder, "*.nupkg");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading folder: " + ex.Message);
                checkBoxSelectAll.Visible = false; // Hide on error
                return;
            }

            if (nupkgFiles.Length == 0)
            {
                MessageBox.Show("No .nupkg files found in selected folder.");
                checkBoxSelectAll.Visible = false; // Hide if no packages
                return;
            }
            foreach (var nupkgPath in nupkgFiles)
            {
                string packageName = Path.GetFileName(nupkgPath);

                string extractPath = Path.Combine(extractFolder, Path.GetFileNameWithoutExtension(nupkgPath));
                if (Directory.Exists(extractPath))
                    Directory.Delete(extractPath, true);

                ZipFile.ExtractToDirectory(nupkgPath, extractPath);

                var nuspecFile = Directory.GetFiles(extractPath, "*.nuspec").FirstOrDefault();
                if (nuspecFile == null)
                {
                    packageDependencies[packageName] = new List<string> { "No .nuspec file found." };
                    checkedListBoxPackages.Items.Add(packageName, false);
                    continue;
                }

                var deps = ParseDependencies(nuspecFile);

                packageDependencies[packageName] = deps;

                checkedListBoxPackages.Items.Add(packageName, false);
            }
            checkBoxSelectAll.Visible = true;
        }

        private List<string> ParseDependencies(string nuspecPath)
        {
            try
            {
                XDocument doc = XDocument.Load(nuspecPath);
                XNamespace ns = doc.Root.GetDefaultNamespace();

                var dependencyElements = doc.Descendants(ns + "dependencies")
                                            .Descendants(ns + "dependency");

                var deps = new List<string>();

                foreach (var dep in dependencyElements)
                {
                    var id = dep.Attribute("id")?.Value;
                    var version = dep.Attribute("version")?.Value;
                    if (!string.IsNullOrEmpty(id))
                    {
                        deps.Add(string.IsNullOrEmpty(version) ? id : $"{id} ({version})");
                    }
                }

                return deps.Count > 0 ? deps.Distinct().ToList() : new List<string> { "No dependencies found." };
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error parsing dependencies: {ex.Message}" };
            }
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkedListBoxPackages.Items.Count == 0)
                return;

            suppressItemCheckEvent = true;

            bool check = checkBoxSelectAll.Checked;
            for (int i = 0; i < checkedListBoxPackages.Items.Count; i++)
            {
                checkedListBoxPackages.SetItemChecked(i, check);
            }

            suppressItemCheckEvent = false;

            UpdateDependenciesTextBox();
        }

        private void checkedListBoxPackages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (suppressItemCheckEvent)
                return;

            // Because the event fires BEFORE the check state changes, we need to delay update to after state changes
            this.BeginInvoke(new Action(() =>
            {
                // Update the Select All checkbox state
                int checkedCount = checkedListBoxPackages.CheckedItems.Count;
                if (e.NewValue == CheckState.Checked)
                {
                    checkedCount++;
                }

                if (e.NewValue == CheckState.Unchecked)
                {
                    checkedCount--;
                }

                if (checkedCount == checkedListBoxPackages.Items.Count)
                {
                    checkBoxSelectAll.CheckedChanged -= checkBoxSelectAll_CheckedChanged;
                    checkBoxSelectAll.Checked = true;
                    checkBoxSelectAll.CheckedChanged += checkBoxSelectAll_CheckedChanged;
                }
                else
                {
                    checkBoxSelectAll.CheckedChanged -= checkBoxSelectAll_CheckedChanged;
                    checkBoxSelectAll.Checked = false;
                    checkBoxSelectAll.CheckedChanged += checkBoxSelectAll_CheckedChanged;
                }

                UpdateDependenciesTextBox();
            }));
        }

        private void UpdateDependenciesTextBox()
        {
            var checkedPackages = checkedListBoxPackages.CheckedItems.Cast<string>().ToList();

            if (checkedPackages.Count == 0)
            {
                textBoxDependencies.Clear();
                return;
            }

            var combinedDependencies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var pkg in checkedPackages)
            {
                if (packageDependencies.TryGetValue(pkg, out var deps))
                {
                    foreach (var d in deps)
                        combinedDependencies.Add(d);
                }
            }

            var depsList = combinedDependencies.ToList();
            depsList.Sort();

            textBoxDependencies.Lines = depsList.ToArray();
        }
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxDependencies.Text))
            {
                Clipboard.SetText(textBoxDependencies.Text);
                MessageBox.Show("Dependencies copied to clipboard.", "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No dependencies to copy.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
