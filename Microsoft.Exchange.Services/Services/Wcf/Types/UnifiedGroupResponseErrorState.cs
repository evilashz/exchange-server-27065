using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B4A RID: 2890
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public enum UnifiedGroupResponseErrorState
	{
		// Token: 0x04002DBD RID: 11709
		NoError,
		// Token: 0x04002DBE RID: 11710
		FailedAAD,
		// Token: 0x04002DBF RID: 11711
		FailedMailbox
	}
}
