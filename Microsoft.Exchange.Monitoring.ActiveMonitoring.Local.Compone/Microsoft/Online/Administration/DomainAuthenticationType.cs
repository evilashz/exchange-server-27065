using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003D5 RID: 981
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "DomainAuthenticationType", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public enum DomainAuthenticationType
	{
		// Token: 0x040010DE RID: 4318
		[EnumMember]
		Managed,
		// Token: 0x040010DF RID: 4319
		[EnumMember]
		Federated
	}
}
