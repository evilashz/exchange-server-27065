using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200013F RID: 319
	internal interface IApplicationServices
	{
		// Token: 0x06000C7D RID: 3197
		Stream CreateTemporaryStorage();

		// Token: 0x06000C7E RID: 3198
		IList<CtsConfigurationSetting> GetConfiguration(string subSectionName);

		// Token: 0x06000C7F RID: 3199
		void RefreshConfiguration();

		// Token: 0x06000C80 RID: 3200
		void LogConfigurationErrorEvent();
	}
}
