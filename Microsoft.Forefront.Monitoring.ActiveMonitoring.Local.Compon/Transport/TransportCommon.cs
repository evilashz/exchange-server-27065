using System;
using System.Management;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport
{
	// Token: 0x02000264 RID: 612
	internal static class TransportCommon
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x0003C396 File Offset: 0x0003A596
		internal static bool IsServiceDisabledAndInactive(string serviceName, ServerComponentEnum serviceComponent)
		{
			return ServerComponentStateManager.GetEffectiveState(serviceComponent) == ServiceState.Inactive && TransportCommon.IsServiceDisabled(serviceName);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		internal static bool IsServiceDisabled(string serviceName)
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
	}
}
