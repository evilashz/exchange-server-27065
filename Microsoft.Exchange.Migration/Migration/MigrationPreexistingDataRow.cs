using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F6 RID: 246
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MigrationPreexistingDataRow : IMigrationDataRow
	{
		// Token: 0x06000C42 RID: 3138 RVA: 0x00035510 File Offset: 0x00033710
		internal MigrationPreexistingDataRow(int rowIndex, MigrationJobItem originalJobItem)
		{
			this.CursorPosition = rowIndex;
			this.Identifier = originalJobItem.Identifier;
			this.MigrationType = originalJobItem.MigrationType;
			this.RecipientType = originalJobItem.RecipientType;
			this.RemoteIdentifier = originalJobItem.RemoteIdentifier;
			this.LocalMailboxIdentifier = originalJobItem.LocalMailboxIdentifier;
			if (originalJobItem.ProvisioningData != null)
			{
				this.ProvisioningData = originalJobItem.ProvisioningData.Clone();
			}
			if (originalJobItem.SubscriptionSettings != null)
			{
				this.SubscriptionSettings = ((JobItemSubscriptionSettingsBase)originalJobItem.SubscriptionSettings).Clone();
			}
			this.SubscriptionId = originalJobItem.SubscriptionId;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x000355A9 File Offset: 0x000337A9
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x000355B1 File Offset: 0x000337B1
		public ISubscriptionSettings SubscriptionSettings { get; private set; }

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x000355BA File Offset: 0x000337BA
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x000355C2 File Offset: 0x000337C2
		public ISubscriptionId SubscriptionId { get; private set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x000355CB File Offset: 0x000337CB
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x000355D3 File Offset: 0x000337D3
		public ProvisioningDataStorageBase ProvisioningData { get; private set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000355DC File Offset: 0x000337DC
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x000355E4 File Offset: 0x000337E4
		public MigrationType MigrationType { get; private set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x000355ED File Offset: 0x000337ED
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x000355F5 File Offset: 0x000337F5
		public MigrationUserRecipientType RecipientType { get; private set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000355FE File Offset: 0x000337FE
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x00035606 File Offset: 0x00033806
		public int CursorPosition { get; internal set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0003560F File Offset: 0x0003380F
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x00035617 File Offset: 0x00033817
		public string Identifier { get; private set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00035620 File Offset: 0x00033820
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x00035628 File Offset: 0x00033828
		public string LocalMailboxIdentifier { get; private set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00035631 File Offset: 0x00033831
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00035639 File Offset: 0x00033839
		public string RemoteIdentifier { get; private set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00035642 File Offset: 0x00033842
		public bool SupportsRemoteIdentifier
		{
			get
			{
				return this.RemoteIdentifier != null;
			}
		}
	}
}
