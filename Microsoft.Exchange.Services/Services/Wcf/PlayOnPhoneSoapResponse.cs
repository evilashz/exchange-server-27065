using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF9 RID: 3321
	[MessageContract(IsWrapped = false)]
	public class PlayOnPhoneSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400308B RID: 12427
		[MessageBodyMember(Name = "PlayOnPhoneResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PlayOnPhoneResponseMessage Body;
	}
}
