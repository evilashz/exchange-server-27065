using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services
{
	// Token: 0x0200085F RID: 2143
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "ProvisioningSource", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	internal enum ProvisioningSource
	{
		// Token: 0x040027D6 RID: 10198
		[EnumMember]
		None,
		// Token: 0x040027D7 RID: 10199
		[EnumMember]
		Other,
		// Token: 0x040027D8 RID: 10200
		[EnumMember]
		BPOS
	}
}
