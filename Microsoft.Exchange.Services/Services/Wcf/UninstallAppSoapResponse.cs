using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D55 RID: 3413
	[MessageContract(IsWrapped = false)]
	public class UninstallAppSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030E7 RID: 12519
		[MessageBodyMember(Name = "UninstallAppResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UninstallAppResponse Body;
	}
}
