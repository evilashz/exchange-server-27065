using System;
using System.Configuration;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000052 RID: 82
	internal sealed class AppConfigAdapter : IConfigAdapter
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003250 File Offset: 0x00001450
		internal static AppConfigAdapter Instance
		{
			get
			{
				return AppConfigAdapter.instance;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00003257 File Offset: 0x00001457
		public string GetSetting(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		// Token: 0x0400009B RID: 155
		private static AppConfigAdapter instance = new AppConfigAdapter();
	}
}
