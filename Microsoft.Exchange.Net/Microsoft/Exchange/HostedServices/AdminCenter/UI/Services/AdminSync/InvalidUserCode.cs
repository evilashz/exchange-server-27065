using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000881 RID: 2177
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "InvalidUserCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	internal enum InvalidUserCode
	{
		// Token: 0x0400285D RID: 10333
		[EnumMember]
		UserDoesNotExist,
		// Token: 0x0400285E RID: 10334
		[EnumMember]
		InvalidFormat
	}
}
