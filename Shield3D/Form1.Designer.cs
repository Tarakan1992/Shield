namespace Shield3D
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
			this.RenderTimer = new System.Windows.Forms.Timer(this.components);
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
			this.AnT.Cursor = System.Windows.Forms.Cursors.Cross;
			this.AnT.DepthBits = ((byte)(16));
			this.AnT.Location = new System.Drawing.Point(0, 1);
			this.AnT.Name = "AnT";
			this.AnT.Size = new System.Drawing.Size(964, 532);
			this.AnT.StencilBits = ((byte)(0));
			this.AnT.TabIndex = 0;
			this.AnT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AnT_KeyDown);
			this.AnT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AnT_MouseMove);
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
			this.Controls.Add(this.AnT);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private Tao.Platform.Windows.SimpleOpenGlControl AnT;
        private System.Windows.Forms.Timer RenderTimer;

    }
}

