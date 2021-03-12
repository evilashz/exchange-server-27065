using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.ProvisioningMonitoring
{
	// Token: 0x0200020A RID: 522
	internal static class ProvisioningMonitoringConfig
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00039184 File Offset: 0x00037384
		internal static bool IsCmdletMonitoringEnabled
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.CmdletMonitoring.Enabled;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000391A8 File Offset: 0x000373A8
		internal static string GetInstanceName(string hostName, string orgName)
		{
			string empty = string.Empty;
			ProvisioningMonitoringConfig.hostIds.TryGetValue(hostName, out empty);
			if (string.IsNullOrEmpty(orgName))
			{
				orgName = "First Organization";
			}
			return string.Format("v1-{0}-{1}-{2}", empty, ProvisioningMonitoringConfig.processId, orgName);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000391EC File Offset: 0x000373EC
		internal static bool IsExceptionWhiteListedForCmdlet(ErrorRecord errorRecord, string cmdletName)
		{
			List<CmdletErrorContext> errorContexts;
			return errorRecord.CategoryInfo.Category == (ErrorCategory)1000 || errorRecord.CategoryInfo.Category == (ErrorCategory)1003 || errorRecord.CategoryInfo.Category == (ErrorCategory)1004 || errorRecord.CategoryInfo.Category == (ErrorCategory)1005 || errorRecord.CategoryInfo.Category == (ErrorCategory)1006 || errorRecord.CategoryInfo.Category == (ErrorCategory)1007 || errorRecord.CategoryInfo.Category == (ErrorCategory)1008 || ProvisioningMonitoringConfig.MatchesErrorContext(ProvisioningMonitoringConfig.commonErrorWhiteList, errorRecord) || (ProvisioningMonitoringConfig.monitoringConfigList.TryGetValue(cmdletName, out errorContexts) && ProvisioningMonitoringConfig.MatchesErrorContext(errorContexts, errorRecord));
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000392A3 File Offset: 0x000374A3
		internal static bool IsCmdletMonitored(string cmdletName)
		{
			return ProvisioningMonitoringConfig.monitoringConfigList.ContainsKey(cmdletName);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000392B0 File Offset: 0x000374B0
		internal static bool IsHostMonitored(string hostName)
		{
			return ProvisioningMonitoringConfig.hostIds.ContainsKey(hostName);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000392BD File Offset: 0x000374BD
		internal static bool IsClientApplicationMonitored(ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication)
		{
			return !ProvisioningMonitoringConfig.excludedClientApplications.Contains(clientApplication);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000392D0 File Offset: 0x000374D0
		internal static void AddCmdletToMonitoredList(string cmdletName)
		{
			List<CmdletErrorContext> value = new List<CmdletErrorContext>();
			ProvisioningMonitoringConfig.monitoringConfigList.Add(cmdletName, value);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000392F0 File Offset: 0x000374F0
		internal static void AddToCmdletWhiteList(string cmdletName, CmdletErrorContext errorContext)
		{
			List<CmdletErrorContext> list;
			if (!ProvisioningMonitoringConfig.monitoringConfigList.TryGetValue(cmdletName, out list))
			{
				list = new List<CmdletErrorContext>();
				ProvisioningMonitoringConfig.monitoringConfigList.Add(cmdletName, list);
			}
			list.Add(errorContext);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00039328 File Offset: 0x00037528
		internal static void AddToCmdletWhiteList(string cmdletName, List<CmdletErrorContext> errorContextList)
		{
			List<CmdletErrorContext> list;
			if (!ProvisioningMonitoringConfig.monitoringConfigList.TryGetValue(cmdletName, out list))
			{
				list = new List<CmdletErrorContext>();
				ProvisioningMonitoringConfig.monitoringConfigList.Add(cmdletName, list);
			}
			list.AddRange(errorContextList);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003935D File Offset: 0x0003755D
		internal static void AddToCommonWhiteList(CmdletErrorContext errorContext)
		{
			ProvisioningMonitoringConfig.commonErrorWhiteList.Add(errorContext);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003936A File Offset: 0x0003756A
		internal static void AddToCommonWhiteList(List<CmdletErrorContext> errorContextList)
		{
			ProvisioningMonitoringConfig.commonErrorWhiteList.AddRange(errorContextList);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00039378 File Offset: 0x00037578
		internal static bool TryGetPidFromInstanceName(string instanceName, ref int pid)
		{
			string[] array = instanceName.Split(new char[]
			{
				'-'
			});
			return array.Length > 3 && array[0] == "v1" && int.TryParse(array[2], out pid);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000393BC File Offset: 0x000375BC
		internal static bool TryGetOrganizationNameFromInstanceName(string instanceName, ref string organizationName)
		{
			string pattern = "v\\d*-\\S*-\\d*-(\\S*)";
			Match match = Regex.Match(instanceName, pattern);
			if (match.Success)
			{
				organizationName = match.Groups[1].Value;
				return true;
			}
			return false;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000393F8 File Offset: 0x000375F8
		private static bool MatchesErrorContext(List<CmdletErrorContext> errorContexts, ErrorRecord errorRecord)
		{
			foreach (CmdletErrorContext cmdletErrorContext in errorContexts)
			{
				if (cmdletErrorContext.MatchesErrorContext(errorRecord.Exception, string.Empty))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400045B RID: 1115
		internal const string InstanceNameFormat = "v1-{0}-{1}-{2}";

		// Token: 0x0400045C RID: 1116
		internal static CmdletHealthCounters NullCmdletHealthCounters = new CmdletHealthCounters();

		// Token: 0x0400045D RID: 1117
		private static Dictionary<string, List<CmdletErrorContext>> monitoringConfigList = new Dictionary<string, List<CmdletErrorContext>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400045E RID: 1118
		private static List<CmdletErrorContext> commonErrorWhiteList = new List<CmdletErrorContext>();

		// Token: 0x0400045F RID: 1119
		private static Dictionary<string, string> hostIds = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"ConsoleHost",
				"CH"
			},
			{
				"ServerRemoteHost",
				"RH"
			},
			{
				"Exchange Management Console",
				"EMC"
			},
			{
				"SimpleDataMigration",
				"ECPBulk"
			}
		};

		// Token: 0x04000460 RID: 1120
		private static List<ExchangeRunspaceConfigurationSettings.ExchangeApplication> excludedClientApplications = new List<ExchangeRunspaceConfigurationSettings.ExchangeApplication>
		{
			ExchangeRunspaceConfigurationSettings.ExchangeApplication.ForwardSync
		};

		// Token: 0x04000461 RID: 1121
		private static string processId = Process.GetCurrentProcess().Id.ToString();
	}
}
