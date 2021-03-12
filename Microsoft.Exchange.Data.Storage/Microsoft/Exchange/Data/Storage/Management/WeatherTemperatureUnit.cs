using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009D9 RID: 2521
	public enum WeatherTemperatureUnit
	{
		// Token: 0x0400334D RID: 13133
		[LocDescription(ServerStrings.IDs.WeatherUnitDefault)]
		Default,
		// Token: 0x0400334E RID: 13134
		[LocDescription(ServerStrings.IDs.WeatherUnitCelsius)]
		Celsius,
		// Token: 0x0400334F RID: 13135
		[LocDescription(ServerStrings.IDs.WeatherUnitFahrenheit)]
		Fahrenheit
	}
}
