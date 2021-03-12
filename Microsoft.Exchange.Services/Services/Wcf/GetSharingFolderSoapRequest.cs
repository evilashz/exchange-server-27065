using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0E RID: 3342
	[MessageContract(IsWrapped = false)]
	public class GetSharingFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030A0 RID: 12448
		[MessageBodyMember(Name = "GetSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetSharingFolderRequest Body;
	}
}
