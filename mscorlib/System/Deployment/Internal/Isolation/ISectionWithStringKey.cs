using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000642 RID: 1602
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8871-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionWithStringKey
	{
		// Token: 0x06004E01 RID: 19969
		void Lookup([MarshalAs(UnmanagedType.LPWStr)] string wzStringKey, [MarshalAs(UnmanagedType.Interface)] out object ppUnknown);

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004E02 RID: 19970
		bool IsCaseInsensitive { get; }
	}
}
