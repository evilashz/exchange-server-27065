using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200069D RID: 1693
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("24abe1f7-a396-4a03-9adf-1d5b86a5569f")]
	[ComImport]
	internal interface IMuiResourceIdLookupMapEntry
	{
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06004F37 RID: 20279
		MuiResourceIdLookupMapEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06004F38 RID: 20280
		uint Count { [SecurityCritical] get; }
	}
}
