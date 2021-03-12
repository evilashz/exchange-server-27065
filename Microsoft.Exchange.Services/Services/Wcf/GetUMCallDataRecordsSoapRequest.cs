using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D80 RID: 3456
	[MessageContract(IsWrapped = false)]
	public class GetUMCallDataRecordsSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003112 RID: 12562
		[MessageBodyMember(Name = "GetUMCallDataRecords", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUMCallDataRecordsRequest Body;
	}
}
