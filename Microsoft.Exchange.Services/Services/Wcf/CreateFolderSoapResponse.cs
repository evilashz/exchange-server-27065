using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA3 RID: 3235
	[MessageContract(IsWrapped = false)]
	public class CreateFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003033 RID: 12339
		[MessageBodyMember(Name = "CreateFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateFolderResponse Body;
	}
}
