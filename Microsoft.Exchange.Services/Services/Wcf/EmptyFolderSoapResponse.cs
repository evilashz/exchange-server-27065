using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA9 RID: 3241
	[MessageContract(IsWrapped = false)]
	public class EmptyFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003039 RID: 12345
		[MessageBodyMember(Name = "EmptyFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public EmptyFolderResponse Body;
	}
}
