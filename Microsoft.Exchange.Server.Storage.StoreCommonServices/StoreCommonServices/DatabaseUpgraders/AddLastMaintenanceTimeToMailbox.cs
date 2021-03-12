using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders
{
	// Token: 0x02000047 RID: 71
	public sealed class AddLastMaintenanceTimeToMailbox : SchemaUpgrader
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0001772E File Offset: 0x0001592E
		public static bool IsReady(Context context, StoreDatabase database)
		{
			return AddLastMaintenanceTimeToMailbox.Instance.TestVersionIsReady(context, database);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001773C File Offset: 0x0001593C
		private AddLastMaintenanceTimeToMailbox() : base(new ComponentVersion(0, 121), new ComponentVersion(0, 122))
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00017754 File Offset: 0x00015954
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(database);
			mailboxTable.LastMailboxMaintenanceTime.MinVersion = base.To.Value;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00017784 File Offset: 0x00015984
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			StoreDatabase storeDatabase = container as StoreDatabase;
			if (storeDatabase.PhysicalDatabase.DatabaseType == DatabaseType.Jet)
			{
				MailboxTable mailboxTable = DatabaseSchema.MailboxTable(storeDatabase);
				mailboxTable.Table.AddColumn(context, mailboxTable.LastMailboxMaintenanceTime);
			}
		}

		// Token: 0x0400027C RID: 636
		public static AddLastMaintenanceTimeToMailbox Instance = new AddLastMaintenanceTimeToMailbox();
	}
}
