using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Practices.Unity;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x0200000B RID: 11
	internal static class SharedDependencies
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002F34 File Offset: 0x00001134
		public static IUnityContainer Container
		{
			get
			{
				if (SharedDependencies.container == null)
				{
					lock (SharedDependencies.objectForLock)
					{
						if (SharedDependencies.container == null)
						{
							SharedDependencies.container = SharedDependencies.Initialize();
						}
					}
				}
				return SharedDependencies.container;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F8C File Offset: 0x0000118C
		public static void SetTestContainer(IUnityContainer c)
		{
			lock (SharedDependencies.objectForLock)
			{
				IUnityContainer unityContainer = SharedDependencies.container;
				SharedDependencies.container = c;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002FD4 File Offset: 0x000011D4
		internal static IAssert Assert
		{
			get
			{
				return SharedDependencies.Container.Resolve<IAssert>();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FE0 File Offset: 0x000011E0
		[Conditional("DEBUG")]
		public static void AssertDbg(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				SharedDependencies.Assert.Debug(condition, formatString, parameters);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FF2 File Offset: 0x000011F2
		public static void AssertRtl(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				SharedDependencies.Assert.Retail(condition, formatString, parameters);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003004 File Offset: 0x00001204
		public static IDiagCoreImpl DiagCoreImpl
		{
			get
			{
				return SharedDependencies.Container.Resolve<IDiagCoreImpl>();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003010 File Offset: 0x00001210
		public static IRegistryKeyFactory RegistryKeyProvider
		{
			get
			{
				return SharedDependencies.Container.Resolve<IRegistryKeyFactory>();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000301C File Offset: 0x0000121C
		public static IManagementClassHelper ManagementClassHelper
		{
			get
			{
				return SharedDependencies.Container.Resolve<IManagementClassHelper>();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003028 File Offset: 0x00001228
		public static IAmServerNameLookup AmServerNameLookup
		{
			get
			{
				return SharedDependencies.Container.Resolve<IAmServerNameLookup>();
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003034 File Offset: 0x00001234
		public static IWritableAD WritableADHelper
		{
			get
			{
				return SharedDependencies.Container.Resolve<IWritableAD>();
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003040 File Offset: 0x00001240
		public static void RegisterAll()
		{
			if (SharedDependencies.container != null)
			{
				SharedDependencies.container.Dispose();
			}
			SharedDependencies.container = SharedDependencies.Initialize();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000305D File Offset: 0x0000125D
		public static void UnregisterAll()
		{
			if (SharedDependencies.container != null)
			{
				SharedDependencies.container.Dispose();
			}
			SharedDependencies.container = new UnityContainer();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000307C File Offset: 0x0000127C
		private static IUnityContainer Initialize()
		{
			return new UnityContainer().RegisterType<IDiagCoreImpl, ReplDiagCoreImpl>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterInstance<IAssert>(Microsoft.Exchange.Cluster.Common.Extensions.Assert.Instance, new ContainerControlledLifetimeManager()).RegisterInstance<IRegistryKeyFactory>(RegistryKeyFactory.Instance, new ContainerControlledLifetimeManager()).RegisterInstance<IManagementClassHelper>(new ManagementClassHelper(), new ContainerControlledLifetimeManager()).RegisterInstance<IWritableAD>(new WritableADHelper(), new ContainerControlledLifetimeManager()).RegisterType<IAmServerNameLookup, AmServerNameCache>(new ContainerControlledLifetimeManager(), new InjectionMember[0]);
		}

		// Token: 0x04000017 RID: 23
		private static IUnityContainer container = null;

		// Token: 0x04000018 RID: 24
		private static object objectForLock = new object();
	}
}
