using System;
using System.ServiceModel;
using Microsoft.Exchange.InfoWorker.Availability;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D06 RID: 3334
	[MessageContract(IsWrapped = false)]
	public class GetUserOofSettingsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003098 RID: 12440
		[MessageBodyMember(Name = "GetUserOofSettingsRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserOofSettingsRequest Body;
	}
}
