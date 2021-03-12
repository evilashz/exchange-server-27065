using System;
using System.Configuration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000548 RID: 1352
	public static class ConfigUtil
	{
		// Token: 0x06003F9E RID: 16286 RVA: 0x000BFE58 File Offset: 0x000BE058
		internal static int ReadInt(string key, int defaultValue)
		{
			int result;
			if (!int.TryParse(ConfigurationManager.AppSettings[key], out result))
			{
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x000BFE7C File Offset: 0x000BE07C
		internal static bool ReadBool(string key, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(ConfigurationManager.AppSettings[key], out result))
			{
				result = defaultValue;
			}
			return result;
		}
	}
}
