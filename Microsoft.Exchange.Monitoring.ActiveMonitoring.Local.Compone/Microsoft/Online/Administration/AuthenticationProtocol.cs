using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D9 RID: 985
	[DataContract(Name = "AuthenticationProtocol", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum AuthenticationProtocol
	{
		// Token: 0x040010F7 RID: 4343
		[EnumMember]
		WsFed,
		// Token: 0x040010F8 RID: 4344
		[EnumMember]
		Samlp
	}
}
