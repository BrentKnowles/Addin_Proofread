// fAdminGrid.Designer.cs
//
// Copyright (c) 2013 Brent Knowles (http://www.brentknowles.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Review documentation at http://www.yourothermind.com for updated implementation notes, license updates
// or other general information/
// 
// Author information available at http://www.brentknowles.com or http://www.amazon.com/Brent-Knowles/e/B0035WW7OW
// Full source code: https://github.com/BrentKnowles/YourOtherMind
//###
namespace WriteThinker
{
    partial class fAdminGrid
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonGrade = new System.Windows.Forms.Button();
            this.buttonOpenGradeFile = new System.Windows.Forms.Button();
            this.buttonSaveGrade = new System.Windows.Forms.Button();
            this.buttonRules = new System.Windows.Forms.Button();
            this.buttonDialog = new System.Windows.Forms.Button();
            this.bInput = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.bFilterView = new System.Windows.Forms.Button();
            this.columnFilter = new System.Windows.Forms.TextBox();
            this.comboColumn = new System.Windows.Forms.ComboBox();
            this.bShowStats = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textFilter = new System.Windows.Forms.TextBox();
            this.textOutput = new System.Windows.Forms.TextBox();
            this.bSql = new System.Windows.Forms.Button();
            this.sql = new System.Windows.Forms.TextBox();
            this.labeltimer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(785, 393);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 393);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labeltimer);
            this.panel2.Controls.Add(this.buttonGrade);
            this.panel2.Controls.Add(this.buttonOpenGradeFile);
            this.panel2.Controls.Add(this.buttonSaveGrade);
            this.panel2.Controls.Add(this.buttonRules);
            this.panel2.Controls.Add(this.buttonDialog);
            this.panel2.Controls.Add(this.bInput);
            this.panel2.Controls.Add(this.textBoxInput);
            this.panel2.Controls.Add(this.bFilterView);
            this.panel2.Controls.Add(this.columnFilter);
            this.panel2.Controls.Add(this.comboColumn);
            this.panel2.Controls.Add(this.bShowStats);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textFilter);
            this.panel2.Controls.Add(this.textOutput);
            this.panel2.Controls.Add(this.bSql);
            this.panel2.Controls.Add(this.sql);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(785, 133);
            this.panel2.TabIndex = 2;
            // 
            // buttonGrade
            // 
            this.buttonGrade.Location = new System.Drawing.Point(657, 61);
            this.buttonGrade.Name = "buttonGrade";
            this.buttonGrade.Size = new System.Drawing.Size(75, 23);
            this.buttonGrade.TabIndex = 15;
            this.buttonGrade.Text = "Grade";
            this.buttonGrade.UseVisualStyleBackColor = true;
            this.buttonGrade.Click += new System.EventHandler(this.buttonGrade_Click);
            // 
            // buttonOpenGradeFile
            // 
            this.buttonOpenGradeFile.Location = new System.Drawing.Point(557, 61);
            this.buttonOpenGradeFile.Name = "buttonOpenGradeFile";
            this.buttonOpenGradeFile.Size = new System.Drawing.Size(106, 23);
            this.buttonOpenGradeFile.TabIndex = 14;
            this.buttonOpenGradeFile.Text = "Open Grade File";
            this.buttonOpenGradeFile.UseVisualStyleBackColor = true;
            this.buttonOpenGradeFile.Click += new System.EventHandler(this.buttonOpenGradeFile_Click);
            // 
            // buttonSaveGrade
            // 
            this.buttonSaveGrade.Location = new System.Drawing.Point(460, 61);
            this.buttonSaveGrade.Name = "buttonSaveGrade";
            this.buttonSaveGrade.Size = new System.Drawing.Size(102, 23);
            this.buttonSaveGrade.TabIndex = 13;
            this.buttonSaveGrade.Text = "SaveGradeFile";
            this.buttonSaveGrade.UseVisualStyleBackColor = true;
            this.buttonSaveGrade.Click += new System.EventHandler(this.buttonSaveGrade_Click);
            // 
            // buttonRules
            // 
            this.buttonRules.Location = new System.Drawing.Point(460, 32);
            this.buttonRules.Name = "buttonRules";
            this.buttonRules.Size = new System.Drawing.Size(75, 23);
            this.buttonRules.TabIndex = 12;
            this.buttonRules.Text = "View Rules";
            this.buttonRules.UseVisualStyleBackColor = true;
            this.buttonRules.Click += new System.EventHandler(this.buttonRules_Click);
            // 
            // buttonDialog
            // 
            this.buttonDialog.Location = new System.Drawing.Point(557, 8);
            this.buttonDialog.Name = "buttonDialog";
            this.buttonDialog.Size = new System.Drawing.Size(75, 23);
            this.buttonDialog.TabIndex = 11;
            this.buttonDialog.Text = "Dialog Review";
            this.buttonDialog.UseVisualStyleBackColor = true;
            this.buttonDialog.Click += new System.EventHandler(this.buttonDialog_Click);
            // 
            // bInput
            // 
            this.bInput.Location = new System.Drawing.Point(358, 103);
            this.bInput.Name = "bInput";
            this.bInput.Size = new System.Drawing.Size(75, 23);
            this.bInput.TabIndex = 10;
            this.bInput.Text = "Input";
            this.bInput.UseVisualStyleBackColor = true;
            this.bInput.Click += new System.EventHandler(this.bInput_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(449, 100);
            this.textBoxInput.MaxLength = 3276700;
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(208, 28);
            this.textBoxInput.TabIndex = 9;
            // 
            // bFilterView
            // 
            this.bFilterView.Location = new System.Drawing.Point(248, 75);
            this.bFilterView.Name = "bFilterView";
            this.bFilterView.Size = new System.Drawing.Size(75, 23);
            this.bFilterView.TabIndex = 8;
            this.bFilterView.Text = "Filter View";
            this.bFilterView.UseVisualStyleBackColor = true;
            this.bFilterView.Click += new System.EventHandler(this.bFilterView_Click);
            // 
            // columnFilter
            // 
            this.columnFilter.Location = new System.Drawing.Point(142, 75);
            this.columnFilter.Name = "columnFilter";
            this.columnFilter.Size = new System.Drawing.Size(100, 20);
            this.columnFilter.TabIndex = 7;
            // 
            // comboColumn
            // 
            this.comboColumn.FormattingEnabled = true;
            this.comboColumn.Location = new System.Drawing.Point(14, 75);
            this.comboColumn.Name = "comboColumn";
            this.comboColumn.Size = new System.Drawing.Size(121, 21);
            this.comboColumn.TabIndex = 6;
            this.comboColumn.DropDown += new System.EventHandler(this.comboColumn_DropDown);
            // 
            // bShowStats
            // 
            this.bShowStats.Location = new System.Drawing.Point(460, 8);
            this.bShowStats.Name = "bShowStats";
            this.bShowStats.Size = new System.Drawing.Size(102, 23);
            this.bShowStats.TabIndex = 5;
            this.bShowStats.Text = "Stats";
            this.bShowStats.UseVisualStyleBackColor = true;
            this.bShowStats.Click += new System.EventHandler(this.bShowStats_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Filter";
            // 
            // textFilter
            // 
            this.textFilter.Location = new System.Drawing.Point(66, 34);
            this.textFilter.Name = "textFilter";
            this.textFilter.Size = new System.Drawing.Size(176, 20);
            this.textFilter.TabIndex = 3;
            // 
            // textOutput
            // 
            this.textOutput.Location = new System.Drawing.Point(348, 11);
            this.textOutput.Multiline = true;
            this.textOutput.Name = "textOutput";
            this.textOutput.Size = new System.Drawing.Size(106, 87);
            this.textOutput.TabIndex = 2;
            // 
            // bSql
            // 
            this.bSql.Location = new System.Drawing.Point(248, 31);
            this.bSql.Name = "bSql";
            this.bSql.Size = new System.Drawing.Size(87, 23);
            this.bSql.TabIndex = 1;
            this.bSql.Text = "ComputeSQL";
            this.bSql.UseVisualStyleBackColor = true;
            this.bSql.Click += new System.EventHandler(this.bSql_Click);
            // 
            // sql
            // 
            this.sql.Location = new System.Drawing.Point(11, 11);
            this.sql.Name = "sql";
            this.sql.Size = new System.Drawing.Size(324, 20);
            this.sql.TabIndex = 0;
            this.sql.Text = "Count(ID)";
            // 
            // labeltimer
            // 
            this.labeltimer.AutoSize = true;
            this.labeltimer.Location = new System.Drawing.Point(288, 108);
            this.labeltimer.Name = "labeltimer";
            this.labeltimer.Size = new System.Drawing.Size(35, 13);
            this.labeltimer.TabIndex = 16;
            this.labeltimer.Text = "label2";
            // 
            // fAdminGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 526);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fAdminGrid";
            this.Text = "fAdminGrid";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button bSql;
        private System.Windows.Forms.TextBox sql;
        private System.Windows.Forms.TextBox textOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFilter;
        private System.Windows.Forms.Button bShowStats;
        private System.Windows.Forms.Button bFilterView;
        private System.Windows.Forms.TextBox columnFilter;
        private System.Windows.Forms.ComboBox comboColumn;
        private System.Windows.Forms.Button bInput;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonDialog;
        private System.Windows.Forms.Button buttonRules;
        private System.Windows.Forms.Button buttonSaveGrade;
        private System.Windows.Forms.Button buttonOpenGradeFile;
        private System.Windows.Forms.Button buttonGrade;
        public System.Windows.Forms.Label labeltimer;
    }
}