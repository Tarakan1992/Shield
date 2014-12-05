using System;

namespace Shield3D
{
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;

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

		public static Vector3D Normal(Vector3D vector)
		{
			var magnitude = Magnitude(vector);

			if (magnitude == 0.0f)
			{
				magnitude = 1.0f;
			}

			return vector / magnitude;
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

		public static Vector3D Cross3(Vector3D vector1, Vector3D vector2, Vector3D vector3)
		{
			return new Vector3D
			{
				X = vector1.Y * vector2.Z * vector3.W +
				   vector1.Z * vector2.W * vector3.Y +
				   vector1.W * vector2.Y * vector3.Z -
				   vector1.Y * vector2.W * vector3.Z -
				   vector1.Z * vector2.Y * vector3.W -
				   vector1.W * vector2.Z * vector3.Y,

				Y = vector1.X * vector2.W * vector3.Z +
				   vector1.Z * vector2.X * vector3.W +
				   vector1.W * vector2.Z * vector3.X -
				   vector1.X * vector2.Z * vector3.W -
				   vector1.Z * vector2.W * vector3.X -
				   vector1.W * vector2.X * vector3.Z,

				Z = vector1.X * vector2.Y * vector3.W +
				   vector1.Y * vector2.W * vector3.X +
				   vector1.W * vector2.X * vector3.Y -
				   vector1.X * vector2.W * vector3.Y -
				   vector1.Y * vector2.X * vector3.W -
				   vector1.W * vector2.Y * vector3.X,

				W = vector1.X * vector2.Z * vector3.Y +
				   vector1.Y * vector2.X * vector3.Z +
				   vector1.Z * vector2.Y * vector3.X -
				   vector1.X * vector2.Y * vector3.Z -
				   vector1.Y * vector2.Z * vector3.X -
				   vector1.Z * vector2.X * vector3.Y,
			};
		}

		public static float DotProduct3(Vector3D vector1, Vector3D vector2)
		{
			return vector1.X*vector2.X + vector1.Y*vector2.Y + vector1.Z*vector2.Z;
		}

		public static float DotProduct4(Vector3D vector1, Vector3D vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		public static Vector3D Normalize(List<Vector3D> vTriangle)
		{
			var vVector1 = new Vector3D
			{
				X = vTriangle[0].X - vTriangle[1].X,
				Y = vTriangle[0].Y - vTriangle[1].Y,
				Z = vTriangle[0].Z - vTriangle[1].Z,
				W = vTriangle[0].W - vTriangle[1].W
			};

			var vVector2 = new Vector3D
			{
				X = vTriangle[1].X - vTriangle[2].X,
				Y = vTriangle[1].Y - vTriangle[2].Y,
				Z = vTriangle[1].Z - vTriangle[2].Z,
				W = vTriangle[1].W - vTriangle[2].W
			};
 
			Vector3D vNormal = Cross(vVector1, vVector2);
 
			// Теперь, имея направление нормали, осталось сделать последнюю вещь. Сейчас её
			// длинна неизвестна, она может быть очень длинной. Мы сделаем её равной 1, это
			// называется нормализация. Чтобы сделать это, мы делим нормаль на её длинну.
			// Ну а как найти длинну? Мы используем эту формулу: magnitude = sqrt(x^2 + y^2 + z^2)
 
			vNormal = Normalize(vNormal);
 
			// Теперь вернём "нормализованную нормаль" =)
			// (*ПРИМЕЧАНИЕ*) если вы хотите увидеть, как работает нормализация, закомментируйте
			// предидущую линию. Вы увидите, как длинна нормаль до нормалицации. Я стого рекомендую
			// всегда использовать эту функцию. И запомните, неважно, какова длинна нормали 
			// (конечно, кроме (0,0,0)), если мы её нормализуем, она всегда будет равна 1.
 
			return vNormal;
		}

		public static bool TEqual(float a, float b, float t)
		{
			return ((a > b - t) && (a < b + t));
		}
	}
}
