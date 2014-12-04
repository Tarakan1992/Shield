using System;

namespace Shield3D
{
	public class TextureImage
	{
		public int BitePerPixel { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int Id { get; set; }
		public IntPtr Image { get; set; }
	}
}
