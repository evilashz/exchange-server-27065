using System;
using System.Text;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200003A RID: 58
	public static class DatabaseSizeCheck
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x00037A18 File Offset: 0x00035C18
		internal static void LaunchDatabaseSizeCheckTask(StoreDatabase database)
		{
			Task<StoreDatabase>.TaskCallback callback = TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.DatabaseSizeCheck, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(DatabaseSizeCheck.DatabaseSizePeriodicCheck));
			RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(callback, database, TimeSpan.FromDays(1.0), false);
			database.TaskList.Add(task, true);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00037A6A File Offset: 0x00035C6A
		private static void DatabaseSizePeriodicCheck(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue)
		{
			DatabaseSizeCheck.CheckDatabaseSize(context, database);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00037A73 File Offset: 0x00035C73
		internal static IDisposable SetGetDatabaseSizeTestHook(DatabaseSizeCheck.GetDatabaseSizeDelegate testDelegate)
		{
			return DatabaseSizeCheck.getDatabaseSizeTestHook.SetTestHook(testDelegate);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00037A80 File Offset: 0x00035C80
		internal static void CheckDatabaseSize(Context context, StoreDatabase database)
		{
			uint warningThresholdPct = 0U;
			uint num = 0U;
			uint num2 = (uint)(database.PhysicalDatabase.PageSize / 1024);
			DatabaseSizeCheck.numberOfPagesInOneGb = 1048576U / num2;
			DatabaseSizeCheck.GetDbSizeLimitParamsFromRegistry(database, out num, out warningThresholdPct);
			uint pageSize = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			DatabaseSizeCheck.getDatabaseSizeTestHook.Value(context, database, out num3, out num4, out pageSize);
			DatabaseSizeCheck.TracePhysicalSize(pageSize, num3);
			uint num5 = DatabaseSizeCheck.ComputeMaximumSizeInPages(num, pageSize);
			uint num6 = DatabaseSizeCheck.ComputeWarningThreshold(num5, warningThresholdPct);
			if (num3 > num6)
			{
				DatabaseSizeCheck.TraceAvailablePages(pageSize, num4);
				num3 -= Math.Min(num4, num3);
			}
			if (num3 > num6)
			{
				if (num3 > num5)
				{
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MaxDbSizeExceededDismountForced, new object[]
					{
						database.MdbName,
						num5,
						num,
						ConfigurationSchema.DatabaseSizeLimitGB.RegistryValueName,
						ConfigurationSchema.LocalDatabaseRegistryKey
					});
					DatabaseSizeCheck.TraceExceedDatabaseLimit(num, pageSize, num3, num5);
					database.PublishHaFailure(FailureTag.ExceededDatabaseMaxSize);
					return;
				}
				DatabaseSizeCheck.TraceExceedWarningThreshold(num, pageSize, num3, num5, num6);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_ApproachingMaxDbSize, new object[]
				{
					database.MdbName,
					num5,
					num
				});
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00037BBC File Offset: 0x00035DBC
		private static void TracePhysicalSize(uint pageSize, uint totalPages)
		{
			if (ExTraceGlobals.DatabaseSizeCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				stringBuilder.Append("Physical size:");
				stringBuilder.Append(DatabaseSizeCheck.GbFromPages(totalPages, pageSize));
				stringBuilder.Append("Gb (");
				stringBuilder.Append(totalPages);
				stringBuilder.Append(" pages)");
				ExTraceGlobals.DatabaseSizeCheckTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00037C30 File Offset: 0x00035E30
		private static void TraceAvailablePages(uint pageSize, uint availablePages)
		{
			if (ExTraceGlobals.DatabaseSizeCheckTracer.IsTraceEnabled(TraceType.DebugTrace) && ExTraceGlobals.DatabaseSizeCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				stringBuilder.Append("Db free space:");
				stringBuilder.Append(DatabaseSizeCheck.GbFromPages(availablePages, pageSize));
				stringBuilder.Append("Gb (");
				stringBuilder.Append(availablePages);
				stringBuilder.Append(" pages)");
				ExTraceGlobals.DatabaseSizeCheckTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00037CB0 File Offset: 0x00035EB0
		private static void TraceExceedDatabaseLimit(uint maxSizeGB, uint pageSize, uint totalPages, uint maxSizeInPages)
		{
			if (ExTraceGlobals.DatabaseSizeCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				stringBuilder.Append("Db size of");
				stringBuilder.Append(DatabaseSizeCheck.GbFromPages(totalPages, pageSize));
				stringBuilder.Append("Gb (");
				stringBuilder.Append(totalPages);
				stringBuilder.Append(" pages) exceeds limit of");
				stringBuilder.Append(maxSizeGB);
				stringBuilder.Append("Gb (");
				stringBuilder.Append(maxSizeInPages);
				stringBuilder.Append("pages).");
				ExTraceGlobals.DatabaseSizeCheckTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00037D4C File Offset: 0x00035F4C
		private static void TraceExceedWarningThreshold(uint maxSizeGB, uint pageSize, uint totalPages, uint maxSizeInPages, uint warningThreshold)
		{
			if (ExTraceGlobals.DatabaseSizeCheckTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(1000);
				stringBuilder.Append("Db size of");
				stringBuilder.Append(DatabaseSizeCheck.GbFromPages(totalPages, pageSize));
				stringBuilder.Append("Gb (");
				stringBuilder.Append(totalPages);
				stringBuilder.Append(" pages) exceeds warning threshold of");
				stringBuilder.Append(DatabaseSizeCheck.GbFromPages(warningThreshold, pageSize));
				stringBuilder.Append("Gb (");
				stringBuilder.Append(warningThreshold);
				stringBuilder.Append("pages). [MaxSize=");
				stringBuilder.Append(maxSizeGB);
				stringBuilder.Append("Gb (");
				stringBuilder.Append(maxSizeInPages);
				stringBuilder.Append("pages)]");
				ExTraceGlobals.DatabaseSizeCheckTracer.TraceDebug(0L, stringBuilder.ToString());
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00037E18 File Offset: 0x00036018
		internal static uint ComputeMaximumSizeInPages(uint maxSizeGB, uint pageSize)
		{
			return DatabaseSizeCheck.PagesFromGb(maxSizeGB, pageSize);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00037E21 File Offset: 0x00036021
		internal static uint ComputeWarningThreshold(uint maxSizePage, uint warningThresholdPct)
		{
			return (uint)(maxSizePage * ((100U - warningThresholdPct) / 100.0));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00037E38 File Offset: 0x00036038
		private static uint GbFromPages(uint sizeInPages, uint pageSize)
		{
			return sizeInPages / DatabaseSizeCheck.numberOfPagesInOneGb;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00037E41 File Offset: 0x00036041
		private static uint PagesFromGb(uint sizeInGb, uint pageSize)
		{
			return sizeInGb * DatabaseSizeCheck.numberOfPagesInOneGb;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00037E4C File Offset: 0x0003604C
		private static void GetDatabaseSize(Context context, StoreDatabase database, out uint totalPages, out uint availablePages, out uint pageSize)
		{
			using (context.AssociateWithDatabase(database))
			{
				database.PhysicalDatabase.GetDatabaseSize(context, out totalPages, out availablePages, out pageSize);
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00037E94 File Offset: 0x00036094
		private static void GetDbSizeLimitParamsFromRegistry(StoreDatabase database, out uint maxSizeGB, out uint warningThresholdPct)
		{
			maxSizeGB = ConfigurationSchema.DatabaseSizeLimitGB.Value;
			if (maxSizeGB != 0U)
			{
				uint val = (database.ServerEdition == ServerEditionType.Enterprise) ? DatabaseSizeCheck.databaseSizeMaxEnterpriseSKU : DatabaseSizeCheck.databaseSizeMaxStandardSKU;
				maxSizeGB = Math.Min(val, maxSizeGB);
			}
			else
			{
				maxSizeGB = ((database.ServerEdition == ServerEditionType.Enterprise) ? DatabaseSizeCheck.DatabaseSizeMaxEnterpriseSKUDefault : 1024U);
			}
			warningThresholdPct = Math.Min(ConfigurationSchema.DatabaseWarningThresholdPercent.Value, 100U);
		}

		// Token: 0x04000325 RID: 805
		private const uint DatabaseSizeMaxStandardSKUDefault = 1024U;

		// Token: 0x04000326 RID: 806
		internal const uint DbSizeWarningThresholdMax = 100U;

		// Token: 0x04000327 RID: 807
		private static uint numberOfPagesInOneGb;

		// Token: 0x04000328 RID: 808
		private static UnlimitedBytes databaseSizeMax = UnlimitedBytes.FromGB(16000L);

		// Token: 0x04000329 RID: 809
		private static uint databaseSizeMaxEnterpriseSKU = (uint)DatabaseSizeCheck.databaseSizeMax.GB;

		// Token: 0x0400032A RID: 810
		private static uint databaseSizeMaxStandardSKU = (uint)DatabaseSizeCheck.databaseSizeMax.GB;

		// Token: 0x0400032B RID: 811
		internal static uint DatabaseSizeMaxEnterpriseSKUDefault = DatabaseSizeCheck.databaseSizeMaxEnterpriseSKU;

		// Token: 0x0400032C RID: 812
		private static Hookable<DatabaseSizeCheck.GetDatabaseSizeDelegate> getDatabaseSizeTestHook = Hookable<DatabaseSizeCheck.GetDatabaseSizeDelegate>.Create(true, new DatabaseSizeCheck.GetDatabaseSizeDelegate(DatabaseSizeCheck.GetDatabaseSize));

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x060005F9 RID: 1529
		public delegate void GetDatabaseSizeDelegate(Context context, StoreDatabase database, out uint totalPages, out uint availablePages, out uint pageSize);
	}
}
