using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D52 RID: 3410
	[MessageContract(IsWrapped = false)]
	public class InstallAppSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030E4 RID: 12516
		[MessageBodyMember(Name = "InstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public InstallAppRequest Body;
	}
}
