using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x0200007C RID: 124
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DataCenterRegion", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.OrchestrationEngine.Common.DataContract")]
	public enum DataCenterRegion
	{
		// Token: 0x0400015D RID: 349
		[EnumMember]
		NONE,
		// Token: 0x0400015E RID: 350
		[EnumMember]
		EU,
		// Token: 0x0400015F RID: 351
		[EnumMember]
		LATAM,
		// Token: 0x04000160 RID: 352
		[EnumMember]
		NA,
		// Token: 0x04000161 RID: 353
		[EnumMember]
		SEA
	}
}
