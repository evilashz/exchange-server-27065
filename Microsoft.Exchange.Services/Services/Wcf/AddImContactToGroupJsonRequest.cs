using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C29 RID: 3113
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImContactToGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F80 RID: 12160
		[DataMember(IsRequired = true, Order = 0)]
		public AddImContactToGroupRequest Body;
	}
}
