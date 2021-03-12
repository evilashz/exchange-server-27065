using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CB5 RID: 3253
	[MessageContract(IsWrapped = false)]
	public class FindFolderSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003045 RID: 12357
		[MessageBodyMember(Name = "FindFolderResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public FindFolderResponse Body;
	}
}
