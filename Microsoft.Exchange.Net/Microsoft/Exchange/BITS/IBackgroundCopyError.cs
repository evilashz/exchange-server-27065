using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.BITS
{
	// Token: 0x02000658 RID: 1624
	[Guid("19C613A0-FCB8-4F28-81AE-897C3D078F81")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IBackgroundCopyError
	{
		// Token: 0x06001DBF RID: 7615
		void GetError(out BG_ERROR_CONTEXT pContext, [MarshalAs(UnmanagedType.Error)] out int pCode);

		// Token: 0x06001DC0 RID: 7616
		void GetFile([MarshalAs(UnmanagedType.Interface)] out IBackgroundCopyFile pVal);

		// Token: 0x06001DC1 RID: 7617
		void GetErrorDescription(uint LanguageId, [MarshalAs(UnmanagedType.LPWStr)] out string pErrorDescription);

		// Token: 0x06001DC2 RID: 7618
		void GetErrorContextDescription(uint LanguageId, [MarshalAs(UnmanagedType.LPWStr)] out string pContextDescription);

		// Token: 0x06001DC3 RID: 7619
		void GetProtocol([MarshalAs(UnmanagedType.LPWStr)] out string pProtocol);
	}
}
