using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000722 RID: 1826
	internal class UserSchema : OrgPersonPresentationObjectSchema
	{
		// Token: 0x0600567B RID: 22139 RVA: 0x00137A87 File Offset: 0x00135C87
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003A7E RID: 14974
		public static readonly ADPropertyDefinition IsSecurityPrincipal = ADMailboxRecipientSchema.IsSecurityPrincipal;

		// Token: 0x04003A7F RID: 14975
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003A80 RID: 14976
		public static readonly ADPropertyDefinition Sid = ADMailboxRecipientSchema.Sid;

		// Token: 0x04003A81 RID: 14977
		public static readonly ADPropertyDefinition SidHistory = ADMailboxRecipientSchema.SidHistory;

		// Token: 0x04003A82 RID: 14978
		public static readonly ADPropertyDefinition UserPrincipalName = ADUserSchema.UserPrincipalName;

		// Token: 0x04003A83 RID: 14979
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = ADUserSchema.ResetPasswordOnNextLogon;

		// Token: 0x04003A84 RID: 14980
		public static readonly ADPropertyDefinition OrganizationalUnit = ADRecipientSchema.OrganizationalUnit;

		// Token: 0x04003A85 RID: 14981
		public static readonly ADPropertyDefinition CertificateSubject = ADUserSchema.CertificateSubject;

		// Token: 0x04003A86 RID: 14982
		public static readonly ADPropertyDefinition RemotePowerShellEnabled = ADRecipientSchema.RemotePowerShellEnabled;

		// Token: 0x04003A87 RID: 14983
		public static readonly ADPropertyDefinition UserAccountControl = ADUserSchema.UserAccountControl;

		// Token: 0x04003A88 RID: 14984
		public static readonly ADPropertyDefinition IsLinked = ADRecipientSchema.IsLinked;

		// Token: 0x04003A89 RID: 14985
		public static readonly ADPropertyDefinition LinkedMasterAccount = ADRecipientSchema.LinkedMasterAccount;

		// Token: 0x04003A8A RID: 14986
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x04003A8B RID: 14987
		public static readonly ADPropertyDefinition NetID = ADUserSchema.NetID;

		// Token: 0x04003A8C RID: 14988
		public static readonly ADPropertyDefinition ConsumerNetID = ADUserSchema.ConsumerNetID;

		// Token: 0x04003A8D RID: 14989
		public static readonly ADPropertyDefinition OriginalNetID = ADUserSchema.OriginalNetID;

		// Token: 0x04003A8E RID: 14990
		public static readonly ADPropertyDefinition LEOEnabled = ADRecipientSchema.LEOEnabled;

		// Token: 0x04003A8F RID: 14991
		public static readonly ADPropertyDefinition ExternalDirectoryObjectId = ADRecipientSchema.ExternalDirectoryObjectId;

		// Token: 0x04003A90 RID: 14992
		public static readonly ADPropertyDefinition SKUAssigned = ADRecipientSchema.SKUAssigned;

		// Token: 0x04003A91 RID: 14993
		public static readonly ADPropertyDefinition IsSoftDeletedByRemove = ADRecipientSchema.IsSoftDeletedByRemove;

		// Token: 0x04003A92 RID: 14994
		public static readonly ADPropertyDefinition IsSoftDeletedByDisable = ADRecipientSchema.IsSoftDeletedByDisable;

		// Token: 0x04003A93 RID: 14995
		public static readonly ADPropertyDefinition WhenSoftDeleted = ADRecipientSchema.WhenSoftDeleted;

		// Token: 0x04003A94 RID: 14996
		public static readonly ADPropertyDefinition PreviousRecipientTypeDetails = ADRecipientSchema.PreviousRecipientTypeDetails;

		// Token: 0x04003A95 RID: 14997
		public static readonly ADPropertyDefinition UpgradeRequest = ADRecipientSchema.UpgradeRequest;

		// Token: 0x04003A96 RID: 14998
		public static readonly ADPropertyDefinition UpgradeStatus = ADRecipientSchema.UpgradeStatus;

		// Token: 0x04003A97 RID: 14999
		public static readonly ADPropertyDefinition UpgradeDetails = ADRecipientSchema.UpgradeDetails;

		// Token: 0x04003A98 RID: 15000
		public static readonly ADPropertyDefinition UpgradeMessage = ADRecipientSchema.UpgradeMessage;

		// Token: 0x04003A99 RID: 15001
		public static readonly ADPropertyDefinition UpgradeStage = ADRecipientSchema.UpgradeStage;

		// Token: 0x04003A9A RID: 15002
		public static readonly ADPropertyDefinition UpgradeStageTimeStamp = ADRecipientSchema.UpgradeStageTimeStamp;

		// Token: 0x04003A9B RID: 15003
		public static readonly ADPropertyDefinition InPlaceHoldsRaw = ADRecipientSchema.InPlaceHoldsRaw;

		// Token: 0x04003A9C RID: 15004
		public static readonly ADPropertyDefinition MailboxRelease = ADUserSchema.MailboxRelease;

		// Token: 0x04003A9D RID: 15005
		public static readonly ADPropertyDefinition ArchiveRelease = ADUserSchema.ArchiveRelease;

		// Token: 0x04003A9E RID: 15006
		public static readonly ADPropertyDefinition MailboxProvisioningConstraint = ADRecipientSchema.MailboxProvisioningConstraint;

		// Token: 0x04003A9F RID: 15007
		public static readonly ADPropertyDefinition MailboxProvisioningPreferences = ADRecipientSchema.MailboxProvisioningPreferences;
	}
}
