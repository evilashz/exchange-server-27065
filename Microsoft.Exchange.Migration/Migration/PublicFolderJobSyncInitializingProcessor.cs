using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000125 RID: 293
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PublicFolderJobSyncInitializingProcessor : MigrationJobSyncInitializingProcessor
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x0004118D File Offset: 0x0003F38D
		protected override IMigrationDataRowProvider GetMigrationDataRowProvider()
		{
			return new PublicFolderCSVDataRowProvider(base.Job, base.DataProvider);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000411A0 File Offset: 0x0003F3A0
		protected override void CreateNewJobItem(IMigrationDataRow dataRow)
		{
			LocalizedException ex;
			MailboxData mailboxData = base.GetMailboxData(dataRow.Identifier, out ex);
			if (mailboxData == null)
			{
				if (ex == null)
				{
					ex = new MigrationObjectNotFoundInADException(dataRow.Identifier, base.DataProvider.ADProvider.GetPreferredDomainController());
				}
				MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, ex, null, null);
				return;
			}
			if (!(dataRow is PublicFolderMigrationDataRow))
			{
				MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, new MigrationUnknownException(), null, null);
				return;
			}
			base.CreateNewJobItem(dataRow, mailboxData, this.DetermineInitialStatus());
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00041239 File Offset: 0x0003F439
		protected override MailboxData InternalGetMailboxData(string identifier)
		{
			return base.DataProvider.ADProvider.GetPublicFolderMailboxDataFromName(identifier, false, true);
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0004124E File Offset: 0x0003F44E
		protected override bool IgnorePostCompleteSubmits
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00041251 File Offset: 0x0003F451
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublicFolderJobSyncInitializingProcessor>(this);
		}
	}
}
