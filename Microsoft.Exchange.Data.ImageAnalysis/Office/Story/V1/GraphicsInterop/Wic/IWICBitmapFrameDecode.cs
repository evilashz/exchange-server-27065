using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000031 RID: 49
	[Guid("3B16811B-6A43-4EC9-A813-3D930C13B940")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapFrameDecode : IWICBitmapSource
	{
		// Token: 0x06000172 RID: 370
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x06000173 RID: 371
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x06000174 RID: 372
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x06000175 RID: 373
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000176 RID: 374
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x06000177 RID: 375
		void GetMetadataQueryReader([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryReader ppIMetadataQueryReader);

		// Token: 0x06000178 RID: 376
		void GetColorContexts([In] int cCount, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref IWICColorContext ppIColorContexts, out int pcActualCount);

		// Token: 0x06000179 RID: 377
		void GetThumbnail([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIThumbnail);
	}
}
