using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000646 RID: 1606
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8860-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ICDF
	{
		// Token: 0x06004E0A RID: 19978
		ISection GetRootSection(uint SectionId);

		// Token: 0x06004E0B RID: 19979
		ISectionEntry GetRootSectionEntry(uint SectionId);

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004E0C RID: 19980
		object _NewEnum { [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004E0D RID: 19981
		uint Count { get; }

		// Token: 0x06004E0E RID: 19982
		object GetItem(uint SectionId);
	}
}
