using System;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000004 RID: 4
	public class CmdletConfiguration
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002208 File Offset: 0x00000408
		internal static CmdletConfigurationEntry[] CmdletConfigurationEntries
		{
			get
			{
				return CmdletConfiguration.cmdletConfigurationEntries;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000220F File Offset: 0x0000040F
		internal static FormatConfigurationEntry[] FormatConfigurationEntries
		{
			get
			{
				return CmdletConfiguration.formatConfigurationEntries;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002218 File Offset: 0x00000418
		public static void PopulateISSCmdletConfigurationEntries()
		{
			for (int i = 0; i < CmdletDynamicParameterTypes.CmdletTypeNames.Length; i++)
			{
				InitialSessionStateBuilder.AddDynamicParameter(CmdletDynamicParameterTypes.CmdletTypeNames[i], CmdletDynamicParameterTypes.DynamicParameterTypes[i]);
			}
		}

		// Token: 0x04000004 RID: 4
		private static CmdletConfigurationEntry[] cmdletConfigurationEntries = new CmdletConfigurationEntry[]
		{
			new CmdletConfigurationEntry("Get-HealthReport", typeof(GetHealthReport), "Microsoft.Office.Datacenter.ActiveMonitoring.Management.dll-Help.xml"),
			new CmdletConfigurationEntry("Get-ServerHealth", typeof(GetServerHealth), "Microsoft.Office.Datacenter.ActiveMonitoring.Management.dll-Help.xml"),
			new CmdletConfigurationEntry("Invoke-MonitoringProbe", typeof(InvokeMonitoringProbe), "Microsoft.Office.Datacenter.ActiveMonitoring.Management.dll-Help.xml")
		};

		// Token: 0x04000005 RID: 5
		private static FormatConfigurationEntry[] formatConfigurationEntries = new FormatConfigurationEntry[]
		{
			new FormatConfigurationEntry("Microsoft.Office.Datacenter.ActiveMonitoring.Management.ps1xml")
		};
	}
}
