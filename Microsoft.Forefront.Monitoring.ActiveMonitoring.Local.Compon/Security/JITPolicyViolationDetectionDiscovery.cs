using System;
using System.Management;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Security
{
	// Token: 0x02000209 RID: 521
	public sealed class JITPolicyViolationDetectionDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002AE34 File Offset: 0x00029034
		protected override void DoWork(CancellationToken cancellationToken)
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("InstalledRoleFeatureID list:");
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_ServerFeature WHERE ParentID = 0"))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						stringBuilder.Append(managementObject["ID"]);
						stringBuilder.Append("|");
						uint num = (uint)managementObject["ID"];
						if (num == 10U)
						{
							flag = true;
						}
					}
				}
			}
			MaintenanceResult result = base.Result;
			result.StateAttribute3 += stringBuilder.ToString();
			if (flag)
			{
				base.Result.StateAttribute1 = "JITPolicyViolationDetectionDiscovery: Domain Controller installed, start install probes.";
				GenericWorkItemHelper.CreateAllDefinitions(new string[]
				{
					"JITPolicyViolationDetection.xml"
				}, base.Broker, base.TraceContext, base.Result);
				return;
			}
			base.Result.StateAttribute1 = "JITPolicyViolationDetectionDiscovery: Domain Controller is not installed, ignore this server.";
		}

		// Token: 0x040007AA RID: 1962
		private const string WmiQueryString = "SELECT * FROM Win32_ServerFeature WHERE ParentID = 0";

		// Token: 0x040007AB RID: 1963
		private const uint DirectoryServiceFeatureId = 10U;
	}
}
