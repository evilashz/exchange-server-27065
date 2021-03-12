using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C79 RID: 3193
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWeatherForecastJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FCC RID: 12236
		[DataMember(IsRequired = true, Order = 0)]
		public GetWeatherForecastResponse Body;
	}
}
