using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C52 RID: 3154
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ProvisionJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FA9 RID: 12201
		[DataMember(IsRequired = true, Order = 0)]
		public ProvisionResponse Body;
	}
}
