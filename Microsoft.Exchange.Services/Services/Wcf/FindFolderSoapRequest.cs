using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB4 RID: 3252
	[MessageContract(IsWrapped = false)]
	public class FindFolderSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003044 RID: 12356
		[MessageBodyMember(Name = "FindFolder", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindFolderRequest Body;
	}
}
