using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BF5 RID: 3061
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetRoomsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F4C RID: 12108
		[DataMember(IsRequired = true, Order = 0)]
		public GetRoomsRequest Body;
	}
}
