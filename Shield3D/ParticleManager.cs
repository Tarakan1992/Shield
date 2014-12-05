using System;
using System.Collections.Generic;

namespace Shield3D
{
	using System.Drawing;

	public class ParticleManager
	{
		private static ParticleManager instance;
		private const int _maxParticles = 256;

		public List<Particle> Particles;

		private ParticleManager()
		{
			Particles = new List<Particle>(_maxParticles);
			for (var i = 0; i < _maxParticles; i++)
			{
				Particles.Add(new Particle());
			}
		}

		public static ParticleManager Instance
		{
			get { return instance ?? (instance = new ParticleManager()); }
		}

		public void Initialization()
		{
			var r = new Random();
			
			foreach (var particle in Particles)
			{
				if (!particle.Initialization(
					new Vector3D {X = 0, Z = 0, Y = 0},
					new Vector3D { X = (float)r.NextDouble()/10, Y = -(float)r.NextDouble()/10, Z = (float)r.NextDouble()/10 },
					1.0f, 0.1f, 10.0f, Color.Brown, TextureManager.Instance.TextureImages[TextureName.Ground].Id))
				{
					throw new Exception("Can't particles initialization!");
				}
			}

			BeginTime = -1;
		}

		public float BeginTime { get; set; }

		public float EndTime { get; set; }
	}
}
