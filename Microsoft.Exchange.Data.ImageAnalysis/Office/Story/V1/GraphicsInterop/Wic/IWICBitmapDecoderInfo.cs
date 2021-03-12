using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002D RID: 45
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("D8CD007F-D08F-4191-9BFC-236EA7F0E4B5")]
	internal interface IWICBitmapDecoderInfo : IWICBitmapCodecInfo, IWICComponentInfo
	{
		// Token: 0x06000136 RID: 310
		void GetComponentType(out WICComponentType pType);

		// Token: 0x06000137 RID: 311
		void GetCLSID(out Guid pclsid);

		// Token: 0x06000138 RID: 312
		void GetSigningStatus(out int pStatus);

		// Token: 0x06000139 RID: 313
		void GetAuthor([In] int cchAuthor, [In] [Out] ref ushort wzAuthor, out int pcchActual);

		// Token: 0x0600013A RID: 314
		void GetVendorGUID(out Guid pguidVendor);

		// Token: 0x0600013B RID: 315
		void GetVersion([In] int cchVersion, [In] [Out] ref ushort wzVersion, out int pcchActual);

		// Token: 0x0600013C RID: 316
		void GetSpecVersion([In] int cchSpecVersion, [In] [Out] ref ushort wzSpecVersion, out int pcchActual);

		// Token: 0x0600013D RID: 317
		void GetFriendlyName([In] int cchFriendlyName, [In] [Out] ref ushort wzFriendlyName, out int pcchActual);

		// Token: 0x0600013E RID: 318
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x0600013F RID: 319
		void GetPixelFormats([In] int cFormats, [In] [Out] ref Guid pguidPixelFormats, out int pcActual);

		// Token: 0x06000140 RID: 320
		void GetColorManagementVersion([In] int cchColorManagementVersion, [In] [Out] ref ushort wzColorManagementVersion, out int pcchActual);

		// Token: 0x06000141 RID: 321
		void GetDeviceManufacturer([In] int cchDeviceManufacturer, [In] [Out] ref ushort wzDeviceManufacturer, out int pcchActual);

		// Token: 0x06000142 RID: 322
		void GetDeviceModels([In] int cchDeviceModels, [In] [Out] ref ushort wzDeviceModels, out int pcchActual);

		// Token: 0x06000143 RID: 323
		void GetMimeTypes([In] int cchMimeTypes, [In] [Out] ref ushort wzMimeTypes, out int pcchActual);

		// Token: 0x06000144 RID: 324
		void GetFileExtensions([In] int cchFileExtensions, [In] [Out] ref ushort wzFileExtensions, out int pcchActual);

		// Token: 0x06000145 RID: 325
		void DoesSupportAnimation(out int pfSupportAnimation);

		// Token: 0x06000146 RID: 326
		void DoesSupportChromakey(out int pfSupportChromakey);

		// Token: 0x06000147 RID: 327
		void DoesSupportLossless(out int pfSupportLossless);

		// Token: 0x06000148 RID: 328
		void DoesSupportMultiframe(out int pfSupportMultiframe);

		// Token: 0x06000149 RID: 329
		void MatchesMimeType([MarshalAs(UnmanagedType.LPWStr)] [In] string wzMimeType, out int pfMatches);

		// Token: 0x0600014A RID: 330
		void Remote_GetPatterns([Out] IntPtr ppPatterns, out int pcPatterns);

		// Token: 0x0600014B RID: 331
		void MatchesPattern([MarshalAs(UnmanagedType.Interface)] [In] IStream pIStream, out int pfMatches);

		// Token: 0x0600014C RID: 332
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] out IWICBitmapDecoder ppIBitmapDecoder);
	}
}
