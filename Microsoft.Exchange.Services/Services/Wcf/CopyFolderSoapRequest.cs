using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB0 RID: 3248
	[MessageContract(IsWrapped = false)]
	public class CopyFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003040 RID: 12352
		[MessageBodyMember(Name = "CopyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CopyFolderRequest Body;
	}
}
