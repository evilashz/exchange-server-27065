using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD6 RID: 3286
	[MessageContract(IsWrapped = false)]
	public class CreateManagedFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003066 RID: 12390
		[MessageBodyMember(Name = "CreateManagedFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateManagedFolderRequest Body;
	}
}
