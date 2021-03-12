using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004C2 RID: 1218
	internal static class DiagnosticsItemFactory
	{
		// Token: 0x06001E7A RID: 7802 RVA: 0x000B7F64 File Offset: 0x000B6164
		public static DiagnosticsItemBase Create(string header)
		{
			DiagnosticsItemBase diagnosticsItemBase = new DiagnosticsItemUnknown();
			if (!string.IsNullOrEmpty(header))
			{
				Match match = DiagnosticsItemFactory.msDiagnosticsPattern.Match(header);
				if (match.Success)
				{
					int num;
					if (int.TryParse(match.Groups["errorid"].Value, out num))
					{
						if (DiagnosticsItemCallRedirect.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallRedirect();
						}
						else if (DiagnosticsItemCallReceived.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallReceived();
						}
						else if (DiagnosticsItemLyncServer.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemLyncServer();
						}
						else if (DiagnosticsItemExchangeServer.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemExchangeServer();
						}
						else if (DiagnosticsItemTrace.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemTrace();
						}
						else if (DiagnosticsItemCallStart.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallStart();
						}
						else if (DiagnosticsItemCallEstablishing.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallEstablishing();
						}
						else if (DiagnosticsItemCallEstablished.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallEstablished();
						}
						else if (DiagnosticsItemCallEstablishFailed.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallEstablishFailed();
						}
						else if (DiagnosticsItemCallDisconnected.IsExpectedErrorId(num))
						{
							diagnosticsItemBase = new DiagnosticsItemCallDisconnected();
						}
						diagnosticsItemBase.ErrorId = num;
					}
					int count = match.Groups["key"].Captures.Count;
					for (int i = 0; i < count; i++)
					{
						diagnosticsItemBase.Add(match.Groups["key"].Captures[i].Value, match.Groups["value"].Captures[i].Value);
					}
				}
			}
			return diagnosticsItemBase;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000B80D8 File Offset: 0x000B62D8
		public static string FormatDiagnostics(int errorid, string reason, params string[] additional)
		{
			string value = string.Format("{0};source=\"{1}\";reason=\"{2}\";service=\"{3}/{4}\"", new object[]
			{
				errorid,
				DiagnosticsItemFactory.GetLocalHostFqdn(),
				reason,
				DiagnosticsItemFactory.GetAssemblyName(),
				DiagnosticsItemFactory.GetAssemblyVersion()
			});
			StringBuilder stringBuilder = new StringBuilder(value);
			if (additional != null)
			{
				foreach (string value2 in additional)
				{
					stringBuilder.Append(";");
					stringBuilder.Append(value2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000B815D File Offset: 0x000B635D
		private static string GetAssemblyVersion()
		{
			return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000B8173 File Offset: 0x000B6373
		private static string GetAssemblyName()
		{
			return Assembly.GetExecutingAssembly().GetName().Name;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000B8184 File Offset: 0x000B6384
		private static string GetLocalHostFqdn()
		{
			IPGlobalProperties ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			if (!string.IsNullOrEmpty(ipglobalProperties.DomainName))
			{
				return string.Format("{0}.{1}", ipglobalProperties.HostName, ipglobalProperties.DomainName);
			}
			return ipglobalProperties.HostName;
		}

		// Token: 0x040015CE RID: 5582
		private static Regex msDiagnosticsPattern = new Regex("(?<errorid>\\d+)(;(?<key>\\w+)=\"(?<value>[^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\")*");
	}
}
