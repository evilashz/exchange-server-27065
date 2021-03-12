using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200006C RID: 108
	internal class MigrationJobItemSummary
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001CFA1 File Offset: 0x0001B1A1
		public bool IsPAW
		{
			get
			{
				return this.Version >= 4L;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001CFB8 File Offset: 0x0001B1B8
		public long Version { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001CFC1 File Offset: 0x0001B1C1
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001CFC9 File Offset: 0x0001B1C9
		public long ItemsSynced { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001CFD2 File Offset: 0x0001B1D2
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		public long ItemsSkipped { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001CFE3 File Offset: 0x0001B1E3
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001CFEB File Offset: 0x0001B1EB
		public MigrationUserRecipientType RecipientType { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001CFFC File Offset: 0x0001B1FC
		public Guid? JobItemGuid { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001D005 File Offset: 0x0001B205
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001D00D File Offset: 0x0001B20D
		public Guid? BatchGuid { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001D016 File Offset: 0x0001B216
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001D01E File Offset: 0x0001B21E
		public Guid? MailboxGuid { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001D027 File Offset: 0x0001B227
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x0001D02F File Offset: 0x0001B22F
		public string MailboxLegacyDN { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001D038 File Offset: 0x0001B238
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x0001D040 File Offset: 0x0001B240
		public string Identifier { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001D049 File Offset: 0x0001B249
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x0001D051 File Offset: 0x0001B251
		public string LocalMailboxIdentifier { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001D05A File Offset: 0x0001B25A
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x0001D062 File Offset: 0x0001B262
		public Guid? MrsId { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001D06B File Offset: 0x0001B26B
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x0001D073 File Offset: 0x0001B273
		public ExDateTime? LastSuccessfulSyncTime { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0001D07C File Offset: 0x0001B27C
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x0001D084 File Offset: 0x0001B284
		public ExDateTime? LastSubscriptionCheckTime { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001D08D File Offset: 0x0001B28D
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x0001D095 File Offset: 0x0001B295
		public MigrationUserStatus? Status { get; set; }

		// Token: 0x06000656 RID: 1622 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		public static MigrationJobItemSummary LoadFromRow(object[] propertyValues)
		{
			if (propertyValues == null)
			{
				return null;
			}
			MigrationJobItemSummary migrationJobItemSummary = new MigrationJobItemSummary();
			for (int i = 0; i < MigrationUser.PropertyDefinitions.Length; i++)
			{
				if (propertyValues[i] != null && !(propertyValues[i] is PropertyError))
				{
					if (MigrationUser.PropertyDefinitions[i] == StoreObjectSchema.ItemClass && !string.Equals((string)propertyValues[i], MigrationBatchMessageSchema.MigrationJobItemClass))
					{
						return null;
					}
					if (MigrationUser.PropertyDefinitions[i] == MigrationUser.BatchIdPropertyDefinition)
					{
						migrationJobItemSummary.BatchGuid = (Guid?)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.IdPropertyDefinition)
					{
						migrationJobItemSummary.JobItemGuid = (Guid?)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.IdentifierPropertyDefinition)
					{
						migrationJobItemSummary.Identifier = (string)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.LocalMailboxIdentifierPropertyDefinition)
					{
						migrationJobItemSummary.LocalMailboxIdentifier = (string)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.RecipientTypePropertyDefinition)
					{
						migrationJobItemSummary.RecipientType = (MigrationUserRecipientType)((int)propertyValues[i]);
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.ItemsSkippedPropertyDefinition)
					{
						migrationJobItemSummary.ItemsSkipped = (long)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.ItemsSyncedPropertyDefinition)
					{
						migrationJobItemSummary.ItemsSynced = (long)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.MailboxGuidPropertyDefinition)
					{
						migrationJobItemSummary.MailboxGuid = new Guid?((Guid)propertyValues[i]);
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.MailboxLegacyDNPropertyDefinition)
					{
						migrationJobItemSummary.MailboxLegacyDN = (string)propertyValues[i];
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.MRSIdPropertyDefinition)
					{
						migrationJobItemSummary.MrsId = new Guid?((Guid)propertyValues[i]);
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.LastSuccessfulSyncTimePropertyDefinition)
					{
						ExDateTime? validExDateTime = MigrationHelperBase.GetValidExDateTime((ExDateTime?)propertyValues[i]);
						if (validExDateTime != null)
						{
							migrationJobItemSummary.LastSuccessfulSyncTime = new ExDateTime?(validExDateTime.Value);
						}
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.StatusPropertyDefinition)
					{
						migrationJobItemSummary.Status = new MigrationUserStatus?((MigrationUserStatus)((int)propertyValues[i]));
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.SubscriptionLastCheckedPropertyDefinition)
					{
						ExDateTime? validExDateTime2 = MigrationHelperBase.GetValidExDateTime((ExDateTime?)propertyValues[i]);
						if (validExDateTime2 != null)
						{
							migrationJobItemSummary.LastSubscriptionCheckTime = new ExDateTime?(validExDateTime2.Value);
						}
					}
					else if (MigrationUser.PropertyDefinitions[i] == MigrationUser.VersionPropertyDefinition)
					{
						migrationJobItemSummary.Version = (long)propertyValues[i];
					}
				}
			}
			return migrationJobItemSummary;
		}
	}
}
