using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3E RID: 3134
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetImGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F95 RID: 12181
		[DataMember(IsRequired = true, Order = 0)]
		public SetImGroupResponseMessage Body;
	}
}
