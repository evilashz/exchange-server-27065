using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D2D RID: 3373
	[MessageContract(IsWrapped = false)]
	public class ExecuteDiagnosticMethodSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030BF RID: 12479
		[MessageBodyMember(Name = "ExecuteDiagnosticMethodResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExecuteDiagnosticMethodResponse Body;
	}
}
