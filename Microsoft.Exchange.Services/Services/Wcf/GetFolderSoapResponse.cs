using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA1 RID: 3233
	[MessageContract(IsWrapped = false)]
	public class GetFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003031 RID: 12337
		[MessageBodyMember(Name = "GetFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetFolderResponse Body;
	}
}
