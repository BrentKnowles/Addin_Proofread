// statPanel.Designer.cs
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
    partial class statPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.labelMinLine = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelpercentdialog = new System.Windows.Forms.Label();
            this.labelnumsentences = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelwords = new System.Windows.Forms.Label();
            this.groupSentences = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelavgwordsper = new System.Windows.Forms.Label();
            this.labelavgsyl = new System.Windows.Forms.Label();
            this.labellongestsentence = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupWords = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelpercentdialogwords = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.groupRead = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.labelpassive = new System.Windows.Forms.Label();
            this.labelgrade = new System.Windows.Forms.Label();
            this.labelreadingease = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupSentences.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupWords.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupRead.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shortest Sentence (words)";
            // 
            // labelMinLine
            // 
            this.labelMinLine.AutoSize = true;
            this.labelMinLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMinLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinLine.Location = new System.Drawing.Point(0, 39);
            this.labelMinLine.Name = "labelMinLine";
            this.labelMinLine.Size = new System.Drawing.Size(39, 13);
            this.labelMinLine.TabIndex = 1;
            this.labelMinLine.Text = "minline";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "# of Sentences";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "% Dialog";
            // 
            // labelpercentdialog
            // 
            this.labelpercentdialog.AutoSize = true;
            this.labelpercentdialog.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelpercentdialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpercentdialog.Location = new System.Drawing.Point(0, 13);
            this.labelpercentdialog.Name = "labelpercentdialog";
            this.labelpercentdialog.Size = new System.Drawing.Size(71, 13);
            this.labelpercentdialog.TabIndex = 5;
            this.labelpercentdialog.Text = "percentdialog";
            // 
            // labelnumsentences
            // 
            this.labelnumsentences.AutoSize = true;
            this.labelnumsentences.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelnumsentences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelnumsentences.Location = new System.Drawing.Point(0, 0);
            this.labelnumsentences.Name = "labelnumsentences";
            this.labelnumsentences.Size = new System.Drawing.Size(53, 13);
            this.labelnumsentences.TabIndex = 4;
            this.labelnumsentences.Text = "numsente";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Word Count";
            // 
            // labelwords
            // 
            this.labelwords.AutoSize = true;
            this.labelwords.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelwords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelwords.Location = new System.Drawing.Point(0, 13);
            this.labelwords.Name = "labelwords";
            this.labelwords.Size = new System.Drawing.Size(35, 13);
            this.labelwords.TabIndex = 7;
            this.labelwords.Text = "label5";
            // 
            // groupSentences
            // 
            this.groupSentences.Controls.Add(this.panel2);
            this.groupSentences.Controls.Add(this.panel1);
            this.groupSentences.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSentences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupSentences.Location = new System.Drawing.Point(0, 0);
            this.groupSentences.Name = "groupSentences";
            this.groupSentences.Size = new System.Drawing.Size(423, 139);
            this.groupSentences.TabIndex = 8;
            this.groupSentences.TabStop = false;
            this.groupSentences.Text = "Sentences";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelavgwordsper);
            this.panel2.Controls.Add(this.labelavgsyl);
            this.panel2.Controls.Add(this.labelMinLine);
            this.panel2.Controls.Add(this.labellongestsentence);
            this.panel2.Controls.Add(this.labelpercentdialog);
            this.panel2.Controls.Add(this.labelnumsentences);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(165, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 120);
            this.panel2.TabIndex = 10;
            // 
            // labelavgwordsper
            // 
            this.labelavgwordsper.AutoSize = true;
            this.labelavgwordsper.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelavgwordsper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelavgwordsper.Location = new System.Drawing.Point(0, 65);
            this.labelavgwordsper.Name = "labelavgwordsper";
            this.labelavgwordsper.Size = new System.Drawing.Size(90, 13);
            this.labelavgwordsper.TabIndex = 10;
            this.labelavgwordsper.Text = "labelavgwordsper";
            // 
            // labelavgsyl
            // 
            this.labelavgsyl.AutoSize = true;
            this.labelavgsyl.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelavgsyl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelavgsyl.Location = new System.Drawing.Point(0, 52);
            this.labelavgsyl.Name = "labelavgsyl";
            this.labelavgsyl.Size = new System.Drawing.Size(59, 13);
            this.labelavgsyl.TabIndex = 9;
            this.labelavgsyl.Text = "labelavgsyl";
            // 
            // labellongestsentence
            // 
            this.labellongestsentence.AutoSize = true;
            this.labellongestsentence.Dock = System.Windows.Forms.DockStyle.Top;
            this.labellongestsentence.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labellongestsentence.Location = new System.Drawing.Point(0, 26);
            this.labellongestsentence.Name = "labellongestsentence";
            this.labellongestsentence.Size = new System.Drawing.Size(41, 13);
            this.labellongestsentence.TabIndex = 8;
            this.labellongestsentence.Text = "longest";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 120);
            this.panel1.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(0, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Average Words per";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Average Syllables";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Longest Sentence (words)";
            // 
            // groupWords
            // 
            this.groupWords.Controls.Add(this.panel4);
            this.groupWords.Controls.Add(this.panel3);
            this.groupWords.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupWords.Location = new System.Drawing.Point(0, 139);
            this.groupWords.Name = "groupWords";
            this.groupWords.Size = new System.Drawing.Size(423, 100);
            this.groupWords.TabIndex = 9;
            this.groupWords.TabStop = false;
            this.groupWords.Text = "Words";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labelwords);
            this.panel4.Controls.Add(this.labelpercentdialogwords);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(165, 16);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(255, 81);
            this.panel4.TabIndex = 10;
            // 
            // labelpercentdialogwords
            // 
            this.labelpercentdialogwords.AutoSize = true;
            this.labelpercentdialogwords.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelpercentdialogwords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpercentdialogwords.Location = new System.Drawing.Point(0, 0);
            this.labelpercentdialogwords.Name = "labelpercentdialogwords";
            this.labelpercentdialogwords.Size = new System.Drawing.Size(41, 13);
            this.labelpercentdialogwords.TabIndex = 8;
            this.labelpercentdialogwords.Text = "label12";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(162, 81);
            this.panel3.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "% Dialog";
            // 
            // groupRead
            // 
            this.groupRead.Controls.Add(this.panel6);
            this.groupRead.Controls.Add(this.panel5);
            this.groupRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupRead.Location = new System.Drawing.Point(0, 239);
            this.groupRead.Name = "groupRead";
            this.groupRead.Size = new System.Drawing.Size(423, 82);
            this.groupRead.TabIndex = 10;
            this.groupRead.TabStop = false;
            this.groupRead.Text = "Readability";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.labelpassive);
            this.panel6.Controls.Add(this.labelgrade);
            this.panel6.Controls.Add(this.labelreadingease);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(165, 16);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(255, 63);
            this.panel6.TabIndex = 9;
            // 
            // labelpassive
            // 
            this.labelpassive.AutoSize = true;
            this.labelpassive.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelpassive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelpassive.Location = new System.Drawing.Point(0, 26);
            this.labelpassive.Name = "labelpassive";
            this.labelpassive.Size = new System.Drawing.Size(41, 13);
            this.labelpassive.TabIndex = 4;
            this.labelpassive.Text = "label11";
            // 
            // labelgrade
            // 
            this.labelgrade.AutoSize = true;
            this.labelgrade.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelgrade.Location = new System.Drawing.Point(0, 13);
            this.labelgrade.Name = "labelgrade";
            this.labelgrade.Size = new System.Drawing.Size(41, 13);
            this.labelgrade.TabIndex = 5;
            this.labelgrade.Text = "label11";
            // 
            // labelreadingease
            // 
            this.labelreadingease.AutoSize = true;
            this.labelreadingease.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelreadingease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelreadingease.Location = new System.Drawing.Point(0, 0);
            this.labelreadingease.Name = "labelreadingease";
            this.labelreadingease.Size = new System.Drawing.Size(35, 13);
            this.labelreadingease.TabIndex = 1;
            this.labelreadingease.Text = "label8";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(162, 63);
            this.panel5.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(0, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Passive Sentences";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Flesch Grade";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Flesch Reading Ease";
            // 
            // statPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupRead);
            this.Controls.Add(this.groupWords);
            this.Controls.Add(this.groupSentences);
            this.Name = "statPanel";
            this.Size = new System.Drawing.Size(423, 321);
            this.groupSentences.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupWords.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupRead.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label labelMinLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label labelpercentdialog;
        public System.Windows.Forms.Label labelnumsentences;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label labelwords;
        private System.Windows.Forms.GroupBox groupSentences;
        private System.Windows.Forms.GroupBox groupWords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupRead;
        public System.Windows.Forms.Label labelreadingease;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label labellongestsentence;
        public System.Windows.Forms.Label labelpassive;
        public System.Windows.Forms.Label labelgrade;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label labelavgsyl;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label labelavgwordsper;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label labelpercentdialogwords;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
    }
}
