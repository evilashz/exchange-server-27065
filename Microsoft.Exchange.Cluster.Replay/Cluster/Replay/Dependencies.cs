using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Exchange.Cluster.Common.Registry;
using Microsoft.Exchange.Cluster.Replay.Dumpster;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.HA.FailureItem;
using Microsoft.Exchange.Isam.Esebcli;
using Microsoft.Practices.Unity;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002CE RID: 718
	internal static class Dependencies
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00079298 File Offset: 0x00077498
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

		// Token: 0x06001BFD RID: 7165 RVA: 0x000792F0 File Offset: 0x000774F0
		public static void SetTestContainer(IUnityContainer c)
		{
			lock (Dependencies.objectForLock)
			{
				IUnityContainer unityContainer = Dependencies.container;
				Dependencies.container = c;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00079338 File Offset: 0x00077538
		public static IWatson Watson
		{
			get
			{
				return Dependencies.Container.Resolve<IWatson>();
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00079344 File Offset: 0x00077544
		internal static IAssert Assert
		{
			get
			{
				return Dependencies.Container.Resolve<IAssert>();
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00079350 File Offset: 0x00077550
		[Conditional("DEBUG")]
		public static void AssertDbg(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				Dependencies.Assert.Debug(condition, formatString, parameters);
			}
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00079362 File Offset: 0x00077562
		public static void AssertRtl(bool condition, string formatString, params object[] parameters)
		{
			if (!condition)
			{
				Dependencies.Assert.Retail(condition, formatString, parameters);
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x00079374 File Offset: 0x00077574
		public static IFailureItemPublisher FailureItemPublisher
		{
			get
			{
				return Dependencies.Container.Resolve<IFailureItemPublisher>();
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x00079380 File Offset: 0x00077580
		public static IRegistryKeyFactory RegistryKeyProvider
		{
			get
			{
				return Dependencies.Container.Resolve<IRegistryKeyFactory>();
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0007938C File Offset: 0x0007758C
		public static IADSessionFactory ADSessionFactory
		{
			get
			{
				return Dependencies.Container.Resolve<IADSessionFactory>();
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x00079398 File Offset: 0x00077598
		public static IReplayAdObjectLookup ReplayAdObjectLookup
		{
			get
			{
				return Dependencies.Container.Resolve<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.IgnoreInvalidAdSession.ToString());
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x000793AF File Offset: 0x000775AF
		public static IReplayAdObjectLookup ReplayAdObjectLookupPartiallyConsistent
		{
			get
			{
				return Dependencies.Container.Resolve<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.PartiallyConsistentAdSession.ToString());
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x000793C6 File Offset: 0x000775C6
		public static IReplayCoreManager ReplayCoreManager
		{
			get
			{
				return Dependencies.Container.Resolve<IReplayCoreManager>();
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000793D2 File Offset: 0x000775D2
		public static IRunConfigurationUpdater ConfigurationUpdater
		{
			get
			{
				return Dependencies.Container.Resolve<IRunConfigurationUpdater>();
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x000793DE File Offset: 0x000775DE
		public static IMonitoringADConfigProvider MonitoringADConfigProvider
		{
			get
			{
				return Dependencies.Container.Resolve<IMonitoringADConfigProvider>();
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x000793EA File Offset: 0x000775EA
		public static IADConfig ADConfig
		{
			get
			{
				return Dependencies.Container.Resolve<IADConfig>();
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000793F6 File Offset: 0x000775F6
		public static ICopyStatusClientLookup MonitoringCopyStatusClientLookup
		{
			get
			{
				return Dependencies.Container.Resolve<ICopyStatusClientLookup>();
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00079402 File Offset: 0x00077602
		public static ISafetyNetVersionCheck SafetyNetVersionCheck
		{
			get
			{
				return Dependencies.Container.Resolve<ISafetyNetVersionCheck>();
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0007940E File Offset: 0x0007760E
		public static IThreadPoolThreadCountManager ThreadPoolThreadCountManager
		{
			get
			{
				return Dependencies.sm_threadpoolManager;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00079415 File Offset: 0x00077615
		public static ITcpConnector TcpConnector
		{
			get
			{
				return Dependencies.Container.Resolve<ITcpConnector>();
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00079421 File Offset: 0x00077621
		public static IFindComponent ComponentFinder
		{
			get
			{
				return Dependencies.Container.Resolve<IFindComponent>();
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0007942D File Offset: 0x0007762D
		public static IManagementClassHelper ManagementClassHelper
		{
			get
			{
				return Dependencies.Container.Resolve<IManagementClassHelper>();
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00079439 File Offset: 0x00077639
		public static IAmRpcClientHelper AmRpcClientWrapper
		{
			get
			{
				return Dependencies.Container.Resolve<IAmRpcClientHelper>();
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00079445 File Offset: 0x00077645
		public static IReplayRpcClient ReplayRpcClientWrapper
		{
			get
			{
				return Dependencies.Container.Resolve<IReplayRpcClient>();
			}
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00079451 File Offset: 0x00077651
		public static IListMDBStatus GetStoreListMDBStatusInstance(string serverNameOrFqdn)
		{
			return Dependencies.GetStoreListMDBStatusInstance(serverNameOrFqdn, null);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0007945C File Offset: 0x0007765C
		public static IListMDBStatus GetStoreListMDBStatusInstance(string serverNameOrFqdn, string clientTypeId)
		{
			IStoreRpcFactory storeRpcFactory = Dependencies.Container.Resolve<IStoreRpcFactory>();
			return storeRpcFactory.ConstructListMDBStatus(serverNameOrFqdn, clientTypeId);
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x0007947C File Offset: 0x0007767C
		public static IStoreMountDismount GetStoreMountDismountInstance(string serverNameOrFqdn)
		{
			IStoreRpcFactory storeRpcFactory = Dependencies.Container.Resolve<IStoreRpcFactory>();
			return storeRpcFactory.ConstructMountDismount(serverNameOrFqdn);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x0007949B File Offset: 0x0007769B
		public static IStoreRpc GetNewStoreControllerInstance(string serverNameOrFqdn)
		{
			return Dependencies.GetNewStoreControllerInstance(serverNameOrFqdn, null);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000794A4 File Offset: 0x000776A4
		public static IStoreRpc GetNewStoreControllerInstance(string serverNameOrFqdn, string clientTypeId)
		{
			IStoreRpcFactory storeRpcFactory = Dependencies.Container.Resolve<IStoreRpcFactory>();
			return storeRpcFactory.Construct(serverNameOrFqdn, clientTypeId);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000794C4 File Offset: 0x000776C4
		public static IStoreRpc GetNewStoreControllerInstanceNoTimeout(string serverNameOrFqdn)
		{
			IStoreRpcFactory storeRpcFactory = Dependencies.Container.Resolve<IStoreRpcFactory>();
			return storeRpcFactory.ConstructWithNoTimeout(serverNameOrFqdn);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000794E3 File Offset: 0x000776E3
		public static void RegisterAll()
		{
			if (Dependencies.container != null)
			{
				Dependencies.container.Dispose();
			}
			Dependencies.container = Dependencies.Initialize();
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00079500 File Offset: 0x00077700
		public static void UnregisterAll()
		{
			if (Dependencies.container != null)
			{
				Dependencies.container.Dispose();
			}
			Dependencies.container = new UnityContainer();
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00079520 File Offset: 0x00077720
		private static IUnityContainer Initialize()
		{
			return new UnityContainer().RegisterType<IWatson, Watson>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterType<IFailureItemPublisher, FailureItemPublisherImpl>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterType<IADSessionFactory, ADSessionWrapperFactoryImpl>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterType<ITcpConnector, TcpConnector>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterType<IStoreRpcFactory, StoreRpcControllerFactory>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterType<IFindComponent, ComponentFinder>(new ContainerControlledLifetimeManager(), new InjectionMember[0]).RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.IgnoreInvalidAdSession.ToString(), new NoncachingReplayAdObjectLookup()).RegisterInstance<IReplayAdObjectLookup>(ReplayAdObjectLookupMapping.PartiallyConsistentAdSession.ToString(), new NoncachingReplayAdObjectLookupPartiallyConsistent()).RegisterInstance<IAssert>(Microsoft.Exchange.Cluster.Common.Extensions.Assert.Instance, new ContainerControlledLifetimeManager()).RegisterInstance<IRegistryKeyFactory>(RegistryKeyFactory.Instance, new ContainerControlledLifetimeManager()).RegisterInstance<IManagementClassHelper>(new ManagementClassHelper(), new ContainerControlledLifetimeManager()).RegisterInstance<IAmRpcClientHelper>(new AmRpcClientWrapper(), new ContainerControlledLifetimeManager()).RegisterInstance<IReplayRpcClient>(new ReplayRpcClientWrapper(), new ContainerControlledLifetimeManager()).RegisterType<IEsebcli, Esebcli>(new InjectionMember[0]);
		}

		// Token: 0x04000BB3 RID: 2995
		private static readonly IThreadPoolThreadCountManager sm_threadpoolManager = new ThreadPoolThreadCountManager();

		// Token: 0x04000BB4 RID: 2996
		private static IUnityContainer container = null;

		// Token: 0x04000BB5 RID: 2997
		private static object objectForLock = new object();
	}
}
