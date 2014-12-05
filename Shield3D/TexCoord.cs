using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shield3D
{
	public class TexCoord
	{
		public float Tu { get; set; }

		public float Tv { get; set; }

		public TexCoord()
		{
			Tu = 0.0f;
			Tv = 0.0f;
		}

		public TexCoord(float u, float v)
		{
			Tu = u;
			Tv = v;
		}
	}
}
