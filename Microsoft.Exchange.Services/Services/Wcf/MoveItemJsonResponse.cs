using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAE RID: 2990
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MoveItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F05 RID: 12037
		[DataMember(IsRequired = true, Order = 0)]
		public MoveItemResponse Body;
	}
}
