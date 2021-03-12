using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D1B RID: 3355
	[MessageContract(IsWrapped = false)]
	public class PerformReminderActionSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030AD RID: 12461
		[MessageBodyMember(Name = "PerformReminderActionResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PerformReminderActionResponse Body;
	}
}
