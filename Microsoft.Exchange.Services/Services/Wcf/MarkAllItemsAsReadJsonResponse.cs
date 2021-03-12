using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C24 RID: 3108
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MarkAllItemsAsReadJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F7B RID: 12155
		[DataMember(IsRequired = true, Order = 0)]
		public MarkAllItemsAsReadResponse Body;
	}
}
