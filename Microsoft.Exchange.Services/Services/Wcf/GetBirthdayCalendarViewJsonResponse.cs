using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA0 RID: 2976
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetBirthdayCalendarViewJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EF7 RID: 12023
		[DataMember(IsRequired = true, Order = 0)]
		public GetBirthdayCalendarViewResponseMessage Body;
	}
}
