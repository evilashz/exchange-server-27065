using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C46 RID: 3142
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsDiagnosticsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F9D RID: 12189
		[DataMember(IsRequired = true, Order = 0)]
		public GetConversationItemsDiagnosticsResponse Body;
	}
}
