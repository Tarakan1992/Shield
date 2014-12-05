using System;

namespace Shield3D
{
	using System.Drawing;
	using Tao.OpenGl;

	public class Particle
	{
		private const float ParticleGravity = -9.81f/10.0f;

		private Vector3D _position;
		private Vector3D _speed;

		private Color _color;
		private float _size;
		private float _life;
		private float _angle;
		private float _angleCurrent;
		private Vector3D _startPosition;

		private int _textureId;

		public bool IsAlive
		{
			get { return _life > 0.0f; }
		}

		public Particle()
		{
			_color = Color.FromArgb(255, 255, 255, 255);
			_size = 0.0f;
			_life = 0.0f;
			_angle = 0.0f;
		}

		public bool Initialization(Vector3D position, Vector3D speed, float life, float size, float angle, Color color,
			int textureId)
		{
			_position = new Vector3D {X = position.X, Y = position.Y, Z = position.Z};
			_startPosition = new Vector3D {X = position.X, Y = position.Y, Z = position.Z};
			_speed = speed;

			if (life <= 0.0f)
			{
				return false;
			}

			_life = life;

			if (size < 0.0f)
			{
				return false;
			}

			_size = size;
			_angle = angle;
			_color = color;

			_textureId = textureId;

			return true;
		}

		public void Process(float dt)
		{
			var r = new Random();

			if (!IsAlive)
			{
				_life = 10.0f;//r.Next(100, 200) / 10.0f;
				_position.Set(_startPosition.X, _startPosition.Y, _startPosition.Z);

				return;
			}

			_position.X += _speed.X*dt;
			_position.Y += _speed.Y*dt;
			_position.Z += _speed.Z*dt;


			_position.Y += ParticleGravity*dt;
			_life -= dt;

			_angleCurrent += _angle*dt;
		}

		public void Render()
		{
			if (!IsAlive)
			{
				return;
			}

			if (_textureId < 0)
			{
				return;
			}

			Gl.glEnable(Gl.GL_TEXTURE_2D);

			//Gl.glDisable(Gl.GL_DEPTH_TEST);

			//Gl.glColor4ub(_color.R, _color.G, _color.B, _color.A);

			Gl.glMatrixMode(Gl.GL_TEXTURE);

			Gl.glPushMatrix();

			Gl.glLoadIdentity();

			Gl.glTranslatef(0.5f, 0.5f, 0.0f); // Центр текстуры - центр новой системы координат
			Gl.glRotatef(_angleCurrent, 0.0f, 0.0f, 1.0f); // Вращаем по оси Z

			Gl.glTranslatef(-0.5f, -0.5f, 0.0f); // Перемещаем назад


			Gl.glBindTexture(Gl.GL_TEXTURE_2D, _textureId);
			
			Gl.glPopMatrix();

			// Привязываем текстуру

			Gl.glMatrixMode(Gl.GL_MODELVIEW);

			Gl.glPushMatrix();

			// Переместим частицы в указанные координаты
			Gl.glTranslatef(_position.X, _position.Y, _position.Z);

			var halfSize = _size / 2;

			// Отрисуем частицы
			Gl.glBegin(Gl.GL_QUADS);
			Gl.glTexCoord2f(0.0f, 1.0f);
			Gl.glVertex3f(-halfSize, halfSize, 0.0f); // Верхняя левая вершина

			Gl.glTexCoord2f(0.0f, 0.0f);
			Gl.glVertex3f(-halfSize, -halfSize, 0.0f); // Нижняя левая вершина

			Gl.glTexCoord2f(1.0f, 0.0f);
			Gl.glVertex3f(halfSize, -halfSize, 0.0f); // Нижняя правая вершина

			Gl.glTexCoord2f(1.0f, 1.0f);
			Gl.glVertex3f(halfSize, halfSize, 0.0f); // Верхняя правая вершина
			Gl.glEnd();

			Gl.glPopMatrix();

			//Gl.glEnable(Gl.GL_DEPTH_TEST); // Переключим Z-буфер в нормальное состояние read-write
			Gl.glDisable(Gl.GL_TEXTURE_2D);
		}
	}
}
