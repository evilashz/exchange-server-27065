using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Migration.Rpc;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000020 RID: 32
	[Cmdlet("Repair", "Migration", DefaultParameterSetName = "UpdateMigrationUser")]
	public sealed class RepairMigration : MigrationOrganizationTaskBase
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000075A8 File Offset: 0x000057A8
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000075CE File Offset: 0x000057CE
		[Parameter(Mandatory = true, ParameterSetName = "EnableConfigFeatures")]
		[Parameter(Mandatory = true, ParameterSetName = "AddOrgUpgradeConstraint")]
		public SwitchParameter Add
		{
			get
			{
				return (SwitchParameter)(base.Fields["Add"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Add"] = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000075E6 File Offset: 0x000057E6
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000760C File Offset: 0x0000580C
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationReport")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationStoreObject")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationCacheEntry")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationFolder")]
		public SwitchParameter Update
		{
			get
			{
				return (SwitchParameter)(base.Fields["Update"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Update"] = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007624 File Offset: 0x00005824
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000764A File Offset: 0x0000584A
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationFolder")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationBatch")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "ExpireOrgUpgradeConstraint")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationReport")]
		[Parameter(Mandatory = true, ParameterSetName = "DisableConfigFeatures")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationStoreObject")]
		public SwitchParameter Remove
		{
			get
			{
				return (SwitchParameter)(base.Fields["Remove"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Remove"] = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007662 File Offset: 0x00005862
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007688 File Offset: 0x00005888
		[Parameter(Mandatory = true, ParameterSetName = "RevertMigrationBatch")]
		[Parameter(Mandatory = true, ParameterSetName = "RevertMigrationUser")]
		public SwitchParameter Revert
		{
			get
			{
				return (SwitchParameter)(base.Fields["Revert"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Revert"] = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000076A0 File Offset: 0x000058A0
		// (set) Token: 0x0600018E RID: 398 RVA: 0x000076C6 File Offset: 0x000058C6
		[Parameter(Mandatory = true, ParameterSetName = "SyncMigrationUser")]
		public SwitchParameter SyncSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["SyncSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SyncSubscription"] = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000076DE File Offset: 0x000058DE
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007704 File Offset: 0x00005904
		[Parameter(Mandatory = true, ParameterSetName = "ResumeMigrationUser")]
		public SwitchParameter ResumeSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["ResumeSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ResumeSubscription"] = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000771C File Offset: 0x0000591C
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007742 File Offset: 0x00005942
		[Parameter(Mandatory = true, ParameterSetName = "FlushMigrationUser")]
		public SwitchParameter FlushSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["FlushSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["FlushSubscription"] = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000775A File Offset: 0x0000595A
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007780 File Offset: 0x00005980
		[Parameter(Mandatory = true, ParameterSetName = "DisableSubscription")]
		public SwitchParameter DisableSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisableSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DisableSubscription"] = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007798 File Offset: 0x00005998
		// (set) Token: 0x06000196 RID: 406 RVA: 0x000077BE File Offset: 0x000059BE
		[Parameter(Mandatory = true, ParameterSetName = "DeleteSubscription")]
		public SwitchParameter DeleteSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["DeleteSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DeleteSubscription"] = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000077D6 File Offset: 0x000059D6
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000077FC File Offset: 0x000059FC
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSubscriptionStatus")]
		public SwitchParameter UpdateSyncSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateSyncSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UpdateSyncSubscription"] = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007814 File Offset: 0x00005A14
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000783A File Offset: 0x00005A3A
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSubscription")]
		public SwitchParameter RemoveSyncSubscription
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveSyncSubscription"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveSyncSubscription"] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007852 File Offset: 0x00005A52
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007878 File Offset: 0x00005A78
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationCacheEntry")]
		public SwitchParameter CacheEntry
		{
			get
			{
				return (SwitchParameter)(base.Fields["CacheEntry"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CacheEntry"] = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007890 File Offset: 0x00005A90
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000078B6 File Offset: 0x00005AB6
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000078CE File Offset: 0x00005ACE
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000078E5 File Offset: 0x00005AE5
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationReport")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationStoreObject")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationReport")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationStoreObject")]
		[ValidateNotNullOrEmpty]
		public string Property
		{
			get
			{
				return (string)base.Fields["Property"];
			}
			set
			{
				base.Fields["Property"] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000078F8 File Offset: 0x00005AF8
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000790F File Offset: 0x00005B0F
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationReport")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationStoreObject")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationReport")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationStoreObject")]
		[ValidateNotNullOrEmpty]
		public string ExtendedProperty
		{
			get
			{
				return (string)base.Fields["ExtendedProperty"];
			}
			set
			{
				base.Fields["ExtendedProperty"] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007922 File Offset: 0x00005B22
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007939 File Offset: 0x00005B39
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSubscriptionStatus")]
		[ValidateNotNullOrEmpty]
		public string Status
		{
			get
			{
				return (string)base.Fields["Status"];
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000794C File Offset: 0x00005B4C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007963 File Offset: 0x00005B63
		[Parameter(Mandatory = false, ParameterSetName = "RemoveSubscription")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationReport")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSubscriptionStatus")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationStoreObject")]
		public string PropertyType
		{
			get
			{
				return (string)base.Fields["PropertyType"];
			}
			set
			{
				base.Fields["PropertyType"] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007976 File Offset: 0x00005B76
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007988 File Offset: 0x00005B88
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationBatch")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationStoreObject")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationReport")]
		public object PropertyValue
		{
			get
			{
				return base.Fields["Value"];
			}
			set
			{
				base.Fields["Value"] = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000799B File Offset: 0x00005B9B
		// (set) Token: 0x060001AA RID: 426 RVA: 0x000079B2 File Offset: 0x00005BB2
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMigrationStoreObject")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMigrationStoreObject")]
		public string Attachment
		{
			get
			{
				return (string)base.Fields["Attachment"];
			}
			set
			{
				base.Fields["Attachment"] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000079C5 File Offset: 0x00005BC5
		// (set) Token: 0x060001AC RID: 428 RVA: 0x000079DC File Offset: 0x00005BDC
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "RevertMigrationBatch")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationBatch")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationBatch")]
		public MigrationBatchIdParameter BatchId
		{
			get
			{
				return (MigrationBatchIdParameter)base.Fields["BatchId"];
			}
			set
			{
				base.Fields["BatchId"] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000079EF File Offset: 0x00005BEF
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00007A06 File Offset: 0x00005C06
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationStoreObject")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationStoreObject")]
		public string StoreObjectId
		{
			get
			{
				return (string)base.Fields["StoreObjectId"];
			}
			set
			{
				base.Fields["StoreObjectId"] = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00007A19 File Offset: 0x00005C19
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00007A30 File Offset: 0x00005C30
		[Parameter(Mandatory = true, ParameterSetName = "RevertMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "DisableSubscription")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSubscription")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSubscriptionStatus")]
		[Parameter(Mandatory = true, ParameterSetName = "FlushMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "ResumeMigrationUser")]
		[Parameter(Mandatory = true, ParameterSetName = "SyncMigrationUser")]
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "DeleteSubscription")]
		public MigrationUserIdParameter UserId
		{
			get
			{
				return (MigrationUserIdParameter)base.Fields["UserId"];
			}
			set
			{
				base.Fields["UserId"] = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007A43 File Offset: 0x00005C43
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00007A5A File Offset: 0x00005C5A
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSubscription")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSubscriptionStatus")]
		[ValidateNotNull]
		[ValidateNotEmptyGuid]
		public Guid? SubscriptionId
		{
			get
			{
				return (Guid?)base.Fields["SubscriptionId"];
			}
			set
			{
				base.Fields["SubscriptionId"] = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007A72 File Offset: 0x00005C72
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00007A89 File Offset: 0x00005C89
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationFolder")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationFolder")]
		[ValidateNotNull]
		public MigrationFolderName? FolderId
		{
			get
			{
				return (MigrationFolderName?)base.Fields["FolderId"];
			}
			set
			{
				base.Fields["FolderId"] = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00007AA1 File Offset: 0x00005CA1
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00007AB8 File Offset: 0x00005CB8
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMigrationReport")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMigrationReport")]
		public MigrationReportId ReportId
		{
			get
			{
				return (MigrationReportId)base.Fields["ReportId"];
			}
			set
			{
				base.Fields["ReportId"] = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00007ACB File Offset: 0x00005CCB
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00007AE2 File Offset: 0x00005CE2
		[Parameter(Mandatory = false, ParameterSetName = "ExpireOrgUpgradeConstraint")]
		public DateTime? ConstraintExpiryDate
		{
			get
			{
				return (DateTime?)base.Fields["ConstraintExpiryDate"];
			}
			set
			{
				base.Fields["ConstraintExpiryDate"] = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007AFA File Offset: 0x00005CFA
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00007B20 File Offset: 0x00005D20
		[Parameter(Mandatory = true, ParameterSetName = "AddOrgUpgradeConstraint")]
		[Parameter(Mandatory = true, ParameterSetName = "ExpireOrgUpgradeConstraint")]
		public SwitchParameter UpgradeConstraint
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpgradeConstraint"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UpgradeConstraint"] = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00007B38 File Offset: 0x00005D38
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00007B4F File Offset: 0x00005D4F
		[Parameter(Mandatory = true, ParameterSetName = "EnableConfigFeatures")]
		[Parameter(Mandatory = true, ParameterSetName = "DisableConfigFeatures")]
		public MigrationFeature? Feature
		{
			get
			{
				return (MigrationFeature?)base.Fields["Feature"];
			}
			set
			{
				base.Fields["Feature"] = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007B67 File Offset: 0x00005D67
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRepairMigration;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007B70 File Offset: 0x00005D70
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!string.IsNullOrEmpty(this.Status))
			{
				if (!string.IsNullOrEmpty(this.Property) || !string.IsNullOrEmpty(this.ExtendedProperty) || !string.IsNullOrEmpty(this.PropertyType))
				{
					this.WriteError(Strings.ValidateRepairUpdateStatus);
					return;
				}
			}
			else if (!string.IsNullOrEmpty(this.Property) || !string.IsNullOrEmpty(this.ExtendedProperty))
			{
				if (this.Update)
				{
					if (!base.Fields.IsChanged("Value"))
					{
						this.WriteInternalError("Please pass in a value when updating a property");
						return;
					}
					if (!string.IsNullOrEmpty(this.ExtendedProperty) && string.IsNullOrEmpty(this.PropertyType))
					{
						this.WriteInternalError(string.Format("Please pass in a type when updating an extended property '{0}'", this.ExtendedProperty));
						return;
					}
					if (string.IsNullOrEmpty(this.ExtendedProperty) && !string.IsNullOrEmpty(this.PropertyType))
					{
						this.WriteInternalError("Type is not needed when setting a regular property.  It's pulled form the property definition");
						return;
					}
				}
				else if (this.Remove)
				{
					if (!base.Fields.IsChanged("Value"))
					{
						this.WriteInternalError("the Value isn't needed when removing a property");
						return;
					}
					if (!string.IsNullOrEmpty(this.PropertyType))
					{
						this.WriteInternalError("the Type isn't needed when removing a property");
						return;
					}
				}
			}
			else if (this.Update && !this.CacheEntry && string.IsNullOrEmpty(this.Attachment))
			{
				this.WriteError(Strings.ValidateRepairUpdateMissingStatus);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007CE1 File Offset: 0x00005EE1
		private bool ShouldRepairContinue(LocalizedString message)
		{
			return this.Force || base.ShouldContinue(message);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007F2C File Offset: 0x0000612C
		protected override void InternalProcessRecord()
		{
			Action updateOperation = delegate()
			{
				if (this.SubscriptionId != null)
				{
					this.InternalProcessSubscription(this.SubscriptionId.Value);
					return;
				}
				if (this.CacheEntry)
				{
					if (!this.ShouldRepairContinue(Strings.ConfirmRepairUpdateCacheEntry(base.Organization.ToString())))
					{
						return;
					}
					this.UpdateCacheEntry();
					return;
				}
				else
				{
					if (this.BatchId != null)
					{
						MigrationJob migrationJob = this.GetMigrationJob(null);
						if (migrationJob == null)
						{
							this.WriteError(Strings.MigrationJobNotFound(this.BatchId.ToString()));
						}
						this.InternalProcessJob(migrationJob);
						return;
					}
					if (this.UserId != null)
					{
						if (this.Remove && string.IsNullOrEmpty(this.Property) && string.IsNullOrEmpty(this.ExtendedProperty))
						{
							if (!this.ShouldRepairContinue(Strings.ConfirmRepairRemoveUsers(this.UserId.ToString())))
							{
								return;
							}
							using (List<MigrationJobItem>.Enumerator enumerator = this.GetMigrationJobItems().GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									MigrationJobItem migrationJobItem = enumerator.Current;
									this.WriteWarning(Strings.WarnRepairRemovingUser(migrationJobItem.ToString()));
									migrationJobItem.Delete(base.DataProvider);
								}
								return;
							}
						}
						MigrationJobItem migrationJobItem2 = this.GetMigrationJobItem();
						this.InternalProcessJobItem(migrationJobItem2);
						return;
					}
					if (this.FolderId != null)
					{
						this.InternalProcessFolder(this.FolderId.Value);
						return;
					}
					if (this.ReportId != null)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairRemoveReport(this.ReportId.ToString())))
						{
							return;
						}
						this.RemoveReportItem(this.ReportId);
						return;
					}
					else
					{
						if (this.StoreObjectId != null)
						{
							this.InternalProcessStoreObject();
							return;
						}
						if (this.UpgradeConstraint)
						{
							this.UpdateOrganizationUpgradeConstraint(this.Add);
							return;
						}
						if (this.Feature != null)
						{
							if (this.Add)
							{
								this.EnableMigrationFeatures(this.Feature.Value);
								return;
							}
							if (this.Remove)
							{
								this.DisableMigrationFeatures(this.Feature.Value);
							}
						}
					}
					return;
				}
			};
			try
			{
				MigrationHelper.RunUpdateOperation(updateOperation);
			}
			catch (LocalizedException exception)
			{
				this.WriteError(exception);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007F68 File Offset: 0x00006168
		private bool TryUpdateMigrationReport(MigrationPersistableBase migrationObject, string msg)
		{
			MigrationJob migrationJob = migrationObject as MigrationJob;
			if (migrationJob == null)
			{
				return false;
			}
			migrationJob.ReportData.Append(Strings.MigrationReportRepaired(base.ExecutingUserIdentityName, msg));
			base.BatchProvider.MailboxProvider.FlushReport(migrationJob.ReportData);
			return true;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007FB0 File Offset: 0x000061B0
		private bool TryGetPropertyDefinition(out PropertyDefinition propertyDefinition)
		{
			propertyDefinition = null;
			if (string.IsNullOrEmpty(this.Property))
			{
				return false;
			}
			Type typeFromHandle = typeof(MigrationBatchMessageSchema);
			FieldInfo field = typeFromHandle.GetField(this.Property, BindingFlags.Static | BindingFlags.Public);
			if (field == null)
			{
				this.WriteInternalError(string.Format("couldn't find property {0} in MigrationBatchMessageSchema", this.Property));
			}
			try
			{
				propertyDefinition = (PropertyDefinition)field.GetValue(null);
				return true;
			}
			catch (NotSupportedException arg)
			{
				this.WriteInternalError(string.Format("couldn't retrieve property value {0} ex:{1}", this.Property, arg));
			}
			catch (FieldAccessException arg2)
			{
				this.WriteInternalError(string.Format("couldn't retrieve property value {0} ex:{1}", this.Property, arg2));
			}
			catch (ArgumentException arg3)
			{
				this.WriteInternalError(string.Format("couldn't retrieve property value {0} ex:{1}", this.Property, arg3));
			}
			return false;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008098 File Offset: 0x00006298
		private Type GetPropertyType()
		{
			if (string.IsNullOrEmpty(this.PropertyType))
			{
				return null;
			}
			Type result = null;
			try
			{
				result = Type.GetType(this.PropertyType, false, false);
			}
			catch (ArgumentException arg)
			{
				this.WriteInternalError(string.Format("couldn't extract type from {0} {1}", this.PropertyType, arg));
			}
			catch (TargetInvocationException arg2)
			{
				this.WriteInternalError(string.Format("couldn't extract type from {0} {1}", this.PropertyType, arg2));
			}
			catch (TypeLoadException arg3)
			{
				this.WriteInternalError(string.Format("couldn't extract type from {0} {1}", this.PropertyType, arg3));
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008140 File Offset: 0x00006340
		private object GetConvertedValue(Type propertyType)
		{
			MigrationUtil.ThrowOnNullArgument(propertyType, "propertyType");
			object result = null;
			try
			{
				if (this.PropertyValue != null && this.PropertyValue.GetType() == propertyType)
				{
					return this.PropertyValue;
				}
				if (propertyType == typeof(byte[]) && this.PropertyValue is string)
				{
					try
					{
						base.WriteVerbose(new LocalizedString("attempting to convert from base64 string."));
						result = Convert.FromBase64String((string)this.PropertyValue);
						goto IL_161;
					}
					catch (FormatException arg)
					{
						base.WriteVerbose(new LocalizedString("attempting to convert from an ascii string:" + arg));
						ASCIIEncoding asciiencoding = new ASCIIEncoding();
						result = asciiencoding.GetBytes((string)this.PropertyValue);
						goto IL_161;
					}
				}
				if (propertyType == typeof(ExDateTime) && this.PropertyValue is string)
				{
					result = ExDateTime.Parse((string)this.PropertyValue);
				}
				else
				{
					if (propertyType == typeof(string[]))
					{
						object[] array = (object[])this.PropertyValue;
						string[] array2 = new string[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = (string)array[i];
						}
						return array2;
					}
					TypeConverter converter = TypeDescriptor.GetConverter(propertyType);
					if (converter == null)
					{
						this.WriteInternalError(string.Format("no converter for {0}", propertyType));
					}
					result = converter.ConvertFrom(this.PropertyValue);
				}
				IL_161:;
			}
			catch (ArgumentException arg2)
			{
				this.WriteInternalError(string.Format("couldn't convert value {0} to {1} ex:{2}", this.PropertyValue, propertyType, arg2));
			}
			catch (FormatException arg3)
			{
				this.WriteInternalError(string.Format("couldn't convert value {0} to {1} ex:{2}", this.PropertyValue, propertyType, arg3));
			}
			catch (NotSupportedException arg4)
			{
				this.WriteInternalError(string.Format("couldn't convert value {0} to {1} ex:{2}", this.PropertyValue, propertyType, arg4));
			}
			return result;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008370 File Offset: 0x00006570
		private void InternalProcessStoreObject()
		{
			StoreObjectId storeObjectId = Microsoft.Exchange.Data.Storage.StoreObjectId.Deserialize(this.StoreObjectId);
			PropertyDefinition[] array = MigrationStoreObject.IdPropertyDefinition;
			PropertyDefinition propertyDefinition = null;
			if (!string.IsNullOrEmpty(this.Property))
			{
				if (!this.TryGetPropertyDefinition(out propertyDefinition))
				{
					return;
				}
				array = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					new PropertyDefinition[]
					{
						propertyDefinition
					},
					MigrationStoreObject.IdPropertyDefinition
				});
			}
			if (Folder.IsFolderId(storeObjectId))
			{
				using (Folder folder = Folder.Bind(base.DataProvider.MailboxSession, storeObjectId, array))
				{
					using (MigrationFolder migrationFolder = new MigrationFolder(folder))
					{
						this.InternalProcessStoreObject(migrationFolder, propertyDefinition, null);
					}
					return;
				}
			}
			using (MigrationMessageItem migrationMessageItem = new MigrationMessageItem(base.DataProvider, storeObjectId, array))
			{
				this.InternalProcessStoreObject(migrationMessageItem, propertyDefinition, null);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000846C File Offset: 0x0000666C
		private bool InternalProcessPersistable(MigrationPersistableBase migrationObject)
		{
			PropertyDefinition migrationPersistableDictionary;
			if (!this.TryGetPropertyDefinition(out migrationPersistableDictionary))
			{
				if (string.IsNullOrEmpty(this.ExtendedProperty))
				{
					return false;
				}
				migrationPersistableDictionary = MigrationBatchMessageSchema.MigrationPersistableDictionary;
			}
			PropertyDefinition[] properties = new PropertyDefinition[]
			{
				migrationPersistableDictionary
			};
			using (IMigrationStoreObject migrationStoreObject = migrationObject.FindStoreObject(base.DataProvider, properties))
			{
				this.InternalProcessStoreObject(migrationStoreObject, migrationPersistableDictionary, migrationObject);
			}
			return true;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000084DC File Offset: 0x000066DC
		private void InternalProcessStoreObject(IMigrationStoreObject storeObject, PropertyDefinition propertyDefinition, MigrationPersistableBase migrationObject)
		{
			storeObject.OpenAsReadWrite();
			string text = (migrationObject != null) ? migrationObject.ToString() : storeObject.Id.ToBase64String();
			bool flag = false;
			if (this.Remove)
			{
				if (!string.IsNullOrEmpty(this.Attachment))
				{
					IMigrationMessageItem migrationMessageItem = storeObject as IMigrationMessageItem;
					if (migrationMessageItem == null)
					{
						this.WriteInternalError("can only remove attachments for messages");
					}
					IMigrationAttachment attachment = migrationMessageItem.GetAttachment(this.Attachment, PropertyOpenMode.ReadOnly);
					using (StreamReader streamReader = new StreamReader(attachment.Stream))
					{
						string value = streamReader.ReadToEnd();
						if (this.ShouldRepairContinue(Strings.ConfirmRepairRemoveProperty(string.Format("Attachment={0}", this.Attachment), value, text)))
						{
							migrationMessageItem.DeleteAttachment(this.Attachment);
							flag = true;
						}
						goto IL_4CF;
					}
				}
				if (!string.IsNullOrEmpty(this.ExtendedProperty))
				{
					PersistableDictionary dictionaryProperty = MigrationHelper.GetDictionaryProperty(storeObject, propertyDefinition, true);
					if (!dictionaryProperty.Contains(this.ExtendedProperty))
					{
						this.WriteInternalError(string.Format("extended property {0} not found in {1}", this.ExtendedProperty, text));
					}
					else if (this.ShouldRepairContinue(Strings.ConfirmRepairRemoveProperty(this.ExtendedProperty.ToString(), dictionaryProperty[this.ExtendedProperty].ToString(), text)))
					{
						object arg = dictionaryProperty[this.ExtendedProperty];
						dictionaryProperty.Remove(this.ExtendedProperty);
						MigrationHelper.SetDictionaryProperty(storeObject, propertyDefinition, dictionaryProperty);
						flag = true;
						string msg = string.Format("removed extended property {0} with value {1} from property {2}", this.ExtendedProperty, arg, propertyDefinition);
						this.TryUpdateMigrationReport(migrationObject, msg);
					}
				}
				else if (!string.IsNullOrEmpty(this.Property))
				{
					object valueOrDefault = storeObject.GetValueOrDefault<object>(propertyDefinition, null);
					if (valueOrDefault == null)
					{
						this.WriteInternalError(string.Format("property {0} not found in {1}", this.Property, text));
					}
					else if (this.ShouldRepairContinue(Strings.ConfirmRepairRemoveProperty(this.Property.ToString(), valueOrDefault.ToString(), text)))
					{
						storeObject.Delete(propertyDefinition);
						flag = true;
						this.TryUpdateMigrationReport(migrationObject, string.Format("removed property {0} with value {1}", this.Property, valueOrDefault));
					}
				}
				else if (this.ShouldRepairContinue(Strings.ConfirmRepairRemoveStoreObject(storeObject.Id.ToString())))
				{
					base.DataProvider.RemoveMessage(storeObject.Id);
				}
			}
			else if (this.Update)
			{
				if (!string.IsNullOrEmpty(this.Attachment))
				{
					IMigrationMessageItem migrationMessageItem2 = storeObject as IMigrationMessageItem;
					if (migrationMessageItem2 == null)
					{
						this.WriteInternalError("can only remove attachments for messages");
					}
					IMigrationAttachment migrationAttachment;
					if (migrationMessageItem2.TryGetAttachment(this.Attachment, PropertyOpenMode.ReadOnly, out migrationAttachment))
					{
						this.WriteInternalError(string.Format("need to remove existing attachment {0} before updating/creating", this.Attachment));
					}
					string text2 = (string)this.GetConvertedValue(typeof(string));
					if (this.ShouldRepairContinue(Strings.ConfirmRepairUpdateProperty(string.Format("Attachment={0}", this.Attachment), null, text2, text)))
					{
						IMigrationAttachment migrationAttachment2;
						migrationAttachment = (migrationAttachment2 = migrationMessageItem2.CreateAttachment(this.Attachment));
						try
						{
							using (StreamWriter streamWriter = new StreamWriter(migrationAttachment.Stream))
							{
								streamWriter.Write(text2);
								streamWriter.Flush();
							}
							migrationAttachment.Save(null);
						}
						finally
						{
							if (migrationAttachment2 != null)
							{
								migrationAttachment2.Dispose();
							}
						}
						flag = true;
					}
				}
				else if (!string.IsNullOrEmpty(this.ExtendedProperty))
				{
					Type propertyType = this.GetPropertyType();
					if (propertyType == null)
					{
						this.WriteInternalError(string.Format("couldn't find type {0} for updating property", this.PropertyType));
					}
					object convertedValue = this.GetConvertedValue(propertyType);
					if (convertedValue == null)
					{
						this.WriteInternalError(string.Format("couldn't convert value {0} to type {1}", this.PropertyValue, propertyType));
					}
					PersistableDictionary dictionaryProperty2 = MigrationHelper.GetDictionaryProperty(storeObject, propertyDefinition, false);
					object obj = null;
					if (dictionaryProperty2.Contains(this.ExtendedProperty))
					{
						obj = dictionaryProperty2[this.ExtendedProperty];
					}
					if (this.ShouldRepairContinue(Strings.ConfirmRepairUpdateProperty(this.ExtendedProperty, (obj == null) ? string.Empty : obj.ToString(), (convertedValue == null) ? string.Empty : convertedValue.ToString(), text)))
					{
						dictionaryProperty2[this.ExtendedProperty] = convertedValue;
						MigrationHelper.SetDictionaryProperty(storeObject, propertyDefinition, dictionaryProperty2);
						flag = true;
						string msg2 = string.Format("setting extended property {0} with value {1} to value {2} from property {3}", new object[]
						{
							this.ExtendedProperty,
							obj,
							convertedValue,
							propertyDefinition
						});
						this.TryUpdateMigrationReport(migrationObject, msg2);
					}
				}
				else
				{
					object convertedValue2 = this.GetConvertedValue(propertyDefinition.Type);
					if (convertedValue2 == null)
					{
						this.WriteInternalError(string.Format("couldn't convert value {0} to type {1}", this.PropertyValue, propertyDefinition.Type));
					}
					object valueOrDefault2 = storeObject.GetValueOrDefault<object>(propertyDefinition, null);
					if (this.ShouldRepairContinue(Strings.ConfirmRepairUpdateProperty(this.Property.ToString(), (valueOrDefault2 == null) ? string.Empty : valueOrDefault2.ToString(), (convertedValue2 == null) ? string.Empty : convertedValue2.ToString(), text)))
					{
						storeObject[propertyDefinition] = convertedValue2;
						flag = true;
						this.TryUpdateMigrationReport(migrationObject, string.Format("setting property {0} with value {1} to value {2}", propertyDefinition, valueOrDefault2, convertedValue2));
					}
				}
			}
			IL_4CF:
			if (flag)
			{
				base.WriteVerbose(new LocalizedString("saving store object..."));
				storeObject.Save(SaveMode.FailOnAnyConflict);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000089FC File Offset: 0x00006BFC
		private void InternalProcessFolder(MigrationFolderName folder)
		{
			if (!this.Remove || !string.IsNullOrEmpty(this.ExtendedProperty) || !string.IsNullOrEmpty(this.Property))
			{
				if (folder == MigrationFolderName.SyncMigration)
				{
					this.InternalProcessPersistable(base.BatchProvider.MigrationSession);
				}
				return;
			}
			if (!this.ShouldRepairContinue(Strings.ConfirmRepairRemoveFolder(folder.ToString())))
			{
				return;
			}
			MigrationFolder.RemoveFolder(base.DataProvider.MailboxSession, folder);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008A74 File Offset: 0x00006C74
		private void InternalProcessJob(MigrationJob job)
		{
			if (this.InternalProcessPersistable(job))
			{
				return;
			}
			if (this.Update)
			{
				if (!this.ShouldRepairContinue(Strings.ConfirmRepairBatch("Update", job.ToString())))
				{
					return;
				}
				this.SetJobStatus(job);
				return;
			}
			else
			{
				if (!this.Remove)
				{
					if (this.Revert)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairBatch("Revert", job.ToString())))
						{
							return;
						}
						this.RevertJob(job, MigrationJobStatus.Failed);
					}
					return;
				}
				if (!this.ShouldRepairContinue(Strings.ConfirmRepairBatch("Remove", job.ToString())))
				{
					return;
				}
				job.Delete(base.DataProvider, true);
				return;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008B1C File Offset: 0x00006D1C
		private void InternalProcessJobItem(MigrationJobItem jobItem)
		{
			if (this.InternalProcessPersistable(jobItem))
			{
				return;
			}
			if (this.Update)
			{
				if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("Update", jobItem.ToString())))
				{
					return;
				}
				this.SetJobItemStatus(jobItem);
				return;
			}
			else if (this.Remove)
			{
				if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("Remove", jobItem.ToString())))
				{
					return;
				}
				jobItem.Delete(base.DataProvider);
				return;
			}
			else
			{
				if (!this.Revert)
				{
					if (this.SyncSubscription)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("SyncSubscription", jobItem.ToString())))
						{
							return;
						}
						if (jobItem.IsPAW)
						{
							return;
						}
						using (ILegacySubscriptionHandler legacySubscriptionHandler = this.GetLegacySubscriptionHandler(jobItem))
						{
							legacySubscriptionHandler.CreateUnderlyingSubscriptions(jobItem);
							return;
						}
					}
					if (this.ResumeSubscription)
					{
						if ((jobItem.MigrationType == MigrationType.IMAP && !this.ShouldRepairContinue(Strings.ConfirmRepairResumeIMAPSubscription(jobItem.ToString()))) || !this.ShouldRepairContinue(Strings.ConfirmRepairUser("ResumeSubscription", jobItem.ToString())))
						{
							return;
						}
						if (jobItem.IsPAW)
						{
							return;
						}
						using (ILegacySubscriptionHandler legacySubscriptionHandler2 = this.GetLegacySubscriptionHandler(jobItem))
						{
							MigrationUserStatus startedStatus = (jobItem.InitialSyncDuration != null) ? MigrationUserStatus.IncrementalSyncing : MigrationUserStatus.Syncing;
							legacySubscriptionHandler2.ResumeUnderlyingSubscriptions(startedStatus, jobItem);
							return;
						}
					}
					if (this.FlushSubscription)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("FlushSubscription", jobItem.ToString())))
						{
							return;
						}
						if (jobItem.IsPAW)
						{
							return;
						}
						using (ILegacySubscriptionHandler legacySubscriptionHandler3 = this.GetLegacySubscriptionHandler(jobItem))
						{
							legacySubscriptionHandler3.SyncToUnderlyingSubscriptions(jobItem);
							return;
						}
					}
					if (this.DisableSubscription)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("DisableSubscription", jobItem.ToString())))
						{
							return;
						}
						if (jobItem.IsPAW)
						{
							return;
						}
						using (ILegacySubscriptionHandler legacySubscriptionHandler4 = this.GetLegacySubscriptionHandler(jobItem))
						{
							legacySubscriptionHandler4.DisableSubscriptions(jobItem);
							return;
						}
					}
					if (this.DeleteSubscription)
					{
						if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("DeleteSubscription", jobItem.ToString())))
						{
							return;
						}
						if (jobItem.IsPAW)
						{
							return;
						}
						using (ILegacySubscriptionHandler legacySubscriptionHandler5 = this.GetLegacySubscriptionHandler(jobItem))
						{
							legacySubscriptionHandler5.DeleteUnderlyingSubscriptions(jobItem);
						}
					}
					return;
				}
				if (!this.ShouldRepairContinue(Strings.ConfirmRepairUser("Revert", jobItem.ToString())))
				{
					return;
				}
				this.RevertJobItem(jobItem, MigrationUserStatus.Corrupted);
				return;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008DBC File Offset: 0x00006FBC
		private void InternalProcessSubscription(Guid subscriptionId)
		{
			if (this.Update)
			{
				UpdateSyncSubscriptionAction updateSyncSubscriptionAction;
				if (!Enum.TryParse<UpdateSyncSubscriptionAction>(this.Status, false, out updateSyncSubscriptionAction))
				{
					this.WriteInternalError(string.Format("couldn't convert status {0} to type {1}", this.Status, updateSyncSubscriptionAction.GetType()));
				}
				this.UpdateTransportSyncSubscription(subscriptionId, updateSyncSubscriptionAction);
				return;
			}
			if (this.RemoveSyncSubscription)
			{
				this.UpdateTransportSyncSubscription(subscriptionId, UpdateSyncSubscriptionAction.Delete);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008E25 File Offset: 0x00007025
		private void WriteError(LocalizedString errorString)
		{
			this.WriteError(new MigrationPermanentException(errorString, "error running repair migration"));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008E38 File Offset: 0x00007038
		private void WriteInternalError(string errorString)
		{
			this.WriteError(new MigrationPermanentException(new LocalizedString(errorString), "error running repair migration"));
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008E50 File Offset: 0x00007050
		private MigrationJob GetMigrationJob(Guid? id)
		{
			MigrationJob migrationJob = null;
			if (id != null)
			{
				base.WriteVerbose(new LocalizedString(string.Format("looking for a job based on guid {0}", id.Value)));
				migrationJob = MigrationJob.GetUniqueByJobId(base.DataProvider, id.Value);
			}
			else if (this.BatchId != null && this.BatchId.MigrationBatchId != null && this.BatchId.MigrationBatchId.Name != MigrationBatchId.Any.ToString())
			{
				base.WriteVerbose(new LocalizedString(string.Format("searching for job with name {0}", this.BatchId)));
				migrationJob = MigrationJob.GetUniqueByBatchId(base.DataProvider, this.BatchId.MigrationBatchId);
			}
			if (migrationJob == null)
			{
				string value = "No batch id, so not scoped to job.  If there's more than one job, please run get-migration(Batch|Job) to get job names";
				base.WriteVerbose(new LocalizedString(value));
				List<MigrationJob> list = new List<MigrationJob>(MigrationJob.Get(base.DataProvider, base.BatchProvider.MigrationSession.Config));
				if (list.Count == 1)
				{
					base.WriteVerbose(new LocalizedString(string.Format("Found a single job {0}", migrationJob)));
					migrationJob = list[0];
				}
			}
			if (migrationJob == null)
			{
				base.WriteVerbose(new LocalizedString("no job was found.."));
			}
			return migrationJob;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008F78 File Offset: 0x00007178
		private void RevertJob(MigrationJob job, MigrationJobStatus status)
		{
			if (job.IsPAW)
			{
				this.WriteError(Strings.ValidateRepairInvalidRevertJobType(job.ToString()));
			}
			if (job.Status != status)
			{
				this.WriteError(Strings.ValidateRepairInvalidRevert(job.ToString(), job.Status.ToString()));
			}
			MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(job.StatusData);
			if (migrationStatusData.RevertStatus())
			{
				base.WriteVerbose(new LocalizedString(string.Format("Reverting MigrationJob {0}", job)));
				job.SetStatusData(base.DataProvider, migrationStatusData);
				if (job.PoisonCount > 0)
				{
					base.WriteVerbose(new LocalizedString(string.Format("Clearing poison count of {0}", job.PoisonCount)));
					job.UpdatePoisonCount(base.DataProvider, 0);
				}
				this.TryUpdateMigrationReport(job, "Corruption reverted");
				return;
			}
			this.WriteError(Strings.ErrorRepairReverting(job.ToString()));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009054 File Offset: 0x00007254
		private void SetJobStatus(MigrationJob job)
		{
			if (job.IsPAW)
			{
				MigrationState migrationState;
				if (!Enum.TryParse<MigrationState>(this.Status, false, out migrationState))
				{
					this.WriteError(Strings.ErrorRepairConvertStatus(this.Status.ToString(), migrationState.GetType().ToString()));
				}
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJob before update {0}", job)));
				job.SetStatus(base.DataProvider, MigrationJobStatus.SyncStarting, migrationState, null, null, null, null, null, null, null, true, null, null);
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJob after update {0}", job)));
			}
			else
			{
				MigrationJobStatus migrationJobStatus;
				if (!Enum.TryParse<MigrationJobStatus>(this.Status, false, out migrationJobStatus))
				{
					this.WriteError(Strings.ErrorRepairConvertStatus(this.Status.ToString(), migrationJobStatus.GetType().ToString()));
				}
				MigrationStatusData<MigrationJobStatus> migrationStatusData = new MigrationStatusData<MigrationJobStatus>(job.StatusData);
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJob before update {0}", job)));
				migrationStatusData.UpdateStatus(migrationJobStatus, null);
				job.SetStatusData(base.DataProvider, migrationStatusData);
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJob after update {0}", job)));
			}
			this.TryUpdateMigrationReport(job, "Status updated");
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000091B4 File Offset: 0x000073B4
		private MigrationJobItem GetMigrationJobItem()
		{
			List<MigrationJobItem> migrationJobItems = this.GetMigrationJobItems();
			if (migrationJobItems.Count <= 0)
			{
				this.WriteError(Strings.MigrationUserNotFound(this.UserId.ToString()));
			}
			else if (migrationJobItems.Count > 1)
			{
				this.WriteError(Strings.ValidateRepairMultipleUsers(this.UserId.ToString(), migrationJobItems.Count));
			}
			return migrationJobItems[0];
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009218 File Offset: 0x00007418
		private List<MigrationJobItem> GetMigrationJobItems()
		{
			if (this.UserId == null || this.UserId.MigrationUserId == null || (string.IsNullOrEmpty(this.UserId.MigrationUserId.Id) && this.UserId.MigrationUserId.JobItemGuid == Guid.Empty))
			{
				this.WriteError(Strings.ValidateRepairInvalidUser);
				return null;
			}
			List<MigrationJobItem> list = null;
			if (this.UserId.MigrationUserId.JobItemGuid != Guid.Empty)
			{
				MigrationJobItem byGuid = MigrationJobItem.GetByGuid(base.DataProvider, this.UserId.MigrationUserId.JobItemGuid);
				if (byGuid == null)
				{
					using (IMigrationDataProvider providerForFolder = base.DataProvider.GetProviderForFolder(MigrationFolderName.CorruptedItems))
					{
						byGuid = MigrationJobItem.GetByGuid(providerForFolder, this.UserId.MigrationUserId.JobItemGuid);
					}
				}
				list = new List<MigrationJobItem>(1);
				if (byGuid != null)
				{
					list.Add(byGuid);
				}
				return list;
			}
			list = new List<MigrationJobItem>(MigrationJobItem.GetByIdentifier(base.DataProvider, null, this.UserId.MigrationUserId.Id, null));
			if (list.Count <= 0)
			{
				using (IMigrationDataProvider providerForFolder2 = base.DataProvider.GetProviderForFolder(MigrationFolderName.CorruptedItems))
				{
					list = new List<MigrationJobItem>(MigrationJobItem.GetByIdentifier(providerForFolder2, null, this.UserId.MigrationUserId.Id, null));
				}
			}
			return list;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000937C File Offset: 0x0000757C
		private void RevertJobItem(MigrationJobItem jobItem, MigrationUserStatus status)
		{
			if (jobItem.IsPAW)
			{
				this.WriteError(Strings.ValidateRepairInvalidRevertJobType(jobItem.ToString()));
			}
			if (jobItem.Status != status)
			{
				this.WriteError(Strings.ValidateRepairInvalidRevert(jobItem.ToString(), jobItem.Status.ToString()));
			}
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(jobItem.StatusData);
			if (migrationStatusData.RevertStatus())
			{
				base.WriteVerbose(new LocalizedString(string.Format("Reverting MigrationUser {0}", jobItem.Identifier)));
				jobItem.SetStatusData(base.DataProvider, migrationStatusData);
				return;
			}
			this.WriteError(Strings.ErrorRepairReverting(jobItem.ToString()));
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000941C File Offset: 0x0000761C
		private void SetJobItemStatus(MigrationJobItem jobItem)
		{
			if (jobItem.IsPAW)
			{
				MigrationState migrationState;
				if (!Enum.TryParse<MigrationState>(this.Status, false, out migrationState))
				{
					this.WriteError(Strings.ErrorRepairConvertStatus(this.Status.ToString(), migrationState.GetType().ToString()));
				}
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJobItem before update {0}", jobItem)));
				if (migrationState == MigrationState.Failed || migrationState == MigrationState.Corrupted)
				{
					jobItem.SetStatus(base.DataProvider, (migrationState == MigrationState.Failed) ? MigrationUserStatus.Failed : MigrationUserStatus.Corrupted, migrationState, null, null, null, null, null, null, false, MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>("repair migration: setting error"));
				}
				else
				{
					MigrationUserStatus status;
					switch (migrationState)
					{
					case MigrationState.Active:
					case MigrationState.Waiting:
						status = MigrationUserStatus.Syncing;
						break;
					case MigrationState.Completed:
						status = MigrationUserStatus.Completed;
						break;
					case MigrationState.Stopped:
						status = MigrationUserStatus.Stopped;
						break;
					default:
						throw new NotSupportedException("Unexpected state " + migrationState);
					}
					jobItem.SetStatus(base.DataProvider, status, migrationState, null, null, null, null, null, null, false, null);
				}
				base.WriteVerbose(new LocalizedString(string.Format("MigrationJobItem after update {0}", jobItem)));
				return;
			}
			MigrationUserStatus migrationUserStatus;
			if (!Enum.TryParse<MigrationUserStatus>(this.Status, false, out migrationUserStatus))
			{
				this.WriteError(Strings.ErrorRepairConvertStatus(this.Status.ToString(), migrationUserStatus.GetType().ToString()));
			}
			base.WriteVerbose(new LocalizedString(string.Format("setting MigrationJob {0} status from {1} to {2}", jobItem, jobItem.Status, migrationUserStatus)));
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(jobItem.StatusData);
			if (MigrationJobItem.IsFailedStatus(migrationUserStatus))
			{
				migrationStatusData.UpdateStatus(migrationUserStatus, MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>("repair migration: setting error"), "repair migrationsetting error", null);
			}
			else
			{
				migrationStatusData.UpdateStatus(migrationUserStatus, null);
			}
			jobItem.SetStatusData(base.DataProvider, migrationStatusData);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000095F4 File Offset: 0x000077F4
		private ILegacySubscriptionHandler GetLegacySubscriptionHandler(MigrationJobItem jobItem)
		{
			MigrationJob migrationJob = this.GetMigrationJob(new Guid?(jobItem.MigrationJobId));
			if (migrationJob == null)
			{
				this.WriteError(Strings.MigrationJobNotFound(jobItem.MigrationJobId.ToString()));
			}
			ILegacySubscriptionHandler legacySubscriptionHandler = LegacySubscriptionHandlerBase.CreateSubscriptionHandler(base.DataProvider, migrationJob);
			if (legacySubscriptionHandler == null)
			{
				this.WriteError(Strings.ValidateRepairMissingSubscriptionHandler(migrationJob.ToString()));
			}
			return legacySubscriptionHandler;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009658 File Offset: 0x00007858
		private void UpdateTransportSyncSubscription(Guid subscriptionId, UpdateSyncSubscriptionAction status)
		{
			if (this.UserId == null || this.UserId.MigrationUserId == null || string.IsNullOrEmpty(this.UserId.MigrationUserId.Id))
			{
				this.WriteError(Strings.ValidateRepairInvalidUser);
			}
			MailboxData mailboxData = null;
			try
			{
				mailboxData = base.DataProvider.ADProvider.GetMailboxDataFromSmtpAddress(this.UserId.MigrationUserId.Id, false, true);
			}
			catch (MigrationRecipientNotFoundException ex)
			{
				this.WriteError(Strings.ValidateRepairMissingSubscription(this.UserId.ToString(), ex.ToString()));
			}
			AggregationSubscriptionType aggregationSubscriptionType = AggregationSubscriptionType.IMAP;
			if (!string.IsNullOrEmpty(this.PropertyType) && !Enum.TryParse<AggregationSubscriptionType>(this.PropertyType, false, out aggregationSubscriptionType))
			{
				this.WriteInternalError(string.Format("couldn't convert type {0} to {1}", this.PropertyType, aggregationSubscriptionType.GetType()));
			}
			UpdateSyncSubscriptionArgs args = new UpdateSyncSubscriptionArgs(base.DataProvider.OrganizationId.OrganizationalUnit, mailboxData.MailboxLegacyDN, subscriptionId, aggregationSubscriptionType, status);
			if (!this.ShouldRepairContinue(Strings.ConfirmRepairSubscription(status.ToString(), aggregationSubscriptionType.ToString(), mailboxData.ToString())))
			{
				return;
			}
			MigrationServiceRpcStub migrationServiceRpcStub = new MigrationServiceRpcStub(mailboxData.MailboxServer);
			migrationServiceRpcStub.UpdateSyncSubscription(args);
			base.WriteVerbose(new LocalizedString(string.Format("SUCCESS updating {0} subscriptionId {1}, action {2} for {3}", new object[]
			{
				aggregationSubscriptionType.ToString(),
				subscriptionId,
				status,
				this.UserId.MigrationUserId.Id
			})));
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000097F0 File Offset: 0x000079F0
		private void RemoveReportItem(MigrationReportId reportId)
		{
			using (IMigrationDataProvider providerForFolder = base.DataProvider.GetProviderForFolder(MigrationFolderName.SyncMigrationReports))
			{
				MigrationReportItem migrationReportItem = MigrationReportItem.Get(providerForFolder, reportId);
				if (migrationReportItem == null)
				{
					this.WriteError(Strings.ValidateRepairMissingReport(reportId.ToString()));
				}
				migrationReportItem.Delete(providerForFolder);
				base.WriteVerbose(new LocalizedString(string.Format("removing report {0} with message {1}", migrationReportItem, reportId)));
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009860 File Offset: 0x00007A60
		private void UpdateCacheEntry()
		{
			MigrationObjectTaskBase<MigrationBatchIdParameter>.RegisterMigrationBatch(this, base.DataProvider.MailboxSession, base.CurrentOrganizationId, true, true);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000987C File Offset: 0x00007A7C
		private void EnableMigrationFeatures(MigrationFeature feature)
		{
			MigrationSession migrationSession = MigrationSession.Get(base.DataProvider);
			IMigrationConfig config = migrationSession.Config;
			config.EnableFeatures(base.DataProvider, feature);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000098AC File Offset: 0x00007AAC
		private void DisableMigrationFeatures(MigrationFeature feature)
		{
			MigrationSession migrationSession = MigrationSession.Get(base.DataProvider);
			IMigrationConfig config = migrationSession.Config;
			config.DisableFeatures(base.DataProvider, feature, this.Force);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000098E4 File Offset: 0x00007AE4
		private void UpdateOrganizationUpgradeConstraint(bool addConstraint)
		{
			IUpgradeConstraintAdapter upgradeConstraintAdapter = new OrganizationUpgradeConstraintAdapter();
			if (addConstraint)
			{
				upgradeConstraintAdapter.AddUpgradeConstraint(base.DataProvider, MigrationSession.Get(base.DataProvider));
				return;
			}
			upgradeConstraintAdapter.MarkUpgradeConstraintForExpiry(base.DataProvider, this.ConstraintExpiryDate);
		}

		// Token: 0x0400008E RID: 142
		private const string ParameterSetUpdateMigrationBatch = "UpdateMigrationBatch";

		// Token: 0x0400008F RID: 143
		private const string ParameterSetUpdateMigrationUser = "UpdateMigrationUser";

		// Token: 0x04000090 RID: 144
		private const string ParameterSetUpdateMigrationFolder = "UpdateMigrationFolder";

		// Token: 0x04000091 RID: 145
		private const string ParameterSetUpdateMigrationReport = "UpdateMigrationReport";

		// Token: 0x04000092 RID: 146
		private const string ParameterSetUpdateSubscriptionStatus = "UpdateSubscriptionStatus";

		// Token: 0x04000093 RID: 147
		private const string ParameterSetUpdateMigrationCacheEntry = "UpdateMigrationCacheEntry";

		// Token: 0x04000094 RID: 148
		private const string ParameterSetUpdateMigrationStoreObject = "UpdateMigrationStoreObject";

		// Token: 0x04000095 RID: 149
		private const string ParameterSetRemoveMigrationBatch = "RemoveMigrationBatch";

		// Token: 0x04000096 RID: 150
		private const string ParameterSetRemoveMigrationUser = "RemoveMigrationUser";

		// Token: 0x04000097 RID: 151
		private const string ParameterSetRemoveSubscription = "RemoveSubscription";

		// Token: 0x04000098 RID: 152
		private const string ParameterSetRemoveMigrationFolder = "RemoveMigrationFolder";

		// Token: 0x04000099 RID: 153
		private const string ParameterSetRemoveMigrationReport = "RemoveMigrationReport";

		// Token: 0x0400009A RID: 154
		private const string ParameterSetRemoveMigrationStoreObject = "RemoveMigrationStoreObject";

		// Token: 0x0400009B RID: 155
		private const string ParameterSetRevertMigrationBatch = "RevertMigrationBatch";

		// Token: 0x0400009C RID: 156
		private const string ParameterSetRevertMigrationUser = "RevertMigrationUser";

		// Token: 0x0400009D RID: 157
		private const string ParameterSetSyncMigrationUser = "SyncMigrationUser";

		// Token: 0x0400009E RID: 158
		private const string ParameterSetResumeMigrationUser = "ResumeMigrationUser";

		// Token: 0x0400009F RID: 159
		private const string ParameterSetFlushMigrationUser = "FlushMigrationUser";

		// Token: 0x040000A0 RID: 160
		private const string ParameterSetDeleteSubscription = "DeleteSubscription";

		// Token: 0x040000A1 RID: 161
		private const string ParameterSetDisableSubscription = "DisableSubscription";

		// Token: 0x040000A2 RID: 162
		private const string ParameterSetEnableConfigFeatures = "EnableConfigFeatures";

		// Token: 0x040000A3 RID: 163
		private const string ParameterSetDisableConfigFeatures = "DisableConfigFeatures";

		// Token: 0x040000A4 RID: 164
		private const string ParameterSetExpireConstraint = "ExpireOrgUpgradeConstraint";

		// Token: 0x040000A5 RID: 165
		private const string ParameterSetAddUpgradeConstraint = "AddOrgUpgradeConstraint";

		// Token: 0x040000A6 RID: 166
		private const string ParameterPropertyValue = "Value";
	}
}
