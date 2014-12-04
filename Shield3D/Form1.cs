using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.DevIl;

namespace Shield3D
{
	using System.Collections.Generic;
	using Tao.DevIl;

	public partial class Form1 : Form
	{
		private CameraManager cameraManager;

        private ModelManager modelManager;

		public Form1()
		{
			InitializeComponent();
			AnT.InitializeContexts();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// инициализация Glut 
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE| Glut.GLUT_DEPTH);

			// очитка окна 
			Gl.glClearColor(0, 0, 0, 1);

			LightManager.Instance.Initialization();

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


            var path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\";

            TextureManager.Instance.LoadTexture(path + @"\Texture\ground.jpg", TextureName.Ground);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Back.bmp", TextureName.Back);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Front.bmp", TextureName.Front);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Bottom.bmp", TextureName.Bottom);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Left.bmp", TextureName.Left);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Right.bmp", TextureName.Rigth);
            TextureManager.Instance.LoadTexture(path + @"\Texture\Top.bmp", TextureName.Top);
            
            modelManager = new ModelManager();
            modelManager.LoadModel();

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

			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, LightManager.Instance.Position);

			cameraManager.Look();

			Painter.CreateSkyBox(0, 0, 0, 400, 200, 400);

			Gl.glEnable(Gl.GL_TEXTURE_2D);

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

			var vectorList = new List<Vector3D>
			{
				new Vector3D {X = -16f, Y = 0f, Z = -16f},
				new Vector3D {X = 16f, Y = 0f, Z = 16f},
				new Vector3D {X = 16f, Y = 0f, Z = 16f}
			};

			var normal = VectorHelper.Normal(vectorList);

			Gl.glNormal3f(normal.X, normal.Y, normal.Z);
			Gl.glTexCoord2f(0, 16f); Gl.glVertex3f(-16f, 0, -16f);    // Низ лево
			Gl.glTexCoord2f(16f, 16f); Gl.glVertex3f(16f, 0, -16f); // Низ право
			Gl.glTexCoord2f(16f, 0); Gl.glVertex3f(16f, 0, 16f);  // Верх право
			Gl.glTexCoord2f(0, 0); Gl.glVertex3f(-16f, 0, 16f); // Низ право

            
			Gl.glEnd();    // Закончили рисовать
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            if (modelManager.isLoad)
            {
                modelManager.DrawModels();
            }

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

			if (e.KeyCode == Keys.L)
			{
				LightManager.Instance.Switch();
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
	}
}
