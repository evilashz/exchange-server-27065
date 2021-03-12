using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3D RID: 3133
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImGroupJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F94 RID: 12180
		[DataMember(IsRequired = true, Order = 0)]
		public SetImGroupRequest Body;
	}
}
