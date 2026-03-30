namespace WindowsFormsApp1
{
    partial class Main_Page
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.checkedListBoxPackages = new System.Windows.Forms.CheckedListBox();
            this.textBoxDependencies = new System.Windows.Forms.TextBox();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSelectFolder
            // 
            this.buttonSelectFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.buttonSelectFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSelectFolder.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.Size = new System.Drawing.Size(120, 30);
            this.buttonSelectFolder.TabIndex = 0;
            this.buttonSelectFolder.Text = "Select Folder";
            this.buttonSelectFolder.UseVisualStyleBackColor = false;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // checkBoxSelectAll
            // 
            this.checkBoxSelectAll.AutoSize = true;
            this.checkBoxSelectAll.Location = new System.Drawing.Point(12, 48);
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.Size = new System.Drawing.Size(101, 24);
            this.checkBoxSelectAll.TabIndex = 1;
            this.checkBoxSelectAll.Text = "Select All";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.Visible = false;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // checkedListBoxPackages
            // 
            this.checkedListBoxPackages.FormattingEnabled = true;
            this.checkedListBoxPackages.Location = new System.Drawing.Point(12, 71);
            this.checkedListBoxPackages.Name = "checkedListBoxPackages";
            this.checkedListBoxPackages.Size = new System.Drawing.Size(350, 349);
            this.checkedListBoxPackages.TabIndex = 2;
            this.checkedListBoxPackages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxPackages_ItemCheck);
            // 
            // textBoxDependencies
            // 
            this.textBoxDependencies.BackColor = System.Drawing.Color.Linen;
            this.textBoxDependencies.Location = new System.Drawing.Point(380, 71);
            this.textBoxDependencies.Multiline = true;
            this.textBoxDependencies.Name = "textBoxDependencies";
            this.textBoxDependencies.ReadOnly = true;
            this.textBoxDependencies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDependencies.Size = new System.Drawing.Size(400, 364);
            this.textBoxDependencies.TabIndex = 3;
            // 
            // buttonCopy
            // 
            this.buttonCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCopy.Location = new System.Drawing.Point(380, 441);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(80, 30);
            this.buttonCopy.TabIndex = 4;
            this.buttonCopy.Text = "Copy";
            this.buttonCopy.UseVisualStyleBackColor = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Dependencies ";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(846, 507);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.textBoxDependencies);
            this.Controls.Add(this.checkedListBoxPackages);
            this.Controls.Add(this.checkBoxSelectAll);
            this.Controls.Add(this.buttonSelectFolder);
            this.Name = "Form1";
            this.Text = "NuGet Package Dependency Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectFolder;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.CheckedListBox checkedListBoxPackages;
        private System.Windows.Forms.TextBox textBoxDependencies;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label label1;
    }
}
