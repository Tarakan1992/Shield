using System;

namespace Shield3D
{
	public static class VectorHelper
	{
		public static float Magnitude(Vector3D vector)
		{
			// Это даст нам величину нашей нормали, 
			// т.е. длину вектора. Мы используем эту информацию для нормализации
			// вектора. Вот формула: magnitude = sqrt(V.x^2 + V.y^2 + V.z^2)   где V - вектор.

			return (float)Math.Sqrt((vector.X * vector.X) +
					(vector.Y * vector.Y) +
					(vector.Z * vector.Z));
		}

		public static Vector3D Normalize(Vector3D vector)
		{
			// Вы спросите, для чего эта ф-я? Мы должны убедиться, что наш вектор нормализирован.
			// Вектор нормализирован - значит, его длинна равна 1. Например,
			// вектор (2,0,0) после нормализации будет (1,0,0).

			// Вычислим величину нормали
			float magnitude = Magnitude(vector);

			// Теперь у нас есть величина, и мы можем разделить наш вектор на его величину.
			// Это сделает длинну вектора равной единице, так с ним будет легче работать.
			vector.X = vector.X / magnitude;
			vector.Y = vector.Y / magnitude;
			vector.Z = vector.Z / magnitude;

			return vector;
		}

		public static Vector3D Cross(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D
			{
				X = ((vector1.Y * vector2.Z) - (vector1.Z * vector2.Y)),
				Y = ((vector1.Z * vector2.X) - (vector1.X * vector2.Z)),
				Z = ((vector1.X * vector2.Y) - (vector1.Y * vector2.X))
			};
		}
	}
}
