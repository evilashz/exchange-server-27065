using System;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LazyIndexing
{
	// Token: 0x02000012 RID: 18
	public static class Globals
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004F84 File Offset: 0x00003184
		public static void Initialize()
		{
			DatabaseSchema.Initialize();
			PhysicalIndexCache.Initialize();
			LogicalIndexVersionHistory.Initialize();
			LogicalIndexCache.Initialize();
			LogicalIndex.Initialize();
			Mailbox.TableSizeStatistics[] array = new Mailbox.TableSizeStatistics[1];
			Mailbox.TableSizeStatistics[] array2 = array;
			int num = 0;
			Mailbox.TableSizeStatistics tableSizeStatistics = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics.TableAccessor = ((Context context) => DatabaseSchema.PseudoIndexMaintenanceTable(context.Database).Table);
			tableSizeStatistics.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array2[num] = tableSizeStatistics;
			Mailbox.RegisterTableSizeStatistics(array);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005008 File Offset: 0x00003208
		public static void Terminate()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000500A File Offset: 0x0000320A
		public static void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			DatabaseSchema.Initialize(database);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005012 File Offset: 0x00003212
		public static void DatabaseMounting(Context context, StoreDatabase database)
		{
			DatabaseSchema.PostMountInitialize(context, database);
			LogicalIndexVersionHistory.MountEventHandler(database);
			PhysicalIndexCache.MountEventHandler(context);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005027 File Offset: 0x00003227
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
			if (!database.IsReadOnly)
			{
				LogicalIndexCache.MountedEventHandler(context);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005037 File Offset: 0x00003237
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
			PhysicalIndexCache.DismountEventHandler(database);
			LogicalIndexVersionHistory.DismountEventHandler(database);
		}
	}
}
