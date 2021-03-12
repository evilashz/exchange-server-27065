using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x0200002A RID: 42
	[Guid("23BC3F0A-698B-4357-886B-F24D50671334")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICComponentInfo
	{
		// Token: 0x0600010F RID: 271
		void GetComponentType(out WICComponentType pType);

		// Token: 0x06000110 RID: 272
		void GetCLSID(out Guid pclsid);

		// Token: 0x06000111 RID: 273
		void GetSigningStatus(out int pStatus);

		// Token: 0x06000112 RID: 274
		void GetAuthor([In] int cchAuthor, [In] [Out] ref ushort wzAuthor, out int pcchActual);

		// Token: 0x06000113 RID: 275
		void GetVendorGUID(out Guid pguidVendor);

		// Token: 0x06000114 RID: 276
		void GetVersion([In] int cchVersion, [In] [Out] ref ushort wzVersion, out int pcchActual);

		// Token: 0x06000115 RID: 277
		void GetSpecVersion([In] int cchSpecVersion, [In] [Out] ref ushort wzSpecVersion, out int pcchActual);

		// Token: 0x06000116 RID: 278
		void GetFriendlyName([In] int cchFriendlyName, [In] [Out] ref ushort wzFriendlyName, out int pcchActual);
	}
}
