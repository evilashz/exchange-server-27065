using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C73 RID: 3187
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class PostModernGroupItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FC6 RID: 12230
		[DataMember(IsRequired = true, Order = 0)]
		public PostModernGroupItemResponse Body;
	}
}
