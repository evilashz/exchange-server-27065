using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC2 RID: 3010
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F19 RID: 12057
		[DataMember(IsRequired = true, Order = 0)]
		public SubscribeResponse Body;
	}
}
