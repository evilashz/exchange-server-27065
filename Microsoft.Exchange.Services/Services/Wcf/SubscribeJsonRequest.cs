using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC1 RID: 3009
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F18 RID: 12056
		[DataMember(IsRequired = true, Order = 0)]
		public SubscribeRequest Body;
	}
}
