using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA5 RID: 2981
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EFC RID: 12028
		[DataMember(IsRequired = true, Order = 0)]
		public CreateItemRequest Body;
	}
}
