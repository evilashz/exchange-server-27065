using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BC4 RID: 3012
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F1B RID: 12059
		[DataMember(IsRequired = true, Order = 0)]
		public UnsubscribeResponse Body;
	}
}
