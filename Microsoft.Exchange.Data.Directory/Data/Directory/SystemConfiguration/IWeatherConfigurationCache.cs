using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000648 RID: 1608
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IWeatherConfigurationCache
	{
		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06004BA4 RID: 19364
		bool IsFeatureEnabled { get; }

		// Token: 0x06004BA5 RID: 19365
		bool IsRestrictedCulture(string culture);

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06004BA6 RID: 19366
		string WeatherServiceUrl { get; }
	}
}
