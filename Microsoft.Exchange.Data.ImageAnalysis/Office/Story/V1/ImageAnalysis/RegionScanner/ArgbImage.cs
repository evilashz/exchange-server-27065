using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Office.Story.V1.GraphicsInterop.Wic;

namespace Microsoft.Office.Story.V1.ImageAnalysis.RegionScanner
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	[Serializable]
	internal class ArgbImage : ImageBase<ArgbPixel, byte>
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00005FE5 File Offset: 0x000041E5
		public ArgbImage(int width, int height) : base(width, height)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005FEF File Offset: 0x000041EF
		public ArgbImage(int width, int height, ArgbPixel[] image) : base(width, height, image)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005FFA File Offset: 0x000041FA
		public ArgbImage(Stream stream) : base(stream, ArgbImage.WicImageFormat)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006008 File Offset: 0x00004208
		public ArgbImage(IWICImagingFactory factory, IWICBitmapSource bitmapSource) : base(factory, bitmapSource, ArgbImage.WicImageFormat)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00006017 File Offset: 0x00004217
		public static Guid WicImageFormat
		{
			get
			{
				return WicGuids.GUID_WICPixelFormat32bppBGRA;
			}
		}

		// Token: 0x04000191 RID: 401
		internal const float MaximumContrastRatio = 21f;

		// Token: 0x04000192 RID: 402
		internal const float MinimumContrastRatio = 1f;
	}
}
