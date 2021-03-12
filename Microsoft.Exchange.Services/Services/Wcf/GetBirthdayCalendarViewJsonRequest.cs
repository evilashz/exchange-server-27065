using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9F RID: 2975
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetBirthdayCalendarViewJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EF6 RID: 12022
		[DataMember(IsRequired = true, Order = 0)]
		public GetBirthdayCalendarViewRequest Body;
	}
}
