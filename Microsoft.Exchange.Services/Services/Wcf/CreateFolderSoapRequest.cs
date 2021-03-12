using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA2 RID: 3234
	[MessageContract(IsWrapped = false)]
	public class CreateFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003032 RID: 12338
		[MessageBodyMember(Name = "CreateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateFolderRequest Body;
	}
}
