using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.GraphicsInterop;
using Microsoft.Office.Story.V1.GraphicsInterop.Wic;
using Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner;

namespace Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection
{
	// Token: 0x02000059 RID: 89
	[DataContract]
	[Serializable]
	internal class SalientObjectAnalysis : AnalysisBase
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00008274 File Offset: 0x00006474
		public SalientObjectAnalysis(ImageSource imageSource)
		{
			if (imageSource == null)
			{
				throw new ArgumentNullException("imageSource");
			}
			this.imageSource = imageSource;
			base.PerformAnalysis();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008297 File Offset: 0x00006497
		private SalientObjectAnalysis()
		{
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000829F File Offset: 0x0000649F
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000082A7 File Offset: 0x000064A7
		[DataMember]
		public LabTiledImage TiledImage { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000082B0 File Offset: 0x000064B0
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x000082B8 File Offset: 0x000064B8
		[DataMember]
		public List<SalientObject> SalientObjects { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000082C1 File Offset: 0x000064C1
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000082C9 File Offset: 0x000064C9
		[DataMember]
		public float SaliencyThreshold { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000082D2 File Offset: 0x000064D2
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x000082DA File Offset: 0x000064DA
		[DataMember]
		public float TotalSalience { get; set; }

		// Token: 0x060002B7 RID: 695 RVA: 0x000082E3 File Offset: 0x000064E3
		protected override bool CanAnalyze()
		{
			return this.imageSource.Height > 40f && this.imageSource.Width > 40f;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000830B File Offset: 0x0000650B
		protected override void AnalysisImplementation()
		{
			this.Detect(this.imageSource);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008319 File Offset: 0x00006519
		protected override void Lock()
		{
			this.imageSource = null;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008322 File Offset: 0x00006522
		protected override void CreateDefaultResults()
		{
			this.SalientObjects = new List<SalientObject>();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008378 File Offset: 0x00006578
		private static List<SalientObject> GroupSalientTiles(LabTiledImage saliencyMap, ref float threshold, int minimumGroupSize = 8)
		{
			if (threshold <= 0f || threshold > 1f)
			{
				CountStatistics countStatistics = saliencyMap.CreateStats((LabTile tile) => (double)tile.Saliency);
				threshold = (1f - (float)countStatistics.Average) * 0.6f;
			}
			SaliencyGrouper saliencyGrouper = new SaliencyGrouper(saliencyMap, threshold);
			List<RegionInfo<ArgbPixel, byte, LabTile>> list = saliencyGrouper.Process(minimumGroupSize);
			list.Sort((RegionInfo<ArgbPixel, byte, LabTile> first, RegionInfo<ArgbPixel, byte, LabTile> second) => second.TileCount.CompareTo(first.TileCount));
			return (from region in list
			select new SalientObject
			{
				Region = region
			}).ToList<SalientObject>();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000842C File Offset: 0x0000662C
		private void Detect(ImageSource imageSource)
		{
			IWICImagingFactory iwicimagingFactory = WicUtility.CreateFactory();
			IWICBitmapFrameDecode iwicbitmapFrameDecode = iwicimagingFactory.Load(imageSource.ImageStream);
			this.DetectSalientRegions(iwicimagingFactory, iwicbitmapFrameDecode);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicbitmapFrameDecode);
			GraphicsInteropNativeMethods.SafeReleaseComObject(iwicimagingFactory);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008474 File Offset: 0x00006674
		private void DetectSalientRegions(IWICImagingFactory factory, IWICBitmapSource bitmapSource)
		{
			int num;
			int num2;
			bitmapSource.GetSize(out num, out num2);
			int patchSize = Math.Max((num + num2) / 2 / 50, 4);
			SaliencyMap saliencyMap = new SaliencyMap(new ArgbImage(factory, bitmapSource), patchSize, 1f);
			this.TiledImage = saliencyMap.TiledImage;
			float saliencyThreshold = this.SaliencyThreshold;
			this.SalientObjects = SalientObjectAnalysis.GroupSalientTiles(this.TiledImage, ref saliencyThreshold, 8);
			this.SaliencyThreshold = saliencyThreshold;
			this.TotalSalience = 0f;
			float num3 = -1f;
			foreach (float num4 in from region in this.SalientObjects
			select region.Region.Cluster.Mass)
			{
				float num5 = num4;
				if (num3 < num5)
				{
					num3 = num5;
				}
				this.TotalSalience += num5;
			}
			foreach (SalientObject salientObject in this.SalientObjects)
			{
				salientObject.SaliencePortion = salientObject.Region.Cluster.Mass / this.TotalSalience;
				salientObject.IsPrimary = (salientObject.Region.Cluster.Mass >= num3);
			}
		}

		// Token: 0x040001DC RID: 476
		private const float MinimumImageSize = 40f;

		// Token: 0x040001DD RID: 477
		private const int TargetPatchCount = 50;

		// Token: 0x040001DE RID: 478
		[NonSerialized]
		private ImageSource imageSource;
	}
}
