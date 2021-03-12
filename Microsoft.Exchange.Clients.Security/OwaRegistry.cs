using System;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x0200002C RID: 44
	internal static class OwaRegistry
	{
		// Token: 0x0600015A RID: 346 RVA: 0x0000A9FC File Offset: 0x00008BFC
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
				ExTraceGlobals.CoreTracer.TraceError<string, bool>(0L, "[OwaRegistry::GetOWARegistryValue] Security exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			catch (UnauthorizedAccessException)
			{
				ExTraceGlobals.CoreTracer.TraceError<string, bool>(0L, "[OwaRegistry::GetOWARegistryValue] Unauthorized exception encountered while retrieving {0} registry value.  Defaulting to {1}", valueName, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x04000166 RID: 358
		internal const string MSExchangeOWARegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04000167 RID: 359
		internal static readonly LazyMember<bool> OwaAllowInternalUntrustedCerts = new LazyMember<bool>(() => OwaRegistry.GetOWARegistryValue("AllowInternalUntrustedCerts", true));
	}
}
