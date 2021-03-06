using System;
using System.Management;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000011 RID: 17
	internal static class WmiHelper
	{
		// Token: 0x0600006D RID: 109 RVA: 0x0000366C File Offset: 0x0000186C
		internal static DateTime GetSystemBootTime()
		{
			DateTime result = DateTime.MinValue;
			ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2");
			ManagementPath path = new ManagementPath("Win32_OperatingSystem");
			using (ManagementClass managementClass = new ManagementClass(scope, path, null))
			{
				using (ManagementObjectCollection instances = managementClass.GetInstances())
				{
					if (instances != null)
					{
						foreach (ManagementBaseObject managementBaseObject in instances)
						{
							ManagementObject managementObject = (ManagementObject)managementBaseObject;
							using (managementObject)
							{
								string text = (string)managementObject["LastBootupTime"];
								if (!string.IsNullOrEmpty(text))
								{
									result = ManagementDateTimeConverter.ToDateTime(text);
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003760 File Offset: 0x00001960
		internal static Exception HandleWmiExceptions(Action action)
		{
			Exception result = null;
			try
			{
				action();
			}
			catch (COMException ex)
			{
				result = ex;
			}
			catch (UnauthorizedAccessException ex2)
			{
				result = ex2;
			}
			catch (ManagementException ex3)
			{
				result = ex3;
			}
			catch (OutOfMemoryException ex4)
			{
				result = ex4;
			}
			return result;
		}
	}
}
