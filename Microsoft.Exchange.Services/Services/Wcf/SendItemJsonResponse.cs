using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAC RID: 2988
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendItemJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F03 RID: 12035
		[DataMember(IsRequired = true, Order = 0)]
		public SendItemResponse Body;
	}
}
