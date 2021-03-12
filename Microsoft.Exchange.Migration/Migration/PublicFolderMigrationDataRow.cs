using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B9 RID: 185
	internal class PublicFolderMigrationDataRow : IMigrationDataRow
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x0002A390 File Offset: 0x00028590
		public PublicFolderMigrationDataRow(int rowIndex, string mailboxName)
		{
			this.LocalMailboxIdentifier = mailboxName;
			this.Identifier = mailboxName;
			this.CursorPosition = rowIndex;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002A3BA File Offset: 0x000285BA
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.PublicFolder;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0002A3C1 File Offset: 0x000285C1
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002A3C4 File Offset: 0x000285C4
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002A3CC File Offset: 0x000285CC
		public string Identifier { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002A3D5 File Offset: 0x000285D5
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0002A3DD File Offset: 0x000285DD
		public string LocalMailboxIdentifier { get; private set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002A3E6 File Offset: 0x000285E6
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002A3EE File Offset: 0x000285EE
		public int CursorPosition { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002A3F7 File Offset: 0x000285F7
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002A3FA File Offset: 0x000285FA
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}
	}
}
