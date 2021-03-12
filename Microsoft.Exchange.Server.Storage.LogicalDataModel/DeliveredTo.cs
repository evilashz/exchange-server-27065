using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200003C RID: 60
	public static class DeliveredTo
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00037F5E File Offset: 0x0003615E
		public static void Initialize()
		{
			if (DeliveredTo.deliveredToCleanupMaintenance == null)
			{
				DeliveredTo.deliveredToCleanupMaintenance = MaintenanceHandler.RegisterDatabaseMaintenance(DeliveredTo.DeliveredToCleanupMaintenanceId, RequiredMaintenanceResourceType.Store, new MaintenanceHandler.DatabaseMaintenanceDelegate(DeliveredTo.DeliveredToCleanupMaintenance), "DeliveredTo.DeliveredToCleanupMaintenance");
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00037F88 File Offset: 0x00036188
		internal static void MountedEventHandler(Context context)
		{
			DeliveredTo.deliveredToCleanupMaintenance.ScheduleMarkForMaintenance(context, TimeSpan.FromDays(1.0));
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00037FA4 File Offset: 0x000361A4
		public static void DeliveredToCleanupMaintenance(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			DeliveredToTable deliveredToTable = DatabaseSchema.DeliveredToTable(context.Database);
			StartStopKey startKey = new StartStopKey(true, new object[]
			{
				DateTime.MinValue
			});
			StartStopKey stopKey = new StartStopKey(true, new object[]
			{
				DateTime.UtcNow - DeliveredTo.CleanupRange
			});
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, deliveredToTable.Table, deliveredToTable.Table.PrimaryKeyIndex, null, null, null, 0, 0, new KeyRange(startKey, stopKey), false, false), false))
			{
				int num = (int)deleteOperator.ExecuteScalar();
			}
			completed = true;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0003806C File Offset: 0x0003626C
		internal static void RemoveAllEntriesForMailbox(Context context, Mailbox mailbox)
		{
			DeliveredToTable deliveredToTable = DatabaseSchema.DeliveredToTable(mailbox.Database);
			SearchCriteriaCompare restriction = Factory.CreateSearchCriteriaCompare(deliveredToTable.MailboxNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxNumber));
			using (DeleteOperator deleteOperator = Factory.CreateDeleteOperator(context.Culture, context, Factory.CreateTableOperator(context.Culture, context, deliveredToTable.Table, deliveredToTable.Table.PrimaryKeyIndex, null, restriction, null, 0, 0, KeyRange.AllRows, false, false), false))
			{
				int num = (int)deleteOperator.ExecuteScalar();
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00038104 File Offset: 0x00036304
		public static bool AlreadyDelivered(Context context, Mailbox mailbox, DateTime submitTime, ExchangeId folderId, string messageId)
		{
			bool result = false;
			DeliveredToTable deliveredToTable = DatabaseSchema.DeliveredToTable(mailbox.Database);
			long deliveredToHash = DeliveredTo.GetDeliveredToHash(messageId, folderId);
			DeliveredTo.SanitizeSubmitTime(messageId, ref submitTime);
			using (DataRow dataRow = Factory.OpenDataRow(context.Culture, context, deliveredToTable.Table, true, new ColumnValue[]
			{
				new ColumnValue(deliveredToTable.MailboxNumber, mailbox.MailboxNumber),
				new ColumnValue(deliveredToTable.SubmitTime, submitTime),
				new ColumnValue(deliveredToTable.MessageIdHash, deliveredToHash)
			}))
			{
				if (dataRow != null)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000381D0 File Offset: 0x000363D0
		public static void AddToDeliveredToTable(Context context, Mailbox mailbox, DateTime submitTime, ExchangeId folderId, string messageId)
		{
			DeliveredToTable deliveredToTable = DatabaseSchema.DeliveredToTable(mailbox.Database);
			long deliveredToHash = DeliveredTo.GetDeliveredToHash(messageId, folderId);
			DeliveredTo.SanitizeSubmitTime(messageId, ref submitTime);
			if (submitTime > DateTime.UtcNow.Add(DeliveredTo.driftThreshold))
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_InvalidSubmitTime, new object[]
				{
					messageId,
					submitTime
				});
				return;
			}
			using (DataRow dataRow = Factory.CreateDataRow(context.Culture, context, deliveredToTable.Table, true, new ColumnValue[]
			{
				new ColumnValue(deliveredToTable.MailboxNumber, mailbox.MailboxNumber),
				new ColumnValue(deliveredToTable.SubmitTime, submitTime),
				new ColumnValue(deliveredToTable.MessageIdHash, deliveredToHash)
			}))
			{
				dataRow.Flush(context);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000382DC File Offset: 0x000364DC
		public static long GetDeliveredToHash(string messageId, ExchangeId folderId)
		{
			if (string.IsNullOrEmpty(messageId))
			{
				return 0L;
			}
			long num = (folderId != ExchangeId.Null) ? folderId.ToLong() : 0L;
			for (int i = 0; i < messageId.Length; i++)
			{
				long num2 = (long)((ulong)messageId[i]);
				num ^= num2;
				for (int j = 0; j < 8; j++)
				{
					long num3 = num & 1L;
					num >>= 1;
					if (0L != num3)
					{
						num ^= -3932672073523589310L;
					}
				}
			}
			return num;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00038354 File Offset: 0x00036554
		private static void SanitizeSubmitTime(string messageId, ref DateTime submitTime)
		{
			if (messageId.StartsWith("ed590c4ca1674effa0067475ab2b93b2_", StringComparison.OrdinalIgnoreCase))
			{
				submitTime = DateTime.MinValue;
				return;
			}
			submitTime.AddMilliseconds((double)(-(double)submitTime.Millisecond));
		}

		// Token: 0x0400032D RID: 813
		public static readonly Guid DeliveredToCleanupMaintenanceId = new Guid("{f6f50b68-76c8-4b41-865f-e984022602ac}");

		// Token: 0x0400032E RID: 814
		internal static TimeSpan CleanupRange = TimeSpan.FromDays(7.0);

		// Token: 0x0400032F RID: 815
		private static IDatabaseMaintenance deliveredToCleanupMaintenance;

		// Token: 0x04000330 RID: 816
		private static TimeSpan driftThreshold = TimeSpan.FromDays(7.0);
	}
}
