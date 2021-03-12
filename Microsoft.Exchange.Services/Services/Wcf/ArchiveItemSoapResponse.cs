using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CC7 RID: 3271
	[MessageContract(IsWrapped = false)]
	public class ArchiveItemSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003057 RID: 12375
		[MessageBodyMember(Name = "ArchiveItemResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ArchiveItemResponse Body;
	}
}
