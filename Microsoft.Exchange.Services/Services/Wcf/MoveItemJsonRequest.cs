using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAD RID: 2989
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F04 RID: 12036
		[DataMember(IsRequired = true, Order = 0)]
		public MoveItemRequest Body;
	}
}
