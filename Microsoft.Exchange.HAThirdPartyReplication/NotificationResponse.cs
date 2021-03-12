using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000006 RID: 6
	[DataContract(Name = "NotificationResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.ThirdPartyReplication")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	public enum NotificationResponse
	{
		// Token: 0x04000004 RID: 4
		[EnumMember]
		Complete,
		// Token: 0x04000005 RID: 5
		[EnumMember]
		Incomplete
	}
}
