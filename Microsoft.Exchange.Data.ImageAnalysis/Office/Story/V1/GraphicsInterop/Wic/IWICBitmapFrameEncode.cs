using System;
using System.Runtime.InteropServices;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000032 RID: 50
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("00000105-A8F2-4877-BA0A-FD2B6645FB94")]
	internal interface IWICBitmapFrameEncode
	{
		// Token: 0x0600017A RID: 378
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IPropertyBag2 pIEncoderOptions);

		// Token: 0x0600017B RID: 379
		void SetSize([In] int uiWidth, [In] int uiHeight);

		// Token: 0x0600017C RID: 380
		void SetResolution([In] double dpiX, [In] double dpiY);

		// Token: 0x0600017D RID: 381
		void SetPixelFormat([In] [Out] ref Guid pPixelFormat);

		// Token: 0x0600017E RID: 382
		void SetColorContexts([In] int cCount, [MarshalAs(UnmanagedType.Interface)] [In] ref IWICColorContext ppIColorContext);

		// Token: 0x0600017F RID: 383
		void SetPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000180 RID: 384
		void SetThumbnail([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIThumbnail);

		// Token: 0x06000181 RID: 385
		void WritePixels([In] int lineCount, [In] int cbStride, [In] int cbBufferSize, [In] ref byte pbPixels);

		// Token: 0x06000182 RID: 386
		void WriteSource([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] [In] params WICRect[] prc);

		// Token: 0x06000183 RID: 387
		void Commit();

		// Token: 0x06000184 RID: 388
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
