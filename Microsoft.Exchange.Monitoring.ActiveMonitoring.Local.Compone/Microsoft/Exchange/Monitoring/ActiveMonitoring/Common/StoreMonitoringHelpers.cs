using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200010A RID: 266
	internal static class StoreMonitoringHelpers
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x0002FED8 File Offset: 0x0002E0D8
		internal static string GetStoreUsageStatisticsFilePath()
		{
			string path = string.Format("StoreUsageStatisticsData-{0}-{1}.csv", DateTime.UtcNow.ToString("yy-MM-dd-HHmmssfff"), Environment.MachineName.ToLower());
			string text = Path.Combine(ExchangeSetupContext.InstallPath, "Diagnostics", "StoreUsageStatistics");
			string result = Path.Combine(text, path);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			return result;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002FF3C File Offset: 0x0002E13C
		internal static CorrelatedMonitorInfo GetStoreCorrelation(string databaseName)
		{
			return StoreMonitoringHelpers.GetStoreCorrelation(databaseName, new string[]
			{
				typeof(MapiExceptionMdbOffline).FullName,
				typeof(MapiExceptionNetworkError).FullName,
				typeof(UnableToFindServerForDatabaseException).FullName
			});
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002FF8D File Offset: 0x0002E18D
		internal static CorrelatedMonitorInfo GetStoreCorrelation(string databaseName, string[] exceptionTypes)
		{
			return new CorrelatedMonitorInfo(string.Format("{0}\\{1}\\{2}", ExchangeComponent.Store.Name, "ActiveDatabaseAvailabilityMonitor", (!string.IsNullOrWhiteSpace(databaseName)) ? databaseName : "*"), StoreMonitoringHelpers.GetRegExMatchFromException(exceptionTypes), CorrelatedMonitorInfo.MatchMode.RegEx);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		internal static ProbeDefinition GetProbeDefinition(string serverName, string probeName, string targetResource, string serviceName)
		{
			List<ProbeDefinition> definitionsFromCrimson = StoreMonitoringHelpers.GetDefinitionsFromCrimson<ProbeDefinition>(serverName);
			foreach (ProbeDefinition probeDefinition in definitionsFromCrimson)
			{
				if (!string.IsNullOrWhiteSpace(probeDefinition.ServiceName) && probeDefinition.ServiceName.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrWhiteSpace(probeDefinition.Name) && probeDefinition.Name.Equals(probeName, StringComparison.InvariantCultureIgnoreCase) && probeDefinition.TargetResource.Equals(targetResource, StringComparison.InvariantCultureIgnoreCase))
				{
					return probeDefinition;
				}
			}
			return null;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00030060 File Offset: 0x0002E260
		internal static string PopulateEscalationMessage(string escalationMessage, ProbeResult probeResult)
		{
			ResponseMessageReader responseMessageReader = new ResponseMessageReader();
			responseMessageReader.AddObject<ProbeResult>("Probe", probeResult);
			return responseMessageReader.ReplaceValues(escalationMessage);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00030088 File Offset: 0x0002E288
		private static List<TDefinition> GetDefinitionsFromCrimson<TDefinition>(string serverName) where TDefinition : WorkDefinition, IPersistence, new()
		{
			List<TDefinition> list = new List<TDefinition>();
			using (CrimsonReader<TDefinition> crimsonReader = new CrimsonReader<TDefinition>())
			{
				crimsonReader.ConnectionInfo = new CrimsonConnectionInfo(serverName);
				foreach (TDefinition item in crimsonReader.ReadAll())
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00030108 File Offset: 0x0002E308
		internal static string GetRegExMatchFromException(string[] exceptionTypes)
		{
			if (exceptionTypes != null && exceptionTypes.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < exceptionTypes.Length; i++)
				{
					string text = Regex.Escape(exceptionTypes[i]);
					if (i == 0)
					{
						stringBuilder.Append(text);
					}
					else
					{
						stringBuilder.AppendFormat("|{0}", text);
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00030164 File Offset: 0x0002E364
		internal static Process[] GetStoreWorkerProcess(string databaseGuid)
		{
			if (!string.IsNullOrWhiteSpace(databaseGuid))
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT CommandLine, ProcessId FROM Win32_Process WHERE Name = 'Microsoft.Exchange.Store.Worker.exe'"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (managementObject["CommandLine"].ToString().IndexOf(databaseGuid, 0, StringComparison.OrdinalIgnoreCase) != -1)
						{
							int num = Convert.ToInt32(managementObject["ProcessId"]);
							if (num != 0)
							{
								return new Process[]
								{
									Process.GetProcessById(num)
								};
							}
						}
					}
				}
			}
			return new Process[0];
		}

		// Token: 0x04000581 RID: 1409
		internal const string StoreWorker = "Microsoft.Exchange.Store.Worker.exe";
	}
}
