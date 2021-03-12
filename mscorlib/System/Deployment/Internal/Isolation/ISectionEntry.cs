using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000644 RID: 1604
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("285a8861-c84a-11d7-850f-005cd062464f")]
	[ComImport]
	internal interface ISectionEntry
	{
		// Token: 0x06004E04 RID: 19972
		object GetField(uint fieldId);

		// Token: 0x06004E05 RID: 19973
		string GetFieldName(uint fieldId);
	}
}
