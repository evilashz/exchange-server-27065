using System;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000058 RID: 88
	public static class Globals
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x000456B0 File Offset: 0x000438B0
		public static void Initialize()
		{
			DatabaseSchema.Initialize();
			LogicalMailbox.Initialize();
			UnifiedMailbox.InitializeUpgraderAction(delegate(Context context)
			{
				FolderTable folderTable = DatabaseSchema.FolderTable(context.Database);
				folderTable.Table.AddColumn(context, folderTable.MailboxNumber);
				MessageTable messageTable = DatabaseSchema.MessageTable(context.Database);
				messageTable.Table.AddColumn(context, messageTable.MailboxNumber);
				AttachmentTable attachmentTable = DatabaseSchema.AttachmentTable(context.Database);
				attachmentTable.Table.AddColumn(context, attachmentTable.MailboxNumber);
				InferenceLogTable inferenceLogTable = DatabaseSchema.InferenceLogTable(context.Database);
				inferenceLogTable.Table.AddColumn(context, inferenceLogTable.MailboxNumber);
			}, delegate(StoreDatabase database)
			{
				FolderTable folderTable = DatabaseSchema.FolderTable(database);
				folderTable.MailboxNumber.MinVersion = UnifiedMailbox.Instance.To.Value;
				MessageTable messageTable = DatabaseSchema.MessageTable(database);
				messageTable.MailboxNumber.MinVersion = UnifiedMailbox.Instance.To.Value;
				AttachmentTable attachmentTable = DatabaseSchema.AttachmentTable(database);
				attachmentTable.MailboxNumber.MinVersion = UnifiedMailbox.Instance.To.Value;
				InferenceLogTable inferenceLogTable = DatabaseSchema.InferenceLogTable(database);
				inferenceLogTable.MailboxNumber.MinVersion = UnifiedMailbox.Instance.To.Value;
			});
			TimedEventDispatcher.RegisterHandler(TimerEventHandler.EventSource, new TimerEventHandler());
			InTransitInfo.Initialize();
			ReceiveFolder.Initialize();
			EventHistory.Initialize();
			DeliveredTo.Initialize();
			ReliableEventNotificationSubscriber.Subscribe(null);
			FolderHierarchy.Initialize();
			Folder.Initialize();
			SearchFolder.Initialize();
			OpenMessageStates.Initialize();
			SubobjectReferenceState.Initialize();
			SubobjectCleanup.Initialize();
			MailboxCleanup.Initialize();
			SpecialFoldersCache.Initialize();
			PerUser.Initialize();
			UserInformation.Initialize();
			Mailbox.TableSizeStatistics[] array = new Mailbox.TableSizeStatistics[5];
			Mailbox.TableSizeStatistics[] array2 = array;
			int num = 0;
			Mailbox.TableSizeStatistics tableSizeStatistics = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics.TableAccessor = ((Context context) => DatabaseSchema.MessageTable(context.Database).Table);
			tableSizeStatistics.TotalPagesProperty = PropTag.Mailbox.MessageTableTotalPages;
			tableSizeStatistics.AvailablePagesProperty = PropTag.Mailbox.MessageTableAvailablePages;
			array2[num] = tableSizeStatistics;
			Mailbox.TableSizeStatistics[] array3 = array;
			int num2 = 1;
			Mailbox.TableSizeStatistics tableSizeStatistics2 = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics2.TableAccessor = ((Context context) => DatabaseSchema.AttachmentTable(context.Database).Table);
			tableSizeStatistics2.TotalPagesProperty = PropTag.Mailbox.AttachmentTableTotalPages;
			tableSizeStatistics2.AvailablePagesProperty = PropTag.Mailbox.AttachmentTableAvailablePages;
			array3[num2] = tableSizeStatistics2;
			Mailbox.TableSizeStatistics[] array4 = array;
			int num3 = 2;
			Mailbox.TableSizeStatistics tableSizeStatistics3 = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics3.TableAccessor = ((Context context) => DatabaseSchema.FolderTable(context.Database).Table);
			tableSizeStatistics3.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics3.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array4[num3] = tableSizeStatistics3;
			Mailbox.TableSizeStatistics[] array5 = array;
			int num4 = 3;
			Mailbox.TableSizeStatistics tableSizeStatistics4 = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics4.TableAccessor = ((Context context) => DatabaseSchema.InferenceLogTable(context.Database).Table);
			tableSizeStatistics4.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics4.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array5[num4] = tableSizeStatistics4;
			Mailbox.TableSizeStatistics[] array6 = array;
			int num5 = 4;
			Mailbox.TableSizeStatistics tableSizeStatistics5 = default(Mailbox.TableSizeStatistics);
			tableSizeStatistics5.TableAccessor = ((Context context) => DatabaseSchema.PerUserTable(context.Database).Table);
			tableSizeStatistics5.TotalPagesProperty = PropTag.Mailbox.OtherTablesTotalPages;
			tableSizeStatistics5.AvailablePagesProperty = PropTag.Mailbox.OtherTablesAvailablePages;
			array6[num5] = tableSizeStatistics5;
			Mailbox.RegisterTableSizeStatistics(array);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0004590B File Offset: 0x00043B0B
		public static void Terminate()
		{
			ReliableEventNotificationSubscriber.Unsubscribe();
			TimedEventDispatcher.UnregisterHandler(TimerEventHandler.EventSource);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0004591C File Offset: 0x00043B1C
		public static void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			DatabaseSchema.Initialize(database);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00045924 File Offset: 0x00043B24
		public static void DatabaseMounting(Context context, StoreDatabase database, bool readOnly)
		{
			DatabaseSchema.PostMountInitialize(context, database);
			PropertySchemaPopulation.MountEventHandler(database);
			DatabaseSchema.MountEventHandlerForFullTextIndex(database);
			EventHistory.MountEventHandler(context, readOnly);
			PerUser.MountEventHandler(context, database, readOnly);
			SubobjectCleanup.MountEventHandler(context);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0004594E File Offset: 0x00043B4E
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
			if (!database.IsReadOnly)
			{
				DatabaseSizeCheck.LaunchDatabaseSizeCheckTask(database);
				DeliveredTo.MountedEventHandler(context);
				EventHistory.MountedEventHandler(context);
				MailboxCleanup.MountedEventHandler(context, database);
				SearchFolder.MountedEventHandler(context);
				SearchQueue.DrainSearchQueueTask.Launch(database);
				SubobjectCleanup.MountedEventHandler(context, database);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00045984 File Offset: 0x00043B84
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
			EventHistory.DismountEventHandler(database);
			PerUser.DismountEventHandler(context, database);
			SubobjectCleanup.DismountEventHandler(database);
		}
	}
}
