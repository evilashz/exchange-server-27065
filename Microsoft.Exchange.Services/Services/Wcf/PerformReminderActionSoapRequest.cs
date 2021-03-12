using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1A RID: 3354
	[MessageContract(IsWrapped = false)]
	public class PerformReminderActionSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030AC RID: 12460
		[MessageBodyMember(Name = "PerformReminderAction", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PerformReminderActionRequest Body;
	}
}
