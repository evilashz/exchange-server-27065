using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7D RID: 3197
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CancelCalendarEventJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FD0 RID: 12240
		[DataMember(IsRequired = true, Order = 0)]
		public CancelCalendarEventResponse Body;
	}
}
