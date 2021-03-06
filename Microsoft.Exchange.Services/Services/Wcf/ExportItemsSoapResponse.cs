using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9F RID: 3231
	[MessageContract(IsWrapped = false)]
	public class ExportItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400302F RID: 12335
		[MessageBodyMember(Name = "ExportItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ExportItemsResponse Body;
	}
}
