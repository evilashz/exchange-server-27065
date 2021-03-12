using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A02 RID: 2562
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum ModernGroupMembershipOperationType
	{
		// Token: 0x0400295F RID: 10591
		Join = 1,
		// Token: 0x04002960 RID: 10592
		Leave,
		// Token: 0x04002961 RID: 10593
		Escalate,
		// Token: 0x04002962 RID: 10594
		DeEscalate,
		// Token: 0x04002963 RID: 10595
		RequestJoin
	}
}
