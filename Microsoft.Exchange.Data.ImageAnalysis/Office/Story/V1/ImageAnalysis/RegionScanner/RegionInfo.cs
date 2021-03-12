using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.CommonMath;
using Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000054 RID: 84
	[DataContract]
	[Serializable]
	internal class RegionInfo<TPixel, TValue, TTile> where TPixel : struct, IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue> where TTile : Tile<TPixel, TValue, TTile>
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00007090 File Offset: 0x00005290
		public RegionInfo(TiledImage<TPixel, TValue, TTile> parent, Func<TTile, Box2D> getOutline, Func<TTile, float> getDensity)
		{
			this.parent = parent;
			this.tileCoordinates = new List<TileCoordinate>();
			this.Cluster = new WeightedCluster<TTile>(getOutline ?? new Func<TTile, Box2D>(this.GetOutline), getDensity ?? new Func<TTile, float>(this.GetDensity));
			this.LuminanceStats = new CountStatistics();
			this.IntensityStats = new CountStatistics();
			this.CutOffRatio = RegionInfo<TPixel, TValue, TTile>.DefaultCutOffRatio;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00007105 File Offset: 0x00005305
		public int TileLinearSize
		{
			get
			{
				return this.parent.TileLinearSize;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00007112 File Offset: 0x00005312
		public Vector2 UnscaledTileSize
		{
			get
			{
				return this.parent.UnscaledTileSize;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000711F File Offset: 0x0000531F
		// (set) Token: 0x06000268 RID: 616 RVA: 0x00007127 File Offset: 0x00005327
		[DataMember]
		public Box2D CutOffRatio { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00007130 File Offset: 0x00005330
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00007138 File Offset: 0x00005338
		[DataMember]
		public CountStatistics LuminanceStats { get; protected set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00007141 File Offset: 0x00005341
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00007149 File Offset: 0x00005349
		[DataMember]
		public CountStatistics IntensityStats { get; protected set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00007152 File Offset: 0x00005352
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000715A File Offset: 0x0000535A
		[DataMember]
		public int RegionId { get; internal set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007171 File Offset: 0x00005371
		public IEnumerable<TTile> Tiles
		{
			get
			{
				return from tileCoordinate in this.tileCoordinates
				select this.parent[tileCoordinate];
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000718A File Offset: 0x0000538A
		public int TileCount
		{
			get
			{
				if (this.tileCoordinates != null)
				{
					return this.tileCoordinates.Count;
				}
				return 0;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000071A1 File Offset: 0x000053A1
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000071A9 File Offset: 0x000053A9
		[DataMember]
		public WeightedCluster<TTile> Cluster { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000071B2 File Offset: 0x000053B2
		// (set) Token: 0x06000274 RID: 628 RVA: 0x000071BA File Offset: 0x000053BA
		[DataMember]
		public Box2D BestOutline { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000071C3 File Offset: 0x000053C3
		// (set) Token: 0x06000276 RID: 630 RVA: 0x000071CB File Offset: 0x000053CB
		[IgnoreDataMember]
		internal int JumpDepth { get; set; }

		// Token: 0x06000277 RID: 631 RVA: 0x000071D4 File Offset: 0x000053D4
		public void AddTile(TTile tile)
		{
			this.tileCoordinates.Add(tile.Coordinate);
			this.LuminanceStats.Add((double)tile.Luminance);
			this.IntensityStats.Add((double)tile.Intensity);
			this.Cluster.Add(tile);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007238 File Offset: 0x00005438
		public TiledImage<TPixel, TValue, TTile> CreateRegionImage()
		{
			Vector2 min = this.Cluster.Outline.Min;
			int num = (int)(this.Cluster.Outline.Width / this.UnscaledTileSize.X);
			int num2 = (int)(this.Cluster.Outline.Height / this.UnscaledTileSize.Y);
			TTile[] array = new TTile[num * num2];
			foreach (TTile ttile in this.Tiles)
			{
				Vector2 vector = (ttile.Location.Min - min) / this.UnscaledTileSize;
				array[(int)vector.X + (int)vector.Y * num] = ttile;
			}
			return new TiledImage<TPixel, TValue, TTile>(array, num, num2, this.TileLinearSize, 1f);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007348 File Offset: 0x00005548
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} tiles @{1} ({2})", new object[]
			{
				this.TileCount,
				this.Cluster.Outline,
				this.Cluster.Center
			});
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000073A0 File Offset: 0x000055A0
		internal void Lock()
		{
			this.Cluster.Lock();
			this.BestOutline = this.ComputeBestOutline();
			this.SortTiles();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000073C0 File Offset: 0x000055C0
		protected virtual float GetDensity(TTile tile)
		{
			if (tile == null)
			{
				throw new ArgumentNullException("tile");
			}
			return tile.Intensity;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000073E2 File Offset: 0x000055E2
		protected virtual Box2D GetOutline(TTile tile)
		{
			if (tile == null)
			{
				throw new ArgumentNullException("tile");
			}
			return tile.Location;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007404 File Offset: 0x00005604
		protected virtual void SortTiles()
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007540 File Offset: 0x00005740
		private Box2D ComputeBestOutline()
		{
			Box2D cutOffCount = this.CutOffRatio * (float)this.TileCount;
			if (cutOffCount.DiagonalSize < 5f)
			{
				return this.Cluster.Outline;
			}
			Histogram<TTile> histogram = new Histogram<TTile>((TTile item) => (double)item.Location.Center.Y, (double)this.parent.Boundary.Y, (double)this.parent.Boundary.Bottom, (double)this.parent.UnscaledTileSize.Y);
			Histogram<TTile> histogram2 = new Histogram<TTile>((TTile item) => (double)item.Location.Center.X, (double)this.parent.Boundary.X, (double)this.parent.Boundary.Right, (double)this.parent.UnscaledTileSize.X);
			foreach (TTile item2 in this.Tiles)
			{
				histogram.Add(item2);
				histogram2.Add(item2);
			}
			double verticalCutThreshold = (from bin in histogram.Bins
			select bin.Items.Count).Average() / 2.0;
			double horizontalCutThreshold = (from bin in histogram2.Bins
			select bin.Items.Count).Average() / 2.0;
			double ymin = histogram.ScanForRange((double)this.Cluster.Outline.Y, (double)this.Cluster.Outline.Bottom, (Bin<TTile> bin) => (double)bin.Items.Count, (double a, double v) => a + v, (double a, double v) => a >= (double)cutOffCount.Y || v > verticalCutThreshold);
			double ymax = histogram.ScanForRange((double)this.Cluster.Outline.Bottom, (double)this.Cluster.Outline.Y, (Bin<TTile> bin) => (double)bin.Items.Count, (double a, double v) => a + v, (double a, double v) => a >= (double)cutOffCount.Bottom || v > verticalCutThreshold);
			double xmin = histogram2.ScanForRange((double)this.Cluster.Outline.X, (double)this.Cluster.Outline.Right, (Bin<TTile> bin) => (double)bin.Items.Count, (double a, double v) => a + v, (double a, double v) => a >= (double)cutOffCount.X || v > horizontalCutThreshold);
			double xmax = histogram2.ScanForRange((double)this.Cluster.Outline.Right, (double)this.Cluster.Outline.X, (Bin<TTile> bin) => (double)bin.Items.Count, (double a, double v) => a + v, (double a, double v) => a >= (double)cutOffCount.Right || v > horizontalCutThreshold);
			Box2D result = new Box2D(xmin, ymin, xmax, ymax);
			return result;
		}

		// Token: 0x040001B3 RID: 435
		private static readonly Box2D DefaultCutOffRatio = new Box2D(0.05f, 0f, 0.05f, 0.05f);

		// Token: 0x040001B4 RID: 436
		[DataMember]
		private readonly List<TileCoordinate> tileCoordinates;

		// Token: 0x040001B5 RID: 437
		[DataMember]
		private TiledImage<TPixel, TValue, TTile> parent;
	}
}
