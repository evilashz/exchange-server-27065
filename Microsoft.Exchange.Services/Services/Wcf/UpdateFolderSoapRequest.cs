using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CAC RID: 3244
	[MessageContract(IsWrapped = false)]
	public class UpdateFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400303C RID: 12348
		[MessageBodyMember(Name = "UpdateFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UpdateFolderRequest Body;
	}
}
