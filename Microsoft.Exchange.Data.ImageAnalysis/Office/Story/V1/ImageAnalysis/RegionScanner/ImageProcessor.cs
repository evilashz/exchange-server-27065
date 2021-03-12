using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x0200004F RID: 79
	internal abstract class ImageProcessor<TPixel, TValue, TTile> where TPixel : struct, IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue> where TTile : Tile<TPixel, TValue, TTile>
	{
		// Token: 0x06000212 RID: 530 RVA: 0x000062A4 File Offset: 0x000044A4
		protected ImageProcessor(TiledImage<TPixel, TValue, TTile> image)
		{
			this.tiledImage = image;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000062B3 File Offset: 0x000044B3
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000062BB File Offset: 0x000044BB
		public bool AllAroundScan { get; set; }

		// Token: 0x06000215 RID: 533 RVA: 0x000062C4 File Offset: 0x000044C4
		public List<RegionInfo<TPixel, TValue, TTile>> Process(int minimumGroupSize)
		{
			List<RegionInfo<TPixel, TValue, TTile>> list = this.ProcessTiles(minimumGroupSize);
			foreach (RegionInfo<TPixel, TValue, TTile> regionInfo in list)
			{
				regionInfo.Lock();
			}
			return list;
		}

		// Token: 0x06000216 RID: 534
		protected abstract RegionInfo<TPixel, TValue, TTile> CreateRegionInfo(TiledImage<TPixel, TValue, TTile> parent);

		// Token: 0x06000217 RID: 535
		protected abstract bool Group(RegionInfo<TPixel, TValue, TTile> context, TTile originatingTile, TTile currentTile);

		// Token: 0x06000218 RID: 536 RVA: 0x0000631C File Offset: 0x0000451C
		private List<RegionInfo<TPixel, TValue, TTile>> ProcessTiles(int minimumGroupSize)
		{
			int num = 0;
			List<RegionInfo<TPixel, TValue, TTile>> list = new List<RegionInfo<TPixel, TValue, TTile>>();
			RegionInfo<TPixel, TValue, TTile> regionInfo = this.CreateRegionInfo(this.tiledImage);
			regionInfo.RegionId = num++;
			for (int i = 0; i < this.tiledImage.HeightInTiles; i++)
			{
				for (int j = 0; j < this.tiledImage.WidthInTiles; j++)
				{
					if (this.ProcessTile(regionInfo, new Point(j, i)))
					{
						if (regionInfo.TileCount > minimumGroupSize)
						{
							list.Add(regionInfo);
						}
						regionInfo = this.CreateRegionInfo(this.tiledImage);
						regionInfo.RegionId = num++;
					}
				}
			}
			return list;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000063B4 File Offset: 0x000045B4
		private bool ProcessTile(RegionInfo<TPixel, TValue, TTile> context, Point location)
		{
			TTile tileOrDefault = this.tiledImage.GetTileOrDefault(location);
			if (tileOrDefault == null || tileOrDefault.IsProcessed)
			{
				return false;
			}
			tileOrDefault.IsProcessed = true;
			if (!this.Group(context, default(TTile), tileOrDefault))
			{
				return false;
			}
			Queue<Point> queue = new Queue<Point>();
			queue.Enqueue(location);
			while (queue.Count > 0)
			{
				location = queue.Dequeue();
				TTile tileOrDefault2 = this.tiledImage.GetTileOrDefault(location);
				context.AddTile(tileOrDefault2);
				foreach (Point pt in ImageProcessor<TPixel, TValue, TTile>.ScanDirections.Take(this.AllAroundScan ? 8 : 4))
				{
					Point point = pt + new Size(location);
					TTile tileOrDefault3 = this.tiledImage.GetTileOrDefault(point);
					if (tileOrDefault3 != null && !tileOrDefault3.IsProcessed)
					{
						tileOrDefault3.IsProcessed = true;
						if (this.Group(context, tileOrDefault2, tileOrDefault3))
						{
							queue.Enqueue(point);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0400019B RID: 411
		private const int DirectionsAll = 8;

		// Token: 0x0400019C RID: 412
		private const int DirectionsNormal = 4;

		// Token: 0x0400019D RID: 413
		private static readonly Point[] ScanDirections = new Point[]
		{
			new Point(1, 0),
			new Point(1, 1),
			new Point(0, 1),
			new Point(-1, 1),
			new Point(-1, 0),
			new Point(-1, -1),
			new Point(0, -1),
			new Point(1, -1)
		};

		// Token: 0x0400019E RID: 414
		private readonly TiledImage<TPixel, TValue, TTile> tiledImage;
	}
}
