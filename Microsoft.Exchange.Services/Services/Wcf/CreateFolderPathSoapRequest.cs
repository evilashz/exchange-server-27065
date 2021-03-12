using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA4 RID: 3236
	[MessageContract(IsWrapped = false)]
	public class CreateFolderPathSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003034 RID: 12340
		[MessageBodyMember(Name = "CreateFolderPath", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateFolderPathRequest Body;
	}
}
