using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC5 RID: 3013
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetEventsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F1C RID: 12060
		[DataMember(IsRequired = true, Order = 0)]
		public GetEventsRequest Body;
	}
}
