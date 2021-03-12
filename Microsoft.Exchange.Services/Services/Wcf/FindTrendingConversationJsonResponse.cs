using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C02 RID: 3074
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindTrendingConversationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F59 RID: 12121
		[DataMember(IsRequired = true, Order = 0)]
		public FindConversationResponseMessage Body;
	}
}
