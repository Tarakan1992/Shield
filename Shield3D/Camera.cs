﻿namespace Shield3D
{
	using System;
	using Tao.OpenGl;

	public class Camera
	{
		#region [Private fields]

		//Вектор для стрейфа (движения влево и вправо) камеры.
		private Vector3D _strafe;

		#endregion

		#region [Public fields]

		/// <summary>
		/// Camera position.
		/// </summary>
		public Vector3D Position { get; private set; }

		/// <summary>
		/// Camera up.
		/// </summary>
		public Vector3D Up { get; private set; }


		/// <summary>
		/// Camera view.
		/// </summary>
		public Vector3D View { get; private set; }

		#endregion

		#region [Constructors]

		public Camera()
		{
			Position = new Vector3D();
			View = new Vector3D();
			Up = new Vector3D();
			_strafe = new Vector3D();
		}

		#endregion

		#region [Public methods]

		public void PositionCamera(float posX, float posY, float posZ,
				float viewX, float viewY, float viewZ,
				float upX, float upY, float upZ)
		{
			//Позиция камеры
			Position.X = posX;
			Position.Y = posY;
			Position.Z = posZ;

			//Куда смотрит, т.е. взгляд
			View.X = viewX;
			View.Y = viewY;
			View.Z = viewZ;

			//Вертикальный вектор камеры
			Up.X = upX;
			Up.Y = upY;
			Up.Z = upZ;
		}


		/// <summary>
		/// Camera strafe.
		/// </summary>
		/// <param name="speed">
		/// Strafe speed.
		/// </param>
		public void Strafe(float speed)
		{
			// добавим вектор стрейфа к позиции
			Position.X += _strafe.X * speed;
			Position.Z += _strafe.Z * speed;

			// Добавим теперь к взгляду
			View.X += _strafe.X * speed;
			View.Z += _strafe.Z * speed;
		}

		/// <summary>
		/// Change camera view to up/down.
		/// </summary>
		/// <param name="speed"></param>
		public void UpDown(float speed)
		{
			Position.Y += speed;
		}

		public void RotateView(float angle, float x, float y, float z)
		{
			var newView = new Vector3D();
			var vView = View - Position;
			

			// Рассчитаем 1 раз синус и косинус переданного угла
			var cosTheta = (float)Math.Cos(angle);
			var sinTheta = (float)Math.Sin(angle); ;

			// Найдем новую позицию X для вращаемой точки
			newView.X = (cosTheta + (1 - cosTheta) * x * x) * vView.X;
			newView.X += ((1 - cosTheta) * x * y - z * sinTheta) * vView.Y;
			newView.X += ((1 - cosTheta) * x * z + y * sinTheta) * vView.Z;

			// Найдем позицию Y
			newView.Y = ((1 - cosTheta) * x * y + z * sinTheta) * vView.X;
			newView.Y += (cosTheta + (1 - cosTheta) * y * y) * vView.Y;
			newView.Y += ((1 - cosTheta) * y * z - x * sinTheta) * vView.Z;

			// И позицию Z
			newView.Z = ((1 - cosTheta) * x * z - y * sinTheta) * vView.X;
			newView.Z += ((1 - cosTheta) * y * z + x * sinTheta) * vView.Y;
			newView.Z += (cosTheta + (1 - cosTheta) * z * z) * vView.Z;

			// Теперь просто добавим новый вектор вращения к нашей позиции, чтобы
			// установить новый взгляд камеры.

			View = Position + newView;
		}

		public void RotatePosition(float angle, float x, float y, float z)
		{
			Position = Position - View;

			var vector = new Vector3D();

			var sinA = (float)Math.Sin(Math.PI * angle / 180.0);
			var cosA = (float)Math.Cos(Math.PI * angle / 180.0);

			// Найдем новую позицию X для вращаемой точки 
			vector.X = (cosA + (1 - cosA) * x * x) * Position.X;
			vector.X += ((1 - cosA) * x * y - z * sinA) * Position.Y;
			vector.X += ((1 - cosA) * x * z + y * sinA) * Position.Z;

			// Найдем позицию Y 
			vector.Y = ((1 - cosA) * x * y + z * sinA) * Position.X;
			vector.Y += (cosA + (1 - cosA) * y * y) * Position.Y;
			vector.Y += ((1 - cosA) * y * z - x * sinA) * Position.Z;

			// И позицию Z 
			vector.Z = ((1 - cosA) * x * z - y * sinA) * Position.X;
			vector.Z += ((1 - cosA) * y * z + x * sinA) * Position.Y;
			vector.Z += (cosA + (1 - cosA) * z * z) * Position.Z;

			Position = View + vector;
		}

		public void MoveCamera(float speed)
		{
			var vector = View - Position;

			//vector.Y = 0.0f; // Это запрещает камере подниматься вверх
			vector = VectorHelper.Normalize(vector);

			Position.X += vector.X * speed;
			Position.Z += vector.Z * speed;
			Position.Y += vector.Y * speed;
			View.X += vector.X * speed;
			View.Z += vector.Z * speed;
			View.Y += vector.Y * speed;
		}

		public void Update()
		{
			var vCross = VectorHelper.Cross(View - Position, Up);

			//Нормализуем вектор стрейфа
			_strafe = VectorHelper.Normalize(vCross);
		}

		#endregion
	}
}
