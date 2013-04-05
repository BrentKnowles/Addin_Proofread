// dialogReview.Designer.cs
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
using CoreUtilities;
using Layout;
using System;
namespace WriteThinker
{
    partial class dialogReview
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
		
			 ButtonVersion = new System.Windows.Forms.Button();
			ButtonVersion.Click += HandleUpdateVersionClick;

            this.components = new System.ComponentModel.Container();
            this.groupCharacters = new System.Windows.Forms.GroupBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.listDialog = new System.Windows.Forms.ListBox();
            this.groupcontext = new System.Windows.Forms.GroupBox();
            this.panelForGrammar = new System.Windows.Forms.Panel();
            this.labelForGrammar = new System.Windows.Forms.Label();
            this.panelExtraDetails = new System.Windows.Forms.Panel();
            this.labelPassive = new System.Windows.Forms.Label();
            this.labelSyllables = new System.Windows.Forms.Label();
            this.panelForDialog = new System.Windows.Forms.Panel();
            this.labelLines = new System.Windows.Forms.Label();
            this.labelLinesValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAlias = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelconfidence = new System.Windows.Forms.Label();
            this.labelNext = new System.Windows.Forms.Label();
            this.labelPrevious = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxExPreview = new Layout.RichTextExtended();
            this.checkBoxShowParts = new System.Windows.Forms.CheckBox();
			this.AlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupCharacters.SuspendLayout();
            this.groupcontext.SuspendLayout();
            this.panelForGrammar.SuspendLayout();
            this.panelExtraDetails.SuspendLayout();
            this.panelForDialog.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupCharacters
            // 
            this.groupCharacters.Controls.Add(this.panelButtons);
            this.groupCharacters.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupCharacters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupCharacters.Location = new System.Drawing.Point(0, 0);
            this.groupCharacters.Name = "groupCharacters";
            this.groupCharacters.Size = new System.Drawing.Size(142, 592);
            this.groupCharacters.TabIndex = 0;
            this.groupCharacters.TabStop = false;
            this.groupCharacters.Text = "ITEM";
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Location = new System.Drawing.Point(12, 21);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(104, 466);
            this.panelButtons.TabIndex = 0;
            // 
            // listDialog
            // 
            this.listDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDialog.FormattingEnabled = true;
            this.listDialog.ItemHeight = 20;
            this.listDialog.Location = new System.Drawing.Point(0, 0);
            this.listDialog.Name = "listDialog";
            this.listDialog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listDialog.Size = new System.Drawing.Size(497, 384);
            this.listDialog.TabIndex = 1;
            this.listDialog.SelectedIndexChanged += new System.EventHandler(this.listDialog_SelectedIndexChanged);



			ButtonVersion.Text = Loc.Instance.GetString ("");
			ButtonVersion.Name="buttonversion";
			ButtonVersion.Dock = System.Windows.Forms.DockStyle.Bottom;

