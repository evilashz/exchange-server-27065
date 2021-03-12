using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000095 RID: 149
	internal abstract class JobItemSubscriptionSettingsBase : SubscriptionSettingsBase, IStepSettings, IMigrationSerializable
	{
		// Token: 0x060008A9 RID: 2217
		public abstract JobItemSubscriptionSettingsBase Clone();

		// Token: 0x060008AA RID: 2218
		public abstract void UpdateFromDataRow(IMigrationDataRow request);

		// Token: 0x060008AB RID: 2219 RVA: 0x00025D68 File Offset: 0x00023F68
		internal static JobItemSubscriptionSettingsBase CreateFromMessage(IMigrationStoreObject message, MigrationType migrationType)
		{
			JobItemSubscriptionSettingsBase jobItemSubscriptionSettingsBase = JobItemSubscriptionSettingsBase.Create(migrationType);
			if (jobItemSubscriptionSettingsBase != null && jobItemSubscriptionSettingsBase.ReadFromMessageItem(message))
			{
				return jobItemSubscriptionSettingsBase;
			}
			return null;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00025D8C File Offset: 0x00023F8C
		internal static JobItemSubscriptionSettingsBase CreateFromDataRow(IMigrationDataRow dataRow)
		{
			if (dataRow is InvalidDataRow)
			{
				return null;
			}
			MigrationPreexistingDataRow migrationPreexistingDataRow = dataRow as MigrationPreexistingDataRow;
			if (migrationPreexistingDataRow != null)
			{
				return (JobItemSubscriptionSettingsBase)migrationPreexistingDataRow.SubscriptionSettings;
			}
			JobItemSubscriptionSettingsBase jobItemSubscriptionSettingsBase = JobItemSubscriptionSettingsBase.Create(dataRow.MigrationType);
			if (jobItemSubscriptionSettingsBase == null)
			{
				return null;
			}
			jobItemSubscriptionSettingsBase.UpdateFromDataRow(dataRow);
			if (jobItemSubscriptionSettingsBase.IsEmpty)
			{
				return null;
			}
			return jobItemSubscriptionSettingsBase;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00025DDC File Offset: 0x00023FDC
		internal static JobItemSubscriptionSettingsBase Create(MigrationType migrationType)
		{
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType == MigrationType.IMAP)
				{
					return new IMAPJobItemSubscriptionSettings();
				}
				if (migrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					return new ExchangeJobItemSubscriptionSettings();
				}
			}
			else
			{
				if (migrationType == MigrationType.ExchangeRemoteMove || migrationType == MigrationType.ExchangeLocalMove)
				{
					return new MoveJobItemSubscriptionSettings();
				}
				if (migrationType == MigrationType.PSTImport)
				{
					return new PSTJobItemSubscriptionSettings();
				}
			}
			return null;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00025E24 File Offset: 0x00024024
		internal static PropertyDefinition[] GetPropertyDefinitions(MigrationType migrationType)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType == MigrationType.IMAP)
				{
					return IMAPJobItemSubscriptionSettings.ImapJobItemSubscriptionSettingsPropertyDefinitions;
				}
				if (migrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					goto IL_36;
				}
				if (migrationType != MigrationType.ExchangeRemoteMove)
				{
					goto IL_42;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType == MigrationType.PSTImport)
				{
					return PSTJobItemSubscriptionSettings.PSTJobItemSubscriptionSettingsPropertyDefinitions;
				}
				if (migrationType != MigrationType.PublicFolder)
				{
					goto IL_42;
				}
				goto IL_36;
			}
			return MoveJobItemSubscriptionSettings.MoveJobItemSubscriptionSettingsPropertyDefinitions;
			IL_36:
			return ExchangeJobItemSubscriptionSettings.ExchangeJobItemSubscriptionSettingsPropertyDefinitions;
			IL_42:
			return null;
		}
	}
}
