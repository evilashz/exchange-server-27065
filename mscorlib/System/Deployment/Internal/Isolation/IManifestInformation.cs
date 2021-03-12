using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000688 RID: 1672
	[Guid("81c85208-fe61-4c15-b5bb-ff5ea66baad9")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IManifestInformation
	{
		// Token: 0x06004F09 RID: 20233
		[SecurityCritical]
		void get_FullPath([MarshalAs(UnmanagedType.LPWStr)] out string FullPath);
	}
}
