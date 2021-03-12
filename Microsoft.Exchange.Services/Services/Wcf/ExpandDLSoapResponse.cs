using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD3 RID: 3283
	[MessageContract(IsWrapped = false)]
	public class ExpandDLSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003063 RID: 12387
		[MessageBodyMember(Name = "ExpandDLResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExpandDLResponse Body;
	}
}
