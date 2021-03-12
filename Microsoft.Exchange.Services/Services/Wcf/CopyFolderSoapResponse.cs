using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB1 RID: 3249
	[MessageContract(IsWrapped = false)]
	public class CopyFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003041 RID: 12353
		[MessageBodyMember(Name = "CopyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CopyFolderResponse Body;
	}
}
