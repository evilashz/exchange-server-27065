using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.CommonMath
{
	// Token: 0x02000013 RID: 19
	internal struct Vector2 : IEquatable<Vector2>
	{
		// Token: 0x06000096 RID: 150 RVA: 0x0000372C File Offset: 0x0000192C
		public Vector2(float x, float y)
		{
			this = default(Vector2);
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003743 File Offset: 0x00001943
		public Vector2(float value)
		{
			this = default(Vector2);
			this.X = value;
			this.Y = value;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000375A File Offset: 0x0000195A
		public static Vector2 Zero
		{
			get
			{
				return Vector2.zero;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003761 File Offset: 0x00001961
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003769 File Offset: 0x00001969
		[DataMember]
		public float X { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003772 File Offset: 0x00001972
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000377A File Offset: 0x0000197A
		[DataMember]
		public float Y { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003783 File Offset: 0x00001983
		[IgnoreDataMember]
		public float Length
		{
			get
			{
				return (float)Math.Sqrt((double)this.LengthSquared);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003792 File Offset: 0x00001992
		[IgnoreDataMember]
		public float LengthSquared
		{
			get
			{
				return this.X * this.X + this.Y * this.Y;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000037AF File Offset: 0x000019AF
		public static bool operator ==(Vector2 value1, Vector2 value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000037B9 File Offset: 0x000019B9
		public static bool operator !=(Vector2 value1, Vector2 value2)
		{
			return !value1.Equals(value2);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000037C6 File Offset: 0x000019C6
		public static Vector2 operator +(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.X + value2.X, value1.Y + value2.Y);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000037EB File Offset: 0x000019EB
		public static Vector2 operator -(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.X - value2.X, value1.Y - value2.Y);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003810 File Offset: 0x00001A10
		public static Vector2 operator *(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.X * value2.X, value1.Y * value2.Y);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003835 File Offset: 0x00001A35
		public static Vector2 operator *(Vector2 value, float scaleFactor)
		{
			return new Vector2(value.X * scaleFactor, value.Y * scaleFactor);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000384E File Offset: 0x00001A4E
		public static Vector2 operator /(Vector2 value1, Vector2 value2)
		{
			return new Vector2(value1.X / value2.X, value1.Y / value2.Y);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003874 File Offset: 0x00001A74
		public static Vector2 operator /(Vector2 value1, float divider)
		{
			float num = 1f / divider;
			return new Vector2(value1.X * num, value1.Y * num);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038A0 File Offset: 0x00001AA0
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{{X:{0} Y:{1}}}", new object[]
			{
				this.X,
				this.Y
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000038E0 File Offset: 0x00001AE0
		public bool Equals(Vector2 other)
		{
			return this.X.ExactEquals(other.X) && this.Y.ExactEquals(other.Y);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000390C File Offset: 0x00001B0C
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Vector2)
			{
				result = this.Equals((Vector2)obj);
			}
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003934 File Offset: 0x00001B34
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		// Token: 0x04000041 RID: 65
		public static readonly int SizeOf = Marshal.SizeOf(typeof(Vector2));

		// Token: 0x04000042 RID: 66
		private static readonly Vector2 zero = default(Vector2);
	}
}
