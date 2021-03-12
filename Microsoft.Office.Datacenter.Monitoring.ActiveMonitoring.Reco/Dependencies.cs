using System;
using Microsoft.Practices.Unity;

namespace Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery
{
	// Token: 0x02000012 RID: 18
	public static class Dependencies
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000037C4 File Offset: 0x000019C4
		public static IUnityContainer Container
		{
			get
			{
				if (Dependencies.container == null)
				{
					lock (Dependencies.objectForLock)
					{
						if (Dependencies.container == null)
						{
							Dependencies.container = Dependencies.Initialize();
						}
					}
				}
				return Dependencies.container;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000381C File Offset: 0x00001A1C
		public static ILamRpc LamRpc
		{
			get
			{
				return Dependencies.Container.Resolve<ILamRpc>();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003828 File Offset: 0x00001A28
		public static IThrottleHelper ThrottleHelper
		{
			get
			{
				return Dependencies.Container.Resolve<IThrottleHelper>();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003834 File Offset: 0x00001A34
		public static void RegisterInterfaces(ILamRpc lamRpc, IThrottleHelper throttleHelper)
		{
			Dependencies.lamRpc = lamRpc;
			Dependencies.throttleHelper = throttleHelper;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003842 File Offset: 0x00001A42
		public static void RegisterAll()
		{
			if (Dependencies.container != null)
			{
				Dependencies.container.Dispose();
			}
			Dependencies.container = Dependencies.Initialize();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000385F File Offset: 0x00001A5F
		public static void UnregisterAll()
		{
			if (Dependencies.container != null)
			{
				Dependencies.container.Dispose();
				Dependencies.container = null;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003878 File Offset: 0x00001A78
		private static IUnityContainer Initialize()
		{
			return new UnityContainer().RegisterInstance<ILamRpc>(Dependencies.lamRpc, new ContainerControlledLifetimeManager()).RegisterInstance<IThrottleHelper>(Dependencies.throttleHelper, new ContainerControlledLifetimeManager());
		}

		// Token: 0x0400003A RID: 58
		private static ILamRpc lamRpc = null;

		// Token: 0x0400003B RID: 59
		private static IThrottleHelper throttleHelper = null;

		// Token: 0x0400003C RID: 60
		private static IUnityContainer container = null;

		// Token: 0x0400003D RID: 61
		private static object objectForLock = new object();
	}
}
