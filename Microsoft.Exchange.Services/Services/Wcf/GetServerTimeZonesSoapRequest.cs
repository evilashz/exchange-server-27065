using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD4 RID: 3284
	[MessageContract(IsWrapped = false)]
	public class GetServerTimeZonesSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003064 RID: 12388
		[MessageBodyMember(Name = "GetServerTimeZones", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetServerTimeZonesRequest Body;
	}
}
