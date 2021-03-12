using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000D RID: 13
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DataSource", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.Database.Scheduler")]
	public enum DataSource
	{
		// Token: 0x0400001C RID: 28
		[EnumMember]
		BOSBIFeed,
		// Token: 0x0400001D RID: 29
		[EnumMember]
		Synthetic
	}
}
