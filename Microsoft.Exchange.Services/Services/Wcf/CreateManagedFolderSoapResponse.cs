using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD7 RID: 3287
	[MessageContract(IsWrapped = false)]
	public class CreateManagedFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003067 RID: 12391
		[MessageBodyMember(Name = "CreateManagedFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateManagedFolderResponse Body;
	}
}
