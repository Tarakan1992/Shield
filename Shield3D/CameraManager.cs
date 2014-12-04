using System;

namespace Shield3D
{
	using System.Drawing;
	using System.Windows.Forms;
	using Tao.OpenGl;

	public class CameraManager
	{
		private readonly Camera camera = new Camera();
		private Point newMousePosition;
		private Point previousMousePosition;
		private float lastRotX = 0.0f;
		private float currentRotX = 0.0f;
		private Form1 form;
		private float speed = 0.8f;

		public void MouseMoveEventHanlder(object sender, MouseEventArgs e)
		{
			newMousePosition.Y = e.Y;
			newMousePosition.X = e.X;
			SetViewByMouse();
		}

		public void KeyDownEventHanlder(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
			{
				camera.MoveCamera(speed);
				camera.Update();
			}

			if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
			{
				camera.MoveCamera(-speed);
				camera.Update();
			}

			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
			{
				camera.Strafe(-speed);
				camera.Update();
			}

			if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
			{
				camera.Strafe(speed);
				camera.Update();
			}
			
		}

		public void Look()
		{
			Glu.gluLookAt(camera.Position.X, camera.Position.Y, camera.Position.Z,
						camera.View.X, camera.View.Y, camera.View.Z,
						camera.Up.X, camera.Up.Y, camera.Up.Z);
		}

		public CameraManager(Form1 form, float posX, float posY, float posZ,
			float viewX, float viewY, float viewZ,
			float upX, float upY, float upZ)
		{
			this.form = form;
			
			newMousePosition = new Point();

			camera.PositionCamera(posX, posY, posZ, viewX, viewY, viewZ, upX, upY, upZ);
		}

		public void Update()
		{
			SetViewByMouse();
			camera.Update();
		}

		private void SetViewByMouse()
		{
			var middleX = form.AnTWidth / 2;
			var middleY = form.AnTHeight / 2;

			// Если курсор остался в том же положении, мы не вращаем камеру
			if (Math.Abs(newMousePosition.X - middleX) < 2 && Math.Abs(newMousePosition.Y - middleY) < 2)
			{
				return;
			}

			form.SetCursorPosition(new Point(middleX, middleY));
			
			// Теперь нам нужно направление (или вектор), куда сдвинулся курсор.
			// Его рассчет - простое вычитание. Просто возьмите среднюю точку и вычтите из неё
			// новую позицию мыши: VECTOR = P1 - P2; где P1 - средняя точка (400,300 при 800х600).
			// После получения дельты X и Y (или направления), я делю значение 
			// на 1000, иначе камера будет жутко быстрой.
			var angleY = (middleX - newMousePosition.X) / 1000.0f;
			var angleZ = (middleY - newMousePosition.Y) / 1000.0f;


			lastRotX = currentRotX;     // Сохраняем последний угол вращения 
			// и используем заново currentRotX

			currentRotX += angleZ;

			var vAxis = VectorHelper.Cross(camera.View - camera.Position, camera.Up);
			vAxis = VectorHelper.Normalize(vAxis);

			// Если текущее вращение больше 1 градуса, обрежем его, чтобы не вращать слишком быстро
			if (currentRotX > 1.0f)
			{
				currentRotX = 1.0f;

				// врощаем на оставшийся угол
				if (lastRotX != 1.0f)
				{
					// Вращаем камеру вокруг нашей оси на заданный угол
					camera.RotateView(1.0f - lastRotX, vAxis.X, vAxis.Y, vAxis.Z);
				}
			}

			// Если угол меньше -1.0f, убедимся, что вращение не продолжится
			else if (currentRotX < -1.0f)
			{
				currentRotX = -1.0f;
				if (lastRotX != -1.0f)
				{
					// Вращаем
					camera.RotateView(-1.0f - lastRotX, vAxis.X, vAxis.Y, vAxis.Z);
				}
			}
			// Если укладываемся в пределы 1.0f -1.0f - просто вращаем
			else
			{
				camera.RotateView(angleZ, vAxis.X, vAxis.Y, vAxis.Z);
			}

			// Всегда вращаем камеру вокруг Y-оси
			camera.RotateView(angleY, 0, 1, 0);
		}
	}
}
