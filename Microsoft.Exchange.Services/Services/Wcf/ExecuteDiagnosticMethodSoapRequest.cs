using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2C RID: 3372
	[MessageContract(IsWrapped = false)]
	public class ExecuteDiagnosticMethodSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030BE RID: 12478
		[MessageBodyMember(Name = "ExecuteDiagnosticMethod", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExecuteDiagnosticMethodRequest Body;
	}
}
