namespace Shield3D
{
	using System;
	using Tao.OpenGl;

	public static class Painter
	{
		public static void CreateSkyBox(float x, float y, float z, float width, float height, float length)
		{
			// Это самая важная функция в этом уроке. Мы накладываем на каждую сторону текстуру,
			// чтобы создать иллюзию 3д мира. Вы заметите, что я изменил текстурные
			// координаты, чтобы стороны корректно выглядели. Также, в зависимости от
			// ситуации, вершины могут быть перевернуты. В этом уроке такого не будет,
			// но имейте в виду такую возможность.

			// Так как мы хотим, чтобы скайбокс был с центром в x-y-z, мы производим
			// небольшие рассчеты. Просто изменяем X,Y и Z на нужные.

			// Это центрирует скайбокс на X,Y,Z
			x = x - width / 2;
			y = y - height / 2;
			z = z - length / 2;

			// забиндим заднюю текстуру на заднюю сторону
            Gl.glEnable(Gl.GL_TEXTURE_2D);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Back].Id);
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины ЗАДНЕЙ стороны
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x + width, y, z);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x + width, y + height, z);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x, y + height, z);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x, y, z);

			Gl.glEnd();

			// Биндим ПЕРЕДНЮЮ текстуру на ПЕРЕДНЮЮ сторону бокса
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Front].Id);

			// Начинаем рисовать сторону
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины ПЕРЕДНЕЙ стороны
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x, y, z + length);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x, y + height, z + length);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x + width, y + height, z + length);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x + width, y, z + length);

			Gl.glEnd();

			// Биндим НИЖНЮЮ текстуру на НИЖНЮЮ сторону бокса
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Bottom].Id);

			// Начинаем рисовать сторону
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины НИЖНЕЙ стороны
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x, y, z);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x, y, z + length);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x + width, y, z + length);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x + width, y, z);
			Gl.glEnd();

			// Биндим ВЕРХНЮЮ текстуру на ВЕРХНЮЮ сторону бокса
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Top].Id);

			// Начинаем рисовать сторону
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины ВЕРХНЕЙ стороны
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x + width, y + height, z);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x + width, y + height, z + length);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x, y + height, z + length);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x, y + height, z);

			Gl.glEnd();

			// Биндим ЛЕВУЮ текстуру на ЛЕВУЮ сторону бокса
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Left].Id);

			// Начинаем рисовать сторону
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины ЛЕВОЙ стороны
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x, y + height, z);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x, y + height, z + length);
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x, y, z + length);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x, y, z);

			Gl.glEnd();

			// Биндим ПРАВУЮ текстуру на ПРАВУЮ сторону бокса
			Gl.glBindTexture(Gl.GL_TEXTURE_2D, TextureManager.Instance.TextureImages[TextureName.Rigth].Id);

			// Начинаем рисовать сторону
			Gl.glBegin(Gl.GL_QUADS);

			// Установим текстурные координаты и вершины ПРАВОЙ стороны
			Gl.glTexCoord2f(0.0f, 0.0f); Gl.glVertex3f(x + width, y, z);
			Gl.glTexCoord2f(1.0f, 0.0f); Gl.glVertex3f(x + width, y, z + length);
			Gl.glTexCoord2f(1.0f, 1.0f); Gl.glVertex3f(x + width, y + height, z + length);
			Gl.glTexCoord2f(0.0f, 1.0f); Gl.glVertex3f(x + width, y + height, z);

			Gl.glEnd();

            Gl.glDisable(Gl.GL_TEXTURE_2D);
		}

		public static void Draw3DSGrid()
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

		public static void DrawSpiralTowers()
		{
			const float PI = 3.14159f;							// Create a constant for PI
			const float kIncrease = PI / 16.0f;					// This is the angle we increase by in radians
			const float kMaxRotation = PI * 6;					// This is 1080 degrees of rotation in radians (3 circles)
			float radius = 40;									// We start with a radius of 40 and decrease towards the center

			// Keep looping until we go past the max degrees of rotation (which is 3 full rotations)
			for (float degree = 0; degree < kMaxRotation; degree += kIncrease)
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

		public static void CreatePyramid(float x, float y, float z, int width, int height)
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
	}
}
