using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD0 RID: 3280
	[MessageContract(IsWrapped = false)]
	public class ResolveNamesSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003060 RID: 12384
		[MessageBodyMember(Name = "ResolveNames", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public ResolveNamesRequest Body;
	}
}
