using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F7 RID: 503
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConfigProvider : IDisposable, IDiagnosable
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001158 RID: 4440
		DateTime LastUpdated { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001159 RID: 4441
		bool IsInitialized { get; }

		// Token: 0x0600115A RID: 4442
		void Initialize();

		// Token: 0x0600115B RID: 4443
		T GetConfig<T>(string settingName);

		// Token: 0x0600115C RID: 4444
		T GetConfig<T>(ISettingsContext context, string settingName);

		// Token: 0x0600115D RID: 4445
		T GetConfig<T>(ISettingsContext context, string settingName, T defaultValue);

		// Token: 0x0600115E RID: 4446
		bool TryGetConfig<T>(ISettingsContext context, string settingName, out T settingValue);
	}
}
