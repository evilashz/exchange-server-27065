using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200073A RID: 1850
	internal class MailUserSchema : MailEnabledOrgPersonSchema
	{
		// Token: 0x06005984 RID: 22916 RVA: 0x0013C189 File Offset: 0x0013A389
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003C13 RID: 15379
		public static readonly ADPropertyDefinition ExchangeUserAccountControl = ADUserSchema.ExchangeUserAccountControl;

		// Token: 0x04003C14 RID: 15380
		public static readonly ADPropertyDefinition ExternalEmailAddress = ADRecipientSchema.ExternalEmailAddress;

		// Token: 0x04003C15 RID: 15381
		public static readonly ADPropertyDefinition UsePreferMessageFormat = ADRecipientSchema.UsePreferMessageFormat;

		// Token: 0x04003C16 RID: 15382
		public static readonly ADPropertyDefinition MessageFormat = ADRecipientSchema.MessageFormat;

		// Token: 0x04003C17 RID: 15383
		public static readonly ADPropertyDefinition MessageBodyFormat = ADRecipientSchema.MessageBodyFormat;

		// Token: 0x04003C18 RID: 15384
		public static readonly ADPropertyDefinition MacAttachmentFormat = ADRecipientSchema.MacAttachmentFormat;

		// Token: 0x04003C19 RID: 15385
		public static readonly ADPropertyDefinition ProtocolSettings = ADRecipientSchema.ReadOnlyProtocolSettings;

		// Token: 0x04003C1A RID: 15386
		public static readonly ADPropertyDefinition RecipientLimits = ADRecipientSchema.RecipientLimits;

		// Token: 0x04003C1B RID: 15387
		public static readonly ADPropertyDefinition RemotePowerShellEnabled = ADRecipientSchema.RemotePowerShellEnabled;

		// Token: 0x04003C1C RID: 15388
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003C1D RID: 15389
		public static readonly ADPropertyDefinition UseMapiRichTextFormat = ADRecipientSchema.UseMapiRichTextFormat;

		// Token: 0x04003C1E RID: 15390
		public static readonly ADPropertyDefinition UserPrincipalName = ADUserSchema.UserPrincipalName;

		// Token: 0x04003C1F RID: 15391
		public static readonly ADPropertyDefinition ImmutableId = ADRecipientSchema.ImmutableId;

		// Token: 0x04003C20 RID: 15392
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x04003C21 RID: 15393
		public static readonly ADPropertyDefinition NetID = ADUserSchema.NetID;

		// Token: 0x04003C22 RID: 15394
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = ADMailboxRecipientSchema.DeliverToMailboxAndForward;

		// Token: 0x04003C23 RID: 15395
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x04003C24 RID: 15396
		public static readonly ADPropertyDefinition MailboxContainerGuid = ADUserSchema.MailboxContainerGuid;

		// Token: 0x04003C25 RID: 15397
		public static readonly ADPropertyDefinition AggregatedMailboxGuids = ADUserSchema.AggregatedMailboxGuids;

		// Token: 0x04003C26 RID: 15398
		public static readonly ADPropertyDefinition UnifiedMailbox = ADUserSchema.UnifiedMailbox;

		// Token: 0x04003C27 RID: 15399
		public static readonly ADPropertyDefinition ForwardingAddress = ADRecipientSchema.ForwardingAddress;

		// Token: 0x04003C28 RID: 15400
		public static readonly ADPropertyDefinition ArchiveGuid = ADUserSchema.ArchiveGuid;

		// Token: 0x04003C29 RID: 15401
		public static readonly ADPropertyDefinition ArchiveName = ADUserSchema.ArchiveName;

		// Token: 0x04003C2A RID: 15402
		public static readonly ADPropertyDefinition ArchiveQuota = ADUserSchema.ArchiveQuota;

		// Token: 0x04003C2B RID: 15403
		public static readonly ADPropertyDefinition ArchiveWarningQuota = ADUserSchema.ArchiveWarningQuota;

		// Token: 0x04003C2C RID: 15404
		public static readonly ADPropertyDefinition ArchiveDatabase = ADUserSchema.ArchiveDatabase;

		// Token: 0x04003C2D RID: 15405
		public static readonly ADPropertyDefinition ArchiveStatus = ADUserSchema.ArchiveStatus;

		// Token: 0x04003C2E RID: 15406
		public static readonly ADPropertyDefinition JournalArchiveAddress = ADRecipientSchema.JournalArchiveAddress;

		// Token: 0x04003C2F RID: 15407
		public static readonly ADPropertyDefinition DisabledArchiveDatabase = ADUserSchema.DisabledArchiveDatabase;

		// Token: 0x04003C30 RID: 15408
		public static readonly ADPropertyDefinition DisabledArchiveGuid = ADUserSchema.DisabledArchiveGuid;

		// Token: 0x04003C31 RID: 15409
		public static readonly ADPropertyDefinition LitigationHoldEnabled = ADUserSchema.LitigationHoldEnabled;

		// Token: 0x04003C32 RID: 15410
		public static readonly ADPropertyDefinition RetentionComment = ADUserSchema.RetentionComment;

		// Token: 0x04003C33 RID: 15411
		public static readonly ADPropertyDefinition RetentionUrl = ADUserSchema.RetentionUrl;

		// Token: 0x04003C34 RID: 15412
		public static readonly ADPropertyDefinition LitigationHoldDate = ADUserSchema.LitigationHoldDate;

		// Token: 0x04003C35 RID: 15413
		public static readonly ADPropertyDefinition LitigationHoldOwner = ADUserSchema.LitigationHoldOwner;

		// Token: 0x04003C36 RID: 15414
		public static readonly ADPropertyDefinition SingleItemRecoveryEnabled = ADUserSchema.SingleItemRecoveryEnabled;

		// Token: 0x04003C37 RID: 15415
		public static readonly ADPropertyDefinition CalendarVersionStoreDisabled = ADUserSchema.CalendarVersionStoreDisabled;

		// Token: 0x04003C38 RID: 15416
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = ADMailboxRecipientSchema.RetainDeletedItemsFor;

		// Token: 0x04003C39 RID: 15417
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEnabled = ADUserSchema.ElcExpirationSuspensionEnabled;

		// Token: 0x04003C3A RID: 15418
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEndDate = ADUserSchema.ElcExpirationSuspensionEndDate;

		// Token: 0x04003C3B RID: 15419
		public static readonly ADPropertyDefinition ElcExpirationSuspensionStartDate = ADUserSchema.ElcExpirationSuspensionStartDate;

		// Token: 0x04003C3C RID: 15420
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = ADUserSchema.MailboxMoveTargetMDB;

		// Token: 0x04003C3D RID: 15421
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = ADUserSchema.MailboxMoveSourceMDB;

		// Token: 0x04003C3E RID: 15422
		public static readonly ADPropertyDefinition MailboxMoveTargetArchiveMDB = ADUserSchema.MailboxMoveTargetArchiveMDB;

		// Token: 0x04003C3F RID: 15423
		public static readonly ADPropertyDefinition MailboxMoveSourceArchiveMDB = ADUserSchema.MailboxMoveSourceArchiveMDB;

		// Token: 0x04003C40 RID: 15424
		public static readonly ADPropertyDefinition MailboxMoveFlags = ADUserSchema.MailboxMoveFlags;

		// Token: 0x04003C41 RID: 15425
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = ADUserSchema.MailboxMoveRemoteHostName;

		// Token: 0x04003C42 RID: 15426
		public static readonly ADPropertyDefinition MailboxMoveBatchName = ADUserSchema.MailboxMoveBatchName;

		// Token: 0x04003C43 RID: 15427
		public static readonly ADPropertyDefinition MailboxMoveStatus = ADUserSchema.MailboxMoveStatus;

		// Token: 0x04003C44 RID: 15428
		public static readonly ADPropertyDefinition MailboxRelease = ADUserSchema.MailboxRelease;

		// Token: 0x04003C45 RID: 15429
		public static readonly ADPropertyDefinition ArchiveRelease = ADUserSchema.ArchiveRelease;

		// Token: 0x04003C46 RID: 15430
		public static readonly ADPropertyDefinition PersistedCapabilities = SharedPropertyDefinitions.PersistedCapabilities;

		// Token: 0x04003C47 RID: 15431
		public static readonly ADPropertyDefinition SKUAssigned = ADRecipientSchema.SKUAssigned;

		// Token: 0x04003C48 RID: 15432
		public static readonly ADPropertyDefinition WhenMailboxCreated = ADMailboxRecipientSchema.WhenMailboxCreated;

		// Token: 0x04003C49 RID: 15433
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = ADUserSchema.ResetPasswordOnNextLogon;

		// Token: 0x04003C4A RID: 15434
		public static readonly ADPropertyDefinition UsageLocation = ADRecipientSchema.UsageLocation;

		// Token: 0x04003C4B RID: 15435
		public static readonly ADPropertyDefinition IsSoftDeletedByRemove = ADRecipientSchema.IsSoftDeletedByRemove;

		// Token: 0x04003C4C RID: 15436
		public static readonly ADPropertyDefinition IsSoftDeletedByDisable = ADRecipientSchema.IsSoftDeletedByDisable;

		// Token: 0x04003C4D RID: 15437
		public static readonly ADPropertyDefinition WhenSoftDeleted = ADRecipientSchema.WhenSoftDeleted;

		// Token: 0x04003C4E RID: 15438
		public static readonly ADPropertyDefinition InPlaceHolds = ADRecipientSchema.InPlaceHolds;

		// Token: 0x04003C4F RID: 15439
		public static readonly ADPropertyDefinition RecoverableItemsQuota = ADUserSchema.RecoverableItemsQuota;

		// Token: 0x04003C50 RID: 15440
		public static readonly ADPropertyDefinition RecoverableItemsWarningQuota = ADUserSchema.RecoverableItemsWarningQuota;

		// Token: 0x04003C51 RID: 15441
		public static readonly ADPropertyDefinition UserCertificate = ADRecipientSchema.Certificate;

		// Token: 0x04003C52 RID: 15442
		public static readonly ADPropertyDefinition UserSMimeCertificate = ADRecipientSchema.SMimeCertificate;

		// Token: 0x04003C53 RID: 15443
		public static readonly ADPropertyDefinition MailboxProvisioningConstraint = ADRecipientSchema.MailboxProvisioningConstraint;

		// Token: 0x04003C54 RID: 15444
		public static readonly ADPropertyDefinition MailboxProvisioningPreferences = ADRecipientSchema.MailboxProvisioningPreferences;
	}
}
