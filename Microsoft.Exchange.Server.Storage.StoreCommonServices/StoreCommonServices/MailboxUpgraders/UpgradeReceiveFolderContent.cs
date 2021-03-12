using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices.MailboxUpgraders
{
	// Token: 0x020000C6 RID: 198
	public sealed class UpgradeReceiveFolderContent : SchemaUpgrader
	{
		// Token: 0x06000832 RID: 2098 RVA: 0x00027E70 File Offset: 0x00026070
		public static bool IsReady(Context context, Mailbox mailbox)
		{
			return UpgradeReceiveFolderContent.Instance.TestVersionIsReady(context, mailbox);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00027E7E File Offset: 0x0002607E
		public static void InitializeUpgraderAction(Action<Context, Mailbox> upgraderAction)
		{
			UpgradeReceiveFolderContent.upgraderAction = upgraderAction;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00027E86 File Offset: 0x00026086
		private UpgradeReceiveFolderContent() : base(new ComponentVersion(0, 10000), new ComponentVersion(0, 10001))
		{
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00027EA4 File Offset: 0x000260A4
		public override void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00027EA6 File Offset: 0x000260A6
		public override void PerformUpgrade(Context context, ISchemaVersion container)
		{
			if (UpgradeReceiveFolderContent.upgraderAction != null)
			{
				UpgradeReceiveFolderContent.upgraderAction(context, (Mailbox)container);
			}
		}

		// Token: 0x040004B2 RID: 1202
		public static UpgradeReceiveFolderContent Instance = new UpgradeReceiveFolderContent();

		// Token: 0x040004B3 RID: 1203
		private static Action<Context, Mailbox> upgraderAction;
	}
}
