using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C01 RID: 3073
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindTrendingConversationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F58 RID: 12120
		[DataMember(IsRequired = true, Order = 0)]
		public FindTrendingConversationRequest Body;
	}
}
