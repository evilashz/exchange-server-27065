using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Diagnostics;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.MapiDisp;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreIntegrityCheck;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.StartupShutdown
{
	// Token: 0x0200006E RID: 110
	public static class Globals
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00010C83 File Offset: 0x0000EE83
		public static Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer CurrentActiveLayer
		{
			get
			{
				return Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00010E1C File Offset: 0x0000F01C
		public static int InitializePhaseOne(Guid? instanceId, Guid? dagOrServerGuid)
		{
			Func<StorePerDatabasePerformanceCountersInstance> instanceAccessor = () => PerformanceCounterFactory.GetDatabaseInstance(null);
			ICachePerformanceCounters mailboxCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.MailboxInfoSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfMailboxInfoLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfMailboxInfoMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfMailboxInfoHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfMailboxInfoInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfMailboxInfoDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfMailboxInfoExpirationQueue);
			ICachePerformanceCounters foreignMailboxCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.ForeignMailboxInfoSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignMailboxInfoLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignMailboxInfoMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignMailboxInfoHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignMailboxInfoInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignMailboxInfoDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfForeignMailboxInfoExpirationQueue);
			ICachePerformanceCounters addressCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.AddressInfoSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfAddressInfoLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfAddressInfoMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfAddressInfoHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfAddressInfoInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfAddressInfoDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfAddressInfoExpirationQueue);
			ICachePerformanceCounters foreignAddressCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.ForeignAddressInfoSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignAddressInfoLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignAddressInfoMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignAddressInfoHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignAddressInfoInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfForeignAddressInfoDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfForeignAddressInfoExpirationQueue);
			ICachePerformanceCounters databaseCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.DatabaseInfoSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDatabaseInfoLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDatabaseInfoMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDatabaseInfoHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDatabaseInfoInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDatabaseInfoDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfDatabaseInfoExpirationQueue);
			ICachePerformanceCounters orgContainerCacheCounters = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.OrgContainerSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfOrgContainerLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfOrgContainerMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfOrgContainerHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfOrgContainerInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfOrgContainerDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfOrgContainerExpirationQueue);
			ICachePerformanceCounters distributionListMembershipCacheCountes = new CachePerformanceCounters<StorePerDatabasePerformanceCountersInstance>(instanceAccessor, (StorePerDatabasePerformanceCountersInstance instance) => instance.DistributionListMembershipSize, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDistributionListMembershipLookups, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDistributionListMembershipMisses, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDistributionListMembershipHits, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDistributionListMembershipInserts, (StorePerDatabasePerformanceCountersInstance instance) => instance.RateOfDistributionListMembershipDeletes, (StorePerDatabasePerformanceCountersInstance instance) => instance.SizeOfDistributionListMembershipExpirationQueue);
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESHA;
			DatabaseType databaseTypeToUse = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.GetDatabaseTypeToUse();
			return Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOne(mailboxCacheCounters, foreignMailboxCacheCounters, addressCacheCounters, foreignAddressCacheCounters, databaseCacheCounters, orgContainerCacheCounters, distributionListMembershipCacheCountes, activeLayer, databaseTypeToUse, instanceId, dagOrServerGuid);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001142C File Offset: 0x0000F62C
		public static int InitializePhaseOne(ICachePerformanceCounters mailboxCacheCounters, ICachePerformanceCounters foreignMailboxCacheCounters, ICachePerformanceCounters addressCacheCounters, ICachePerformanceCounters foreignAddressCacheCounters, ICachePerformanceCounters databaseCacheCounters, ICachePerformanceCounters orgContainerCacheCounters, ICachePerformanceCounters distributionListMembershipCacheCountes, Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer, DatabaseType databaseTypeToUse, Guid? instanceId, Guid? dagOrServerGuid)
		{
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.databaseType = databaseTypeToUse;
			Microsoft.Exchange.Server.Storage.Common.Globals.Initialize(instanceId, dagOrServerGuid);
			LockManager.SetExternalConditionValidator(new Func<LockManager.LockType, TimeSpan, bool>(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ValidateLockAcquisitionTimeout));
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDBMS)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOnePhysicalAccess(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.databaseType);
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFTI)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOneFullTextIndex();
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.Initialize(new StoreDatabase.InitInMemoryDatabaseSchemaHandlerDelegate(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitInMemoryDatabaseSchemaHandler), new StoreDatabase.MountingHandlerDelegate(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseMountingHandler), new StoreDatabase.MountedHandlerDelegate(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseMountedHandler), new StoreDatabase.DismountingHandlerDelegate(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseDismountingHandler));
				PerformanceCounterFactory.CreateClientTypeInstances(true);
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDS)
			{
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Initialize(mailboxCacheCounters, foreignMailboxCacheCounters, addressCacheCounters, foreignAddressCacheCounters, databaseCacheCounters, orgContainerCacheCounters, distributionListMembershipCacheCountes);
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDS;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI)
			{
				Microsoft.Exchange.Protocols.MAPI.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX)
			{
				Microsoft.Exchange.Protocols.FastTransfer.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG)
			{
				Microsoft.Exchange.Server.Storage.StoreIntegrityCheck.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD;
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOneDiagnostics();
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDS)
			{
				using (Context context = Context.CreateForSystem())
				{
					ErrorCode errorCode = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.PrimeDirectoryCaches(context);
					if (errorCode != ErrorCode.NoError)
					{
						string text = Convert.ToBase64String(DiagnosticContext.PackInfo());
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_StartupFailureDueToADError, new object[]
						{
							errorCode,
							text
						});
						return (int)errorCode.Propagate((LID)29872U);
					}
				}
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOneMapiDisp();
			}
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				Microsoft.Exchange.Server.Storage.AdminInterface.Globals.Initialize();
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI;
			}
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseOneSetActiveLayer(activeLayer);
			return 0;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000115F0 File Offset: 0x0000F7F0
		public static int InitializePhaseTwo(bool startPeriodicTasks, Guid? instanceId)
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				AdminRpcInterface.StartAcceptingCalls();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD && startPeriodicTasks)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.StartAllTasks();
			}
			int result = 0;
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESHA)
			{
				result = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitializePhaseTwoHA(instanceId);
			}
			return result;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001162F File Offset: 0x0000F82F
		public static int TerminatePhaseOne()
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseOneMapiDisp();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				AdminRpcInterface.StopAcceptingCalls();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDBMS)
			{
				Storage.DismountAllDatabases();
			}
			return 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001165C File Offset: 0x0000F85C
		public static int TerminatePhaseTwo()
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESHA)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseTwoHA();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDBMS && Storage.TaskList != null)
			{
				Storage.TaskList.StopAndShutdown();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				Microsoft.Exchange.Server.Storage.AdminInterface.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseTwoDiagnostics();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG)
			{
				Microsoft.Exchange.Server.Storage.StoreIntegrityCheck.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX)
			{
				Microsoft.Exchange.Protocols.FastTransfer.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI)
			{
				Microsoft.Exchange.Protocols.MAPI.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDS)
			{
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				PerformanceCounterFactory.CloseClientTypeInstances();
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.Terminate();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFTI)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.TerminatePhaseTwoFullTextIndex();
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDBMS)
			{
				Microsoft.Exchange.Server.Storage.PhysicalAccess.Globals.Terminate();
			}
			Microsoft.Exchange.Server.Storage.Common.Globals.Terminate();
			return 0;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001173C File Offset: 0x0000F93C
		public static DatabaseType GetDatabaseTypeToUse()
		{
			IRegistryReader instance = RegistryReader.Instance;
			DatabaseType value = (DatabaseType)instance.GetValue<int>(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "DatabaseType", 0);
			if (value == DatabaseType.Sql)
			{
				return DatabaseType.Sql;
			}
			return DatabaseType.Jet;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00011770 File Offset: 0x0000F970
		private static void InitInMemoryDatabaseSchemaHandler(Context context, StoreDatabase database)
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.InitInMemoryDatabaseSchema(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.InitInMemoryDatabaseSchema(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.InitInMemoryDatabaseSchema(context, database);
			}
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer2 = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer3 = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer4 = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.InitInMemoryDatabaseSchemaDiagnostics(context, database);
			}
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer5 = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer;
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				SchemaUpgradeService.InitInMemoryDatabaseSchema(context, database);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000117F4 File Offset: 0x0000F9F4
		private static void DatabaseMountingHandler(Context context, StoreDatabase database, bool readOnly)
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.DatabaseMounting(context, database, readOnly);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.DatabaseMounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.DatabaseMounting(context, database, readOnly);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI)
			{
				Microsoft.Exchange.Protocols.MAPI.Globals.DatabaseMounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX)
			{
				Microsoft.Exchange.Protocols.FastTransfer.Globals.DatabaseMounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG)
			{
				Microsoft.Exchange.Server.Storage.StoreIntegrityCheck.Globals.DatabaseMounting(context, database, readOnly);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.DatabaseMounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseMountingHandlerDiagnostics(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				Microsoft.Exchange.Server.Storage.AdminInterface.Globals.DatabaseMounting(context, database);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00011890 File Offset: 0x0000FA90
		private static void DatabaseMountedHandler(Context context, StoreDatabase database)
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI)
			{
				Microsoft.Exchange.Protocols.MAPI.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX)
			{
				Microsoft.Exchange.Protocols.FastTransfer.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG)
			{
				Microsoft.Exchange.Server.Storage.StoreIntegrityCheck.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.DatabaseMounted(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseMountedHandlerDiagnostics(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				Microsoft.Exchange.Server.Storage.AdminInterface.Globals.DatabaseMounted(context, database);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00011928 File Offset: 0x0000FB28
		private static void DatabaseDismountingHandler(Context context, StoreDatabase database)
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESAI)
			{
				Microsoft.Exchange.Server.Storage.AdminInterface.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.DatabaseDismountingHandlerDiagnostics(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD)
			{
				Microsoft.Exchange.Server.Storage.MapiDisp.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ISINTEG)
			{
				Microsoft.Exchange.Server.Storage.StoreIntegrityCheck.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFX)
			{
				Microsoft.Exchange.Protocols.FastTransfer.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMAPI)
			{
				Microsoft.Exchange.Protocols.MAPI.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSTOR)
			{
				Microsoft.Exchange.Server.Storage.LogicalDataModel.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESLI)
			{
				Microsoft.Exchange.Server.Storage.LazyIndexing.Globals.DatabaseDismounting(context, database);
			}
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESSC)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.DatabaseDismounting(context, database);
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000119C0 File Offset: 0x0000FBC0
		private static bool ValidateLockAcquisitionTimeout(LockManager.LockType lockType, TimeSpan timeout)
		{
			return LockManager.LockLevelFromLockType(lockType) != LockManager.LockLevel.Mailbox || !(timeout == LockManager.InfiniteTimeout) || !(Thread.CurrentThread.Name == "RPC Server Thread");
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000119F4 File Offset: 0x0000FBF4
		private static void InitializePhaseOnePhysicalAccess(DatabaseType databaseTypeToUse)
		{
			Factory.JetHADatabaseCreator creator = JetHADatabase.GetCreator();
			Microsoft.Exchange.Server.Storage.PhysicalAccess.Globals.Initialize(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.databaseType, creator);
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDBMS;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00011A18 File Offset: 0x0000FC18
		private static void InitializePhaseOneFullTextIndex()
		{
			Microsoft.Exchange.Server.Storage.FullTextIndex.Globals.Initialize();
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESFTI;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00011A25 File Offset: 0x0000FC25
		private static void InitializePhaseOneDiagnostics()
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.Initialize();
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESDG;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00011A33 File Offset: 0x0000FC33
		private static void InitializePhaseOneMapiDisp()
		{
			MapiRpc.Initialize(new MapiRpc());
			MapiRpc.Instance.Initialize();
			PoolRpcServer.Initialize();
			Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESMD;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00011A56 File Offset: 0x0000FC56
		private static void InitializePhaseOneSetActiveLayer(Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer)
		{
			if (activeLayer >= Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESHA)
			{
				Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.activeLayer = Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer.ESHA;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00011A64 File Offset: 0x0000FC64
		private static int InitializePhaseTwoHA(Guid? instanceId)
		{
			int result = 0;
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.databaseType == DatabaseType.Jet)
			{
				result = JetBackupRestore.RegisterEseback(instanceId);
			}
			return result;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00011A82 File Offset: 0x0000FC82
		private static void TerminatePhaseOneMapiDisp()
		{
			PoolRpcServer.StopAcceptingCalls();
			Microsoft.Exchange.Server.Storage.MapiDisp.Globals.SignalStopToAllTasks();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00011A8E File Offset: 0x0000FC8E
		private static void TerminatePhaseTwoDiagnostics()
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.Terminate();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00011A95 File Offset: 0x0000FC95
		private static void TerminatePhaseTwoHA()
		{
			if (Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.databaseType == DatabaseType.Jet)
			{
				JetBackupRestore.UnregisterEseback();
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		private static void TerminatePhaseTwoFullTextIndex()
		{
			Microsoft.Exchange.Server.Storage.FullTextIndex.Globals.Terminate();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00011AAB File Offset: 0x0000FCAB
		private static void InitInMemoryDatabaseSchemaDiagnostics(Context context, StoreDatabase database)
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.InitInMemoryDatabaseSchema(context, database);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		private static void DatabaseMountingHandlerDiagnostics(Context context, StoreDatabase database)
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.DatabaseMounting(context, database);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00011ABD File Offset: 0x0000FCBD
		private static void DatabaseMountedHandlerDiagnostics(Context context, StoreDatabase database)
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.DatabaseMounted(context, database);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00011AC6 File Offset: 0x0000FCC6
		private static void DatabaseDismountingHandlerDiagnostics(Context context, StoreDatabase database)
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.DatabaseDismounting(context, database);
		}

		// Token: 0x04000230 RID: 560
		private static Microsoft.Exchange.Server.Storage.StartupShutdown.Globals.ActiveLayer activeLayer;

		// Token: 0x04000231 RID: 561
		private static DatabaseType databaseType;

		// Token: 0x0200006F RID: 111
		public enum ActiveLayer
		{
			// Token: 0x04000265 RID: 613
			NONE,
			// Token: 0x04000266 RID: 614
			ESDBMS,
			// Token: 0x04000267 RID: 615
			ESFTI,
			// Token: 0x04000268 RID: 616
			ESSC,
			// Token: 0x04000269 RID: 617
			ESLI,
			// Token: 0x0400026A RID: 618
			ESSTOR,
			// Token: 0x0400026B RID: 619
			ESDS,
			// Token: 0x0400026C RID: 620
			ESMAPI,
			// Token: 0x0400026D RID: 621
			ESFX,
			// Token: 0x0400026E RID: 622
			ISINTEG,
			// Token: 0x0400026F RID: 623
			ESMD,
			// Token: 0x04000270 RID: 624
			ESDG,
			// Token: 0x04000271 RID: 625
			ESAI,
			// Token: 0x04000272 RID: 626
			ESHA
		}
	}
}
