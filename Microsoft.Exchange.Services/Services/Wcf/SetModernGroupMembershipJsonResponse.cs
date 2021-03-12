using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C71 RID: 3185
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetModernGroupMembershipJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FC4 RID: 12228
		[DataMember(IsRequired = true, Order = 0)]
		public SetModernGroupMembershipResponse Body;
	}
}
