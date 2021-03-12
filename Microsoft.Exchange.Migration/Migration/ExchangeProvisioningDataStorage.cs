using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000A4 RID: 164
	internal class ExchangeProvisioningDataStorage : ProvisioningDataStorageBase
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00027837 File Offset: 0x00025A37
		public ExchangeProvisioningDataStorage(MigrationUserRecipientType recipientType, bool isPAW = true) : base(recipientType, isPAW)
		{
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00027841 File Offset: 0x00025A41
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00027849 File Offset: 0x00025A49
		public ExchangeMigrationRecipient ExchangeRecipient { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00027852 File Offset: 0x00025A52
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0002785A File Offset: 0x00025A5A
		public string EncryptedPassword { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00027863 File Offset: 0x00025A63
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0002786B File Offset: 0x00025A6B
		public bool ForceChangePassword { get; private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00027874 File Offset: 0x00025A74
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return ExchangeProvisioningDataStorage.ExchangeProvisioningDataPropertyDefinitions;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002787C File Offset: 0x00025A7C
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			try
			{
				this.ExchangeRecipient = ExchangeMigrationRecipient.Create(message, this.RecipientType, this.IsPAW);
			}
			catch (InvalidDataException)
			{
				return false;
			}
			this.EncryptedPassword = MigrationHelper.GetProperty<string>(message, MigrationBatchMessageSchema.MigrationJobItemExchangeMbxEncryptedPassword, false);
			this.ForceChangePassword = message.GetValueOrDefault<bool>(MigrationBatchMessageSchema.MigrationJobItemForceChangePassword, false);
			return true;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000278E0 File Offset: 0x00025AE0
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			if (this.ExchangeRecipient != null)
			{
				this.ExchangeRecipient.WriteToMessageItem(message, loaded);
			}
			message[MigrationBatchMessageSchema.MigrationJobItemForceChangePassword] = this.ForceChangePassword;
			if (string.IsNullOrEmpty(this.EncryptedPassword))
			{
				message.Delete(MigrationBatchMessageSchema.MigrationJobItemExchangeMbxEncryptedPassword);
				return;
			}
			message[MigrationBatchMessageSchema.MigrationJobItemExchangeMbxEncryptedPassword] = this.EncryptedPassword;
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00027944 File Offset: 0x00025B44
		public override ProvisioningDataStorageBase Clone()
		{
			return new ExchangeProvisioningDataStorage(this.RecipientType, this.IsPAW)
			{
				ExchangeRecipient = this.ExchangeRecipient,
				EncryptedPassword = this.EncryptedPassword,
				ForceChangePassword = this.ForceChangePassword
			};
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00027988 File Offset: 0x00025B88
		public override void UpdateFromDataRow(IMigrationDataRow dataRow)
		{
			ExchangeMigrationDataRow exchangeMigrationDataRow = dataRow as ExchangeMigrationDataRow;
			if (exchangeMigrationDataRow == null)
			{
				throw new ArgumentException("expected a ExchangeMigrationDataRow", "dataRow");
			}
			this.EncryptedPassword = exchangeMigrationDataRow.EncryptedPassword;
			this.ForceChangePassword = exchangeMigrationDataRow.ForceChangePassword;
			NspiMigrationDataRow nspiMigrationDataRow = exchangeMigrationDataRow as NspiMigrationDataRow;
			if (nspiMigrationDataRow != null)
			{
				this.ExchangeRecipient = nspiMigrationDataRow.Recipient;
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x000279E0 File Offset: 0x00025BE0
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("ExchangeProvisioningDataStorage", new object[]
			{
				new XElement("ForceChangePassword", this.ForceChangePassword),
				new XElement("EncryptedPasswordSet", !string.IsNullOrEmpty(this.EncryptedPassword))
			});
			if (this.ExchangeRecipient != null)
			{
				xelement.Add(this.ExchangeRecipient.GetDiagnosticInfo(dataProvider, argument));
			}
			return xelement;
		}

		// Token: 0x04000390 RID: 912
		internal static readonly PropertyDefinition[] ExchangeProvisioningDataPropertyDefinitions = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			ExchangeMigrationRecipient.ExchangeMigrationRecipientPropertyDefinitions,
			new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemExchangeMbxEncryptedPassword,
				MigrationBatchMessageSchema.MigrationJobItemForceChangePassword
			}
		});
	}
}
