using System;

namespace Microsoft.Exchange.Services.Services.jsonservice.types.Weather.Weather4
{
	// Token: 0x02000B51 RID: 2897
	public interface IWeatherLocation
	{
		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x0600520A RID: 21002
		// (set) Token: 0x0600520B RID: 21003
		string Attribution { get; set; }

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x0600520C RID: 21004
		// (set) Token: 0x0600520D RID: 21005
		WeatherConditions Conditions { get; set; }

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x0600520E RID: 21006
		// (set) Token: 0x0600520F RID: 21007
		string DegreeUnit { get; set; }

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06005210 RID: 21008
		// (set) Token: 0x06005211 RID: 21009
		long EntityId { get; set; }

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06005212 RID: 21010
		// (set) Token: 0x06005213 RID: 21011
		string ErrorMessage { get; set; }

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06005214 RID: 21012
		// (set) Token: 0x06005215 RID: 21013
		WeatherForecast[] Forecast { get; set; }

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06005216 RID: 21014
		// (set) Token: 0x06005217 RID: 21015
		string FullName { get; set; }

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06005218 RID: 21016
		// (set) Token: 0x06005219 RID: 21017
		double Latitude { get; set; }

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x0600521A RID: 21018
		// (set) Token: 0x0600521B RID: 21019
		double Longitude { get; set; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x0600521C RID: 21020
		// (set) Token: 0x0600521D RID: 21021
		string Name { get; set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x0600521E RID: 21022
		// (set) Token: 0x0600521F RID: 21023
		string ShortAttribution { get; set; }

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06005220 RID: 21024
		// (set) Token: 0x06005221 RID: 21025
		string Url { get; set; }
	}
}
