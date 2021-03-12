using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2F RID: 3119
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddNewImContactToGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F86 RID: 12166
		[DataMember(IsRequired = true, Order = 0)]
		public AddNewImContactToGroupRequest Body;
	}
}
