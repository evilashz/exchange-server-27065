using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9F RID: 3487
	[MessageContract(IsWrapped = false)]
	public class SetClutterStateSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003131 RID: 12593
		[MessageBodyMember(Name = "SetClutterStateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetClutterStateResponse Body;
	}
}
