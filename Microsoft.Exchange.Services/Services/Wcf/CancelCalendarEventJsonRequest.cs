using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7C RID: 3196
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CancelCalendarEventJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FCF RID: 12239
		[DataMember(IsRequired = true, Order = 0)]
		public CancelCalendarEventRequest Body;
	}
}
