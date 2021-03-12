using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000B0 RID: 176
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ValidationStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts")]
	public enum ValidationStatus
	{
		// Token: 0x04000282 RID: 642
		[EnumMember]
		Invalid,
		// Token: 0x04000283 RID: 643
		[EnumMember]
		NotStarted,
		// Token: 0x04000284 RID: 644
		[EnumMember]
		InProgress,
		// Token: 0x04000285 RID: 645
		[EnumMember]
		Error,
		// Token: 0x04000286 RID: 646
		[EnumMember]
		Success,
		// Token: 0x04000287 RID: 647
		[EnumMember]
		Failure
	}
}
