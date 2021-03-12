using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D7F RID: 3455
	[MessageContract(IsWrapped = false)]
	public class CreateUMCallDataRecordSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003111 RID: 12561
		[MessageBodyMember(Name = "CreateUMCallDataRecordResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public CreateUMCallDataRecordResponseMessage Body;
	}
}
