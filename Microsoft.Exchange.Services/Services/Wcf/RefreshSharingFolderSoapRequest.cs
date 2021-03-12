using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D0C RID: 3340
	[MessageContract(IsWrapped = false)]
	public class RefreshSharingFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400309E RID: 12446
		[MessageBodyMember(Name = "RefreshSharingFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RefreshSharingFolderRequest Body;
	}
}
