using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C7B RID: 3195
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindWeatherLocationsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FCE RID: 12238
		[DataMember(IsRequired = true, Order = 0)]
		public FindWeatherLocationsResponse Body;
	}
}
