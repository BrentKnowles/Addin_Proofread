namespace WriteThinker
{
    partial class fStatPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statPanel1 = new WriteThinker.statPanel();
            this.labelNoText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // statPanel1
            // 
            this.statPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statPanel1.Location = new System.Drawing.Point(0, 0);
            this.statPanel1.Name = "statPanel1";
            this.statPanel1.Size = new System.Drawing.Size(366, 394);
            this.statPanel1.TabIndex = 0;
            // 
            // labelNoText
            // 
            this.labelNoText.AutoSize = true;
            this.labelNoText.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelNoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoText.Location = new System.Drawing.Point(0, 0);
            this.labelNoText.Name = "labelNoText";
            this.labelNoText.Padding = new System.Windows.Forms.Padding(10);
            this.labelNoText.Size = new System.Drawing.Size(204, 44);
            this.labelNoText.TabIndex = 1;
            this.labelNoText.Text = "The note had no text";
            this.labelNoText.Visible = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(280, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fStatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 394);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelNoText);
            this.Controls.Add(this.statPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fStatPanel";
            this.Text = "Statistics";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public statPanel statPanel1;
        public System.Windows.Forms.Label labelNoText;
        private System.Windows.Forms.Button button1;

    }
}