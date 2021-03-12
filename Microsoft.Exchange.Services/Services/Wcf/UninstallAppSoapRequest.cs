using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D54 RID: 3412
	[MessageContract(IsWrapped = false)]
	public class UninstallAppSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030E6 RID: 12518
		[MessageBodyMember(Name = "UninstallApp", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UninstallAppRequest Body;
	}
}
