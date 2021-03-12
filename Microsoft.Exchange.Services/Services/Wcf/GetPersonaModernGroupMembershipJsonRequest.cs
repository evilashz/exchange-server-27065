using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C64 RID: 3172
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaModernGroupMembershipJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FB8 RID: 12216
		[DataMember(IsRequired = true, Order = 0)]
		public GetPersonaModernGroupMembershipRequest Body;
	}
}
