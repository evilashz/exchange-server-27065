using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000098 RID: 152
	internal abstract class JobSubscriptionSettingsBase : SubscriptionSettingsBase, IJobSubscriptionSettings, ISubscriptionSettings, IMigrationSerializable
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00026194 File Offset: 0x00024394
		public static void ValidatePrimaryArchiveExclusivity(bool? primaryOnly, bool? archiveOnly)
		{
			if ((primaryOnly != null || archiveOnly != null) && (primaryOnly == null || archiveOnly == null || (primaryOnly == archiveOnly && primaryOnly.Value)))
			{
				throw new ArgumentException("PrimaryOnly and ArchiveOnly must only be specified together, and if they are specified they must be mutually exclusive or both false.");
			}
		}

		// Token: 0x060008C7 RID: 2247
		public abstract void WriteToBatch(MigrationBatch batch);

		// Token: 0x060008C8 RID: 2248 RVA: 0x00026202 File Offset: 0x00024402
		public virtual void WriteExtendedProperties(PersistableDictionary dictionary)
		{
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00026204 File Offset: 0x00024404
		public virtual bool ReadExtendedProperties(PersistableDictionary dictionary)
		{
			return false;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00026208 File Offset: 0x00024408
		internal static JobSubscriptionSettingsBase Create(MigrationType migrationType, bool isPAW)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType == MigrationType.ExchangeOutlookAnywhere)
					{
						return new ExchangeJobSubscriptionSettings();
					}
					if (migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_56;
					}
				}
				else
				{
					if (!isPAW)
					{
						return new IMAPJobSubscriptionSettings();
					}
					return new IMAPPAWJobSubscriptionSettings();
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType == MigrationType.PSTImport)
				{
					return new PSTJobSubscriptionSettings();
				}
				if (migrationType != MigrationType.PublicFolder)
				{
					goto IL_56;
				}
				return new PublicFolderJobSubscriptionSettings();
			}
			return new MoveJobSubscriptionSettings(migrationType == MigrationType.ExchangeLocalMove);
			IL_56:
			return null;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002626C File Offset: 0x0002446C
		internal static PropertyDefinition[] GetPropertyDefinitions(MigrationType migrationType, bool isPAW = false)
		{
			if (migrationType <= MigrationType.ExchangeRemoteMove)
			{
				if (migrationType != MigrationType.IMAP)
				{
					if (migrationType == MigrationType.ExchangeOutlookAnywhere)
					{
						return ExchangeJobSubscriptionSettings.ExchangeJobSubscriptionSettingsPropertyDefinitions;
					}
					if (migrationType != MigrationType.ExchangeRemoteMove)
					{
						goto IL_51;
					}
				}
				else
				{
					if (!isPAW)
					{
						return SubscriptionSettingsBase.SubscriptionSettingsBasePropertyDefinitions;
					}
					return IMAPPAWJobSubscriptionSettings.IMAPJobSubscriptionSettingsPropertyDefinitions;
				}
			}
			else if (migrationType != MigrationType.ExchangeLocalMove)
			{
				if (migrationType == MigrationType.PSTImport)
				{
					return PSTJobSubscriptionSettings.JobSubscriptionSettingsPropertyDefinitions;
				}
				if (migrationType != MigrationType.PublicFolder)
				{
					goto IL_51;
				}
				return PublicFolderJobSubscriptionSettings.DefaultPropertyDefinitions;
			}
			return MoveJobSubscriptionSettings.MoveJobSubscriptionSettingsPropertyDefinitions;
			IL_51:
			return null;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000262CC File Offset: 0x000244CC
		internal static IJobSubscriptionSettings CreateFromBatch(MigrationBatch batch, bool isPAW)
		{
			JobSubscriptionSettingsBase jobSubscriptionSettingsBase = JobSubscriptionSettingsBase.Create(batch.MigrationType, isPAW);
			if (jobSubscriptionSettingsBase == null)
			{
				return null;
			}
			jobSubscriptionSettingsBase.InitalizeFromBatch(batch);
			jobSubscriptionSettingsBase.LastModifiedTime = (ExDateTime)batch.SubscriptionSettingsModified;
			return jobSubscriptionSettingsBase;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00026304 File Offset: 0x00024504
		internal static IJobSubscriptionSettings CreateFromMessage(IMigrationStoreObject message, MigrationType migrationType, PersistableDictionary extendedProperties, bool isPAW)
		{
			JobSubscriptionSettingsBase jobSubscriptionSettingsBase = JobSubscriptionSettingsBase.Create(migrationType, isPAW);
			if (jobSubscriptionSettingsBase != null && (jobSubscriptionSettingsBase.ReadFromMessageItem(message) | jobSubscriptionSettingsBase.ReadExtendedProperties(extendedProperties)))
			{
				return jobSubscriptionSettingsBase;
			}
			return null;
		}

		// Token: 0x060008CE RID: 2254
		protected abstract void InitalizeFromBatch(MigrationBatch batch);
	}
}
