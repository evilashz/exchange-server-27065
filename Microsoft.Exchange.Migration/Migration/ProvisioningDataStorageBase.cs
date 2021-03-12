using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000087 RID: 135
	internal abstract class ProvisioningDataStorageBase : IStepSettings, IMigrationSerializable
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x00022BF3 File Offset: 0x00020DF3
		public ProvisioningDataStorageBase(MigrationUserRecipientType recipientType, bool isPAW = true)
		{
			this.RecipientType = recipientType;
			this.IsPAW = isPAW;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600078E RID: 1934
		public abstract PropertyDefinition[] PropertyDefinitions { get; }

		// Token: 0x0600078F RID: 1935
		public abstract bool ReadFromMessageItem(IMigrationStoreObject message);

		// Token: 0x06000790 RID: 1936
		public abstract void WriteToMessageItem(IMigrationStoreObject message, bool loaded);

		// Token: 0x06000791 RID: 1937
		public abstract ProvisioningDataStorageBase Clone();

		// Token: 0x06000792 RID: 1938
		public abstract void UpdateFromDataRow(IMigrationDataRow dataRow);

		// Token: 0x06000793 RID: 1939
		public abstract XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument);

		// Token: 0x06000794 RID: 1940 RVA: 0x00022C0C File Offset: 0x00020E0C
		internal static ProvisioningDataStorageBase Create(MigrationType migrationType, MigrationUserRecipientType recipientType, bool isPAW)
		{
			switch (migrationType)
			{
			case MigrationType.XO1:
				MigrationUtil.AssertOrThrow(isPAW, "How do we have XO1 running on pre-PAW?!", new object[0]);
				return new XO1ProvisioningDataStorage(recipientType);
			case MigrationType.ExchangeOutlookAnywhere:
				return new ExchangeProvisioningDataStorage(recipientType, isPAW);
			}
			return null;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00022C54 File Offset: 0x00020E54
		internal static ProvisioningDataStorageBase CreateFromMessage(IMigrationStoreObject message, MigrationType migrationType, MigrationUserRecipientType recipientType, bool isPAW = false)
		{
			ProvisioningDataStorageBase provisioningDataStorageBase = ProvisioningDataStorageBase.Create(migrationType, recipientType, isPAW);
			if (provisioningDataStorageBase != null && provisioningDataStorageBase.ReadFromMessageItem(message))
			{
				return provisioningDataStorageBase;
			}
			return null;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00022C7C File Offset: 0x00020E7C
		internal static ProvisioningDataStorageBase CreateFromDataRow(IMigrationDataRow dataRow, bool isPAW)
		{
			if (dataRow is InvalidDataRow)
			{
				return null;
			}
			MigrationPreexistingDataRow migrationPreexistingDataRow = dataRow as MigrationPreexistingDataRow;
			if (migrationPreexistingDataRow != null)
			{
				return migrationPreexistingDataRow.ProvisioningData;
			}
			ProvisioningDataStorageBase provisioningDataStorageBase = ProvisioningDataStorageBase.Create(dataRow.MigrationType, dataRow.RecipientType, isPAW);
			if (provisioningDataStorageBase == null)
			{
				return null;
			}
			provisioningDataStorageBase.UpdateFromDataRow(dataRow);
			return provisioningDataStorageBase;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00022CC4 File Offset: 0x00020EC4
		internal static PropertyDefinition[] GetPropertyDefinitions(MigrationType migrationType)
		{
			switch (migrationType)
			{
			case MigrationType.XO1:
				return XO1ProvisioningDataStorage.XO1ProvisioningDataPropertyDefinitions;
			case MigrationType.ExchangeOutlookAnywhere:
				return ExchangeProvisioningDataStorage.ExchangeProvisioningDataPropertyDefinitions;
			}
			return null;
		}

		// Token: 0x04000335 RID: 821
		protected readonly MigrationUserRecipientType RecipientType;

		// Token: 0x04000336 RID: 822
		protected readonly bool IsPAW;
	}
}
