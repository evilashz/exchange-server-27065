using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D18 RID: 3352
	[MessageContract(IsWrapped = false)]
	public class GetRemindersSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030AA RID: 12458
		[MessageBodyMember(Name = "GetReminders", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetRemindersRequest Body;
	}
}
