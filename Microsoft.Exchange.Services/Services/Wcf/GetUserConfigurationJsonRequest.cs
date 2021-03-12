using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BD7 RID: 3031
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserConfigurationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F2E RID: 12078
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserConfigurationRequest Body;
	}
}
