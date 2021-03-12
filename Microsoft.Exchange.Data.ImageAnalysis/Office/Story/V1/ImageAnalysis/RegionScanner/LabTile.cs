using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000051 RID: 81
	[DataContract]
	[Serializable]
	internal class LabTile : Tile<ArgbPixel, byte, LabTile>
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000671E File Offset: 0x0000491E
		public LabTile()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006726 File Offset: 0x00004926
		public LabTile(TiledImage<ArgbPixel, byte, LabTile> parent, TileCoordinate location) : base(parent, location)
		{
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006730 File Offset: 0x00004930
		// (set) Token: 0x06000234 RID: 564 RVA: 0x00006738 File Offset: 0x00004938
		[DataMember]
		public float L { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00006741 File Offset: 0x00004941
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00006749 File Offset: 0x00004949
		[DataMember]
		public float A { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006752 File Offset: 0x00004952
		// (set) Token: 0x06000238 RID: 568 RVA: 0x0000675A File Offset: 0x0000495A
		[DataMember]
		public float B { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00006763 File Offset: 0x00004963
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000676B File Offset: 0x0000496B
		[DataMember]
		public float Saliency { get; set; }

		// Token: 0x0600023B RID: 571 RVA: 0x00006774 File Offset: 0x00004974
		public float ComputeLabDistance(LabTile other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			float num = (this.L - other.L) * (this.L - other.L) + (this.A - other.A) * (this.A - other.A) + (this.B - other.B) * (this.B - other.B);
			return (float)Math.Sqrt((double)num);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000067EC File Offset: 0x000049EC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}\t{1}\t{2} @ {3}", new object[]
			{
				this.L,
				this.A,
				this.B,
				base.Location
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006848 File Offset: 0x00004A48
		internal override void RegisterPixel(ArgbPixel pixel)
		{
			this.L += (float)pixel.R;
			this.A += (float)pixel.G;
			this.B += (float)pixel.B;
			base.RegisterPixel(pixel);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000689C File Offset: 0x00004A9C
		internal override void Lock()
		{
			float num = this.L / (float)base.RegisteredPixels * 0.003921569f;
			float num2 = this.A / (float)base.RegisteredPixels * 0.003921569f;
			float num3 = this.B / (float)base.RegisteredPixels * 0.003921569f;
			LabTile.RgbToLab(num, num2, num3, out num, out num2, out num3);
			this.L = num;
			this.A = num2;
			this.B = num3;
			base.Lock();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006911 File Offset: 0x00004B11
		private static float Fxyz(float t)
		{
			return (float)((t > 0.008856f) ? Math.Pow((double)t, 0.3333333333333333) : ((double)(7.787f * t + 0.13793103f)));
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000693C File Offset: 0x00004B3C
		private static float LinearToSrgb(float linear)
		{
			if (linear <= 0.04045f)
			{
				return linear / 12.92f;
			}
			return (float)Math.Pow((double)((linear + 0.055f) / 1.055f), 2.200000047683716);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000696C File Offset: 0x00004B6C
		private static void RgbToLab(float rLinear, float gLinear, float bLinear, out float l, out float a, out float b)
		{
			float num = LabTile.LinearToSrgb(rLinear);
			float num2 = LabTile.LinearToSrgb(gLinear);
			float num3 = LabTile.LinearToSrgb(bLinear);
			float num4 = num * 0.4124f + num2 * 0.3576f + num3 * 0.1805f;
			float num5 = num * 0.2126f + num2 * 0.7152f + num3 * 0.0722f;
			float num6 = num * 0.0193f + num2 * 0.1192f + num3 * 0.9505f;
			num4 = ((num4 > 0.9505f) ? 0.9505f : ((num4 < 0f) ? 0f : num4));
			num5 = ((num5 > 1f) ? 1f : ((num5 < 0f) ? 0f : num5));
			num6 = ((num6 > 1.089f) ? 1.089f : ((num6 < 0f) ? 0f : num6));
			l = 116f * LabTile.Fxyz(num5 * 1f) - 16f;
			a = 500f * (LabTile.Fxyz(num4 * 1.0520779f) - LabTile.Fxyz(num5 * 1f));
			b = 200f * (LabTile.Fxyz(num5 * 1f) - LabTile.Fxyz(num6 * 0.9182736f));
		}
	}
}
