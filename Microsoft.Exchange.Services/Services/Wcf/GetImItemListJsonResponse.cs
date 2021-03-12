using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C34 RID: 3124
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetImItemListJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F8B RID: 12171
		[DataMember(IsRequired = true, Order = 0)]
		public GetImItemListResponseMessage Body;
	}
}
