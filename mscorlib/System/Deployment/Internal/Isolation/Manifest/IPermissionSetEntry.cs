using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D3 RID: 1747
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("EBE5A1ED-FEBC-42c4-A9E1-E087C6E36635")]
	[ComImport]
	internal interface IPermissionSetEntry
	{
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06004FB8 RID: 20408
		PermissionSetEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06004FB9 RID: 20409
		string Id { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004FBA RID: 20410
		string XmlSegment { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
