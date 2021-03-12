using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200086E RID: 2158
	[DataContract(Name = "Role", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum Role
	{
		// Token: 0x04002833 RID: 10291
		[EnumMember]
		Administrator,
		// Token: 0x04002834 RID: 10292
		[EnumMember]
		ReadOnlyAdministrator
	}
}
