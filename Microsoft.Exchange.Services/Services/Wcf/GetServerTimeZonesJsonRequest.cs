using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BBD RID: 3005
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetServerTimeZonesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F14 RID: 12052
		[DataMember(IsRequired = true, Order = 0)]
		public GetServerTimeZonesRequest Body;
	}
}
