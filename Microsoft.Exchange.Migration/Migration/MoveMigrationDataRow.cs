using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F3 RID: 243
	internal class MoveMigrationDataRow : IMigrationDataRow
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0003502E File Offset: 0x0003322E
		public MoveMigrationDataRow(int rowIndex, string emailAddress, MigrationBatchDirection jobDirection, CsvRow row, bool jobArchiveOnly)
		{
			this.rowIndex = rowIndex;
			this.emailAddress = emailAddress;
			this.jobDirection = jobDirection;
			this.jobArchiveOnly = jobArchiveOnly;
			this.InitializeFromRow(row);
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0003505C File Offset: 0x0003325C
		public MigrationType MigrationType
		{
			get
			{
				switch (this.jobDirection)
				{
				case MigrationBatchDirection.Local:
					return MigrationType.ExchangeLocalMove;
				case MigrationBatchDirection.Onboarding:
				case MigrationBatchDirection.Offboarding:
					return MigrationType.ExchangeRemoteMove;
				}
				throw new MigrationDataCorruptionException(string.Format("Unknown batch type '{0}'", this.jobDirection));
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x000350AC File Offset: 0x000332AC
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				bool value;
				if (this.ArchiveOnly != null && this.PrimaryOnly != null)
				{
					value = this.ArchiveOnly.Value;
				}
				else
				{
					value = this.jobArchiveOnly;
				}
				switch (this.jobDirection)
				{
				case MigrationBatchDirection.Local:
					if (value)
					{
						return MigrationUserRecipientType.MailboxOrMailuser;
					}
					return MigrationUserRecipientType.Mailbox;
				case MigrationBatchDirection.Onboarding:
					return MigrationUserRecipientType.Mailuser;
				case MigrationBatchDirection.Offboarding:
					if (value)
					{
						return MigrationUserRecipientType.Mailuser;
					}
					return MigrationUserRecipientType.Mailbox;
				}
				throw new MigrationDataCorruptionException(string.Format("Unknown batch direction '{0}'", this.jobDirection));
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0003514C File Offset: 0x0003334C
		public string Identifier
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x00035154 File Offset: 0x00033354
		public string LocalMailboxIdentifier
		{
			get
			{
				return this.Identifier;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0003515C File Offset: 0x0003335C
		public int CursorPosition
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x00035164 File Offset: 0x00033364
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0003516C File Offset: 0x0003336C
		public string TargetDatabase { get; private set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00035175 File Offset: 0x00033375
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0003517D File Offset: 0x0003337D
		public string TargetArchiveDatabase { get; private set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x00035186 File Offset: 0x00033386
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0003518E File Offset: 0x0003338E
		public string TargetDeliveryDomain { get; private set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x00035197 File Offset: 0x00033397
		// (set) Token: 0x06000C2E RID: 3118 RVA: 0x0003519F File Offset: 0x0003339F
		public Unlimited<int>? BadItemLimit { get; private set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x000351A8 File Offset: 0x000333A8
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x000351B0 File Offset: 0x000333B0
		public Unlimited<int>? LargeItemLimit { get; private set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000351B9 File Offset: 0x000333B9
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x000351C1 File Offset: 0x000333C1
		public bool? PrimaryOnly { get; private set; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000351CA File Offset: 0x000333CA
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x000351D2 File Offset: 0x000333D2
		public bool? ArchiveOnly { get; private set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x000351DB File Offset: 0x000333DB
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x000351DE File Offset: 0x000333DE
		public string RemoteIdentifier
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000351E4 File Offset: 0x000333E4
		private static string GetValueOrDefault(CsvRow row, string columnName, string defaultValue)
		{
			string text;
			if (!row.TryGetColumnValue(columnName, out text) || string.IsNullOrEmpty(text))
			{
				return defaultValue;
			}
			return text;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00035208 File Offset: 0x00033408
		private static Unlimited<int>? GetUnlimitedValueOrDefault(CsvRow row, string columnName, Unlimited<int>? defaultValue)
		{
			string text;
			if (!row.TryGetColumnValue(columnName, out text) || string.IsNullOrEmpty(text))
			{
				return defaultValue;
			}
			return new Unlimited<int>?(Unlimited<int>.Parse(text));
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00035238 File Offset: 0x00033438
		private static bool GetBoolValue(CsvRow row, string columnName, bool defaultValue)
		{
			string value;
			if (!row.TryGetColumnValue(columnName, out value) || string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}
			return bool.Parse(value);
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00035264 File Offset: 0x00033464
		private static TEnum? GetEnumValue<TEnum>(CsvRow row, string columnName, TEnum? defaultValue) where TEnum : struct
		{
			string value;
			if (!row.TryGetColumnValue(columnName, out value) || string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}
			return new TEnum?((TEnum)((object)Enum.Parse(typeof(TEnum), value)));
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000352A4 File Offset: 0x000334A4
		private void InitializeFromRow(CsvRow row)
		{
			this.TargetDatabase = MoveMigrationDataRow.GetValueOrDefault(row, "TargetDatabase", null);
			this.TargetArchiveDatabase = MoveMigrationDataRow.GetValueOrDefault(row, "TargetArchiveDatabase", null);
			this.BadItemLimit = MoveMigrationDataRow.GetUnlimitedValueOrDefault(row, "BadItemLimit", null);
			this.LargeItemLimit = MoveMigrationDataRow.GetUnlimitedValueOrDefault(row, "LargeItemLimit", null);
			MigrationMailboxType? enumValue = MoveMigrationDataRow.GetEnumValue<MigrationMailboxType>(row, "MailboxType", null);
			if (enumValue != null)
			{
				this.PrimaryOnly = new bool?(enumValue.Value == MigrationMailboxType.PrimaryOnly);
				this.ArchiveOnly = new bool?(enumValue.Value == MigrationMailboxType.ArchiveOnly);
			}
		}

		// Token: 0x0400049D RID: 1181
		private readonly string emailAddress;

		// Token: 0x0400049E RID: 1182
		private readonly MigrationBatchDirection jobDirection;

		// Token: 0x0400049F RID: 1183
		private readonly bool jobArchiveOnly;

		// Token: 0x040004A0 RID: 1184
		private readonly int rowIndex;
	}
}
