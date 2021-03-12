using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085D RID: 2141
	[DataContract(Name = "ResponseStatus", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum ResponseStatus
	{
		// Token: 0x040027C3 RID: 10179
		[EnumMember]
		Success = 1,
		// Token: 0x040027C4 RID: 10180
		[EnumMember]
		TransientFailure,
		// Token: 0x040027C5 RID: 10181
		[EnumMember]
		PermanentFailure,
		// Token: 0x040027C6 RID: 10182
		[EnumMember]
		ContractError
	}
}
