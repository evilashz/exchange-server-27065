using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2A RID: 3114
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImContactToGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F81 RID: 12161
		[DataMember(IsRequired = true, Order = 0)]
		public AddImContactToGroupResponseMessage Body;
	}
}
