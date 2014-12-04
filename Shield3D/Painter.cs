namespace Shield3D
{
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
	}
}
