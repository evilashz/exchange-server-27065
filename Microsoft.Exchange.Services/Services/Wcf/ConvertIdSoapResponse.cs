using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9B RID: 3227
	[MessageContract(IsWrapped = false)]
	public class ConvertIdSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400302B RID: 12331
		[MessageBodyMember(Name = "ConvertIdResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ConvertIdResponse Body;
	}
}
