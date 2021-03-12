using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.CommonMath;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000050 RID: 80
	[DataContract]
	[Serializable]
	internal class Tile<TPixel, TValue, TTile> where TPixel : struct, IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue> where TTile : Tile<TPixel, TValue, TTile>
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000659E File Offset: 0x0000479E
		internal Tile()
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000065A6 File Offset: 0x000047A6
		internal Tile(TiledImage<TPixel, TValue, TTile> parent, TileCoordinate coordinate)
		{
			this.Parent = parent;
			this.Coordinate = coordinate;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000065BC File Offset: 0x000047BC
		// (set) Token: 0x0600021E RID: 542 RVA: 0x000065C4 File Offset: 0x000047C4
		[DataMember]
		public TiledImage<TPixel, TValue, TTile> Parent { get; internal set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000065CD File Offset: 0x000047CD
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000065D5 File Offset: 0x000047D5
		[DataMember]
		public TileCoordinate Coordinate { get; internal set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000065DE File Offset: 0x000047DE
		// (set) Token: 0x06000222 RID: 546 RVA: 0x000065E6 File Offset: 0x000047E6
		[DataMember]
		public float Luminance { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000065EF File Offset: 0x000047EF
		// (set) Token: 0x06000224 RID: 548 RVA: 0x000065F7 File Offset: 0x000047F7
		[DataMember]
		public float Intensity { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00006600 File Offset: 0x00004800
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00006608 File Offset: 0x00004808
		[DataMember]
		public int RegisteredPixels { get; internal set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00006611 File Offset: 0x00004811
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00006619 File Offset: 0x00004819
		[DataMember]
		public bool IsProcessed { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00006622 File Offset: 0x00004822
		public int TotalPixels
		{
			get
			{
				return this.TileLinearSize * this.TileLinearSize;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006631 File Offset: 0x00004831
		public int TileLinearSize
		{
			get
			{
				return this.Parent.TileLinearSize;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000663E File Offset: 0x0000483E
		public Box2D Location
		{
			get
			{
				return this.Parent.GetTileOutline(this.Coordinate);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006651 File Offset: 0x00004851
		public bool IsBoundary
		{
			get
			{
				return this.RegisteredPixels < this.TotalPixels;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006664 File Offset: 0x00004864
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} Luminance, {1}processed @ {2}", new object[]
			{
				this.Luminance,
				this.IsProcessed ? string.Empty : "not ",
				this.Location
			});
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000066BB File Offset: 0x000048BB
		internal void Reset()
		{
			this.IsProcessed = false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000066C4 File Offset: 0x000048C4
		internal virtual void RegisterPixel(TPixel pixel)
		{
			this.Intensity += pixel.Intensity;
			this.RegisteredPixels++;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000066EE File Offset: 0x000048EE
		internal virtual void Lock()
		{
			if (this.RegisteredPixels > 0)
			{
				this.Intensity /= (float)this.RegisteredPixels;
				this.Luminance = MathHelper.ChannelLuminance(this.Intensity);
			}
		}
	}
}
