using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C15 RID: 3093
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncAutoCompleteRecipientsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F6C RID: 12140
		[DataMember(IsRequired = true, Order = 0)]
		public SyncAutoCompleteRecipientsRequest Body;
	}
}
