using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2E RID: 3118
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F85 RID: 12165
		[DataMember(IsRequired = true, Order = 0)]
		public AddImGroupResponseMessage Body;
	}
}
