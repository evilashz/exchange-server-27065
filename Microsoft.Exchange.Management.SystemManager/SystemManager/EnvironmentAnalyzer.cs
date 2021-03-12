using System;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000106 RID: 262
	internal sealed class EnvironmentAnalyzer
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0002121C File Offset: 0x0001F41C
		public static string CheckEnvironment()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EnvironmentAnalyzer.EnvironmentAnalyzerDelegate environmentAnalyzerDelegate in EnvironmentAnalyzer.checkerList)
			{
				string value = environmentAnalyzerDelegate();
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.AppendLine(value);
					stringBuilder.AppendLine();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00021270 File Offset: 0x0001F470
		public static string GetInstallPath()
		{
			return ConfigurationContext.Setup.InstallPath;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00021277 File Offset: 0x0001F477
		public static string GetLocalServerName()
		{
			if (string.IsNullOrEmpty(EnvironmentAnalyzer.localServerName))
			{
				EnvironmentAnalyzer.localServerName = NativeHelpers.GetLocalComputerFqdn(false);
			}
			return EnvironmentAnalyzer.localServerName;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00021295 File Offset: 0x0001F495
		public static bool IsWorkGroup()
		{
			EnvironmentAnalyzer.CheckInstalledTopology();
			return EnvironmentAnalyzer.installedTopology == EnvironmentAnalyzer.Topology.WorkGroup;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x000212A4 File Offset: 0x0001F4A4
		private static void CheckInstalledTopology()
		{
			if (EnvironmentAnalyzer.installedTopology == EnvironmentAnalyzer.Topology.Unchecked)
			{
				string path = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MSExchange";
				if (WinformsHelper.IsValidRegKey(path))
				{
					EnvironmentAnalyzer.installedTopology = EnvironmentAnalyzer.Topology.WorkGroup;
					return;
				}
				EnvironmentAnalyzer.installedTopology = EnvironmentAnalyzer.Topology.ServerForest;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000212D4 File Offset: 0x0001F4D4
		private static bool IsExchangeInstalled()
		{
			string path = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\";
			return WinformsHelper.IsValidRegKey(path);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000212F0 File Offset: 0x0001F4F0
		private static string CheckExchangeInstalled()
		{
			string result = string.Empty;
			if (!EnvironmentAnalyzer.IsExchangeInstalled())
			{
				result = Strings.ExchangeIsNotInstalledCorrectly;
			}
			return result;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00021318 File Offset: 0x0001F518
		private static string CheckDataCenterCompatibility()
		{
			string result = string.Empty;
			if (Datacenter.IsMultiTenancyEnabled())
			{
				result = Strings.BlockEMCInDataCenter;
			}
			return result;
		}

		// Token: 0x04000416 RID: 1046
		private static EnvironmentAnalyzer.Topology installedTopology = EnvironmentAnalyzer.Topology.Unchecked;

		// Token: 0x04000417 RID: 1047
		private static EnvironmentAnalyzer.EnvironmentAnalyzerDelegate[] checkerList = new EnvironmentAnalyzer.EnvironmentAnalyzerDelegate[]
		{
			new EnvironmentAnalyzer.EnvironmentAnalyzerDelegate(EnvironmentAnalyzer.CheckExchangeInstalled),
			new EnvironmentAnalyzer.EnvironmentAnalyzerDelegate(EnvironmentAnalyzer.CheckDataCenterCompatibility)
		};

		// Token: 0x04000418 RID: 1048
		private static string localServerName;

		// Token: 0x02000107 RID: 263
		private enum Topology
		{
			// Token: 0x0400041A RID: 1050
			Unchecked,
			// Token: 0x0400041B RID: 1051
			WorkGroup,
			// Token: 0x0400041C RID: 1052
			ServerForest
		}

		// Token: 0x02000108 RID: 264
		// (Invoke) Token: 0x0600097A RID: 2426
		private delegate string EnvironmentAnalyzerDelegate();
	}
}
