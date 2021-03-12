using System;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x02000055 RID: 85
	internal class SaliencyGrouper : ImageProcessor<ArgbPixel, byte, LabTile>
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000792C File Offset: 0x00005B2C
		public SaliencyGrouper(TiledImage<ArgbPixel, byte, LabTile> image, float saliencyThreshold) : base(image)
		{
			this.SaliencyThreshold = saliencyThreshold;
			base.AllAroundScan = true;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007943 File Offset: 0x00005B43
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000794B File Offset: 0x00005B4B
		public float SaliencyThreshold { get; set; }

		// Token: 0x06000290 RID: 656 RVA: 0x00007954 File Offset: 0x00005B54
		protected override bool Group(RegionInfo<ArgbPixel, byte, LabTile> context, LabTile originatingTile, LabTile currentTile)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (currentTile == null)
			{
				throw new ArgumentNullException("currentTile");
			}
			return currentTile.Saliency > this.SaliencyThreshold;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00007988 File Offset: 0x00005B88
		protected override RegionInfo<ArgbPixel, byte, LabTile> CreateRegionInfo(TiledImage<ArgbPixel, byte, LabTile> parent)
		{
			return new RegionInfo<ArgbPixel, byte, LabTile>(parent, null, (LabTile tile) => tile.Saliency);
		}
	}
}
