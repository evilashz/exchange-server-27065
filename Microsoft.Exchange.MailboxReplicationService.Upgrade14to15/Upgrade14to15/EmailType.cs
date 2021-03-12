using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200000C RID: 12
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "EmailType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.Database.ObjectModel")]
	public enum EmailType
	{
		// Token: 0x04000019 RID: 25
		[EnumMember]
		Admin = 1,
		// Token: 0x0400001A RID: 26
		[EnumMember]
		PartnerOfRecord
	}
}
