using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C82 RID: 3202
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetClutterStateJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FD5 RID: 12245
		[DataMember(IsRequired = true, Order = 0)]
		public SetClutterStateRequest Body;
	}
}
