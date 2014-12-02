namespace Shield3D
{
	public class Vector3D
	{
		#region [Public fields]

		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

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

		#endregion
	}
}
