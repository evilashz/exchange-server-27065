using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CF8 RID: 3320
	[MessageContract(IsWrapped = false)]
	public class PlayOnPhoneSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400308A RID: 12426
		[MessageBodyMember(Name = "PlayOnPhone", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public PlayOnPhoneRequest Body;
	}
}
