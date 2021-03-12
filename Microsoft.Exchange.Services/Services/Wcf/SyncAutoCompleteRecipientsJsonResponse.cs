using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C16 RID: 3094
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SyncAutoCompleteRecipientsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F6D RID: 12141
		[DataMember(IsRequired = true, Order = 0)]
		public SyncAutoCompleteRecipientsResponseMessage Body;
	}
}
