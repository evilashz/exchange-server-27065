using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000140 RID: 320
	[Flags]
	public enum VariantConfigurationBehavior
	{
		// Token: 0x040004E3 RID: 1251
		Default = 0,
		// Token: 0x040004E4 RID: 1252
		EvaluateTeams = 2,
		// Token: 0x040004E5 RID: 1253
		EvaluateFlights = 4,
		// Token: 0x040004E6 RID: 1254
		DelayLoadDataSources = 8
	}
}
