using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD5 RID: 3285
	[MessageContract(IsWrapped = false)]
	public class GetServerTimeZonesSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003065 RID: 12389
		[MessageBodyMember(Name = "GetServerTimeZonesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetServerTimeZonesResponse Body;
	}
}
