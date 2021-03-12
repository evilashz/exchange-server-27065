using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBE RID: 3006
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetServerTimeZonesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F15 RID: 12053
		[DataMember(IsRequired = true, Order = 0)]
		public GetServerTimeZonesResponse Body;
	}
}
