using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000122 RID: 290
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class IMAPJobSyncInitializingProcessor : MigrationJobSyncInitializingProcessor
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000408FC File Offset: 0x0003EAFC
		protected override bool IgnorePostCompleteSubmits
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000408FF File Offset: 0x0003EAFF
		protected override IMigrationDataRowProvider GetMigrationDataRowProvider()
		{
			return new IMAPCSVDataRowProvider(base.Job, base.DataProvider);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00040914 File Offset: 0x0003EB14
		protected override void CreateNewJobItem(IMigrationDataRow dataRow)
		{
			LocalizedException ex;
			MailboxData mailboxData = base.GetMailboxData(dataRow.Identifier, out ex);
			if (mailboxData == null)
			{
				if (ex == null)
				{
					ex = new MigrationUnknownException();
				}
				MigrationJobItem.CreateFailed(base.DataProvider, base.Job, dataRow, ex, null, null);
				return;
			}
			base.CreateNewJobItem(dataRow, mailboxData, MigrationUserStatus.Queued);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00040964 File Offset: 0x0003EB64
		protected override MigrationBatchError ValidateDataRow(IMigrationDataRow row)
		{
			MigrationBatchError migrationBatchError = base.ValidateDataRow(row);
			if (migrationBatchError != null)
			{
				return migrationBatchError;
			}
			IMAPMigrationDataRow imapmigrationDataRow = (IMAPMigrationDataRow)row;
			ImapEndpoint imapEndpoint = (ImapEndpoint)base.Job.SourceEndpoint;
			if (imapEndpoint.Authentication == IMAPAuthenticationMechanism.Basic)
			{
				string imapUserId = imapmigrationDataRow.ImapUserId;
				string token = MigrationUtil.EncryptedStringToClearText(imapmigrationDataRow.EncryptedPassword);
				if (MigrationUtil.HasUnicodeCharacters(imapUserId) || MigrationUtil.HasUnicodeCharacters(token))
				{
					return this.GetValidationError(row, Strings.CannotSpecifyUnicodeUserIdPasswordWithBasicAuth);
				}
			}
			return null;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000409D4 File Offset: 0x0003EBD4
		protected override MigrationBatchError GetValidationError(IMigrationDataRow dataRow, LocalizedString locErrorString)
		{
			IMAPMigrationDataRow imapmigrationDataRow = (IMAPMigrationDataRow)dataRow;
			return new MigrationBatchError
			{
				RowIndex = imapmigrationDataRow.CursorPosition,
				EmailAddress = dataRow.Identifier,
				LocalizedErrorMessage = locErrorString
			};
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00040A10 File Offset: 0x0003EC10
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IMAPJobSyncInitializingProcessor>(this);
		}
	}
}
