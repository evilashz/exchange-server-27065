using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000D0 RID: 208
	internal class XO1ProvisioningDataStorage : ProvisioningDataStorageBase
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x0002F364 File Offset: 0x0002D564
		public XO1ProvisioningDataStorage(MigrationUserRecipientType recipientType) : base(recipientType, true)
		{
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x0002F36E File Offset: 0x0002D56E
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0002F376 File Offset: 0x0002D576
		public long Puid { get; private set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002F37F File Offset: 0x0002D57F
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x0002F387 File Offset: 0x0002D587
		public string FirstName { get; private set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0002F390 File Offset: 0x0002D590
		// (set) Token: 0x06000B26 RID: 2854 RVA: 0x0002F398 File Offset: 0x0002D598
		public string LastName { get; private set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0002F3A1 File Offset: 0x0002D5A1
		// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0002F3A9 File Offset: 0x0002D5A9
		public ExTimeZone TimeZone { get; private set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0002F3B2 File Offset: 0x0002D5B2
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x0002F3BA File Offset: 0x0002D5BA
		public int LocaleId { get; private set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002F3C3 File Offset: 0x0002D5C3
		// (set) Token: 0x06000B2C RID: 2860 RVA: 0x0002F3CB File Offset: 0x0002D5CB
		public string[] EmailAddresses { get; private set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002F3D4 File Offset: 0x0002D5D4
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x0002F3DC File Offset: 0x0002D5DC
		public long AccountSize { get; private set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002F3E5 File Offset: 0x0002D5E5
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return XO1ProvisioningDataStorage.XO1ProvisioningDataPropertyDefinitions;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0002F3EC File Offset: 0x0002D5EC
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.Puid = message.GetValueOrDefault<long>(MigrationBatchMessageSchema.MigrationJobItemPuid, 0L);
			this.TimeZone = MigrationHelper.GetExTimeZoneProperty(message, MigrationBatchMessageSchema.MigrationJobItemTimeZone);
			this.LocaleId = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobItemLocaleId, 0);
			this.AccountSize = message.GetValueOrDefault<long>(MigrationBatchMessageSchema.MigrationJobItemAccountSize, 0L);
			string valueOrDefault = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemAliases, null);
			if (!string.IsNullOrEmpty(valueOrDefault))
			{
				this.EmailAddresses = valueOrDefault.Split(new char[]
				{
					'\u0001'
				});
			}
			this.FirstName = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemFirstName, null);
			this.LastName = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemLastName, null);
			return true;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002F494 File Offset: 0x0002D694
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemPuid] = this.Puid;
			message[MigrationBatchMessageSchema.MigrationJobItemAccountSize] = this.AccountSize;
			message[MigrationBatchMessageSchema.MigrationJobItemLocaleId] = this.LocaleId;
			if (this.TimeZone != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemTimeZone] = this.TimeZone.Id;
			}
			if (!string.IsNullOrEmpty(this.FirstName))
			{
				message[MigrationBatchMessageSchema.MigrationJobItemFirstName] = this.FirstName;
			}
			if (!string.IsNullOrEmpty(this.LastName))
			{
				message[MigrationBatchMessageSchema.MigrationJobItemLastName] = this.LastName;
			}
			if (this.EmailAddresses != null && this.EmailAddresses.Length > 0)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemAliases] = string.Join(string.Empty + '\u0001', this.EmailAddresses);
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002F578 File Offset: 0x0002D778
		public override ProvisioningDataStorageBase Clone()
		{
			return new XO1ProvisioningDataStorage(this.RecipientType)
			{
				Puid = this.Puid,
				FirstName = this.FirstName,
				LastName = this.LastName,
				TimeZone = this.TimeZone,
				LocaleId = this.LocaleId,
				EmailAddresses = this.EmailAddresses,
				AccountSize = this.AccountSize
			};
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		public override void UpdateFromDataRow(IMigrationDataRow dataRow)
		{
			XO1MigrationDataRow xo1MigrationDataRow = dataRow as XO1MigrationDataRow;
			if (xo1MigrationDataRow == null)
			{
				throw new ArgumentException("expected a XO1MigrationDataRow", "dataRow");
			}
			this.Puid = xo1MigrationDataRow.Puid;
			this.FirstName = xo1MigrationDataRow.FirstName;
			this.LastName = xo1MigrationDataRow.LastName;
			this.TimeZone = xo1MigrationDataRow.TimeZone;
			this.LocaleId = xo1MigrationDataRow.LocaleId;
			this.EmailAddresses = xo1MigrationDataRow.EmailAddresses;
			this.AccountSize = xo1MigrationDataRow.AccountSize;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002F664 File Offset: 0x0002D864
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			return new XElement("XO1ProvisioningDataStorage", new object[]
			{
				new XElement("Puid", this.Puid),
				new XElement("FirstName", this.FirstName),
				new XElement("LastName", this.LastName),
				new XElement("TimeZone", this.TimeZone),
				new XElement("LocaleId", this.LocaleId),
				new XElement("EmailAddresses", string.Join(", ", this.EmailAddresses ?? new string[0])),
				new XElement("AccountSize", this.AccountSize)
			});
		}

		// Token: 0x0400043F RID: 1087
		internal static readonly PropertyDefinition[] XO1ProvisioningDataPropertyDefinitions = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobItemPuid,
			MigrationBatchMessageSchema.MigrationJobItemFirstName,
			MigrationBatchMessageSchema.MigrationJobItemLastName,
			MigrationBatchMessageSchema.MigrationJobItemTimeZone,
			MigrationBatchMessageSchema.MigrationJobItemLocaleId,
			MigrationBatchMessageSchema.MigrationJobItemAliases,
			MigrationBatchMessageSchema.MigrationJobItemAccountSize
		};
	}
}
