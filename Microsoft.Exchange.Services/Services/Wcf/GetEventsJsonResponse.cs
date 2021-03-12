using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC6 RID: 3014
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetEventsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F1D RID: 12061
		[DataMember(IsRequired = true, Order = 0)]
		public GetEventsResponse Body;
	}
}
