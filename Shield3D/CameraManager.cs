using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield3D
{
	using System.Windows.Forms;
	using Tao.OpenGl;

	public class CameraManager
	{
		private readonly Camera camera = new Camera();
		private float myMouseXcoordVar;
		private float myMouseYcoordVar;
		private float myMouseYcoord;
		private float myMouseXcoord;
		private float rotateCameraX;
		private bool mouseRotate;
		private bool mouseMove;

		public void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				mouseRotate = true; //Если нажата левая кнопка мыши

			if (e.Button == MouseButtons.Middle)
				mouseMove = true; //Если нажата средняя кнопка мыши

			myMouseYcoord = e.X; //Передаем в нашу глобальную переменную позицию мыши по Х
			myMouseXcoord = e.Y;
		}

		public void MouseUpHandler(object sender, MouseEventArgs e)
		{
			mouseRotate = false;
			mouseMove = false;
		}

		public void MouseMoveHanlder(object sender, MouseEventArgs e)
		{
			myMouseXcoordVar = e.Y;
			myMouseYcoordVar = e.X;
		}

		public void Look()
		{
			Glu.gluLookAt(camera.Position.X, camera.Position.Y, camera.Position.Z,
						camera.View.X, camera.View.Y, camera.View.Z,
						camera.Up.X, camera.Up.Y, camera.Up.Z);
		}

		public CameraManager(float posX, float posY, float posZ,
			float viewX, float viewY, float viewZ,
			float upX, float upY, float upZ)
		{
			camera.PositionCamera(posX, posY, posZ, viewX, viewY, viewZ, upX, upY, upZ);
		}

		public void Update()
		{
			if (mouseRotate) //Если нажата левая кнопка мыши
			{
				camera.RotatePosition(myMouseYcoordVar - myMouseYcoord, 0, 1, 0); //крутим камеру, в моем случае это от 3го лица;
				rotateCameraX = rotateCameraX + (myMouseXcoordVar - myMouseXcoord);
				camera.UpDown((myMouseXcoordVar - myMouseXcoord) / 10);

				myMouseYcoord = myMouseYcoordVar;
				myMouseXcoord = myMouseXcoordVar;
			}
			else
			{
				if (mouseMove)
				{
					camera.MoveCamera((myMouseXcoordVar - myMouseXcoord) / 50);
					camera.Strafe(-((myMouseYcoordVar - myMouseYcoord) / 50));

					myMouseYcoord = myMouseYcoordVar;
					myMouseXcoord = myMouseXcoordVar;

				}
			}

			camera.Update();
		}

	}
}
