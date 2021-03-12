using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D09 RID: 3337
	[MessageContract(IsWrapped = false)]
	public class SetUserOofSettingsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400309B RID: 12443
		[MessageBodyMember(Name = "SetUserOofSettingsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetUserOofSettingsResponse Body;
	}
}
