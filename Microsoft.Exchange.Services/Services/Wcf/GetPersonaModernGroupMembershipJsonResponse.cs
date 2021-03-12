using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C65 RID: 3173
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaModernGroupMembershipJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FB9 RID: 12217
		[DataMember(IsRequired = true, Order = 0)]
		public GetPersonaModernGroupMembershipResponse Body;
	}
}
