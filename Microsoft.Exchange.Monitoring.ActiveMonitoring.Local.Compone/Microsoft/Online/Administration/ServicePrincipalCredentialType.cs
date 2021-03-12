using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003B6 RID: 950
	[DataContract(Name = "ServicePrincipalCredentialType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum ServicePrincipalCredentialType
	{
		// Token: 0x04001036 RID: 4150
		[EnumMember]
		Other,
		// Token: 0x04001037 RID: 4151
		[EnumMember]
		Asymmetric,
		// Token: 0x04001038 RID: 4152
		[EnumMember]
		Symmetric,
		// Token: 0x04001039 RID: 4153
		[EnumMember]
		Password
	}
}
