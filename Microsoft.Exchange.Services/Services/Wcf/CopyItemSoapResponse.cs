using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC5 RID: 3269
	[MessageContract(IsWrapped = false)]
	public class CopyItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003055 RID: 12373
		[MessageBodyMember(Name = "CopyItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CopyItemResponse Body;
	}
}
