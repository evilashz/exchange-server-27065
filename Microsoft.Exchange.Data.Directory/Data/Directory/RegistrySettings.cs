using System;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020007AB RID: 1963
	internal static class RegistrySettings
	{
		// Token: 0x020007AC RID: 1964
		internal static class MSExchangeADAccess
		{
			// Token: 0x0400417C RID: 16764
			private const string MsExchangeADAccessRegistryPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess";
		}

		// Token: 0x020007AD RID: 1965
		internal class ExchangeServerCurrentVersion
		{
			// Token: 0x170022D0 RID: 8912
			// (get) Token: 0x060061A7 RID: 24999 RVA: 0x0014E08A File Offset: 0x0014C28A
			internal static GlsEnvironmentType GlsEnvironmentType
			{
				get
				{
					return Globals.GetEnumValueFromRegistry<GlsEnvironmentType>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", RegistrySettings.ExchangeServerCurrentVersion.GlobalDirectoryEnvironmentTypeValue, GlsEnvironmentType.Prod, ExTraceGlobals.GLSTracer);
				}
			}

			// Token: 0x170022D1 RID: 8913
			// (get) Token: 0x060061A8 RID: 25000 RVA: 0x0014E0A4 File Offset: 0x0014C2A4
			public static string SmtpNextHopDomainFormat
			{
				get
				{
					string valueFromRegistry = Globals.GetValueFromRegistry<string>("SOFTWARE\\Microsoft\\ExchangeServer\\v15", RegistrySettings.ExchangeServerCurrentVersion.SmtpNextHopDomainFormatValue, null, ExTraceGlobals.GLSTracer);
					if (string.IsNullOrEmpty(valueFromRegistry))
					{
						throw new GlsPermanentException(DirectoryStrings.PermanentGlsError(string.Format("{0} is not present", RegistrySettings.ExchangeServerCurrentVersion.SmtpNextHopDomainFormatValue)));
					}
					return valueFromRegistry;
				}
			}

			// Token: 0x170022D2 RID: 8914
			// (get) Token: 0x060061A9 RID: 25001 RVA: 0x0014E0EC File Offset: 0x0014C2EC
			internal static int GLSTenantCacheExpiry
			{
				get
				{
					return Globals.GetIntValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeServer\\v15", RegistrySettings.ExchangeServerCurrentVersion.GLSTenantCacheExpiryValue, (int)TimeSpan.FromHours(1.0).TotalSeconds, 0);
				}
			}

			// Token: 0x170022D3 RID: 8915
			// (get) Token: 0x060061AA RID: 25002 RVA: 0x0014E120 File Offset: 0x0014C320
			private static Random Random
			{
				get
				{
					if (RegistrySettings.ExchangeServerCurrentVersion.randomObject == null)
					{
						RegistrySettings.ExchangeServerCurrentVersion.randomObject = new Random();
					}
					return RegistrySettings.ExchangeServerCurrentVersion.randomObject;
				}
			}

			// Token: 0x170022D4 RID: 8916
			// (get) Token: 0x060061AB RID: 25003 RVA: 0x0014E138 File Offset: 0x0014C338
			internal static int NotificationThrottlingTimeMinutes
			{
				get
				{
					return Globals.GetIntValueFromRegistry(RegistrySettings.ExchangeServerCurrentVersion.NotificationThrottlingRegistryKeyName, 15, 0) + RegistrySettings.ExchangeServerCurrentVersion.Random.Next(15);
				}
			}

			// Token: 0x0400417D RID: 16765
			private const string registryKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

			// Token: 0x0400417E RID: 16766
			private static readonly string GlobalDirectoryEnvironmentTypeValue = "GlobalDirectoryEnvironmentType";

			// Token: 0x0400417F RID: 16767
			private static readonly string SmtpNextHopDomainFormatValue = "SmtpNextHopDomainFormat";

			// Token: 0x04004180 RID: 16768
			private static readonly string GLSTenantCacheExpiryValue = "GLSTenantCacheExpiry";

			// Token: 0x04004181 RID: 16769
			[ThreadStatic]
			private static Random randomObject;

			// Token: 0x04004182 RID: 16770
			private static readonly string NotificationThrottlingRegistryKeyName = "NotificationThrottling";
		}
	}
}
