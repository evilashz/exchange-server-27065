using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C53 RID: 3155
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeprovisionJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FAA RID: 12202
		[DataMember(IsRequired = true, Order = 0)]
		public DeprovisionRequest Body;
	}
}
