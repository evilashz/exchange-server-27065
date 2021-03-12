using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000002 RID: 2
	internal sealed class InternalConfiguration
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		static InternalConfiguration()
		{
			InternalConfiguration.ReadEaiConfig();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		internal static void ReadEaiConfig()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("EaiEnabled");
						if (value != null)
						{
							bool.TryParse(value.ToString(), out InternalConfiguration.eaiEnabled);
						}
					}
				}
			}
			catch (SecurityException exception)
			{
				InternalConfiguration.HandleRegistryReadError(exception);
			}
			catch (UnauthorizedAccessException exception2)
			{
				InternalConfiguration.HandleRegistryReadError(exception2);
			}
			catch (IOException exception3)
			{
				InternalConfiguration.HandleRegistryReadError(exception3);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000217C File Offset: 0x0000037C
		public static bool IsEaiEnabled()
		{
			return InternalConfiguration.eaiEnabled;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002183 File Offset: 0x00000383
		private static void HandleRegistryReadError(Exception exception)
		{
			ExTraceGlobals.CommonTracer.TraceError<string, string>(0L, "Exception occurred while reading EAI configuration from registry.{0}{1}", Environment.NewLine, exception.ToString());
		}

		// Token: 0x04000001 RID: 1
		internal const string RegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x04000002 RID: 2
		internal const string EaiEnabledRegistryValueName = "EaiEnabled";

		// Token: 0x04000003 RID: 3
		internal const bool EaiEnabledDefaultValue = false;

		// Token: 0x04000004 RID: 4
		private static bool eaiEnabled;
	}
}
