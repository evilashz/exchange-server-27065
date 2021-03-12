using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C70 RID: 3184
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveModernGroupJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FC3 RID: 12227
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveModernGroupResponse Body;
	}
}
