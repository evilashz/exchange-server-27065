using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C2D RID: 3117
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddImGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F84 RID: 12164
		[DataMember(IsRequired = true, Order = 0)]
		public AddImGroupRequest Body;
	}
}
