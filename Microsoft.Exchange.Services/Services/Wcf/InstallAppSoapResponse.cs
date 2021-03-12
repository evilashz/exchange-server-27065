using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D53 RID: 3411
	[MessageContract(IsWrapped = false)]
	public class InstallAppSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030E5 RID: 12517
		[MessageBodyMember(Name = "InstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public InstallAppResponse Body;
	}
}
