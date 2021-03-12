using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002F RID: 47
	[Guid("94C9B4EE-A09F-4F92-8A1E-4A9BCE7E76FB")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICBitmapEncoderInfo : IWICBitmapCodecInfo, IWICComponentInfo
	{
		// Token: 0x06000157 RID: 343
		void GetComponentType(out WICComponentType pType);

		// Token: 0x06000158 RID: 344
		void GetCLSID(out Guid pclsid);

		// Token: 0x06000159 RID: 345
		void GetSigningStatus(out int pStatus);

		// Token: 0x0600015A RID: 346
		void GetAuthor([In] int cchAuthor, [In] [Out] ref ushort wzAuthor, out int pcchActual);

		// Token: 0x0600015B RID: 347
		void GetVendorGUID(out Guid pguidVendor);

		// Token: 0x0600015C RID: 348
		void GetVersion([In] int cchVersion, [In] [Out] ref ushort wzVersion, out int pcchActual);

		// Token: 0x0600015D RID: 349
		void GetSpecVersion([In] int cchSpecVersion, [In] [Out] ref ushort wzSpecVersion, out int pcchActual);

		// Token: 0x0600015E RID: 350
		void GetFriendlyName([In] int cchFriendlyName, [In] [Out] ref ushort wzFriendlyName, out int pcchActual);

		// Token: 0x0600015F RID: 351
		void GetContainerFormat(out Guid pguidContainerFormat);

		// Token: 0x06000160 RID: 352
		void GetPixelFormats([In] int cFormats, [In] [Out] ref Guid pguidPixelFormats, out int pcActual);

		// Token: 0x06000161 RID: 353
		void GetColorManagementVersion([In] int cchColorManagementVersion, [In] [Out] ref ushort wzColorManagementVersion, out int pcchActual);

		// Token: 0x06000162 RID: 354
		void GetDeviceManufacturer([In] int cchDeviceManufacturer, [In] [Out] ref ushort wzDeviceManufacturer, out int pcchActual);

		// Token: 0x06000163 RID: 355
		void GetDeviceModels([In] int cchDeviceModels, [In] [Out] ref ushort wzDeviceModels, out int pcchActual);

		// Token: 0x06000164 RID: 356
		void GetMimeTypes([In] int cchMimeTypes, [In] [Out] ref ushort wzMimeTypes, out int pcchActual);

		// Token: 0x06000165 RID: 357
		void GetFileExtensions([In] int cchFileExtensions, [In] [Out] ref ushort wzFileExtensions, out int pcchActual);

		// Token: 0x06000166 RID: 358
		void DoesSupportAnimation(out int pfSupportAnimation);

		// Token: 0x06000167 RID: 359
		void DoesSupportChromakey(out int pfSupportChromakey);

		// Token: 0x06000168 RID: 360
		void DoesSupportLossless(out int pfSupportLossless);

		// Token: 0x06000169 RID: 361
		void DoesSupportMultiframe(out int pfSupportMultiframe);

		// Token: 0x0600016A RID: 362
		void MatchesMimeType([MarshalAs(UnmanagedType.LPWStr)] [In] string wzMimeType, out int pfMatches);

		// Token: 0x0600016B RID: 363
		void CreateInstance([MarshalAs(UnmanagedType.Interface)] out IWICBitmapEncoder ppIBitmapEncoder);
	}
}
