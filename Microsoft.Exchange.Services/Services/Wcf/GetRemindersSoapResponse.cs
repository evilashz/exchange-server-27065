using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D19 RID: 3353
	[MessageContract(IsWrapped = false)]
	public class GetRemindersSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030AB RID: 12459
		[MessageBodyMember(Name = "GetRemindersResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRemindersResponse Body;
	}
}
