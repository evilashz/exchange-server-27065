using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C23 RID: 3107
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAllItemsAsReadJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F7A RID: 12154
		[DataMember(IsRequired = true, Order = 0)]
		public MarkAllItemsAsReadRequest Body;
	}
}
