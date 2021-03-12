using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000053 RID: 83
	[DataContract]
	[Serializable]
	internal class LabTiledImage : TiledImage<ArgbPixel, byte, LabTile>
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000706C File Offset: 0x0000526C
		public LabTiledImage(ImageBase<ArgbPixel, byte> image, int tileSize, float scale) : base(image, tileSize, scale, false, new Func<TiledImage<ArgbPixel, byte, LabTile>, TileCoordinate, LabTile>(LabTiledImage.CreateTile))
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00007084 File Offset: 0x00005284
		private static LabTile CreateTile(TiledImage<ArgbPixel, byte, LabTile> parent, TileCoordinate location)
		{
			return new LabTile(parent, location);
		}
	}
}
