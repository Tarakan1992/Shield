using System;
using System.Drawing;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.DevIl;

namespace Shield3D
{
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

		private void visualizeButton_Click(object sender, EventArgs e)
		{
            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            //Gl.glLoadIdentity();
            //Gl.glColor3f(1.0f, 0, 0);

            //Gl.glPushMatrix();
            //Gl.glTranslated(0, 0, -6);
            //Gl.glRotated(45, 1, 1, 0);

            //// рисуем сферу с помощью библиотеки FreeGLUT 
            //Glut.glutWireSphere(2, 32, 32);

            //Gl.glPopMatrix();
            //Gl.glFlush();
            //AnT.Invalidate();

		}

		private void exitButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
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
            			
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\ground.jpg", TextureName.Ground);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Back.bmp", TextureName.Back);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Front.bmp", TextureName.Front);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Bottom.bmp", TextureName.Bottom);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Left.bmp", TextureName.Left);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Right.bmp", TextureName.Rigth);
            //TextureManager.Instance.LoadTexture(@"D:\MG\Shield\Shield3D\Texture\Top.bmp", TextureName.Top);

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

			cameraManager.Look();

			// установка положения камеры (наблюдателя). Как видно из кода
			// дополнительно на полложение наблюдателя по оси Z влияет значение
			// установленное в ползунке, доступном для пользователя.

			// таким образом, при перемещении ползунка, наблюдатель будет отдалятся или приближатся к объекту наблюдения 
            //Gl.glTranslated(0, 0, -7 - trackBar1.Value);
            //// 2 поворота (углы rot_1 и rot_2)
            //Gl.glRotated(rot_1, 1, 0, 0);
            //Gl.glRotated(rot_2, 0, 1, 0);

			// устанавливаем размер точек равный 5 
			Gl.glPointSize(5.0f);

            //// условие switch определяет установленный режим отображения, на основе выбранного пункта элемента 
            //// comboBox, установленного в форме программы 
            //switch (comboBox1.SelectedIndex)
            //{
            //    case 0: // отображение в виде точек 
            //        {

            //            // режим вывода геометрии - точки 
            //            Gl.glBegin(Gl.GL_POINTS);

            //            // выводим всю ранее просчитанную геометрию объекта 
            //            for (int ax = 0; ax < count_elements; ax++)
            //            {

            //                for (int bx = 0; bx < Iter; bx++)
            //                {

            //                    // отрисовка точки 
            //                    Gl.glVertex3d(ResaultGeometric[ax, bx, 0], ResaultGeometric[ax, bx, 1], ResaultGeometric[ax, bx, 2]);

            //                }

            //            }
            //            // завершаем режим рисования 
            //            Gl.glEnd();

            //            break;

            //        }
            //    case 1: // отображение объекта в сеточном режиме, используя режим GL_LINES_STRIP 
            //        {

            //            // устанавливаем режим отрисвки линиями (последовательность линий) 
            //            Gl.glBegin(Gl.GL_LINE_STRIP);
            //            for (int ax = 0; ax < count_elements; ax++)
            //            {

            //                for (int bx = 0; bx < Iter; bx++)
            //                {


            //                    Gl.glVertex3d(ResaultGeometric[ax, bx, 0], ResaultGeometric[ax, bx, 1], ResaultGeometric[ax, bx, 2]);
            //                    Gl.glVertex3d(ResaultGeometric[ax + 1, bx, 0], ResaultGeometric[ax + 1, bx, 1], ResaultGeometric[ax + 1, bx, 2]);

            //                    if (bx + 1 < Iter - 1)
            //                    {

            //                        Gl.glVertex3d(ResaultGeometric[ax + 1, bx + 1, 0], ResaultGeometric[ax + 1, bx + 1, 1], ResaultGeometric[ax + 1, bx + 1, 2]);

            //                    }
            //                    else
            //                    {

            //                        Gl.glVertex3d(ResaultGeometric[ax + 1, 0, 0], ResaultGeometric[ax + 1, 0, 1], ResaultGeometric[ax + 1, 0, 2]);

            //                    }

            //                }

            //            }
            //            Gl.glEnd();
            //            break;

            //        }
            //    case 2: // отрисовка оболочки с расчетом нормалей для корректного затенения граней объекта 
            //        {

            //            Gl.glBegin(Gl.GL_QUADS); // режим отрисовки полигонов состоящих из 4 вершин 
            //            for (int ax = 0; ax < count_elements; ax++)
            //            {

            //                for (int bx = 0; bx < Iter; bx++)
            //                {

            //                    // вспомогательные переменные, для более наглядного использования кода при расчете нормалей 
            //                    double x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, z1 = 0, z2 = 0, z3 = 0, z4 = 0;

            //                    // первая вершина 
            //                    x1 = ResaultGeometric[ax, bx, 0];
            //                    y1 = ResaultGeometric[ax, bx, 1];
            //                    z1 = ResaultGeometric[ax, bx, 2];

            //                    if (ax + 1 < count_elements) // если текущий ax не последний 
            //                    {

            //                        // берем следующую точку последовательности 
            //                        x2 = ResaultGeometric[ax + 1, bx, 0];
            //                        y2 = ResaultGeometric[ax + 1, bx, 1];
            //                        z2 = ResaultGeometric[ax + 1, bx, 2];

            //                        if (bx + 1 < Iter - 1) // если текущий bx не последний 
            //                        {

            //                            // берем следующую точку последовательности и следующий медивн 
            //                            x3 = ResaultGeometric[ax + 1, bx + 1, 0];
            //                            y3 = ResaultGeometric[ax + 1, bx + 1, 1];
            //                            z3 = ResaultGeometric[ax + 1, bx + 1, 2];

            //                            // точка соотвествующуя по номеру , только на соседнем медиане 
            //                            x4 = ResaultGeometric[ax, bx + 1, 0];
            //                            y4 = ResaultGeometric[ax, bx + 1, 1];
            //                            z4 = ResaultGeometric[ax, bx + 1, 2];

            //                        }
            //                        else
            //                        {

            //                            // если это последний медиан - то в качесвте след. мы берем начальный (замыкаем геометрию фигуры) 
            //                            x3 = ResaultGeometric[ax + 1, 0, 0];
            //                            y3 = ResaultGeometric[ax + 1, 0, 1];
            //                            z3 = ResaultGeometric[ax + 1, 0, 2];

            //                            x4 = ResaultGeometric[ax, 0, 0];
            //                            y4 = ResaultGeometric[ax, 0, 1];
            //                            z4 = ResaultGeometric[ax, 0, 2];

            //                        }

            //                    }
            //                    else // данный элемент ax последний, следовательно мы будем использовать начальный (нулевой) вместо данного ax 
            //                    {

            //                        // слудуещей точкой будет нулевая ax 
            //                        x2 = ResaultGeometric[0, bx, 0];
            //                        y2 = ResaultGeometric[0, bx, 1];
            //                        z2 = ResaultGeometric[0, bx, 2];


            //                        if (bx + 1 < Iter - 1)
            //                        {

            //                            x3 = ResaultGeometric[0, bx + 1, 0];
            //                            y3 = ResaultGeometric[0, bx + 1, 1];
            //                            z3 = ResaultGeometric[0, bx + 1, 2];

            //                            x4 = ResaultGeometric[ax, bx + 1, 0];
            //                            y4 = ResaultGeometric[ax, bx + 1, 1];
            //                            z4 = ResaultGeometric[ax, bx + 1, 2];

            //                        }
            //                        else
            //                        {

            //                            x3 = ResaultGeometric[0, 0, 0];
            //                            y3 = ResaultGeometric[0, 0, 1];
            //                            z3 = ResaultGeometric[0, 0, 2];

            //                            x4 = ResaultGeometric[ax, 0, 0];
            //                            y4 = ResaultGeometric[ax, 0, 1];
            //                            z4 = ResaultGeometric[ax, 0, 2];

            //                        }

            //                    }


            //                    // переменные для расчета нормал 
            //                    double n1 = 0, n2 = 0, n3 = 0;

            //                    // нормаль будем расчитывать как векторное произведение граней полигона 
            //                    // для нулевого элемента нормаль мы будем считать немного по другому. 

            //                    // на самом деле разница в расчете нормали актуальна только для 1 и последнего и первого полигона на медиане

            //                    if (ax == 0) // при расчете нормали для ax мы будем использовать точки 1,2,3 
            //                    {

            //                        n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
            //                        n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
            //                        n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

            //                    }
            //                    else // для остальных - 1,3,4 
            //                    {

            //                        n1 = (y4 - y3) * (z1 - z3) - (y1 - y3) * (z4 - z3);
            //                        n2 = (z4 - z3) * (x1 - x3) - (z1 - z3) * (x4 - x3);
            //                        n3 = (x4 - x3) * (y1 - y3) - (x1 - x3) * (y4 - y3);

            //                    }

            //                    // если не включен режим GL_NORMILIZE то мы должны в обязательном порядке 
            //                    // произвести нормализацию вектора нормали, перед тем как передать информацию о нормали 
            //                    double n5 = (double)Math.Sqrt(n1 * n1 + n2 * n2 + n3 * n3);
            //                    n1 /= (n5 + 0.01);
            //                    n2 /= (n5 + 0.01);
            //                    n3 /= (n5 + 0.01);

            //                    // передаем информацию о нормали 
            //                    Gl.glNormal3d(-n1, -n2, -n3);

            //                    // передаем 4 вершины для отрисовки полигона 
            //                    Gl.glVertex3d(x1, y1, z1);
            //                    Gl.glVertex3d(x2, y2, z2);
            //                    Gl.glVertex3d(x3, y3, z3);
            //                    Gl.glVertex3d(x4, y4, z4);

            //                }

            //            }

            //            // завершаем выбранный режим рисования полигонов 
            //            Gl.glEnd();
            //            break;

            //        }
            //}
            
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
