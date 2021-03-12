using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A8 RID: 168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class IMAPMigrationDataRow : IMigrationDataRow
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x00027FB8 File Offset: 0x000261B8
		public IMAPMigrationDataRow(int rowIndex, SmtpAddress destinationEmail, string imapUserId, string encryptedPassword, string migrationUserRootFolder)
		{
			MigrationUtil.ThrowOnNullArgument(destinationEmail, "destinationEmail");
			MigrationUtil.ThrowOnNullOrEmptyArgument(imapUserId, "imapUserId");
			MigrationUtil.ThrowOnNullOrEmptyArgument(encryptedPassword, "encryptedPassword");
			if (rowIndex < 1)
			{
				throw new ArgumentException("RowIndex should not be less than 1");
			}
			this.CursorPosition = rowIndex;
			this.DestinationEmailAddress = destinationEmail;
			this.ImapUserId = imapUserId;
			this.EncryptedPassword = encryptedPassword;
			this.MigrationUserRootFolder = migrationUserRootFolder;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00028026 File Offset: 0x00026226
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0002802E File Offset: 0x0002622E
		public SmtpAddress DestinationEmailAddress { get; private set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00028037 File Offset: 0x00026237
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.IMAP;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0002803A File Offset: 0x0002623A
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0002803D File Offset: 0x0002623D
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00028045 File Offset: 0x00026245
		public int CursorPosition { get; private set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0002804E File Offset: 0x0002624E
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00028056 File Offset: 0x00026256
		public string ImapUserId { get; private set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x0002805F File Offset: 0x0002625F
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00028067 File Offset: 0x00026267
		public string EncryptedPassword { get; private set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00028070 File Offset: 0x00026270
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00028078 File Offset: 0x00026278
		public string MigrationUserRootFolder { get; private set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00028084 File Offset: 0x00026284
		public string Identifier
		{
			get
			{
				return this.DestinationEmailAddress.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000957 RID: 2391 RVA: 0x000280AA File Offset: 0x000262AA
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.Identifier;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x000280B2 File Offset: 0x000262B2
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000280B5 File Offset: 0x000262B5
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}
	}
}
