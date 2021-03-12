using System;
using System.Security.Principal;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.FullTextIndex;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000073 RID: 115
	public static class Globals
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001CB6E File Offset: 0x0001AD6E
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0001CB75 File Offset: 0x0001AD75
		public static uint MaxRPCThreadCount
		{
			get
			{
				return Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.maxRPCThreadCount;
			}
			set
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.maxRPCThreadCount = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001CB7D File Offset: 0x0001AD7D
		internal static ClientSecurityContext ProcessSecurityContext
		{
			get
			{
				return Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.systemSecurityContext;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public static void Initialize(StoreDatabase.InitInMemoryDatabaseSchemaHandlerDelegate initInMemoryDatabaseSchemaHandler, StoreDatabase.MountingHandlerDelegate mountingHandler, StoreDatabase.MountedHandlerDelegate mountedHandler, StoreDatabase.DismountingHandlerDelegate dismountingHandler)
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.systemSecurityContext = new ClientSecurityContext(current);
			StoreDatabase.InitInMemoryDatabaseSchemaHandler = initInMemoryDatabaseSchemaHandler;
			StoreDatabase.MountingHandler = mountingHandler;
			StoreDatabase.MountedHandler = mountedHandler;
			StoreDatabase.DismountingHandler = dismountingHandler;
			Context.Initialize();
			Storage.Initialize();
			DatabaseSchema.Initialize();
			StoreDatabase.Initialize();
			Mailbox.Initialize();
			AssistantActivityMonitor.Initialize();
			MaintenanceHandler.Initialize();
			MailboxTaskQueue.Initialize();
			MailboxStateCache.Initialize();
			PropertySchema.Initialize();
			SharedObjectPropertyBagDataCache.Initialize();
			MailboxState.Initialize();
			ReplidGuidMap.Initialize();
			GlobalNamedPropertyMap.Initialize();
			NamedPropertyMap.Initialize();
			ChangeNumberAndIdCounters.Initialize();
			TimedEventsQueue.Initialize();
			PerformanceCounterFactory.CreateClientTypeInstances(false);
			FullTextIndexLogger.Initialize();
			BufferedPerformanceCounter.Initialize();
			LazyMailboxActionList.Initialize();
			ClientActivityStrings.Initialize();
			RopSummaryCollector.Initialize();
			QueryPlanner.Initialize();
			Mailbox.TableSizeStatistics[] array = new Mailbox.TableSizeStatistics[2];
			Mailbox.TableSizeStatistics[] array2 = array;
			int num = 0;
			Mailbox.TableSizeStatistics tableSizeStatistics = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics.TableAccessor = ((Context context) => DatabaseSchema.ExtendedPropertyNameMappingTable(context.Database).Table);
			tableSizeStatistics.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array2[num] = tableSizeStatistics;
			Mailbox.TableSizeStatistics[] array3 = array;
			int num2 = 1;
			Mailbox.TableSizeStatistics tableSizeStatistics2 = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics2.TableAccessor = ((Context context) => DatabaseSchema.ReplidGuidMapTable(context.Database).Table);
			tableSizeStatistics2.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics2.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array3[num2] = tableSizeStatistics2;
			Mailbox.RegisterTableSizeStatistics(array);
			RopSummaryResolver.Add(OperationType.Rop, (byte operationId) => ((RopId)operationId).ToString());
			RopSummaryResolver.Add(OperationType.Task, (byte operationId) => ((TaskTypeId)operationId).ToString());
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001CD68 File Offset: 0x0001AF68
		public static void Terminate()
		{
			BufferedPerformanceCounter.Terminate();
			StoreDatabase.Terminate();
			Storage.Terminate();
			if (Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.systemSecurityContext != null)
			{
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.systemSecurityContext.Dispose();
				Microsoft.Exchange.Server.Storage.StoreCommonServices.Globals.systemSecurityContext = null;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001CD90 File Offset: 0x0001AF90
		public static void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			DatabaseSchema.Initialize(database);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001CD98 File Offset: 0x0001AF98
		public static void DatabaseMounting(Context context, StoreDatabase database, bool readOnly)
		{
			DatabaseSchema.PostMountInitialize(context, database);
			PerformanceCounterFactory.CreateDatabaseInstance(database);
			if (Microsoft.Exchange.Server.Storage.Common.Globals.IsMultiProcess)
			{
				PerformanceCounterFactory.DefaultDatabaseInstanceName = database.MdbName;
				TempStream.Configure(database.PhysicalDatabase.FilePath);
			}
			MailboxStateCache.MountHandler(database, context);
			AssistantActivityMonitor.MountHandler(context, database);
			MaintenanceHandler.MountHandler(context);
			PropertySchema.MountEventHandler(database);
			PropertySchemaPopulation.MountEventHandler(database);
			MailboxQuarantiner.MountEventHandler(database);
			TimedEventsQueue.MountEventHandler(context, database, readOnly);
			ResourceMonitorDigest.MountEventHandler(database);
			RopSummaryCollector.MountHandler(context);
			if (database.PhysicalDatabase.DatabaseType == DatabaseType.Sql)
			{
				BadPlanDetector.MountEventHandler(database);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001CE23 File Offset: 0x0001B023
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
			if (!database.IsReadOnly)
			{
				AssistantActivityMonitor.MountedHandler(context, database);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001CE34 File Offset: 0x0001B034
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
			PropertySchema.DismountEventHandler(database);
			MaintenanceHandler.DismountHandler(database);
			AssistantActivityMonitor.DismountHandler(database);
			MailboxStateCache.DismountHandler(context, database);
			PerformanceCounterFactory.CloseDatabaseInstance(database);
		}

		// Token: 0x040003AD RID: 941
		private static ClientSecurityContext systemSecurityContext;

		// Token: 0x040003AE RID: 942
		private static uint maxRPCThreadCount;
	}
}
