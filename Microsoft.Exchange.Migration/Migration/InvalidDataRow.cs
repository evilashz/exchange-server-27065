using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200000E RID: 14
	internal class InvalidDataRow : IMigrationDataRow
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026B0 File Offset: 0x000008B0
		public InvalidDataRow(int cursorPosition, MigrationBatchError error, MigrationType migrationType = MigrationType.None)
		{
			this.cursorPosition = cursorPosition;
			this.error = error;
			this.migrationType = migrationType;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026CD File Offset: 0x000008CD
		public MigrationType MigrationType
		{
			get
			{
				return this.migrationType;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026D5 File Offset: 0x000008D5
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026D8 File Offset: 0x000008D8
		public string Identifier
		{
			get
			{
				return this.error.EmailAddress;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026E5 File Offset: 0x000008E5
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.Identifier;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026ED File Offset: 0x000008ED
		public int CursorPosition
		{
			get
			{
				return this.cursorPosition;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026F5 File Offset: 0x000008F5
		public MigrationBatchError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026FD File Offset: 0x000008FD
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002700 File Offset: 0x00000900
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400000A RID: 10
		private readonly int cursorPosition;

		// Token: 0x0400000B RID: 11
		private readonly MigrationBatchError error;

		// Token: 0x0400000C RID: 12
		private readonly MigrationType migrationType;
	}
}
