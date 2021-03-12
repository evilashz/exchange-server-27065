using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B7 RID: 951
	[DataContract(Name = "ServicePrincipalCredentialUsage", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum ServicePrincipalCredentialUsage
	{
		// Token: 0x0400103B RID: 4155
		[EnumMember]
		Other,
		// Token: 0x0400103C RID: 4156
		[EnumMember]
		Sign,
		// Token: 0x0400103D RID: 4157
		[EnumMember]
		Verify
	}
}
