using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000057 RID: 87
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "WorkItemStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.WcfService.Contract.WorkloadService")]
	public enum WorkItemStatus
	{
		// Token: 0x040000F1 RID: 241
		[EnumMember]
		NotStarted,
		// Token: 0x040000F2 RID: 242
		[EnumMember]
		InProgress,
		// Token: 0x040000F3 RID: 243
		[EnumMember]
		Warning,
		// Token: 0x040000F4 RID: 244
		[EnumMember]
		Error,
		// Token: 0x040000F5 RID: 245
		[EnumMember]
		Cancelled,
		// Token: 0x040000F6 RID: 246
		[EnumMember]
		Complete,
		// Token: 0x040000F7 RID: 247
		[EnumMember]
		ForceComplete
	}
}
