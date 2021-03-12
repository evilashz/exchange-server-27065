using System;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Story.V1.GraphicsInterop.Wic
{
	// Token: 0x02000037 RID: 55
	[Guid("B84E2C09-78C9-4AC4-8BD3-524AE1663A2F")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IWICFastMetadataEncoder
	{
		// Token: 0x0600019B RID: 411
		void Commit();

		// Token: 0x0600019C RID: 412
		void GetMetadataQueryWriter([MarshalAs(UnmanagedType.Interface)] out IWICMetadataQueryWriter ppIMetadataQueryWriter);
	}
}
