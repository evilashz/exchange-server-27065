using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C83 RID: 3203
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetClutterStateJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FD6 RID: 12246
		[DataMember(IsRequired = true, Order = 0)]
		public SetClutterStateResponse Body;
	}
}
