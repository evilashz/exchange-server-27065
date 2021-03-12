using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000094 RID: 148
	internal interface IEasFeaturesManager
	{
		// Token: 0x0600086F RID: 2159
		bool IsEnabled(EasFeature featureId);

		// Token: 0x06000870 RID: 2160
		bool IsOverridden(EasFeature featureId);
	}
}
