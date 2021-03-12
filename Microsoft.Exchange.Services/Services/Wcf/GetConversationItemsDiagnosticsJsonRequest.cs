using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C45 RID: 3141
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetConversationItemsDiagnosticsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F9C RID: 12188
		[DataMember(IsRequired = true, Order = 0)]
		public GetConversationItemsDiagnosticsRequest Body;
	}
}
