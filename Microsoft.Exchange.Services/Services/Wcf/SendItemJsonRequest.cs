using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAB RID: 2987
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F02 RID: 12034
		[DataMember(IsRequired = true, Order = 0)]
		public SendItemRequest Body;
	}
}
