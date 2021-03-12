using System;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B50 RID: 2896
	public interface IWeatherForecast
	{
		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06005200 RID: 20992
		// (set) Token: 0x06005201 RID: 20993
		int High { get; set; }

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005202 RID: 20994
		// (set) Token: 0x06005203 RID: 20995
		int Low { get; set; }

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06005204 RID: 20996
		// (set) Token: 0x06005205 RID: 20997
		int PrecipitationCertainty { get; set; }

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x06005206 RID: 20998
		// (set) Token: 0x06005207 RID: 20999
		int SkyCodeDay { get; set; }

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06005208 RID: 21000
		// (set) Token: 0x06005209 RID: 21001
		string SkyTextDay { get; set; }
	}
}
