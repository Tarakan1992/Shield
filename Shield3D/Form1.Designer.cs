﻿namespace Shield3D
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.AnT = new Tao.Platform.Windows.SimpleOpenGlControl();
			this.visualizeButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.RenderTimer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// AnT
			// 
			this.AnT.AccumBits = ((byte)(0));
			this.AnT.AutoCheckErrors = false;
			this.AnT.AutoFinish = false;
			this.AnT.AutoMakeCurrent = true;
			this.AnT.AutoSwapBuffers = true;
			this.AnT.BackColor = System.Drawing.Color.Black;
			this.AnT.ColorBits = ((byte)(32));
			this.AnT.DepthBits = ((byte)(16));
			this.AnT.Location = new System.Drawing.Point(0, 1);
			this.AnT.Name = "AnT";
			this.AnT.Size = new System.Drawing.Size(787, 532);
			this.AnT.StencilBits = ((byte)(0));
			this.AnT.TabIndex = 0;
			// 
			// visualizeButton
			// 
			this.visualizeButton.Location = new System.Drawing.Point(809, 70);
			this.visualizeButton.Name = "visualizeButton";
			this.visualizeButton.Size = new System.Drawing.Size(75, 23);
			this.visualizeButton.TabIndex = 1;
			this.visualizeButton.Text = "Visualize";
			this.visualizeButton.UseVisualStyleBackColor = true;
			this.visualizeButton.Click += new System.EventHandler(this.visualizeButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Location = new System.Drawing.Point(809, 123);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 2;
			this.exitButton.Text = "Exit";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "GL_POINTS",
            "GL_LINES",
            "GL_QUADS"});
			this.comboBox1.Location = new System.Drawing.Point(809, 27);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 3;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(899, 54);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackBar1.Size = new System.Drawing.Size(45, 104);
			this.trackBar1.TabIndex = 4;
			// 
			// RenderTimer
			// 
			this.RenderTimer.Interval = 30;
			this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 545);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.visualizeButton);
			this.Controls.Add(this.AnT);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

        private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Button visualizeButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Timer RenderTimer;

    }
}

