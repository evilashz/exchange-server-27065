using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7A RID: 3194
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindWeatherLocationsJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FCD RID: 12237
		[DataMember(IsRequired = true, Order = 0)]
		public FindWeatherLocationsRequest Body;
	}
}
