using System;
using System.Security;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Win32;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020006E2 RID: 1762
	internal static class SslConfiguration
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x00041C7F File Offset: 0x0003FE7F
		internal static bool AllowInternalUntrustedCerts
		{
			get
			{
				if (!SslConfiguration.attemptedToReadFromRegistry)
				{
					SslConfiguration.ReadFromRegistry();
				}
				return SslConfiguration.allowInternalUntrustedCerts;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x00041C92 File Offset: 0x0003FE92
		internal static bool AllowExternalUntrustedCerts
		{
			get
			{
				if (!SslConfiguration.attemptedToReadFromRegistry)
				{
					SslConfiguration.ReadFromRegistry();
				}
				return SslConfiguration.allowExternalUntrustedCerts;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00041CA8 File Offset: 0x0003FEA8
		private static void ReadFromRegistry()
		{
			lock (SslConfiguration.locker)
			{
				if (!SslConfiguration.attemptedToReadFromRegistry)
				{
					RegistryKey registryKey = null;
					try
					{
						registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA");
						if (registryKey != null)
						{
							SslConfiguration.allowInternalUntrustedCerts = SslConfiguration.ReadBoolValue(registryKey, "AllowInternalUntrustedCerts", SslConfiguration.allowInternalUntrustedCertsDefault);
							SslConfiguration.allowExternalUntrustedCerts = SslConfiguration.ReadBoolValue(registryKey, "AllowExternalUntrustedCerts", SslConfiguration.allowExternalUntrustedCertsDefault);
						}
						else
						{
							SslConfiguration.ConfigurationTracer.TraceError(0L, "Error reading SSL configuration values from Registry: keyContainer is null. Using default values.");
						}
					}
					catch (SecurityException arg)
					{
						SslConfiguration.ConfigurationTracer.TraceError<SecurityException>(0L, "Exception {0} encountered while reading SSL configuration values from Registry. Using default values.", arg);
					}
					finally
					{
						if (registryKey != null)
						{
							registryKey.Close();
						}
						SslConfiguration.attemptedToReadFromRegistry = true;
					}
				}
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00041D7C File Offset: 0x0003FF7C
		private static bool ReadBoolValue(RegistryKey keyContainer, string valueName, bool defaultValue)
		{
			if (keyContainer != null)
			{
				object value = keyContainer.GetValue(valueName);
				if (value != null && value is int)
				{
					SslConfiguration.ConfigurationTracer.TraceDebug<string, object>(0L, "Registry value {0} was found. Its value is {1}", valueName, value);
					return (int)value != 0;
				}
				SslConfiguration.ConfigurationTracer.TraceDebug<string, bool>(0L, "Registry value {0} was not found or invalid. Returning default value: {1}.", valueName, defaultValue);
			}
			else
			{
				SslConfiguration.ConfigurationTracer.TraceDebug<string, bool>(0L, "Container {0} was not found in registry. Returning default value: {1}.", "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA", defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x04001F80 RID: 8064
		private const string containerPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA";

		// Token: 0x04001F81 RID: 8065
		private const string allowInternalUntrustedCertsName = "AllowInternalUntrustedCerts";

		// Token: 0x04001F82 RID: 8066
		private const string allowExternalUntrustedCertsName = "AllowExternalUntrustedCerts";

		// Token: 0x04001F83 RID: 8067
		private static bool allowInternalUntrustedCertsDefault = true;

		// Token: 0x04001F84 RID: 8068
		private static bool allowExternalUntrustedCertsDefault = false;

		// Token: 0x04001F85 RID: 8069
		private static bool allowInternalUntrustedCerts = SslConfiguration.allowInternalUntrustedCertsDefault;

		// Token: 0x04001F86 RID: 8070
		private static bool allowExternalUntrustedCerts = SslConfiguration.allowExternalUntrustedCertsDefault;

		// Token: 0x04001F87 RID: 8071
		private static readonly Trace ConfigurationTracer = ExTraceGlobals.ConfigurationTracer;

		// Token: 0x04001F88 RID: 8072
		private static bool attemptedToReadFromRegistry = false;

		// Token: 0x04001F89 RID: 8073
		private static object locker = new object();
	}
}
