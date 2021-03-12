using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA8 RID: 3240
	[MessageContract(IsWrapped = false)]
	public class EmptyFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003038 RID: 12344
		[MessageBodyMember(Name = "EmptyFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public EmptyFolderRequest Body;
	}
}
