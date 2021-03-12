using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C30 RID: 3120
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddNewImContactToGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F87 RID: 12167
		[DataMember(IsRequired = true, Order = 0)]
		public AddNewImContactToGroupResponseMessage Body;
	}
}
