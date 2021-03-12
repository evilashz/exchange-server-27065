using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000030 RID: 48
	[Guid("5009834F-2D6A-41CE-9E1B-17C5AFF7A782")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapFlipRotator : IWICBitmapSource
	{
		// Token: 0x0600016C RID: 364
		void GetSize(out int puiWidth, out int puiHeight);

		// Token: 0x0600016D RID: 365
		void GetPixelFormat(out Guid pPixelFormat);

		// Token: 0x0600016E RID: 366
		void GetResolution(out double pDpiX, out double pDpiY);

		// Token: 0x0600016F RID: 367
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000170 RID: 368
		void CopyPixels([In] ref WICRect prc, [In] int cbStride, [In] int cbBufferSize, IntPtr pbBuffer);

		// Token: 0x06000171 RID: 369
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pISource, [In] WICBitmapTransformOptions options);
	}
}
