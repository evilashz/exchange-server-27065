using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C31 RID: 3121
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddNewTelUriContactToGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F88 RID: 12168
		[DataMember(IsRequired = true, Order = 0)]
		public AddNewTelUriContactToGroupRequest Body;
	}
}
