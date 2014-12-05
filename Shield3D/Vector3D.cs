namespace Shield3D
{
	public class Vector3D
	{
		#region [Public fields]

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
		public float W { get; set; }

		#endregion

		#region [Constructors]

		public Vector3D()
		{
			X = Y = Z = 0.0f;
		}

		public Vector3D(float x, float y, float z)
		{
		   // Инициализирует переменные значениями X, Y, и Z.
		   X = x;
		   Y = y;
		   Z = z;
		}


		public Vector3D(float x, float y, float z, float w)
		{
		   // Инициализирует переменные значениями X, Y, Z и W.
		   X = x;
		   Y = y;
		   Z = z;
		   W = w;
		}

		public void CVector4(Vector3D vector)
		{
		   // Инициализирует обьект со значениями вектора "v".
		   X = vector.X;
		   Y = vector.Y;
		   Z = vector.Z;
		   W = vector.W;
		}

		#endregion

		#region [Operators]

		public static Vector3D operator +(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D
			{
				X = vector1.X + vector2.X,
				Y = vector1.Y + vector2.Y,
				Z = vector1.Z + vector2.Z
			};
		}

		public static Vector3D operator -(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D
			{
				X = vector1.X - vector2.X,
				Y = vector1.Y - vector2.Y,
				Z = vector1.Z - vector2.Z
			};
		}

		public static Vector3D operator -(Vector3D vector1)
		{
			return new Vector3D
			{
				X = -vector1.X,
				Y = -vector1.Y,
				Z = -vector1.Z,
				W = -vector1.W
			};
		}

		public static Vector3D operator *(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1.X * vector2.X, vector1.Y * vector2.Y, vector1.Z * vector2.Z);
		}

		public static Vector3D operator *(Vector3D vector1, float f)
		{
			return new Vector3D(vector1.X*f, vector1.Y*f, vector1.Z*f);
		}

		public static Vector3D operator /(Vector3D vector1, Vector3D vector2)
		{
			return new Vector3D(vector1.X/vector2.X, vector1.Y/vector2.Y, vector1.Z/vector2.Z);
		}

		public static bool operator == (Vector3D vector1, Vector3D vector2)
		{
			return (VectorHelper.TEqual(vector1.X, vector2.X, .001f)) && 
					(VectorHelper.TEqual(vector1.Y, vector2.Y, .001f)) &&
					(VectorHelper.TEqual(vector1.Z, vector2.Z, .001f));

		}

		public static bool operator !=(Vector3D vector1, Vector3D vector2)
		{
			return !(vector1 == vector2);
		}

		public static Vector3D operator /(Vector3D vector1, float f)
		{
			return vector1*(1/f);
		}

		#endregion

		public void Set(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public void Set(Vector3D vector)
		{
			X = vector.X;
			Y = vector.Y;
			Z = vector.Z;
		}

		
	}
}
