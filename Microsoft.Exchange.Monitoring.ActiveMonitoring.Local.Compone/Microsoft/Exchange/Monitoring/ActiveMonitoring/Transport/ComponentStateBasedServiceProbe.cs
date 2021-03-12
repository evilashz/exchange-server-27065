using System;
using System.Management;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Transport
{
	// Token: 0x020004ED RID: 1261
	public class ComponentStateBasedServiceProbe : GenericServiceProbe
	{
		// Token: 0x06001F35 RID: 7989 RVA: 0x000BEF7C File Offset: 0x000BD17C
		protected override bool ShouldRun()
		{
			if (!base.ShouldRun())
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.ServiceTracer, base.TraceContext, "Skipping probe execution as base.ShouldRun returned false.", null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Transport\\Probes\\ComponentStateBasedServiceProbe.cs", 41);
				return false;
			}
			if (!base.Definition.Attributes.ContainsKey("ServerComponentName"))
			{
				throw new ArgumentException(string.Format("{0} attribute is missing from probe definition.", "ServerComponentName"));
			}
			string text = base.Definition.Attributes["ServerComponentName"];
			if (!ServerComponentStateManager.IsValidComponent(text))
			{
				throw new ArgumentException(string.Format("{0} is not a valid value for {1} attribute.", text, "ServerComponentName"));
			}
			ServerComponentEnum serverComponentEnum = (ServerComponentEnum)Enum.Parse(typeof(ServerComponentEnum), text);
			string windowsServiceName = base.GetWindowsServiceName();
			bool flag = ComponentStateBasedServiceProbe.IsServiceDisabled(windowsServiceName);
			if (ServerComponentStateManager.GetEffectiveState(serverComponentEnum) == ServiceState.Inactive && flag)
			{
				WTFDiagnostics.TraceDebug<string, ServerComponentEnum>(ExTraceGlobals.ServiceTracer, base.TraceContext, "Skipping probe execution as service ({0}) is disabled and component state ({1}) is marked as inactive.", windowsServiceName, serverComponentEnum, null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Transport\\Probes\\ComponentStateBasedServiceProbe.cs", 66);
				base.Result.StateAttribute1 = string.Format("{0} Inactive and Disabled", text);
				return false;
			}
			return true;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x000BF08C File Offset: 0x000BD28C
		private static bool IsServiceDisabled(string serviceName)
		{
			ObjectQuery query = new ObjectQuery(string.Format("SELECT StartMode FROM Win32_Service WHERE Name='{0}'", serviceName));
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					using (managementObject)
					{
						return managementObject["StartMode"].ToString() == "Disabled";
					}
				}
				throw new ArgumentException(string.Format("No service (Name={0}) found.", serviceName));
			}
			bool result;
			return result;
		}

		// Token: 0x040016D3 RID: 5843
		internal const string ServerComponentName = "ServerComponentName";
	}
}
