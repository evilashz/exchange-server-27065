using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D07 RID: 3335
	[MessageContract(IsWrapped = false)]
	public class GetUserOofSettingsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003099 RID: 12441
		[MessageBodyMember(Name = "GetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserOofSettingsResponse Body;
	}
}
