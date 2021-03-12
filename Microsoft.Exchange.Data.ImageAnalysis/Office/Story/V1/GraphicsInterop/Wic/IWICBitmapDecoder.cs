using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002C RID: 44
	[Guid("9EDDE9E7-8DEE-47EA-99DF-E6FAF2ED44BF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapDecoder
	{
		// Token: 0x0600012B RID: 299
		void QueryCapability([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, out int pdwCapability);

		// Token: 0x0600012C RID: 300
		void Initialize([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [In] WICDecodeOptions cacheOptions);

		// Token: 0x0600012D RID: 301
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x0600012E RID: 302
		void GetDecoderInfo([MarshalAs(UnmanagedType.Interface)] out IWICBitmapDecoderInfo ppIDecoderInfo);

		// Token: 0x0600012F RID: 303
		void CopyPalette([MarshalAs(UnmanagedType.Interface)] [In] IWICPalette pIPalette);

		// Token: 0x06000130 RID: 304
		void GetMetadataQueryReader([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryReader ppIMetadataQueryReader);

		// Token: 0x06000131 RID: 305
		void GetPreview([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIBitmapSource);

		// Token: 0x06000132 RID: 306
		void GetColorContexts([In] int cCount, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref IWICColorContext ppIColorContexts, out int pcActualCount);

		// Token: 0x06000133 RID: 307
		void GetThumbnail([MarshalAs(UnmanagedType.Interface)] out IWICBitmapSource ppIThumbnail);

		// Token: 0x06000134 RID: 308
		void GetFrameCount(out int pCount);

		// Token: 0x06000135 RID: 309
		void GetFrame([In] int index, [MarshalAs(UnmanagedType.Interface)] out IWICBitmapFrameDecode ppIBitmapFrame);
	}
}
