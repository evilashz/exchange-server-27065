using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync
{
	// Token: 0x02000880 RID: 2176
	[DataContract(Name = "RemoveGroupErrorCode", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services.AdminSync")]
	[GeneratedCode("System.Runtime.Serialization", "3.0.0.0")]
	internal enum RemoveGroupErrorCode
	{
		// Token: 0x04002858 RID: 10328
		[EnumMember]
		InvalidFormat,
		// Token: 0x04002859 RID: 10329
		[EnumMember]
		InternalServerError,
		// Token: 0x0400285A RID: 10330
		[EnumMember]
		GroupDoesNotExist,
		// Token: 0x0400285B RID: 10331
		[EnumMember]
		AccessDenied
	}
}
