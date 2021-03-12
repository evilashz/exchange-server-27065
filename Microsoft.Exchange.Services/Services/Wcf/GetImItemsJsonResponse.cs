using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C36 RID: 3126
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetImItemsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F8D RID: 12173
		[DataMember(IsRequired = true, Order = 0)]
		public GetImItemsResponseMessage Body;
	}
}
