using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA7 RID: 2983
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EFE RID: 12030
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteItemRequest Body;
	}
}
