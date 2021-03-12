using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Office.Story.V1.GraphicsInterop.Misc;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000039 RID: 57
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("EC5EC8A9-C395-4314-9C77-54D7A935FF70")]
	internal interface IWICImagingFactory
	{
		// Token: 0x060001A4 RID: 420
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromFilename([MarshalAs(UnmanagedType.LPWStr)] [In] string wzFilename, [In] NullableGuid pguidVendor, [In] GenericAccess dwDesiredAccess, [In] WICDecodeOptions metadataOptions);

		// Token: 0x060001A5 RID: 421
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromStream([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] [In] [Out] Guid[] pguidVendor, [In] WICDecodeOptions metadataOptions);

		// Token: 0x060001A6 RID: 422
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoderFromFileHandle([In] IntPtr hFile, [In] ref Guid pguidVendor, [In] WICDecodeOptions metadataOptions);

		// Token: 0x060001A7 RID: 423
		void CreateComponentInfo([In] ref Guid clsidComponent, [MarshalAs(UnmanagedType.Interface)] out IWICComponentInfo ppIInfo);

		// Token: 0x060001A8 RID: 424
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapDecoder CreateDecoder([In] ref Guid guidContainerFormat, [In] ref Guid pguidVendor);

		// Token: 0x060001A9 RID: 425
		[return: MarshalAs(UnmanagedType.Interface)]
		IWICBitmapEncoder CreateEncoder([In] ref Guid guidContainerFormat, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] [In] [Out] Guid[] pguidVendor);

		// Token: 0x060001AA RID: 426
		void CreatePalette([MarshalAs(UnmanagedType.Interface)] out IWICPalette ppIPalette);

		// Token: 0x060001AB RID: 427
		void CreateFormatConverter([MarshalAs(UnmanagedType.Interface)] out IWICFormatConverter ppIFormatConverter);

		// Token: 0x060001AC RID: 428
		void CreateBitmapScaler([MarshalAs(UnmanagedType.Interface)] out IWICBitmapScaler ppIBitmapScaler);

		// Token: 0x060001AD RID: 429
		void CreateBitmapClipper([MarshalAs(UnmanagedType.Interface)] out IWICBitmapClipper ppIBitmapClipper);

		// Token: 0x060001AE RID: 430
		void CreateBitmapFlipRotator([MarshalAs(UnmanagedType.Interface)] out IWICBitmapFlipRotator ppIBitmapFlipRotator);

		// Token: 0x060001AF RID: 431
		void CreateStream([MarshalAs(UnmanagedType.Interface)] out IWICStream ppIWICStream);

		// Token: 0x060001B0 RID: 432
		void CreateColorContext([MarshalAs(UnmanagedType.Interface)] out IWICColorContext ppIWICColorContext);

		// Token: 0x060001B1 RID: 433
		void CreateColorTransformer([MarshalAs(UnmanagedType.Interface)] out IWICColorTransform ppIWICColorTransform);

		// Token: 0x060001B2 RID: 434
		void CreateBitmap([In] int uiWidth, [In] int uiHeight, [In] ref Guid pixelFormat, [In] WICBitmapCreateCacheOption option, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B3 RID: 435
		void CreateBitmapFromSource([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [In] WICBitmapCreateCacheOption option, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B4 RID: 436
		void CreateBitmapFromSourceRect([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapSource pIBitmapSource, [In] int X, [In] int Y, [In] int Width, [In] int Height, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B5 RID: 437
		void CreateBitmapFromMemory([In] int uiWidth, [In] int uiHeight, [In] ref Guid pixelFormat, [In] int cbStride, [In] int cbBufferSize, [In] IntPtr pbBuffer, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B6 RID: 438
		void CreateBitmapFromHBITMAP([In] ref HBITMAP hBitmap, [In] ref HPALETTE hPalette, [In] WICBitmapAlphaChannelOption options, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B7 RID: 439
		void CreateBitmapFromHICON([In] ref IntPtr hIcon, [MarshalAs(UnmanagedType.Interface)] out IWICBitmap ppIBitmap);

		// Token: 0x060001B8 RID: 440
		void CreateComponentEnumerator([In] int componentTypes, [In] int options, [MarshalAs(UnmanagedType.Interface)] out IEnumUnknown ppIEnumUnknown);

		// Token: 0x060001B9 RID: 441
		void CreateFastMetadataEncoderFromDecoder([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapDecoder pIDecoder, [MarshalAs(UnmanagedType.Interface)] out IWICFastMetadataEncoder ppIFastEncoder);

		// Token: 0x060001BA RID: 442
		void CreateFastMetadataEncoderFromFrameDecode([MarshalAs(UnmanagedType.Interface)] [In] IWICBitmapFrameDecode pIFrameDecoder, [MarshalAs(UnmanagedType.Interface)] out IWICFastMetadataEncoder ppIFastEncoder);

		// Token: 0x060001BB RID: 443
		void CreateQueryWriter([In] ref Guid guidMetadataFormat, [In] ref Guid pguidVendor, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIQueryWriter);

		// Token: 0x060001BC RID: 444
		void CreateQueryWriterFromReader([MarshalAs(UnmanagedType.Interface)] [In] IWICMetadataQueryReader pIQueryReader, [In] ref Guid pguidVendor, [MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIQueryWriter);
	}
}
