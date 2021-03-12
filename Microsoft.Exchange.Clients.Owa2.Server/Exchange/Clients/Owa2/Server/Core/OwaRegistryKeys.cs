using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Win32;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001FA RID: 506
	public static class OwaRegistryKeys
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x00044A93 File Offset: 0x00042C93
		public static string IMImplementationDllPath
		{
			get
			{
				return OwaRegistryKeys.GetStringValue(OwaRegistryKeys.implementationDllPathKey);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00044A9F File Offset: 0x00042C9F
		public static string IMImplementationDllPathKey
		{
			get
			{
				return OwaRegistryKeys.implementationDllPathKey.Name;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00044AAB File Offset: 0x00042CAB
		public static string InstalledOwaVersion
		{
			get
			{
				return OwaRegistryKeys.GetStringValue(OwaRegistryKeys.owaVersion);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00044AB7 File Offset: 0x00042CB7
		public static string InstalledNextOwaVersion
		{
			get
			{
				return OwaRegistryKeys.GetStringValue(OwaRegistryKeys.nextOwaVersion);
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00044BFC File Offset: 0x00042DFC
		public static void Initialize()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaRegistryKeys.Initialize");
			foreach (string text in OwaRegistryKeys.keySetMap.Keys)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(text))
				{
					if (registryKey != null)
					{
						foreach (OwaRegistryKey owaRegistryKey in OwaRegistryKeys.keySetMap[text])
						{
							OwaRegistryKeys.keyValueCache[owaRegistryKey] = OwaRegistryKeys.ReadKeyValue(registryKey, owaRegistryKey);
						}
					}
				}
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00044CD8 File Offset: 0x00042ED8
		public static void UpdateOwaSetupVersionsCache()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(OwaRegistryKeys.OwaSetupInstallKey))
			{
				if (registryKey != null)
				{
					foreach (OwaRegistryKey owaRegistryKey in OwaRegistryKeys.keySetMap[OwaRegistryKeys.OwaSetupInstallKey])
					{
						OwaRegistryKeys.keyValueCache[owaRegistryKey] = OwaRegistryKeys.ReadKeyValue(registryKey, owaRegistryKey);
					}
				}
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00044D6C File Offset: 0x00042F6C
		private static object ReadKeyValue(RegistryKey keyContainer, OwaRegistryKey owaKey)
		{
			ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Reading registry key \"{0}\"", owaKey.Name);
			object obj;
			if (owaKey.KeyType == typeof(int))
			{
				obj = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (obj.GetType() != typeof(int))
				{
					obj = null;
				}
			}
			else if (owaKey.KeyType == typeof(uint))
			{
				obj = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (obj.GetType() != typeof(uint))
				{
					obj = null;
				}
			}
			else if (owaKey.KeyType == typeof(string))
			{
				obj = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (obj.GetType() != typeof(string))
				{
					obj = null;
				}
			}
			else
			{
				if (!(owaKey.KeyType == typeof(bool)))
				{
					return null;
				}
				object value = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (value.GetType() != typeof(int))
				{
					obj = null;
				}
				else
				{
					obj = ((int)value != 0);
				}
			}
			if (obj == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Couldn't find key or key format/type was incorrect, using default value");
				obj = owaKey.DefaultValue;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string, object>(0L, "Configuration registry key \"{0}\" read with value=\"{1}\"", owaKey.Name, obj);
			return obj;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00044EFC File Offset: 0x000430FC
		private static T GetValue<T>(OwaRegistryKey key)
		{
			object obj = null;
			if (OwaRegistryKeys.keyValueCache.TryGetValue(key, out obj))
			{
				return (T)((object)obj);
			}
			return (T)((object)key.DefaultValue);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00044F2C File Offset: 0x0004312C
		private static string GetStringValue(OwaRegistryKey key)
		{
			return OwaRegistryKeys.GetValue<string>(key);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00044F34 File Offset: 0x00043134
		private static bool GetBoolValue(OwaRegistryKey key)
		{
			return OwaRegistryKeys.GetValue<bool>(key);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00044F3C File Offset: 0x0004313C
		private static int GetIntValue(OwaRegistryKey key)
		{
			return OwaRegistryKeys.GetValue<int>(key);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00044F44 File Offset: 0x00043144
		private static uint GetUIntValue(OwaRegistryKey key)
		{
			return OwaRegistryKeys.GetValue<uint>(key);
		}

		// Token: 0x04000A81 RID: 2689
		internal static readonly string OwaSetupInstallKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000A82 RID: 2690
		internal static readonly string OwaRegKeyBase = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04000A83 RID: 2691
		private static OwaRegistryKey implementationDllPathKey = new OwaRegistryKey("ImplementationDLLPath", typeof(string), string.Empty);

		// Token: 0x04000A84 RID: 2692
		private static OwaRegistryKey owaVersion = new OwaRegistryKey("OwaVersion", typeof(string), string.Empty);

		// Token: 0x04000A85 RID: 2693
		private static OwaRegistryKey nextOwaVersion = new OwaRegistryKey("NextOwaVersion", typeof(string), string.Empty);

		// Token: 0x04000A86 RID: 2694
		private static List<OwaRegistryKey> owaIMKeys = new List<OwaRegistryKey>(1)
		{
			OwaRegistryKeys.implementationDllPathKey
		};

		// Token: 0x04000A87 RID: 2695
		private static List<OwaRegistryKey> owaSetupKeys = new List<OwaRegistryKey>(2)
		{
			OwaRegistryKeys.owaVersion,
			OwaRegistryKeys.nextOwaVersion
		};

		// Token: 0x04000A88 RID: 2696
		private static Dictionary<string, List<OwaRegistryKey>> keySetMap = new Dictionary<string, List<OwaRegistryKey>>
		{
			{
				OwaRegistryKeys.OwaRegKeyBase + "\\InstantMessaging",
				OwaRegistryKeys.owaIMKeys
			},
			{
				OwaRegistryKeys.OwaSetupInstallKey,
				OwaRegistryKeys.owaSetupKeys
			}
		};

		// Token: 0x04000A89 RID: 2697
		private static int keySetMapTotalCount = OwaRegistryKeys.keySetMap.Sum((KeyValuePair<string, List<OwaRegistryKey>> kvPair) => kvPair.Value.Count);

		// Token: 0x04000A8A RID: 2698
		private static Dictionary<OwaRegistryKey, object> keyValueCache = new Dictionary<OwaRegistryKey, object>(OwaRegistryKeys.keySetMapTotalCount);
	}
}
