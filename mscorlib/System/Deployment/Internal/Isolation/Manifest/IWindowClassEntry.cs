using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CA RID: 1738
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("8AD3FC86-AFD3-477a-8FD5-146C291195BA")]
	[ComImport]
	internal interface IWindowClassEntry
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004FA8 RID: 20392
		WindowClassEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004FA9 RID: 20393
		string ClassName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004FAA RID: 20394
		string HostDll { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004FAB RID: 20395
		bool fVersioned { [SecurityCritical] get; }
	}
}
