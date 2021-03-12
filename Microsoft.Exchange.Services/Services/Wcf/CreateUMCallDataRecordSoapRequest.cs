using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7E RID: 3454
	[MessageContract(IsWrapped = false)]
	public class CreateUMCallDataRecordSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003110 RID: 12560
		[MessageBodyMember(Name = "CreateUMCallDataRecord", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUMCallDataRecordRequest Body;
	}
}
