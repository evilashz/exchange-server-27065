using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7F RID: 3199
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserUnifiedGroupsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FD2 RID: 12242
		[DataMember(IsRequired = true, Order = 0)]
		public GetUserUnifiedGroupsResponseMessage Body;
	}
}
