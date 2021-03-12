using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0F RID: 3343
	[MessageContract(IsWrapped = false)]
	public class GetSharingFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030A1 RID: 12449
		[MessageBodyMember(Name = "GetSharingFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSharingFolderResponseMessage Body;
	}
}
