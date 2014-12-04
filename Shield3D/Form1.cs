using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;

namespace Shield3D
{
	using Tao.DevIl;

	public partial class Form1 : Form
	{
		private CameraManager cameraManager;

		public Form1()
		{
			InitializeComponent();
			AnT.InitializeContexts();
			Cursor.Hide();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// инициализация Glut 
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE| Glut.GLUT_DEPTH);

			// очитка окна 
			Gl.glClearColor(0, 0, 0, 1);

			// установка порта вывода в соотвествии с размерами элемента anT 
			Gl.glViewport(0, 0, AnT.Width, AnT.Height);

			// активация проекционной матрицы 
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			// очистка матрицы 
			Gl.glLoadIdentity();

			// установка перспективы 
			Glu.gluPerspective(45f, AnT.Width / AnT.Height, 1, 500);

			// установка объектно-видовой матрицы 
			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			// начальные настройки OpenGL 
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			//Gl.glEnable(Gl.GL_LIGHTING);
			//Gl.glEnable(Gl.GL_LIGHT0);

			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\ground.jpg", TextureName.Ground);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Back.bmp", TextureName.Back);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Front.bmp", TextureName.Front);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Bottom.bmp", TextureName.Bottom);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Left.bmp", TextureName.Left);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Right.bmp", TextureName.Rigth);
			TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Top.bmp", TextureName.Top);
	

			cameraManager = new CameraManager(this, 0, 1.5f, 6, 0, 1.5f, 5, 0, 1, 0);

			// активация таймера 
			RenderTimer.Start();
		}

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
			Draw();
			cameraManager.Update();
		}

		private void Draw()
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);	// Clear The Screen And The Depth Buffer
			Gl.glLoadIdentity();									// Reset The matrix

			cameraManager.Look();

			Painter.CreateSkyBox(0, 0, 0, 400, 200, 400);

			//// Draw a grid so we can get a good idea of movement around the world		
			//Draw3DSGrid();

			//// Draw the pyramids that spiral in to the center
			//DrawSpiralTowers();

			Gl.glEnable(Gl.GL_TEXTURE_2D);

			// Биндим текстуру. То есть указываем OpenGL, какую именно текстуру мы хотим 
			// использовать:
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Ground].Id);

			Gl.glBegin(Gl.GL_QUADS);      // Начинаем рисовать квадрат
			// При наложении текстуры нужно указывать _текстурные_координаты_. 
			// Для простой квадратной текстуры координаты распологаются так:
			/*
			0,1 -------------1,1
			|       |
			|       |
			|       |
			|       |
			0,0 ------------- 1,0
        
			Первая цифра - U (X)-координата, Вторая - V (Y). То есть чтобы растянуть 
			текстуру полностью, указываем единицы, чтобы повторить текстуру 2 
			раза - указываем двойки, и т.д.

			Одно замечание к текстурам .jpg - их Y (V)-координаты необходимо переворачивать.
			*/
			Gl.glTexCoord2f(0, 16f); Gl.glVertex3f(-16f, 0, -16f);    // Низ лево
			Gl.glTexCoord2f(16f, 16f); Gl.glVertex3f(16f, 0, -16f); // Низ право
			Gl.glTexCoord2f(16f, 0); Gl.glVertex3f(16f, 0, 16f);  // Верх право
			Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-16f, 0, 16f); // Низ право

			Gl.glEnd();    // Закончили рисовать

			// обновляем элемент AnT 
			AnT.Invalidate();
		}

		public int AnTWidth
		{ 
			get { return AnT.Width; }
		}

		public int AnTHeight
		{
			get { return AnT.Height; }
		}

		private void AnT_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Application.Exit();
			}

			cameraManager.KeyDownEventHanlder(sender, e);
		}

		public void SetCursorPosition(Point position)
		{
			Cursor.Position = this.PointToScreen(position);
		}

		private void AnT_MouseMove(object sender, MouseEventArgs e)
		{
			cameraManager.MouseMoveEventHanlder(sender, e);
		}

		private void CreatePyramid(float x, float y, float z, int width, int height)
		{
			// Below we create a pyramid (hence CreatePyramid() :).  This entails
			// 4 triangles for the sides and one QUAD for the bottom.  We could have done
			// 2 triangles instead of a QUAD but it's less code and seemed appropriate.
			// We also assign some different colors to each vertex to add better visibility.
			// The pyramid will be centered around (x, y, z).  This code is also used in the
			// lighting and fog tutorial on our site at www.GameTutorials.com.

			// Start rendering the 4 triangles for the sides
			Gl.glBegin(Gl.GL_TRIANGLES);

			// These vertices create the Back Side
			Gl.glColor3ub(255, 0, 0); Gl.glVertex3f(x, y + height, z);					// Top point
			Gl.glColor3ub(0, 255, 255); Gl.glVertex3f(x - width, y - height, z - width);	// Bottom left point
			Gl.glColor3ub(255, 0, 255); Gl.glVertex3f(x + width, y - height, z - width);  // Bottom right point

			// These vertices create the Front Side
			Gl.glColor3ub(255, 0, 0); Gl.glVertex3f(x, y + height, z);					// Top point
			Gl.glColor3ub(0, 255, 255); Gl.glVertex3f(x + width, y - height, z + width);  // Bottom right point
			Gl.glColor3ub(255, 0, 255); Gl.glVertex3f(x - width, y - height, z + width);	// Bottom left point

			// These vertices create the Front Left Side
			Gl.glColor3ub(255, 0, 0); Gl.glVertex3f(x, y + height, z);					// Top point
			Gl.glColor3ub(255, 0, 255); Gl.glVertex3f(x - width, y - height, z + width);	// Front bottom point
			Gl.glColor3ub(0, 255, 255); Gl.glVertex3f(x - width, y - height, z - width);	// Bottom back point

			// These vertices create the Front Right Side
			Gl.glColor3ub(255, 0, 0); Gl.glVertex3f(x, y + height, z);					// Top point
			Gl.glColor3ub(255, 0, 255); Gl.glVertex3f(x + width, y - height, z - width);	// Bottom back point
			Gl.glColor3ub(0, 255, 255); Gl.glVertex3f(x + width, y - height, z + width);	// Front bottom point

			Gl.glEnd();

			// Now render the bottom of our pyramid

			Gl.glBegin(Gl.GL_QUADS);

			// These vertices create the bottom of the pyramid
			Gl.glColor3ub(0, 0, 255); Gl.glVertex3f(x - width, y - height, z + width);	// Front left point
			Gl.glColor3ub(0, 0, 255); Gl.glVertex3f(x + width, y - height, z + width);    // Front right point
			Gl.glColor3ub(0, 0, 255); Gl.glVertex3f(x + width, y - height, z - width);    // Back right point
			Gl.glColor3ub(0, 0, 255); Gl.glVertex3f(x - width, y - height, z - width);    // Back left point
			Gl.glEnd();
		}

		void Draw3DSGrid()
		{
			// This function was added to give a better feeling of moving around.
			// A black background just doesn't give it to ya :)  We just draw 100
			// green lines vertical and horizontal along the X and Z axis.

			// Turn the lines GREEN
			Gl.glColor3ub(0, 255, 0);

			// Draw a 1x1 grid along the X and Z axis'
			for (float i = -50; i <= 50; i += 1)
			{
				// Start drawing some lines
				Gl.glBegin(Gl.GL_LINES);

				// Do the horizontal lines (along the X)
				Gl.glVertex3f(-50, 0, i);
				Gl.glVertex3f(50, 0, i);

				// Do the vertical lines (along the Z)
				Gl.glVertex3f(i, 0, -50);
				Gl.glVertex3f(i, 0, 50);

				// Stop drawing lines
				Gl.glEnd();
			}
		}

		void DrawSpiralTowers()
		{
			const float PI = 3.14159f;							// Create a constant for PI
			const float kIncrease = PI / 16.0f;					// This is the angle we increase by in radians
			const float kMaxRotation = PI * 6;					// This is 1080 degrees of rotation in radians (3 circles)
			float radius = 40;									// We start with a radius of 40 and decrease towards the center

			// Keep looping until we go past the max degrees of rotation (which is 3 full rotations)
			for(float degree = 0; degree < kMaxRotation; degree += kIncrease)
			{
				// Here we use polar coordinates for the rotations, but we swap the y with the z
				var x = (float)(radius * Math.Cos(degree));			// Get the x position for the current rotation and radius
				var z = (float)(radius * Math.Sin(degree));			// Get the z position for the current rotation and radius

				// Create a pyramid for every degree in our spiral with a width of 1 and height of 3 
				CreatePyramid(x, 3, z, 1, 3);
	
				// Decrease the radius by the constant amount so the pyramids spirals towards the center
				radius -= 40 / (kMaxRotation / kIncrease);
			}	
		}
	}
}
