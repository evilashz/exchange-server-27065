using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x0200004E RID: 78
	[DataContract]
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	internal struct ArgbPixel : IPixel<byte>, IEquatable<ArgbPixel>
	{
		// Token: 0x060001FF RID: 511 RVA: 0x0000601E File Offset: 0x0000421E
		public ArgbPixel(Color color)
		{
			this.ARGB = 0U;
			this.A = color.A;
			this.R = color.R;
			this.G = color.G;
			this.B = color.B;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000605B File Offset: 0x0000425B
		[IgnoreDataMember]
		public float Luminance
		{
			get
			{
				return 0.2126f * this.Rl + 0.7152f * this.Gl + 0.0722f * this.Bl;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00006083 File Offset: 0x00004283
		[IgnoreDataMember]
		public float Intensity
		{
			get
			{
				return 0.299f * this.Rs + 0.587f * this.Gs + 0.114f * this.Bs;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000060AB File Offset: 0x000042AB
		public int Bands
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000060AE File Offset: 0x000042AE
		internal float Rs
		{
			get
			{
				return (float)this.R * 0.003921569f;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000060BD File Offset: 0x000042BD
		internal float Gs
		{
			get
			{
				return (float)this.G * 0.003921569f;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000060CC File Offset: 0x000042CC
		internal float Bs
		{
			get
			{
				return (float)this.G * 0.003921569f;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000060DB File Offset: 0x000042DB
		internal float Rl
		{
			get
			{
				return ArgbPixel.ChannelLuminance[(int)this.R];
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000060E9 File Offset: 0x000042E9
		internal float Gl
		{
			get
			{
				return ArgbPixel.ChannelLuminance[(int)this.G];
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000060F7 File Offset: 0x000042F7
		internal float Bl
		{
			get
			{
				return ArgbPixel.ChannelLuminance[(int)this.B];
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00006108 File Offset: 0x00004308
		private static float[] ChannelLuminance
		{
			get
			{
				if (ArgbPixel.channelLuminanceCache == null)
				{
					float[] array = new float[256];
					for (int i = 0; i <= 255; i++)
					{
						float num = (float)i * 0.003921569f;
						array[i] = ((num <= 0.03928f) ? (num / 12.92f) : ((float)Math.Pow((double)((num + 0.055f) / 1.055f), 2.4000000953674316)));
					}
					ArgbPixel.channelLuminanceCache = array;
				}
				return ArgbPixel.channelLuminanceCache;
			}
		}

		// Token: 0x1700004D RID: 77
		public byte this[int band]
		{
			get
			{
				switch (band)
				{
				case 0:
					return this.B;
				case 1:
					return this.G;
				case 2:
					return this.R;
				case 3:
					return this.A;
				default:
					throw new IndexOutOfRangeException("band");
				}
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000061C9 File Offset: 0x000043C9
		public static bool operator ==(ArgbPixel one, ArgbPixel another)
		{
			return one.Equals(another);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000061D3 File Offset: 0x000043D3
		public static bool operator !=(ArgbPixel one, ArgbPixel another)
		{
			return !one.Equals(another);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000061E0 File Offset: 0x000043E0
		public int CompareByIntensity(IPixel<byte> another)
		{
			if (another == null)
			{
				throw new ArgumentNullException("another");
			}
			return this.Intensity.CompareTo(another.Intensity);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000620F File Offset: 0x0000440F
		public override bool Equals(object other)
		{
			return other is ArgbPixel && this.Equals((ArgbPixel)other);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006227 File Offset: 0x00004427
		public override int GetHashCode()
		{
			return this.ARGB.GetHashCode();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006234 File Offset: 0x00004434
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "A:{0:X2} R:{1:X2} G:{2:X2} B:{3:X2}", new object[]
			{
				this.A,
				this.R,
				this.G,
				this.B
			});
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006290 File Offset: 0x00004490
		public bool Equals(ArgbPixel other)
		{
			return this.ARGB.Equals(other.ARGB);
		}

		// Token: 0x04000193 RID: 403
		internal const int MaxChannelValue = 255;

		// Token: 0x04000194 RID: 404
		internal const float NormalizationCoefficient = 0.003921569f;

		// Token: 0x04000195 RID: 405
		[DataMember]
		[FieldOffset(0)]
		public uint ARGB;

		// Token: 0x04000196 RID: 406
		[IgnoreDataMember]
		[NonSerialized]
		[FieldOffset(3)]
		public byte A;

		// Token: 0x04000197 RID: 407
		[IgnoreDataMember]
		[NonSerialized]
		[FieldOffset(2)]
		public byte R;

		// Token: 0x04000198 RID: 408
		[IgnoreDataMember]
		[NonSerialized]
		[FieldOffset(1)]
		public byte G;

		// Token: 0x04000199 RID: 409
		[IgnoreDataMember]
		[NonSerialized]
		[FieldOffset(0)]
		public byte B;

		// Token: 0x0400019A RID: 410
		private static float[] channelLuminanceCache;
	}
}
