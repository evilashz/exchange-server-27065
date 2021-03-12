using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F5 RID: 2549
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum ModernGroupMembersSortOrder
	{
		// Token: 0x04002936 RID: 10550
		None,
		// Token: 0x04002937 RID: 10551
		OwnerAndDisplayName,
		// Token: 0x04002938 RID: 10552
		PeopleIKnow,
		// Token: 0x04002939 RID: 10553
		DisplayName
	}
}
