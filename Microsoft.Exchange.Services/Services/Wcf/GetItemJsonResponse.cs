using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BA4 RID: 2980
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EFB RID: 12027
		[DataMember(IsRequired = true, Order = 0)]
		public GetItemResponse Body;
	}
}
