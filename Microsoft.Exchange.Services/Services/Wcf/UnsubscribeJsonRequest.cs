using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC3 RID: 3011
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F1A RID: 12058
		[DataMember(IsRequired = true, Order = 0)]
		public UnsubscribeRequest Body;
	}
}
