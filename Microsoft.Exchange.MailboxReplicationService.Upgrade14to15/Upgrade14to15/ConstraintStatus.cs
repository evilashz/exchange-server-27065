using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007E RID: 126
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "ConstraintStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	public enum ConstraintStatus
	{
		// Token: 0x0400016A RID: 362
		[EnumMember]
		Red = 1,
		// Token: 0x0400016B RID: 363
		[EnumMember]
		Yellow,
		// Token: 0x0400016C RID: 364
		[EnumMember]
		Green
	}
}
