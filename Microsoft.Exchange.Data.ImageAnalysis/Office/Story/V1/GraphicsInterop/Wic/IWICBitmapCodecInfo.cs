using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002B RID: 43
	[Guid("E87A44C4-B76E-4C47-8B09-298EB12A2714")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapCodecInfo : IWICComponentInfo
	{
		// Token: 0x06000117 RID: 279
		void GetComponentType(out WICComponentType pType);

		// Token: 0x06000118 RID: 280
		void GetCLSID(out Guid pclsid);

		// Token: 0x06000119 RID: 281
		void GetSigningStatus(out int pStatus);

		// Token: 0x0600011A RID: 282
		void GetAuthor([In] int cchAuthor, [In] [Out] ref ushort wzAuthor, out int pcchActual);

		// Token: 0x0600011B RID: 283
		void GetVendorGUID(out Guid pguidVendor);

		// Token: 0x0600011C RID: 284
		void GetVersion([In] int cchVersion, [In] [Out] ref ushort wzVersion, out int pcchActual);

		// Token: 0x0600011D RID: 285
		void GetSpecVersion([In] int cchSpecVersion, [In] [Out] ref ushort wzSpecVersion, out int pcchActual);

		// Token: 0x0600011E RID: 286
		void GetFriendlyName([In] int cchFriendlyName, [In] [Out] ref ushort wzFriendlyName, out int pcchActual);

		// Token: 0x0600011F RID: 287
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x06000120 RID: 288
		void GetPixelFormats([In] int cFormats, [In] [Out] ref Guid pguidPixelFormats, out int pcActual);

		// Token: 0x06000121 RID: 289
		void GetColorManagementVersion([In] int cchColorManagementVersion, [In] [Out] ref ushort wzColorManagementVersion, out int pcchActual);

		// Token: 0x06000122 RID: 290
		void GetDeviceManufacturer([In] int cchDeviceManufacturer, [In] [Out] ref ushort wzDeviceManufacturer, out int pcchActual);

		// Token: 0x06000123 RID: 291
		void GetDeviceModels([In] int cchDeviceModels, [In] [Out] ref ushort wzDeviceModels, out int pcchActual);

		// Token: 0x06000124 RID: 292
		void GetMimeTypes([In] int cchMimeTypes, [In] [Out] ref ushort wzMimeTypes, out int pcchActual);

		// Token: 0x06000125 RID: 293
		void GetFileExtensions([In] int cchFileExtensions, [In] [Out] ref ushort wzFileExtensions, out int pcchActual);

		// Token: 0x06000126 RID: 294
		void DoesSupportAnimation(out int pfSupportAnimation);

		// Token: 0x06000127 RID: 295
		void DoesSupportChromakey(out int pfSupportChromakey);

		// Token: 0x06000128 RID: 296
		void DoesSupportLossless(out int pfSupportLossless);

		// Token: 0x06000129 RID: 297
		void DoesSupportMultiframe(out int pfSupportMultiframe);

		// Token: 0x0600012A RID: 298
		void MatchesMimeType([MarshalAs(UnmanagedType.LPWStr)] [In] string wzMimeType, out int pfMatches);
	}
}
