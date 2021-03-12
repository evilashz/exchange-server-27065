using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D81 RID: 3457
	[MessageContract(IsWrapped = false)]
	public class GetUMCallDataRecordsSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003113 RID: 12563
		[MessageBodyMember(Name = "GetUMCallDataRecordsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMCallDataRecordsResponseMessage Body;
	}
}
