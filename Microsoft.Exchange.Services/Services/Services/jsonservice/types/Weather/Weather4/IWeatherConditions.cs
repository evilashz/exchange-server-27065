using System;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B4F RID: 2895
	public interface IWeatherConditions
	{
		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060051FA RID: 20986
		// (set) Token: 0x060051FB RID: 20987
		int SkyCode { get; set; }

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x060051FC RID: 20988
		// (set) Token: 0x060051FD RID: 20989
		string SkyText { get; set; }

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060051FE RID: 20990
		// (set) Token: 0x060051FF RID: 20991
		int Temperature { get; set; }
	}
}
