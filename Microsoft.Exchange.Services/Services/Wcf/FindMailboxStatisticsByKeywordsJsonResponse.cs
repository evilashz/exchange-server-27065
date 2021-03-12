using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C10 RID: 3088
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMailboxStatisticsByKeywordsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F67 RID: 12135
		[DataMember(IsRequired = true, Order = 0)]
		public FindMailboxStatisticsByKeywordsResponse Body;
	}
}
