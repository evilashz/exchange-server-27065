using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1C RID: 3100
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetHoldOnMailboxesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F73 RID: 12147
		[DataMember(IsRequired = true, Order = 0)]
		public GetHoldOnMailboxesResponse Body;
	}
}
