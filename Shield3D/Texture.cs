namespace Shield3D
{
	using System;
	using System.Collections.Generic;
	using Tao.DevIl;
	using Tao.OpenGl;

	public class TextureManager
	{
		public Dictionary<TextureName, TextureImage> TextureImages { get; private set; }

		private static TextureManager instance;

		private TextureManager()
		{
			Il.ilInit();
			Ilu.iluInit();
			TextureImages = new Dictionary<TextureName, TextureImage>();
		}

		public void LoadTexture(string filename, TextureName name)
		{
			var texture = new TextureImage();

			if (!Il.ilLoadImage(filename))
			{
				throw new Exception("Cann't load texture image!");
			}

			texture.Width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
			texture.Height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);
			texture.BitePerPixel = Il.ilGetInteger(Il.IL_IMAGE_BYTES_PER_PIXEL);

			texture.Image = Il.ilGetData();

			Il.ilEnable(Il.IL_CONV_PAL);

			int type = Il.ilGetInteger(Il.IL_IMAGE_FORMAT);

			int imageId;
			Gl.glGenTextures(1, out imageId);
			texture.Id = imageId;

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, imageId);

			Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, texture.BitePerPixel, texture.Width, texture.Height, type,
				Gl.GL_UNSIGNED_BYTE, texture.Image);

			//Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, texture.Width, texture.Height, 0,
			//	texture.BitePerPixel == 24 ? Gl.GL_RGB : Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE,
			//	texture.Image);

			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR_MIPMAP_NEAREST);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);

			TextureImages.Add(name, texture);

			Il.ilDeleteImages(1, ref imageId);
		}

		public static TextureManager Instance
		{
			get { return instance ?? (instance = new TextureManager()); }
		}
	}
}
