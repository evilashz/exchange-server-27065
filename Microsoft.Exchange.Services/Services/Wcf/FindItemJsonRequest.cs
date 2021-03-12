using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9D RID: 2973
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002EF4 RID: 12020
		[DataMember(IsRequired = true, Order = 0)]
		public FindItemRequest Body;
	}
}
