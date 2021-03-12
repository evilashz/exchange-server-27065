using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000B RID: 11
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Status", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.Database.ObjectModel")]
	public enum Status
	{
		// Token: 0x04000010 RID: 16
		[EnumMember]
		Cancelled = 1,
		// Token: 0x04000011 RID: 17
		[EnumMember]
		Complete,
		// Token: 0x04000012 RID: 18
		[EnumMember]
		ForceComplete,
		// Token: 0x04000013 RID: 19
		[EnumMember]
		NotReady,
		// Token: 0x04000014 RID: 20
		[EnumMember]
		NotStarted,
		// Token: 0x04000015 RID: 21
		[EnumMember]
		InProgress,
		// Token: 0x04000016 RID: 22
		[EnumMember]
		Warning,
		// Token: 0x04000017 RID: 23
		[EnumMember]
		Error
	}
}
