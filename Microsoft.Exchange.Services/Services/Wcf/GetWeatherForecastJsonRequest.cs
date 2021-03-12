using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C78 RID: 3192
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWeatherForecastJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FCB RID: 12235
		[DataMember(IsRequired = true, Order = 0)]
		public GetWeatherForecastRequest Body;
	}
}
