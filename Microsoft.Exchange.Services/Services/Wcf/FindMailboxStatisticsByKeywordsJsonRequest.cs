using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0F RID: 3087
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMailboxStatisticsByKeywordsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F66 RID: 12134
		[DataMember(IsRequired = true, Order = 0)]
		public FindMailboxStatisticsByKeywordsRequest Body;
	}
}
