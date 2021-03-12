using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000050 RID: 80
	public sealed class UnifiedMailbox : SchemaUpgrader
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00017EC0 File Offset: 0x000160C0
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return UnifiedMailbox.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00017ECE File Offset: 0x000160CE
		public static void InitializeUpgraderAction(Action<Context> upgraderAction, Action<StoreDatabase> inMemorySchemaInitializationAction)
		{
			UnifiedMailbox.upgraderAction = upgraderAction;
			UnifiedMailbox.inMemorySchemaInitializationAction = inMemorySchemaInitializationAction;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00017EDC File Offset: 0x000160DC
		private UnifiedMailbox() : base(new ComponentVersion(0, 126), new ComponentVersion(0, 127))
		{
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00017EF4 File Offset: 0x000160F4
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			MailboxIdentityTable mailboxIdentityTable = DatabaseSchema.MailboxIdentityTable(database);
			mailboxIdentityTable.NextMessageDocumentId.MinVersion = base.To.Value;
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(database);
			mailboxTable.MailboxPartitionNumber.MinVersion = base.To.Value;
			mailboxTable.UnifiedMailboxGuid.MinVersion = base.To.Value;
			mailboxTable.UnifiedMailboxGuidIndex.MinVersion = base.To.Value;
			if (UnifiedMailbox.inMemorySchemaInitializationAction != null)
			{
				UnifiedMailbox.inMemorySchemaInitializationAction(database);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00017F88 File Offset: 0x00016188
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			StoreDatabase storeDatabase = container as StoreDatabase;
			if (storeDatabase.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				MailboxIdentityTable mailboxIdentityTable = DatabaseSchema.MailboxIdentityTable(storeDatabase);
				mailboxIdentityTable.Table.AddColumn(context, mailboxIdentityTable.NextMessageDocumentId);
				MailboxTable mailboxTable = DatabaseSchema.MailboxTable(storeDatabase);
				mailboxTable.Table.AddColumn(context, mailboxTable.MailboxPartitionNumber);
				mailboxTable.Table.AddColumn(context, mailboxTable.UnifiedMailboxGuid);
				mailboxTable.Table.CreateIndex(context, mailboxTable.UnifiedMailboxGuidIndex, null);
				if (UnifiedMailbox.upgraderAction != null)
				{
					UnifiedMailbox.upgraderAction(context);
				}
			}
		}

		// Token: 0x0400028B RID: 651
		public static UnifiedMailbox Instance = new UnifiedMailbox();

		// Token: 0x0400028C RID: 652
		private static Action<Context> upgraderAction;

		// Token: 0x0400028D RID: 653
		private static Action<StoreDatabase> inMemorySchemaInitializationAction;
	}
}
