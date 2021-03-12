using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002E RID: 46
	[Guid("00000103-A8F2-4877-BA0A-FD2B6645FB94")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapEncoder
	{
		// Token: 0x0600014D RID: 333
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] WICBitmapEncoderCacheOption cacheOption);

		// Token: 0x0600014E RID: 334
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x0600014F RID: 335
		void GetEncoderInfo([MarshalAs(UnmanagedType.Interface)] out IWICBitmapEncoderInfo ppIEncoderInfo);

		// Token: 0x06000150 RID: 336
		void SetColorContexts([In] int cCount, [MarshalAs(UnmanagedType.Interface)] [In] ref IWICColorContext ppIColorContext);

		// Token: 0x06000151 RID: 337
		void SetPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000152 RID: 338
		void SetThumbnail([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIThumbnail);

		// Token: 0x06000153 RID: 339
		void SetPreview([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIPreview);

		// Token: 0x06000154 RID: 340
		void CreateNewFrame([MarshalAs(UnmanagedType.Interface)] out IWICBitmapFrameEncode ppIFrameEncode, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown, SizeConst = 1)] [Out] IPropertyBag2[] ppIEncoderOptions);

		// Token: 0x06000155 RID: 341
		void Commit();

		// Token: 0x06000156 RID: 342
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
