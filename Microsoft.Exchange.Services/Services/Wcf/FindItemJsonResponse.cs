using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B9E RID: 2974
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002EF5 RID: 12021
		[DataMember(IsRequired = true, Order = 0)]
		public FindItemResponse Body;
	}
}
