namespace Shield3D
{
	using Tao.OpenGl;

	public class LightManager
	{
		private static LightManager instance;
		private float[] ambience = { 0.3f, 0.3f, 0.3f, 1.0f };
		private float[] diffuse = { 0.5f, 0.5f, 0.5f, 1.0f };
		private bool isTurnOn;


		private LightManager()
		{
			Position = new float[]{0, 180, 0, 1};
			isTurnOn = false;
		}

		public static LightManager Instance
		{
			get { return instance ?? (instance = new LightManager()); }
		}


		public float[] Ambience
		{
			get { return ambience; }
		}

		public float[] Diffuse
		{
			get { return diffuse; }
		}

		public float[] Position { get; set; }

		public void Switch()
		{
			isTurnOn = !isTurnOn;
			
			if (isTurnOn)
			{
				Gl.glEnable(Gl.GL_LIGHTING);
			}
			else
			{
				Gl.glDisable(Gl.GL_LIGHTING);
			}
		}

		public void Initialization()
		{
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, ambience);
			// И диффузный цвет (цвет света)
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, diffuse);

			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, Position);

			// После инициализации источника света нам нужно включить его:
			Gl.glEnable(Gl.GL_LIGHT0);

			// Но недостаточно включить один источник; кроме этого нужно включить само 
			// освещение в OpenGL:

			// Следующая строка позволяет закрашивать полигоны цветом при включенном освещении:
			Gl.glEnable(Gl.GL_COLOR_MATERIAL);
		}
	}
}
