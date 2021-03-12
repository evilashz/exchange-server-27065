using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000062 RID: 98
	internal sealed class ScopeMappingEndpointManager
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x0001980F File Offset: 0x00017A0F
		private ScopeMappingEndpointManager()
		{
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00019818 File Offset: 0x00017A18
		internal static ScopeMappingEndpointManager Instance
		{
			get
			{
				if (ScopeMappingEndpointManager.instance == null)
				{
					lock (ScopeMappingEndpointManager.locker)
					{
						if (ScopeMappingEndpointManager.instance == null)
						{
							ScopeMappingEndpointManager.instance = new ScopeMappingEndpointManager();
						}
					}
				}
				return ScopeMappingEndpointManager.instance;
			}
		}

		// Token: 0x040003FD RID: 1021
		private static ScopeMappingEndpointManager instance = null;

		// Token: 0x040003FE RID: 1022
		private static object locker = new object();
	}
}
