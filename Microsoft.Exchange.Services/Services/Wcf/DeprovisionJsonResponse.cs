using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C54 RID: 3156
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeprovisionJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FAB RID: 12203
		[DataMember(IsRequired = true, Order = 0)]
		public DeprovisionResponse Body;
	}
}
