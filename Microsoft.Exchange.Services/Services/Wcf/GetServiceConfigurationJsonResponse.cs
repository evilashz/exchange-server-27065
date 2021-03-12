using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BDC RID: 3036
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetServiceConfigurationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F33 RID: 12083
		[DataMember(IsRequired = true, Order = 0)]
		public GetServiceConfigurationResponseMessage Body;
	}
}
