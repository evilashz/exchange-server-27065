using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CAA RID: 3242
	[MessageContract(IsWrapped = false)]
	public class DeleteFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400303A RID: 12346
		[MessageBodyMember(Name = "DeleteFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteFolderRequest Body;
	}
}
