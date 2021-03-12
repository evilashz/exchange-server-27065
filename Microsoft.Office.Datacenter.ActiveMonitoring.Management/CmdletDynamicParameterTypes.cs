using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management
{
	// Token: 0x02000003 RID: 3
	internal class CmdletDynamicParameterTypes
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002194 File Offset: 0x00000394
		internal static string[] CmdletTypeNames
		{
			get
			{
				return new string[]
				{
					"Microsoft.Office.Datacenter.ActiveMonitoring.Management.GetHealthReport",
					"Microsoft.Office.Datacenter.ActiveMonitoring.Management.GetServerHealth",
					"Microsoft.Office.Datacenter.ActiveMonitoring.Management.InvokeMonitoringProbe"
				};
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C4 File Offset: 0x000003C4
		internal static Type[] DynamicParameterTypes
		{
			get
			{
				return new Type[]
				{
					typeof(GetHealthReport),
					typeof(GetServerHealth),
					typeof(InvokeMonitoringProbe)
				};
			}
		}
	}
}
