using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A7 RID: 167
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class NspiMigrationDataRow : ExchangeMigrationDataRow
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x00027EE3 File Offset: 0x000260E3
		private NspiMigrationDataRow(int rowIndex, ExchangeMigrationRecipient recipient) : base(rowIndex, recipient.Identifier, MigrationUserRecipientType.Mailbox)
		{
			this.Recipient = recipient;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00027EFA File Offset: 0x000260FA
		public override MigrationUserRecipientType RecipientType
		{
			get
			{
				return this.Recipient.RecipientType;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00027F08 File Offset: 0x00026108
		public static bool TryCreate(PropRow row, int index, long[] properties, out IMigrationDataRow dataRow, out MigrationBatchError error)
		{
			MigrationUtil.ThrowOnNullArgument(row, "row");
			MigrationUtil.ThrowOnNullArgument(properties, "properties");
			if (row.Properties.Count != properties.Length)
			{
				throw new ArgumentOutOfRangeException("row", "row.Properties.Count != properties.Length");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ExchangeMigrationRecipient exchangeMigrationRecipient = NspiMigrationDataReader.TryCreateRecipient(row, properties, false);
			if (exchangeMigrationRecipient == null)
			{
				error = null;
				dataRow = null;
				return false;
			}
			if (string.IsNullOrEmpty(exchangeMigrationRecipient.Identifier))
			{
				error = new MigrationBatchError
				{
					EmailAddress = null,
					RowIndex = index,
					LocalizedErrorMessage = ServerStrings.MigrationNSPIMissingRequiredField(PropTag.SmtpAddress)
				};
				dataRow = null;
				return false;
			}
			error = null;
			dataRow = new NspiMigrationDataRow(index, exchangeMigrationRecipient);
			return true;
		}

		// Token: 0x040003A8 RID: 936
		public readonly ExchangeMigrationRecipient Recipient;
	}
}
