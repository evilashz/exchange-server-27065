using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200087B RID: 2171
	[DataContract(Name = "InvalidContractCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum InvalidContractCode
	{
		// Token: 0x0400284A RID: 10314
		[EnumMember]
		BatchSizeExceededLimit,
		// Token: 0x0400284B RID: 10315
		[EnumMember]
		NullOrEmptyParameterSpecified,
		// Token: 0x0400284C RID: 10316
		[EnumMember]
		InvalidParameterSpecified
	}
}
