using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CAF RID: 3247
	[MessageContract(IsWrapped = false)]
	public class MoveFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400303F RID: 12351
		[MessageBodyMember(Name = "MoveFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MoveFolderResponse Body;
	}
}
