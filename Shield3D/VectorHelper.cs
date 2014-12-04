using System;

namespace Shield3D
{
	using System.Collections.Generic;

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

		public static Vector3D Normal(List<Vector3D> vTriangle)
		{
			Vector3D vVector1 = vTriangle[2] - vTriangle[0];
			Vector3D vVector2 = vTriangle[1] - vTriangle[0];
 
			// В функцию передаются три вектора - треугольник. Мы получаем vVector1 и vVector2 - его
			// стороны. Теперь, имея 2 стороны треугольника, мы можем получить из них cross().
			// (*ЗАМЕЧАНИЕ*) Важно: первым вектором мы передаём низ треугольника, а вторым - левую
			// сторону. Если мы поменяем их местами, нормаль будет повернута в противоположную
			// сторону. В нашем случае мы приняли решение всегда работать против часовой.
 
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
	}
}
