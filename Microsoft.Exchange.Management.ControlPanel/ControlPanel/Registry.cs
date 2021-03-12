using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200055D RID: 1373
	internal static class Registry
	{
		// Token: 0x0600402A RID: 16426 RVA: 0x000C364C File Offset: 0x000C184C
		static Registry()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA", false))
			{
				if (registryKey != null)
				{
					Registry.AllowInternalUntrustedCerts = registryKey.GetBoolean("AllowInternalUntrustedCerts", Registry.AllowInternalUntrustedCerts);
					Registry.AllowProxyingWithoutSsl = registryKey.GetBoolean("AllowProxyingWithoutSsl", Registry.AllowProxyingWithoutSsl);
					Registry.SslOffloaded = registryKey.GetBoolean("SSLOffloaded", Registry.SslOffloaded);
				}
			}
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000C36DC File Offset: 0x000C18DC
		private static bool GetBoolean(this RegistryKey key, string valueName, bool defaultValue)
		{
			object value = key.GetValue(valueName, defaultValue);
			if (value is int)
			{
				return (int)value != 0;
			}
			return defaultValue;
		}

		// Token: 0x04002AB9 RID: 10937
		private const string OwaRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04002ABA RID: 10938
		private const string AllowInternalUntrustedCertsKey = "AllowInternalUntrustedCerts";

		// Token: 0x04002ABB RID: 10939
		private const string AllowProxyingWithoutSslKey = "AllowProxyingWithoutSsl";

		// Token: 0x04002ABC RID: 10940
		private const string SslOffloadedKey = "SSLOffloaded";

		// Token: 0x04002ABD RID: 10941
		public static readonly bool AllowInternalUntrustedCerts = true;

		// Token: 0x04002ABE RID: 10942
		public static readonly bool AllowProxyingWithoutSsl = false;

		// Token: 0x04002ABF RID: 10943
		public static readonly bool SslOffloaded = false;
	}
}
