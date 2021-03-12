using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x0200087A RID: 2170
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	[DataContract(Name = "ErrorCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	internal enum ErrorCode
	{
		// Token: 0x04002847 RID: 10311
		[EnumMember]
		InvalidFormat,
		// Token: 0x04002848 RID: 10312
		[EnumMember]
		InternalServerError
	}
}
