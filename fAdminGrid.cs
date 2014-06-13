// fAdminGrid.cs
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using RulesMess;
using Layout;
using CoreUtilities;


namespace WriteThinker
{
    public partial class fAdminGrid : Form
    {
        public fAdminGrid()
        {
			this.Icon = LayoutDetails.Instance.MainFormIcon;
			this.TopMost = true;

			FormUtils.SizeFormsForAccessibility(this, LayoutDetails.Instance.MainFormFontSize);
            InitializeComponent();

        }
        public WriteThink writethink; // object set in AdminGrid

        
       

        /// <summary>
        /// Loads the specified datatable into the gridview
        /// </summary>
        /// <param name="table"></param>
        public void LoadTable(DataView view)
        {
           
                     

            dataGridView1.DataSource = view;

           
            // make "Text" column wid
            DataGridViewColumn textCol = dataGridView1.Columns["Text"];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                DataGridViewColumn column = dataGridView1.Columns[i];
                if (column.Name == "Text")
                {
                    column.Width = 500;
                }
                    else
                    if (column.Name == "hesaidshesaid")
                    {
                        column.Width = 100;
                    }
                    else
                    {
                        column.Width = 40;
                    }
            }
          
        }

        private void bSql_Click(object sender, EventArgs e)
        {
            if (writethink.view == null)
            {
                throw new Exception("view not initialized. Which should be impossible for you to press this button.");

            }

            
            object value = writethink.table.Compute(sql.Text, textFilter.Text);
            textOutput.Text = value.ToString();

          //  view.RowFilter = "Count(Id)";
           // view.
            
        }

        private void bShowStats_Click(object sender, EventArgs e)
        {
            // shows the stats panel
            if (writethink == null)
            {
                throw new Exception("You must assign writeThink before using stat functions");
            }
            writethink.ShowStatPanel(this.Icon);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bFilterView_Click(object sender, EventArgs e)
        {
            string sColumn = comboColumn.Text;
            string sValue = columnFilter.Text;
            
            string sExpression = "";
            
            // we have to have different epxressions based on 
            // type of column
            Type type = (Type)  ((DataColumn)comboColumn.SelectedItem).DataType;
            if (type != null)
            {
                if (type == typeof(string))
                {
                    sExpression = String.Format("{0} LIKE '{1}'", sColumn, sValue);
                }
                else
                    if (type == typeof(Int32))
                    {
                        sExpression = String.Format("{0} = {1}", sColumn, sValue);
                    }
                    else
                {
                    sExpression = String.Format("{0} = '{1}'", sColumn, sValue);
                }
            }
            writethink.view.RowFilter = sExpression;
         
            
        }
        /// <summary>
        /// fill in the list of column names from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboColumn_DropDown(object sender, EventArgs e)
        {
            comboColumn.Items.Clear();
            foreach (DataColumn col in writethink.table.Columns)
            {
                comboColumn.Items.Add(col);
            }
          
            comboColumn.ValueMember = "DataType";
            comboColumn.DisplayMember = "ColumnName";

        }

        private void bInput_Click(object sender, EventArgs e)
        {
            // for testing paste will be collector

            CharacterInDialogClass[] characters = new CharacterInDialogClass[5];

                characters[0] = new CharacterInDialogClass();
                characters[0].Name = "Izzy";
                characters[0].Alias = new string[2] { "Isabel", "Mosh" };
                characters[0].Gender = CharacterInDialogClass.gender.female;
                characters[0].Tilt = 75;

                characters[1] = new CharacterInDialogClass();
                characters[1].Name = "Commander";
                characters[1].Alias = new string[3] { "Meredith", "Ferguson", "Ferguson's" };
                characters[1].Gender = CharacterInDialogClass.gender.female;
                characters[1].Tilt = 50;

                characters[2] = new CharacterInDialogClass();
                characters[2].Name = "Rutger";
                characters[2].Alias = new string[2] { "Dr.", "doctor" };
                characters[2].Gender = CharacterInDialogClass.gender.male;
                characters[2].Tilt = 75;

                characters[3] = new CharacterInDialogClass();
                characters[3].Name = "Alex";
               
                characters[3].Gender = CharacterInDialogClass.gender.male;
                characters[3].Tilt = 25;


                characters[4] = new CharacterInDialogClass();
                characters[4].Name = "Janice";
              
                characters[4].Gender = CharacterInDialogClass.gender.female;
                characters[4].Tilt = 40;

         


            writethink.LoadTextStringIntoDatabase(textBoxInput.Text, characters);
            writethink.view = new DataView(writethink.table);
        }

        private void buttonDialog_Click(object sender, EventArgs e)
        {
            dialogReview review = new dialogReview();
            review.Setup(writethink.characters, writethink.view, "fromAdminGrid");
            review.Show();
        }

        private void buttonRules_Click(object sender, EventArgs e)
        {
            // This is a temporary only thing while I explore the idea
            // of controlling all this using a RULES - AI
            


            /*
            knowledgeBase.Add(new Relationship("frog", "is", "fun"));
            knowledgeBase.Add(new Relationship("mouse", "is", "dead"));
            */
            KnowledgeBase knowledgeBase = writethink.kb;
            //knowledgeBase.Load(Application.StartupPath + "\\knowledge.xml");

           // List<RulesMess.Rule> rb = new List<RulesMess.Rule>();
         
            
          //  InferenceEngine ie = new InferenceEngine();

         //   ie.Infer(rb, knowledgeBase);

         //   Console.WriteLine(knowledgeBase.ToString());
            fRulesInspector inspector = new fRulesInspector();
            
            //knowledgeBase.Save(Application.StartupPath + "\\knowledge.xml");
            
            
            //inspector.formLoad(Application.StartupPath + "\\knowledge.xml");
            inspector.formLoad(knowledgeBase);
            inspector.ShowDialog();
        }

        /// <summary>
        /// Saves the current datagrid as an xml file so that it can be later used to 
        /// 'compare' results (for purposes of unit testing as well as possible AI improvement)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveGrade_Click(object sender, EventArgs e)
        {
            
            
            
            
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = new DataSet();
                if (writethink.table.DataSet != null)
                {
                    ds = writethink.table.DataSet;
                }
                else
                {
                    ds.Tables.Add(writethink.table);
                }
                ds.WriteXml(save.FileName, XmlWriteMode.WriteSchema);
                ds = null;
               
            }
            

        }

        private void buttonOpenGradeFile_Click(object sender, EventArgs e)
        {
        
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                writethink.LoadFromFile(open.FileName);
                writethink.view = new DataView(writethink.table);
                dataGridView1.DataSource = writethink.view;
           //     ds = null;
            }
        }

        /// <summary>
        /// compares the file seletected from open dialog
        /// with the current file in memeroya nd gives a grade
        /// 
        /// Assumes the "gradefile" is 100% accurate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGrade_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                WriteThinker.WriteThink grade = new WriteThinker.WriteThink();
                grade.LoadFromFile(open.FileName);
                CoreUtilities.NewMessage.Show(writethink.Grade(grade.table).ToString());
            }
        }
    }// class
}