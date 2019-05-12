namespace Zarodkowanie
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.randomButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.collumnsRowButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RowNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ColumnNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RadiusNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AmountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.radiusAmountButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.randomNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.mooreCheckBox = new System.Windows.Forms.CheckBox();
            this.vonNeumanCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNumericUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountNumericUpDown)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(951, 695);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button1.Location = new System.Drawing.Point(998, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(166, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "Let the grid begin";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.gridButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.CausesValidation = false;
            this.textBox1.Location = new System.Drawing.Point(16, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 26);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.CausesValidation = false;
            this.textBox2.Location = new System.Drawing.Point(16, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(166, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "height";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PaleGreen;
            this.button2.Location = new System.Drawing.Point(998, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(166, 39);
            this.button2.TabIndex = 6;
            this.button2.Text = "Start the show";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.startButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(982, 431);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 131);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "size";
            // 
            // randomButton
            // 
            this.randomButton.BackColor = System.Drawing.Color.PaleGreen;
            this.randomButton.Location = new System.Drawing.Point(10, 25);
            this.randomButton.Name = "randomButton";
            this.randomButton.Size = new System.Drawing.Size(166, 39);
            this.randomButton.TabIndex = 8;
            this.randomButton.Text = "Seed randomly";
            this.randomButton.UseVisualStyleBackColor = false;
            this.randomButton.Click += new System.EventHandler(this.randomButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.PaleVioletRed;
            this.clearButton.Location = new System.Drawing.Point(998, 102);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(166, 39);
            this.clearButton.TabIndex = 9;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(982, 568);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 139);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Boundry Conditions";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(23, 78);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(119, 24);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Nonperiodic";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.checkBox2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(23, 39);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Periodic";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // collumnsRowButton
            // 
            this.collumnsRowButton.BackColor = System.Drawing.Color.PaleGreen;
            this.collumnsRowButton.Location = new System.Drawing.Point(10, 25);
            this.collumnsRowButton.Name = "collumnsRowButton";
            this.collumnsRowButton.Size = new System.Drawing.Size(166, 39);
            this.collumnsRowButton.TabIndex = 11;
            this.collumnsRowButton.Text = "Col / Row";
            this.collumnsRowButton.UseVisualStyleBackColor = false;
            this.collumnsRowButton.Click += new System.EventHandler(this.collumnsRowButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox3.Controls.Add(this.RowNumericUpDown);
            this.groupBox3.Controls.Add(this.ColumnNumericUpDown);
            this.groupBox3.Controls.Add(this.collumnsRowButton);
            this.groupBox3.Location = new System.Drawing.Point(1203, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 113);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Col / Row";
            // 
            // RowNumericUpDown
            // 
            this.RowNumericUpDown.BackColor = System.Drawing.Color.White;
            this.RowNumericUpDown.Location = new System.Drawing.Point(106, 70);
            this.RowNumericUpDown.Name = "RowNumericUpDown";
            this.RowNumericUpDown.ReadOnly = true;
            this.RowNumericUpDown.Size = new System.Drawing.Size(76, 26);
            this.RowNumericUpDown.TabIndex = 14;
            this.RowNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RowNumericUpDown.ValueChanged += new System.EventHandler(this.RowNumericUpDown_ValueChanged);
            // 
            // ColumnNumericUpDown
            // 
            this.ColumnNumericUpDown.BackColor = System.Drawing.Color.White;
            this.ColumnNumericUpDown.Location = new System.Drawing.Point(16, 70);
            this.ColumnNumericUpDown.Name = "ColumnNumericUpDown";
            this.ColumnNumericUpDown.ReadOnly = true;
            this.ColumnNumericUpDown.Size = new System.Drawing.Size(76, 26);
            this.ColumnNumericUpDown.TabIndex = 13;
            this.ColumnNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ColumnNumericUpDown.ValueChanged += new System.EventHandler(this.ColumnNumericUpDown_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.RadiusNumericUpDown);
            this.groupBox4.Controls.Add(this.AmountNumericUpDown);
            this.groupBox4.Controls.Add(this.radiusAmountButton);
            this.groupBox4.Location = new System.Drawing.Point(1203, 147);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 133);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "With radius";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Radius";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Amount";
            // 
            // RadiusNumericUpDown
            // 
            this.RadiusNumericUpDown.Location = new System.Drawing.Point(96, 90);
            this.RadiusNumericUpDown.Name = "RadiusNumericUpDown";
            this.RadiusNumericUpDown.Size = new System.Drawing.Size(76, 26);
            this.RadiusNumericUpDown.TabIndex = 14;
            this.RadiusNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RadiusNumericUpDown.ValueChanged += new System.EventHandler(this.radiusNumericUpDown2_ValueChanged);
            // 
            // AmountNumericUpDown
            // 
            this.AmountNumericUpDown.Location = new System.Drawing.Point(6, 90);
            this.AmountNumericUpDown.Name = "AmountNumericUpDown";
            this.AmountNumericUpDown.Size = new System.Drawing.Size(76, 26);
            this.AmountNumericUpDown.TabIndex = 15;
            this.AmountNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AmountNumericUpDown.ValueChanged += new System.EventHandler(this.amountNumericUpDown1_ValueChanged);
            // 
            // radiusAmountButton
            // 
            this.radiusAmountButton.BackColor = System.Drawing.Color.PaleGreen;
            this.radiusAmountButton.Location = new System.Drawing.Point(10, 25);
            this.radiusAmountButton.Name = "radiusAmountButton";
            this.radiusAmountButton.Size = new System.Drawing.Size(166, 39);
            this.radiusAmountButton.TabIndex = 14;
            this.radiusAmountButton.Text = "Seed with radius";
            this.radiusAmountButton.UseVisualStyleBackColor = false;
            this.radiusAmountButton.Click += new System.EventHandler(this.radiusAmountButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.randomNumericUpDown);
            this.groupBox5.Controls.Add(this.randomButton);
            this.groupBox5.Location = new System.Drawing.Point(1203, 304);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 122);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Random";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "amount";
            // 
            // randomNumericUpDown
            // 
            this.randomNumericUpDown.Location = new System.Drawing.Point(31, 90);
            this.randomNumericUpDown.Name = "randomNumericUpDown";
            this.randomNumericUpDown.Size = new System.Drawing.Size(120, 26);
            this.randomNumericUpDown.TabIndex = 9;
            this.randomNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.randomNumericUpDown.ValueChanged += new System.EventHandler(this.randomNumericUpDown_ValueChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox6.Controls.Add(this.nextButton);
            this.groupBox6.Controls.Add(this.previousButton);
            this.groupBox6.Location = new System.Drawing.Point(982, 147);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(200, 87);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Step By Step";
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.Color.PaleGreen;
            this.nextButton.Location = new System.Drawing.Point(113, 41);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 40);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.BackColor = System.Drawing.Color.PaleVioletRed;
            this.previousButton.Location = new System.Drawing.Point(6, 41);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 40);
            this.previousButton.TabIndex = 0;
            this.previousButton.Text = "Prev";
            this.previousButton.UseVisualStyleBackColor = false;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.AntiqueWhite;
            this.groupBox7.Controls.Add(this.mooreCheckBox);
            this.groupBox7.Controls.Add(this.vonNeumanCheckBox);
            this.groupBox7.Location = new System.Drawing.Point(982, 298);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(200, 122);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Neighbourhood";
            // 
            // mooreCheckBox
            // 
            this.mooreCheckBox.AutoSize = true;
            this.mooreCheckBox.Location = new System.Drawing.Point(23, 81);
            this.mooreCheckBox.Name = "mooreCheckBox";
            this.mooreCheckBox.Size = new System.Drawing.Size(80, 24);
            this.mooreCheckBox.TabIndex = 1;
            this.mooreCheckBox.Text = "Moore";
            this.mooreCheckBox.UseVisualStyleBackColor = true;
            this.mooreCheckBox.Click += new System.EventHandler(this.mooreCheckBox_Click);
            // 
            // vonNeumanCheckBox
            // 
            this.vonNeumanCheckBox.AutoSize = true;
            this.vonNeumanCheckBox.Checked = true;
            this.vonNeumanCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vonNeumanCheckBox.Location = new System.Drawing.Point(23, 39);
            this.vonNeumanCheckBox.Name = "vonNeumanCheckBox";
            this.vonNeumanCheckBox.Size = new System.Drawing.Size(120, 24);
            this.vonNeumanCheckBox.TabIndex = 0;
            this.vonNeumanCheckBox.Text = "vonNeuman";
            this.vonNeumanCheckBox.UseVisualStyleBackColor = true;
            this.vonNeumanCheckBox.Click += new System.EventHandler(this.vonNeumanCheckBox_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1409, 732);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RowNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnNumericUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountNumericUpDown)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.randomNumericUpDown)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button randomButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button collumnsRowButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown RowNumericUpDown;
        private System.Windows.Forms.NumericUpDown ColumnNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown RadiusNumericUpDown;
        private System.Windows.Forms.NumericUpDown AmountNumericUpDown;
        private System.Windows.Forms.Button radiusAmountButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown randomNumericUpDown;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox mooreCheckBox;
        private System.Windows.Forms.CheckBox vonNeumanCheckBox;
    }
}

