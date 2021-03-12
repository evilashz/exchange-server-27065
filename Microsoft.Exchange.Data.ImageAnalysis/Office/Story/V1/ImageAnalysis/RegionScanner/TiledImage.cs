using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.CommonMath;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000052 RID: 82
	[DataContract]
	[Serializable]
	internal class TiledImage<TPixel, TValue, TTile> : IEnumerable<TTile>, IEnumerable where TPixel : struct, IPixel<TValue> where TValue : struct, IComparable, IFormattable, IComparable<TValue>, IEquatable<TValue> where TTile : Tile<TPixel, TValue, TTile>
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00006AA4 File Offset: 0x00004CA4
		public TiledImage(ImageBase<TPixel, TValue> image, int tileSize, float scale, bool boundaryTilesAllowed, Func<TiledImage<TPixel, TValue, TTile>, TileCoordinate, TTile> tileCreator)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.Scale = scale;
			this.BoundaryTilesAllowed = boundaryTilesAllowed;
			this.Boundary = Box2D.FromSize(0f, 0f, (float)image.Width * this.Scale, (float)image.Height * this.Scale);
			this.TileLinearSize = tileSize;
			this.TileSize = new Vector2((float)tileSize);
			this.UnscaledTileSize = this.TileSize * this.Scale;
			this.CreateTiles(image, tileCreator);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006B3C File Offset: 0x00004D3C
		public TiledImage(TTile[] tiles, int widthInTiles, int heightInTiles, int tileLinearSize, float scale)
		{
			if (tiles == null)
			{
				throw new ArgumentNullException("tiles");
			}
			if (widthInTiles < 1 || heightInTiles < 1)
			{
				throw new ArgumentException("Width and height should be positive number above zero.");
			}
			if (tiles.Length < widthInTiles * heightInTiles)
			{
				throw new ArgumentException("Array of tiles must have at least widthInTiles * heightInTiles tiles.");
			}
			this.Scale = scale;
			this.TileLinearSize = tileLinearSize;
			this.WidthInTiles = widthInTiles;
			this.HeightInTiles = heightInTiles;
			this.Boundary = Box2D.FromSize(0f, 0f, (float)(this.TileLinearSize * this.WidthInTiles) * this.Scale, (float)(this.TileLinearSize * heightInTiles) * this.Scale);
			this.TileSize = new Vector2((float)this.TileLinearSize);
			this.UnscaledTileSize = this.TileSize * this.Scale;
			this.tiles = tiles;
			for (int i = 0; i < this.tiles.Length; i++)
			{
				TTile ttile = tiles[i];
				if (ttile != null)
				{
					if (ttile.Parent == null)
					{
						ttile.Parent = this;
						ttile.Coordinate = this.CreateCoordinate(i);
					}
					else if (ttile.TileLinearSize != this.TileLinearSize)
					{
						throw new InvalidOperationException("Parented tile size does not match this image.");
					}
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006C81 File Offset: 0x00004E81
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00006C89 File Offset: 0x00004E89
		[DataMember]
		public bool BoundaryTilesAllowed { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006C92 File Offset: 0x00004E92
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00006C9A File Offset: 0x00004E9A
		[DataMember]
		public float Scale { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00006CA3 File Offset: 0x00004EA3
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00006CAB File Offset: 0x00004EAB
		[DataMember]
		public int WidthInTiles { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00006CB4 File Offset: 0x00004EB4
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00006CBC File Offset: 0x00004EBC
		[DataMember]
		public int HeightInTiles { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00006CC5 File Offset: 0x00004EC5
		// (set) Token: 0x0600024D RID: 589 RVA: 0x00006CCD File Offset: 0x00004ECD
		[DataMember]
		public Vector2 TileSize { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00006CD6 File Offset: 0x00004ED6
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00006CDE File Offset: 0x00004EDE
		[DataMember]
		public int TileLinearSize { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00006CE7 File Offset: 0x00004EE7
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00006CEF File Offset: 0x00004EEF
		[DataMember]
		public Box2D Boundary { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public int AreaInTiles
		{
			get
			{
				return this.WidthInTiles * this.HeightInTiles;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00006D07 File Offset: 0x00004F07
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00006D0F File Offset: 0x00004F0F
		[DataMember]
		internal Vector2 UnscaledTileSize { get; private set; }

		// Token: 0x17000066 RID: 102
		public TTile this[int tile]
		{
			get
			{
				return this.tiles[tile];
			}
			private set
			{
				this.tiles[tile] = value;
			}
		}

		// Token: 0x17000067 RID: 103
		public TTile this[int x, int y]
		{
			get
			{
				return this[y * this.WidthInTiles + x];
			}
		}

		// Token: 0x17000068 RID: 104
		public TTile this[TileCoordinate coordinate]
		{
			get
			{
				return this[(int)coordinate.Index];
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006D56 File Offset: 0x00004F56
		public TileCoordinate CreateCoordinate(int n)
		{
			return new TileCoordinate(n);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006D60 File Offset: 0x00004F60
		public TTile GetTileOrDefault(Point location)
		{
			TTile result = default(TTile);
			int x = location.X;
			int y = location.Y;
			if (x >= 0 && x < this.WidthInTiles && y >= 0 && y < this.HeightInTiles)
			{
				result = this[x, y];
			}
			return result;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public CountStatistics CreateStats(Func<TTile, double> extractor)
		{
			return new CountStatistics(from tile in this.tiles
			select extractor(tile));
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public Box2D GetTileOutline(TileCoordinate coordinate)
		{
			int num = (int)coordinate.Index % this.WidthInTiles;
			int num2 = (int)coordinate.Index / this.WidthInTiles;
			return Box2D.FromSize(this.Boundary.Min + this.UnscaledTileSize * new Vector2((float)num, (float)num2), this.UnscaledTileSize);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00006E58 File Offset: 0x00005058
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0} by {1} tiles of size {2} and type {3}.", new object[]
			{
				this.WidthInTiles,
				this.HeightInTiles,
				this.TileLinearSize,
				typeof(TTile).Name
			});
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006EB8 File Offset: 0x000050B8
		public IEnumerator<TTile> GetEnumerator()
		{
			return ((IEnumerable<TTile>)this.tiles).GetEnumerator();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006ECA File Offset: 0x000050CA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.tiles.GetEnumerator();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006ED7 File Offset: 0x000050D7
		private static TTile CreateTile(TiledImage<TPixel, TValue, TTile> parent, TileCoordinate coordinate)
		{
			return (TTile)((object)new Tile<TPixel, TValue, TTile>(parent, coordinate));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006EE8 File Offset: 0x000050E8
		private void CreateTiles(ImageBase<TPixel, TValue> image, Func<TiledImage<TPixel, TValue, TTile>, TileCoordinate, TTile> tileCreator)
		{
			tileCreator = (tileCreator ?? new Func<TiledImage<TPixel, TValue, TTile>, TileCoordinate, TTile>(TiledImage<TPixel, TValue, TTile>.CreateTile));
			this.WidthInTiles = image.Width / this.TileLinearSize;
			this.HeightInTiles = image.Height / this.TileLinearSize;
			if (this.BoundaryTilesAllowed)
			{
				if (image.Width % this.TileLinearSize > 0)
				{
					this.WidthInTiles++;
				}
				if (image.Height % this.TileLinearSize > 0)
				{
					this.HeightInTiles++;
				}
			}
			this.tiles = new TTile[this.WidthInTiles * this.HeightInTiles];
			for (int i = 0; i < this.tiles.Length; i++)
			{
				this[i] = tileCreator(this, this.CreateCoordinate(i));
			}
			int num = this.BoundaryTilesAllowed ? image.Width : (this.WidthInTiles * this.TileLinearSize);
			int num2 = this.BoundaryTilesAllowed ? image.Height : (this.HeightInTiles * this.TileLinearSize);
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num; k++)
				{
					TTile ttile = this[k / this.TileLinearSize, j / this.TileLinearSize];
					ttile.RegisterPixel(image[k, j]);
				}
			}
			foreach (TTile ttile2 in this.tiles)
			{
				ttile2.Lock();
			}
		}

		// Token: 0x040001AA RID: 426
		[DataMember]
		private TTile[] tiles;
	}
}