            // 
            // groupcontext
            // 
            this.groupcontext.Controls.Add(this.panelForGrammar);
            this.groupcontext.Controls.Add(this.panelExtraDetails);
			//panelExtraDetails.SendToBack();
            this.groupcontext.Controls.Add(this.panelForDialog);
            this.groupcontext.Controls.Add(this.buttonCopy);
			this.groupcontext.Controls.Add (ButtonVersion);
            this.groupcontext.Controls.Add(this.textBox1);
            this.groupcontext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupcontext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupcontext.Location = new System.Drawing.Point(0, 0);
            this.groupcontext.Name = "groupcontext";
            this.groupcontext.Size = new System.Drawing.Size(248, 592);
            this.groupcontext.TabIndex = 2;
            this.groupcontext.TabStop = false;
            this.groupcontext.Text = Loc.Instance.GetString ("Details");
            this.groupcontext.Enter += new System.EventHandler(this.groupcontext_Enter);
            // 
            // panelForGrammar
            // 
            this.panelForGrammar.Controls.Add(this.labelForGrammar);
            this.panelForGrammar.Location = new System.Drawing.Point(60, 301);
            this.panelForGrammar.Name = "panelForGrammar";
            this.panelForGrammar.Size = new System.Drawing.Size(269, 202);
            this.panelForGrammar.TabIndex = 13;
            // 
            // labelForGrammar
            // 
            this.labelForGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelForGrammar.Location = new System.Drawing.Point(0, 0);
            this.labelForGrammar.Name = "labelForGrammar";
            this.labelForGrammar.Size = new System.Drawing.Size(269, 202);
            this.labelForGrammar.TabIndex = 0;
            this.labelForGrammar.Text = "label5";
            // 
            // panelExtraDetails
            // 
            this.panelExtraDetails.Controls.Add(this.labelPassive);
            this.panelExtraDetails.Controls.Add(this.labelSyllables);
            this.panelExtraDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelExtraDetails.Location = new System.Drawing.Point(3, 365);
            this.panelExtraDetails.Name = "panelExtraDetails";
            this.panelExtraDetails.Size = new System.Drawing.Size(242, 100);
            this.panelExtraDetails.TabIndex = 13;
            // 
            // labelPassive
            // 
            this.labelPassive.AutoSize = true;
            this.labelPassive.Location = new System.Drawing.Point(3, 59);
            this.labelPassive.Name = "labelPassive";
            this.labelPassive.Size = new System.Drawing.Size(57, 16);
            this.labelPassive.TabIndex = 1;
            this.labelPassive.Text = "Passive";
            // 
            // labelSyllables
            // 
            this.labelSyllables.AutoSize = true;
            this.labelSyllables.Location = new System.Drawing.Point(3, 16);
            this.labelSyllables.Name = "labelSyllables";
            this.labelSyllables.Size = new System.Drawing.Size(64, 16);
            this.labelSyllables.TabIndex = 0;
            this.labelSyllables.Text = "Syllables";
            // 
            // panelForDialog
            // 
            this.panelForDialog.Controls.Add(this.labelLines);
            this.panelForDialog.Controls.Add(this.labelLinesValue);
            this.panelForDialog.Controls.Add(this.label1);
            this.panelForDialog.Controls.Add(this.labelAlias);
            this.panelForDialog.Controls.Add(this.label2);
            this.panelForDialog.Controls.Add(this.label4);
            this.panelForDialog.Controls.Add(this.labelconfidence);
            this.panelForDialog.Controls.Add(this.labelNext);
            this.panelForDialog.Controls.Add(this.labelPrevious);
            this.panelForDialog.Controls.Add(this.label3);
            this.panelForDialog.Location = new System.Drawing.Point(45, 21);
            this.panelForDialog.Name = "panelForDialog";
            this.panelForDialog.Size = new System.Drawing.Size(312, 244);
            this.panelForDialog.TabIndex = 12;
            // 
            // labelLines
            // 
            this.labelLines.AutoSize = true;
            this.labelLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLines.Location = new System.Drawing.Point(12, 11);
            this.labelLines.Name = "labelLines";
            this.labelLines.Size = new System.Drawing.Size(45, 16);
            this.labelLines.TabIndex = 0;
            this.labelLines.Text = "Lines";
            // 
            // labelLinesValue
            // 
            this.labelLinesValue.AutoSize = true;
            this.labelLinesValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLinesValue.Location = new System.Drawing.Point(103, 11);
            this.labelLinesValue.Name = "labelLinesValue";
            this.labelLinesValue.Size = new System.Drawing.Size(45, 16);
            this.labelLinesValue.TabIndex = 1;
            this.labelLinesValue.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Previous";
            // 
            // labelAlias
            // 
            this.labelAlias.AutoSize = true;
            this.labelAlias.Location = new System.Drawing.Point(102, 28);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(0, 16);
            this.labelAlias.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Confidence";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Alias";
            // 
            // labelconfidence
            // 
            this.labelconfidence.AutoSize = true;
            this.labelconfidence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelconfidence.Location = new System.Drawing.Point(103, 48);
            this.labelconfidence.Name = "labelconfidence";
            this.labelconfidence.Size = new System.Drawing.Size(12, 16);
            this.labelconfidence.TabIndex = 4;
            this.labelconfidence.Text = "-";
            // 
            // labelNext
            // 
            this.labelNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNext.Location = new System.Drawing.Point(12, 175);
            this.labelNext.Name = "labelNext";
            this.labelNext.Size = new System.Drawing.Size(170, 76);
            this.labelNext.TabIndex = 7;
            this.labelNext.Text = "-";
            // 
            // labelPrevious
            // 
            this.labelPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrevious.Location = new System.Drawing.Point(12, 94);
            this.labelPrevious.Name = "labelPrevious";
            this.labelPrevious.Size = new System.Drawing.Size(171, 60);
            this.labelPrevious.TabIndex = 5;
            this.labelPrevious.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Next";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonCopy.Location = new System.Drawing.Point(3, 465);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(242, 27);
            this.buttonCopy.TabIndex = 11;
            this.buttonCopy.Text = "Copy to Clipboard";
            this.toolTip1.SetToolTip(this.buttonCopy, "Will copy the selected lines of dialog into text that can be pasted into another " +
                    "program");
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.LightGray;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(3, 492);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(242, 97);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Dialog Preview - this feature is in beta. Please send feedback to support@youroth" +
                "ermind.com. Depending on interest, dialog accuracy\r\n will be improved (Click to " +
                "Close)";
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(142, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupcontext);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(749, 592);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listDialog);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBoxExPreview);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxShowParts);
			this.splitContainer2.Panel2.Controls.Add(this.AlwaysOnTop);
            this.splitContainer2.Size = new System.Drawing.Size(497, 592);
            this.splitContainer2.SplitterDistance = 394;
            this.splitContainer2.TabIndex = 2;
            // 
            // richTextBoxExPreview
            // 
            this.richTextBoxExPreview.AllowDrop = true;
           // this.richTextBoxExPreview.AutoFormatForWriters = false;
           // this.richTextBoxExPreview.BulletStyle = RichTextBoxLinks.RichTextBoxEx.AdvRichTextBulletStyle.NoNumber;
          //  this.richTextBoxExPreview.BulletType = RichTextBoxLinks.RichTextBoxEx.AdvRichTextBulletType.Number;
            this.richTextBoxExPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxExPreview.EnableAutoDragDrop = true;
            this.richTextBoxExPreview.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           // this.richTextBoxExPreview.Icons = null;
            this.richTextBoxExPreview.Location = new System.Drawing.Point(0, 0);
           // this.richTextBoxExPreview.MyOwnerGUID = "";
            this.richTextBoxExPreview.Name = "richTextBoxExPreview";
            //this.richTextBoxExPreview.PageNumbersForFirstPage = false;
           // this.richTextBoxExPreview.pagepos = RichTextBoxLinks.RichTextBoxEx.PageNumPrintPosition.TopRight;
            this.richTextBoxExPreview.ShowPartsOfSpeechMode = true;
            this.richTextBoxExPreview.Size = new System.Drawing.Size(497, 177);
            this.richTextBoxExPreview.TabIndex = 0;
            this.richTextBoxExPreview.Text = "";
            // 
            // checkBoxShowParts
            // 
            this.checkBoxShowParts.AutoSize = true;
            this.checkBoxShowParts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxShowParts.Location = new System.Drawing.Point(0, 177);
            this.checkBoxShowParts.Name = "checkBoxShowParts";
            this.checkBoxShowParts.Size = new System.Drawing.Size(497, 17);
            this.checkBoxShowParts.TabIndex = 1;
            this.checkBoxShowParts.Text = "Show Parts of Speech";
            this.checkBoxShowParts.UseVisualStyleBackColor = true;
            this.checkBoxShowParts.CheckedChanged += new System.EventHandler(this.checkBoxShowParts_CheckedChanged);

			this.AlwaysOnTop.AutoSize = true;
			this.AlwaysOnTop.Checked= true;
			this.AlwaysOnTop.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.AlwaysOnTop.Location = new System.Drawing.Point(0, 177);
			this.AlwaysOnTop.Name = "alwaysOnTop";
			this.AlwaysOnTop.Size = new System.Drawing.Size(497, 17);
			this.AlwaysOnTop.TabIndex = 1;
			this.AlwaysOnTop.Text = Loc.Instance.GetString( "On Top of Other Forms");
			this.AlwaysOnTop.UseVisualStyleBackColor = true;
			this.AlwaysOnTop.CheckedChanged += new System.EventHandler(this.AlwaysOnTop_Changed);
            // 
            // dialogReview
            // 
            this.ClientSize = new System.Drawing.Size(891, 592);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupCharacters);
            this.Name = "dialogReview";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.dialogReview_Load);
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.dialogReview_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dialogReview_FormClosing);
            this.groupCharacters.ResumeLayout(false);
            this.groupcontext.ResumeLayout(false);
            this.groupcontext.PerformLayout();
            this.panelForGrammar.ResumeLayout(false);
            this.panelExtraDetails.ResumeLayout(false);
            this.panelExtraDetails.PerformLayout();
            this.panelForDialog.ResumeLayout(false);
            this.panelForDialog.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void HandleUpdateVersionClick (object sender, System.EventArgs e)
        {
			LayoutDetails.Instance.TransactionsList.AddEvent (new Transactions.TransactionUpdateProofreadVersion(DateTime.Now, LayoutDetails.Instance.CurrentLayout.GUID, (sender as System.Windows.Forms.Button).Text));
			// we do this otherwise user does not see the NewMessage
			this.AlwaysOnTop.Checked = false;
			NewMessage.Show (Loc.Instance.GetString ("Version Updated."));
        }

		void AlwaysOnTop_Changed (object sender, System.EventArgs e)
		{
			this.TopMost = (sender as System.Windows.Forms.CheckBox).Checked;
		}

        #endregion

        private System.Windows.Forms.GroupBox groupCharacters;
        private System.Windows.Forms.ListBox listDialog;
        private System.Windows.Forms.GroupBox groupcontext;
        private System.Windows.Forms.Label labelconfidence;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLinesValue;
        private System.Windows.Forms.Label labelLines;
        private System.Windows.Forms.Label labelPrevious;
        private System.Windows.Forms.Label labelNext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Layout.RichTextExtended richTextBoxExPreview;
        private System.Windows.Forms.Panel panelForGrammar;
        private System.Windows.Forms.Label labelForGrammar;
        private System.Windows.Forms.Panel panelForDialog;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelExtraDetails;
        private System.Windows.Forms.Label labelPassive;
        private System.Windows.Forms.Label labelSyllables;
        private System.Windows.Forms.CheckBox checkBoxShowParts;

		private System.Windows.Forms.CheckBox AlwaysOnTop;
		System.Windows.Forms.Button ButtonVersion;

    }
}