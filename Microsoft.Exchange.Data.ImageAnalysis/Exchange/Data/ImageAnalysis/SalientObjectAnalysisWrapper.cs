using System;
using System.Collections.Generic;
using Microsoft.Office.Story.V1.ImageAnalysis;
using Microsoft.Office.Story.V1.ImageAnalysis.SalientObjectDetection;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x02000010 RID: 16
	internal class SalientObjectAnalysisWrapper : ISalientObjectAnalysis
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00002E60 File Offset: 0x00001060
		internal SalientObjectAnalysisWrapper(byte[] image, int imageWidth, int imageHeight)
		{
			this.image = image;
			this.imageWidth = imageWidth;
			this.imageHeight = imageHeight;
			this.imageSource = null;
			this.info = null;
			this.salientAnalysis = null;
			this.salientRegions = null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002E9C File Offset: 0x0000109C
		public KeyValuePair<List<RegionRect>, ImageAnalysisResult> GetSalientRectsAsList()
		{
			ImageAnalysisResult imageAnalysisResult = this.EnsureSalientObjectAnalysisWrapper();
			if (this.salientRegions == null && imageAnalysisResult == ImageAnalysisResult.SalientRegionSuccess)
			{
				this.salientRegions = new List<RegionRect>();
				foreach (SalientObject salientObject in this.salientAnalysis.SalientObjects)
				{
					RegionRect item = new RegionRect((int)salientObject.Region.BestOutline.Left, (int)salientObject.Region.BestOutline.Top, (int)salientObject.Region.BestOutline.Right, (int)salientObject.Region.BestOutline.Bottom);
					this.salientRegions.Add(item);
				}
				imageAnalysisResult = ImageAnalysisResult.SalientRegionSuccess;
			}
			else
			{
				imageAnalysisResult = ImageAnalysisResult.UnableToPerformSalientRegionAnalysis;
			}
			return new KeyValuePair<List<RegionRect>, ImageAnalysisResult>(this.salientRegions, imageAnalysisResult);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002F8C File Offset: 0x0000118C
		public KeyValuePair<byte[], ImageAnalysisResult> GetSalientRectsAsByteArray()
		{
			ImageAnalysisResult imageAnalysisResult = this.EnsureSalientObjectAnalysisWrapper();
			if (this.salientRegionsAsByte == null && imageAnalysisResult == ImageAnalysisResult.SalientRegionSuccess)
			{
				this.salientRegionsAsByte = new byte[this.salientAnalysis.SalientObjects.Count * 4];
				int num = 0;
				foreach (SalientObject salientObject in this.salientAnalysis.SalientObjects)
				{
					this.salientRegionsAsByte[num] = (byte)((int)salientObject.Region.BestOutline.Top * 255 / this.imageHeight);
					this.salientRegionsAsByte[num + 1] = (byte)((int)salientObject.Region.BestOutline.Left * 255 / this.imageWidth);
					this.salientRegionsAsByte[num + 2] = (byte)((int)salientObject.Region.BestOutline.Bottom * 255 / this.imageHeight);
					this.salientRegionsAsByte[num + 3] = (byte)((int)salientObject.Region.BestOutline.Right * 255 / this.imageWidth);
					num += 4;
				}
				imageAnalysisResult = ImageAnalysisResult.SalientRegionSuccess;
			}
			else
			{
				imageAnalysisResult = ImageAnalysisResult.UnableToPerformSalientRegionAnalysis;
			}
			return new KeyValuePair<byte[], ImageAnalysisResult>(this.salientRegionsAsByte, imageAnalysisResult);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030E8 File Offset: 0x000012E8
		private ImageAnalysisResult EnsureSalientObjectAnalysisWrapper()
		{
			try
			{
				if (this.imageSource == null)
				{
					this.imageSource = new ImageSource(this.image);
				}
				if (this.info == null)
				{
					this.info = new ImageInfo(this.imageSource);
				}
				if (this.salientAnalysis == null)
				{
					this.salientAnalysis = this.info.PerformSalientObjectAnalysis();
				}
			}
			catch (Exception)
			{
				return ImageAnalysisResult.UnableToPerformSalientRegionAnalysis;
			}
			return ImageAnalysisResult.SalientRegionSuccess;
		}

		// Token: 0x04000035 RID: 53
		private readonly int imageWidth;

		// Token: 0x04000036 RID: 54
		private readonly int imageHeight;

		// Token: 0x04000037 RID: 55
		private ImageSource imageSource;

		// Token: 0x04000038 RID: 56
		private ImageInfo info;

		// Token: 0x04000039 RID: 57
		private SalientObjectAnalysis salientAnalysis;

		// Token: 0x0400003A RID: 58
		private List<RegionRect> salientRegions;

		// Token: 0x0400003B RID: 59
		private byte[] salientRegionsAsByte;

		// Token: 0x0400003C RID: 60
		private byte[] image;
	}
}
