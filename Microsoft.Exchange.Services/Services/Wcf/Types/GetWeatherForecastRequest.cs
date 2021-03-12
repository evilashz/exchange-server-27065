using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B59 RID: 2905
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWeatherForecastRequest : BaseRequest
	{
		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06005261 RID: 21089 RVA: 0x0010B3CD File Offset: 0x001095CD
		// (set) Token: 0x06005262 RID: 21090 RVA: 0x0010B3D5 File Offset: 0x001095D5
		[DataMember]
		public WeatherLocationId[] WeatherLocationIds { get; set; }

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06005263 RID: 21091 RVA: 0x0010B3DE File Offset: 0x001095DE
		// (set) Token: 0x06005264 RID: 21092 RVA: 0x0010B3E6 File Offset: 0x001095E6
		[DataMember(IsRequired = true)]
		public TemperatureUnit TemperatureUnit { get; set; }

		// Token: 0x06005265 RID: 21093 RVA: 0x0010B3EF File Offset: 0x001095EF
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x0010B3F9 File Offset: 0x001095F9
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
