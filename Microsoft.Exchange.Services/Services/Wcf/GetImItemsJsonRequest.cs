using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C35 RID: 3125
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetImItemsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F8C RID: 12172
		[DataMember(IsRequired = true, Order = 0)]
		public GetImItemsRequest Body;
	}
}
