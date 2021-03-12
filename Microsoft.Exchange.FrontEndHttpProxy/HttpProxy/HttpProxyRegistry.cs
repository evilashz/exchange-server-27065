using System;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000037 RID: 55
	internal static class HttpProxyRegistry
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00009E48 File Offset: 0x00008048
		private static bool GetOWARegistryValue(string valueName, bool defaultValue)
		{
			bool result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA", false))
				{
					object value = registryKey.GetValue(valueName);
					if (value == null || !(value is int))
					{
						result = defaultValue;
					}
					else
					{
						result = ((int)value != 0);
					}
				}
			}
			catch (SecurityException)
			{
				ExTraceGlobals.VerboseTracer.TraceError<string, bool>(0L, "[HttpProxyRegistry::GetOWARegistryValue] Security exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			catch (UnauthorizedAccessException)
			{
				ExTraceGlobals.VerboseTracer.TraceError<string, bool>(0L, "[HttpProxyRegistry::GetOWARegistryValue] Unauthorized exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x040000D8 RID: 216
		internal const string MSExchangeOWARegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x040000D9 RID: 217
		public static readonly LazyMember<bool> OwaAllowInternalUntrustedCerts = new LazyMember<bool>(() => HttpProxyRegistry.GetOWARegistryValue("AllowInternalUntrustedCerts", true));

		// Token: 0x040000DA RID: 218
		public static readonly LazyMember<bool> AreGccStoredSecretKeysValid = new LazyMember<bool>(() => HttpProxyRegistry.AreGccStoredSecretKeysValid.Member);
	}
}
