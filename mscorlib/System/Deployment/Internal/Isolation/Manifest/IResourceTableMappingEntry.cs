using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CD RID: 1741
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("70A4ECEE-B195-4c59-85BF-44B6ACA83F07")]
	[ComImport]
	internal interface IResourceTableMappingEntry
	{
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004FAD RID: 20397
		ResourceTableMappingEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06004FAE RID: 20398
		string id { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06004FAF RID: 20399
		string FinalStringMapped { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
