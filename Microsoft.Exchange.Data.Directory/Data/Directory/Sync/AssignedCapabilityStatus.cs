using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000940 RID: 2368
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public enum AssignedCapabilityStatus
	{
		// Token: 0x040048A7 RID: 18599
		Enabled,
		// Token: 0x040048A8 RID: 18600
		Warning,
		// Token: 0x040048A9 RID: 18601
		Suspended,
		// Token: 0x040048AA RID: 18602
		Deleted,
		// Token: 0x040048AB RID: 18603
		LockedOut
	}
}
