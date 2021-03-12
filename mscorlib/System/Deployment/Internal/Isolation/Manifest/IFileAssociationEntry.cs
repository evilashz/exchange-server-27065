using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006AF RID: 1711
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0C66F299-E08E-48c5-9264-7CCBEB4D5CBB")]
	[ComImport]
	internal interface IFileAssociationEntry
	{
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004F6D RID: 20333
		FileAssociationEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004F6E RID: 20334
		string Extension { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004F6F RID: 20335
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004F70 RID: 20336
		string ProgID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004F71 RID: 20337
		string DefaultIcon { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004F72 RID: 20338
		string Parameter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
