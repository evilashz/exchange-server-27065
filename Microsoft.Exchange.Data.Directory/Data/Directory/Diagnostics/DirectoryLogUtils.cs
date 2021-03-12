using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000BD RID: 189
	internal static class DirectoryLogUtils
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0002C2C8 File Offset: 0x0002A4C8
		internal static bool GetRegistryBool(RegistryKey regkey, string key, bool defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(num.Value);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0002C30C File Offset: 0x0002A50C
		internal static int GetRegistryInt(RegistryKey regkey, string key, int defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return num.Value;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002C348 File Offset: 0x0002A548
		internal static string GetExchangeInstallPath()
		{
			string result = string.Empty;
			try
			{
				result = ExchangeSetupContext.InstallPath;
			}
			catch
			{
			}
			return result;
		}
	}
}
