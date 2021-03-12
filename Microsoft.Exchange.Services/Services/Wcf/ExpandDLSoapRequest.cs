using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD2 RID: 3282
	[MessageContract(IsWrapped = false)]
	public class ExpandDLSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003062 RID: 12386
		[MessageBodyMember(Name = "ExpandDL", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExpandDLRequest Body;
	}
}
