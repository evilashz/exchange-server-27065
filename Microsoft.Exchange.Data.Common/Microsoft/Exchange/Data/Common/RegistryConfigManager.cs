using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x0200000E RID: 14
	public class RegistryConfigManager
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003514 File Offset: 0x00001714
		private static void ReadAllConfigsIfRequired()
		{
			long ticks = DateTime.UtcNow.Ticks;
			if (RegistryConfigManager.lastAccessTicks != 0L && RegistryConfigManager.lastAccessTicks <= ticks)
			{
				if (ticks - RegistryConfigManager.lastAccessTicks <= RegistryConfigManager.RegistryReadIntervalTicks)
				{
					return;
				}
			}
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("Iso2022JpEncodingOverride");
						if (value != null && value is int)
						{
							RegistryConfigManager.iso2022JpEncodingOverride = (int)value;
						}
						object value2 = registryKey.GetValue("HtmlEncapsulationOverride");
						if (value2 != null && value2 is int)
						{
							RegistryConfigManager.htmlEncapsulationOverride = (int)value2;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				RegistryConfigManager.lastAccessTicks = ticks;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000035E8 File Offset: 0x000017E8
		internal static int Iso2022JpEncodingOverride
		{
			get
			{
				RegistryConfigManager.ReadAllConfigsIfRequired();
				return RegistryConfigManager.iso2022JpEncodingOverride;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000035F4 File Offset: 0x000017F4
		internal static bool HtmlEncapsulationOverride
		{
			get
			{
				RegistryConfigManager.ReadAllConfigsIfRequired();
				return RegistryConfigManager.htmlEncapsulationOverride != 0;
			}
		}

		// Token: 0x04000022 RID: 34
		internal const string RegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x04000023 RID: 35
		internal const string Iso2022JpEncodingOverrideRegistryValueName = "Iso2022JpEncodingOverride";

		// Token: 0x04000024 RID: 36
		internal const string HtmlEncapsulationOverrideRegistryValueName = "HtmlEncapsulationOverride";

		// Token: 0x04000025 RID: 37
		internal static readonly long RegistryReadIntervalTicks = 600000000L;

		// Token: 0x04000026 RID: 38
		private static long lastAccessTicks;

		// Token: 0x04000027 RID: 39
		private static int iso2022JpEncodingOverride = 1;

		// Token: 0x04000028 RID: 40
		private static int htmlEncapsulationOverride = 1;
	}
}
