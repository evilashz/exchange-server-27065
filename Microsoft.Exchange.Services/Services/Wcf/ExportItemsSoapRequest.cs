using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9E RID: 3230
	[MessageContract(IsWrapped = false)]
	public class ExportItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400302E RID: 12334
		[MessageBodyMember(Name = "ExportItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExportItemsRequest Body;
	}
}
