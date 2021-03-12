using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000123 RID: 291
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MoveJobSyncInitializingProcessor : MigrationJobSyncInitializingProcessor
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00040A20 File Offset: 0x0003EC20
		protected override bool IgnorePostCompleteSubmits
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00040A23 File Offset: 0x0003EC23
		protected override IMigrationDataRowProvider GetMigrationDataRowProvider()
		{
			return new MoveCsvDataRowProvider(base.Job, base.DataProvider);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00040A38 File Offset: 0x0003EC38
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
			MoveMigrationDataRow moveMigrationDataRow = dataRow as MoveMigrationDataRow;
			if (moveMigrationDataRow == null)
			{
				MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, new MigrationUnknownException(), null, null);
				return;
			}
			MigrationUserRecipientType recipientType = moveMigrationDataRow.RecipientType;
			if (recipientType != MigrationUserRecipientType.Mailbox)
			{
				switch (recipientType)
				{
				case MigrationUserRecipientType.Mailuser:
					if (mailboxData.RecipientType != MigrationUserRecipientType.Mailuser)
					{
						MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, new InvalidRecipientTypeException(MigrationUserRecipientType.Mailbox.ToString(), MigrationUserRecipientType.Mailuser.ToString()), null, null);
						return;
					}
					break;
				case MigrationUserRecipientType.MailboxOrMailuser:
					break;
				default:
					MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, new UnsupportedRecipientTypeForProtocolException(moveMigrationDataRow.RecipientType.ToString(), base.Job.MigrationType.ToString()), null, null);
					return;
				}
			}
			else if (mailboxData.RecipientType != MigrationUserRecipientType.Mailbox)
			{
				MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, new InvalidRecipientTypeException(MigrationUserRecipientType.Mailuser.ToString(), MigrationUserRecipientType.Mailbox.ToString()), null, null);
				return;
			}
			base.CreateNewJobItem(dataRow, mailboxData, this.DetermineInitialStatus());
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		protected override MigrationBatchError GetValidationError(IMigrationDataRow dataRow, LocalizedString locErrorString)
		{
			MoveMigrationDataRow moveMigrationDataRow = (MoveMigrationDataRow)dataRow;
			return new MigrationBatchError
			{
				RowIndex = moveMigrationDataRow.CursorPosition,
				EmailAddress = dataRow.Identifier,
				LocalizedErrorMessage = locErrorString
			};
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00040C04 File Offset: 0x0003EE04
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MoveJobSyncInitializingProcessor>(this);
		}
	}
}
