using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D08 RID: 3336
	[MessageContract(IsWrapped = false)]
	public class SetUserOofSettingsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400309A RID: 12442
		[MessageBodyMember(Name = "SetUserOofSettingsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetUserOofSettingsRequest Body;
	}
}
