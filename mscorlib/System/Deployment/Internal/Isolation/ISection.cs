using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000641 RID: 1601
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8862-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISection
	{
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004DFD RID: 19965
		object _NewEnum { [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004DFE RID: 19966
		uint Count { get; }

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004DFF RID: 19967
		uint SectionID { get; }

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004E00 RID: 19968
		string SectionName { [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}
