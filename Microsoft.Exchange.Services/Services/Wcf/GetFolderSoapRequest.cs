using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CA0 RID: 3232
	[MessageContract(IsWrapped = false)]
	public class GetFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003030 RID: 12336
		[MessageBodyMember(Name = "GetFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetFolderRequest Body;
	}
}
