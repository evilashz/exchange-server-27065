using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000CF RID: 207
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class XO1MigrationDataRow : IMigrationDataRow
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0002F250 File Offset: 0x0002D450
		public XO1MigrationDataRow(int rowIndex, string email, long puid, string firstName, string lastName, ExTimeZone timeZone, int localeId, string[] emailAddresses, long accountSize)
		{
			if (rowIndex < 1)
			{
				throw new ArgumentException("RowIndex should not be less than 1");
			}
			this.CursorPosition = rowIndex;
			this.Identifier = email;
			this.Puid = puid;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.TimeZone = timeZone;
			this.LocaleId = localeId;
			this.EmailAddresses = emailAddresses;
			this.AccountSize = accountSize;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0002F2B7 File Offset: 0x0002D4B7
		public MigrationType MigrationType
		{
			get
			{
				return MigrationType.XO1;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002F2BA File Offset: 0x0002D4BA
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return MigrationUserRecipientType.Mailbox;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002F2BD File Offset: 0x0002D4BD
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0002F2C5 File Offset: 0x0002D4C5
		public int CursorPosition { get; private set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002F2CE File Offset: 0x0002D4CE
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0002F2D6 File Offset: 0x0002D4D6
		public string Identifier { get; private set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0002F2DF File Offset: 0x0002D4DF
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.Identifier;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0002F2E7 File Offset: 0x0002D4E7
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002F2EA File Offset: 0x0002D4EA
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002F2ED File Offset: 0x0002D4ED
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0002F2F5 File Offset: 0x0002D4F5
		public long Puid { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002F2FE File Offset: 0x0002D4FE
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0002F306 File Offset: 0x0002D506
		public string FirstName { get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0002F30F File Offset: 0x0002D50F
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0002F317 File Offset: 0x0002D517
		public string LastName { get; private set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0002F320 File Offset: 0x0002D520
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x0002F328 File Offset: 0x0002D528
		public ExTimeZone TimeZone { get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002F331 File Offset: 0x0002D531
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0002F339 File Offset: 0x0002D539
		public int LocaleId { get; private set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0002F342 File Offset: 0x0002D542
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0002F34A File Offset: 0x0002D54A
		public string[] EmailAddresses { get; private set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0002F353 File Offset: 0x0002D553
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0002F35B File Offset: 0x0002D55B
		public long AccountSize { get; private set; }
	}
}
