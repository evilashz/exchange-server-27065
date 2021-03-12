using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ConfigurationSettings
{
	// Token: 0x020001F1 RID: 497
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IConfigDriver : IDisposable
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001127 RID: 4391
		bool IsInitialized { get; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001128 RID: 4392
		DateTime LastUpdated { get; }

		// Token: 0x06001129 RID: 4393
		void Initialize();

		// Token: 0x0600112A RID: 4394
		bool TryGetBoxedSetting(ISettingsContext context, string settingName, Type settingType, out object settingValue);

		// Token: 0x0600112B RID: 4395
		XElement GetDiagnosticInfo(string argument);
	}
}
