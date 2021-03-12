using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA8 RID: 2984
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EFF RID: 12031
		[DataMember(IsRequired = true, Order = 0)]
		public DeleteItemResponse Body;
	}
}
