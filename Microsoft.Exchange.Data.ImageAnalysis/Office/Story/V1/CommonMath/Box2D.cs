using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.CommonMath
{
	// Token: 0x02000011 RID: 17
	internal struct Box2D : IEquatable<Box2D>
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000315C File Offset: 0x0000135C
		public Box2D(Vector2 min, Vector2 max)
		{
			this = default(Box2D);
			this.Min = min;
			this.Max = max;
			if (min.X > max.X || min.Y > max.Y)
			{
				this = Box2D.Null;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000031A9 File Offset: 0x000013A9
		public Box2D(double xmin, double ymin, double xmax, double ymax)
		{
			this = new Box2D((float)xmin, (float)ymin, (float)xmax, (float)ymax);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031BA File Offset: 0x000013BA
		public Box2D(float xmin, float ymin, float xmax, float ymax)
		{
			this = new Box2D(new Vector2(xmin, ymin), new Vector2(xmax, ymax));
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000031D1 File Offset: 0x000013D1
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000031D9 File Offset: 0x000013D9
		[DataMember]
		public Vector2 Min { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000031E2 File Offset: 0x000013E2
		// (set) Token: 0x06000077 RID: 119 RVA: 0x000031EA File Offset: 0x000013EA
		[DataMember]
		public Vector2 Max { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000031F4 File Offset: 0x000013F4
		public float X
		{
			get
			{
				return this.Min.X;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003210 File Offset: 0x00001410
		public float Left
		{
			get
			{
				return this.Min.X;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000322C File Offset: 0x0000142C
		public float Y
		{
			get
			{
				return this.Min.Y;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003248 File Offset: 0x00001448
		public float Top
		{
			get
			{
				return this.Min.Y;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003264 File Offset: 0x00001464
		public float Right
		{
			get
			{
				return this.Max.X;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003280 File Offset: 0x00001480
		public float Bottom
		{
			get
			{
				return this.Max.Y;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000329C File Offset: 0x0000149C
		public float Width
		{
			get
			{
				float num = this.Max.X - this.Min.X;
				if (num <= 0f)
				{
					return 0f;
				}
				return num;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000032D8 File Offset: 0x000014D8
		public float Height
		{
			get
			{
				float num = this.Max.Y - this.Min.Y;
				if (num <= 0f)
				{
					return 0f;
				}
				return num;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003314 File Offset: 0x00001514
		public float DiagonalSize
		{
			get
			{
				return this.Size.Length;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000332F File Offset: 0x0000152F
		public float Area
		{
			get
			{
				return this.Width * this.Height;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000333E File Offset: 0x0000153E
		public Vector2 Center
		{
			get
			{
				return this.Min + (this.Max - this.Min) / 2f;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003368 File Offset: 0x00001568
		public bool IsNull
		{
			get
			{
				return this.Min.X > this.Max.X || this.Min.Y > this.Max.Y;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000033B3 File Offset: 0x000015B3
		public Vector2 Size
		{
			get
			{
				return this.Max - this.Min;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000033C6 File Offset: 0x000015C6
		public static Box2D FromSize(float x, float y, float width, float height)
		{
			return new Box2D(x, y, x + width, y + height);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033D5 File Offset: 0x000015D5
		public static Box2D FromSize(float width, float height)
		{
			return new Box2D(0f, 0f, width, height);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033E8 File Offset: 0x000015E8
		public static Box2D FromSize(Vector2 location, Vector2 size)
		{
			return new Box2D(location, location + size);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000033F7 File Offset: 0x000015F7
		public static bool operator ==(Box2D box1, Box2D box2)
		{
			return box1.Min == box2.Min && box1.Max == box2.Max;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003423 File Offset: 0x00001623
		public static bool operator !=(Box2D box1, Box2D box2)
		{
			return !(box1 == box2);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003430 File Offset: 0x00001630
		public static Box2D operator +(Box2D box1, Box2D box2)
		{
			return new Box2D(Math.Min(box1.Min.X, box2.Min.X), Math.Min(box1.Min.Y, box2.Min.Y), Math.Max(box1.Max.X, box2.Max.X), Math.Max(box1.Max.Y, box2.Max.Y));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000034D2 File Offset: 0x000016D2
		public static Box2D operator *(Box2D box, float scale)
		{
			return new Box2D(box.Min * scale, box.Max * scale);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034F3 File Offset: 0x000016F3
		public static Box2D operator *(Box2D box, Vector2 scale)
		{
			return new Box2D(box.Min * scale, box.Max * scale);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003514 File Offset: 0x00001714
		public Box2D Intersect(Box2D other)
		{
			return new Box2D(Math.Max(this.Min.X, other.Min.X), Math.Max(this.Min.Y, other.Min.Y), Math.Min(this.Max.X, other.Max.X), Math.Min(this.Max.Y, other.Max.Y));
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000035B4 File Offset: 0x000017B4
		public override int GetHashCode()
		{
			return this.Max.GetHashCode() ^ this.Min.GetHashCode();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000035EA File Offset: 0x000017EA
		public bool Equals(Box2D other)
		{
			return this == other;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035F8 File Offset: 0x000017F8
		public override bool Equals(object obj)
		{
			return obj is Box2D && this == (Box2D)obj;
		}

		// Token: 0x0400003D RID: 61
		public static readonly Box2D Null = new Box2D
		{
			Min = new Vector2(float.PositiveInfinity, float.PositiveInfinity),
			Max = new Vector2(float.NegativeInfinity, float.NegativeInfinity)
		};

		// Token: 0x0400003E RID: 62
		public static readonly Box2D Zero = default(Box2D);
	}
}
