using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement
{
	// Token: 0x020000AC RID: 172
	[DataContract(Name = "PopulationStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.SyntheticSvc.Contracts")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum PopulationStatus
	{
		// Token: 0x04000263 RID: 611
		[EnumMember]
		Invalid,
		// Token: 0x04000264 RID: 612
		[EnumMember]
		NotStarted,
		// Token: 0x04000265 RID: 613
		[EnumMember]
		InProgress,
		// Token: 0x04000266 RID: 614
		[EnumMember]
		Error,
		// Token: 0x04000267 RID: 615
		[EnumMember]
		Complete
	}
}
