using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CAB RID: 3243
	[MessageContract(IsWrapped = false)]
	public class DeleteFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400303B RID: 12347
		[MessageBodyMember(Name = "DeleteFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DeleteFolderResponse Body;
	}
}
