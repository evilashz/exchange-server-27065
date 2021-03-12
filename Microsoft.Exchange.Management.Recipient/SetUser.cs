using System;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000105 RID: 261
	[Cmdlet("Set", "User", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetUser : SetADUserBase<UserIdParameter, User>
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00045981 File Offset: 0x00043B81
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x000459A7 File Offset: 0x00043BA7
		[Parameter(Mandatory = false)]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x000459BF File Offset: 0x00043BBF
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x000459E5 File Offset: 0x00043BE5
		[Parameter(Mandatory = false)]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000459FD File Offset: 0x00043BFD
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x00045A23 File Offset: 0x00043C23
		[Parameter(Mandatory = false)]
		public SwitchParameter EnableAccount
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnableAccount"] ?? false);
			}
			set
			{
				base.Fields["EnableAccount"] = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00045A3B File Offset: 0x00043C3B
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x00045A52 File Offset: 0x00043C52
		[Parameter(Mandatory = false)]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields[UserSchema.LinkedMasterAccount];
			}
			set
			{
				base.Fields[UserSchema.LinkedMasterAccount] = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00045A65 File Offset: 0x00043C65
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00045A7C File Offset: 0x00043C7C
		[Parameter(Mandatory = false)]
		public string LinkedDomainController
		{
			get
			{
				return (string)base.Fields["LinkedDomainController"];
			}
			set
			{
				base.Fields["LinkedDomainController"] = value;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x00045A8F File Offset: 0x00043C8F
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x00045AA6 File Offset: 0x00043CA6
		[Parameter(Mandatory = false)]
		public PSCredential LinkedCredential
		{
			get
			{
				return (PSCredential)base.Fields["LinkedCredential"];
			}
			set
			{
				base.Fields["LinkedCredential"] = value;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00045AB9 File Offset: 0x00043CB9
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x00045AD0 File Offset: 0x00043CD0
		[Parameter(Mandatory = false)]
		public NetID BusinessNetID
		{
			get
			{
				return (NetID)base.Fields["BusinessNetID"];
			}
			set
			{
				base.Fields["BusinessNetID"] = value;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00045AE3 File Offset: 0x00043CE3
		// (set) Token: 0x06001309 RID: 4873 RVA: 0x00045B09 File Offset: 0x00043D09
		[Parameter(Mandatory = false)]
		public SwitchParameter CopyShadowAttributes
		{
			get
			{
				return (SwitchParameter)(base.Fields["CopyShadowAttributes"] ?? false);
			}
			set
			{
				base.Fields["CopyShadowAttributes"] = value;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00045B21 File Offset: 0x00043D21
		// (set) Token: 0x0600130B RID: 4875 RVA: 0x00045B47 File Offset: 0x00043D47
		[Parameter(Mandatory = false)]
		public SwitchParameter GenerateExternalDirectoryObjectId
		{
			get
			{
				return (SwitchParameter)(base.Fields["GenerateExternalDirectoryObjectId"] ?? false);
			}
			set
			{
				base.Fields["GenerateExternalDirectoryObjectId"] = value;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00045B5F File Offset: 0x00043D5F
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x00045B76 File Offset: 0x00043D76
		[Parameter(Mandatory = false)]
		public bool LEOEnabled
		{
			get
			{
				return (bool)base.Fields[ADRecipientSchema.LEOEnabled];
			}
			set
			{
				base.Fields[ADRecipientSchema.LEOEnabled] = value;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00045B8E File Offset: 0x00043D8E
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x00045BA5 File Offset: 0x00043DA5
		[Parameter(Mandatory = false)]
		public string UpgradeMessage
		{
			get
			{
				return (string)base.Fields["UpgradeMessage"];
			}
			set
			{
				base.Fields["UpgradeMessage"] = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00045BB8 File Offset: 0x00043DB8
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00045BCF File Offset: 0x00043DCF
		[Parameter(Mandatory = false)]
		public string UpgradeDetails
		{
			get
			{
				return (string)base.Fields["UpgradeDetails"];
			}
			set
			{
				base.Fields["UpgradeDetails"] = value;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00045BE2 File Offset: 0x00043DE2
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00045BF9 File Offset: 0x00043DF9
		[Parameter(Mandatory = false)]
		public UpgradeStage? UpgradeStage
		{
			get
			{
				return (UpgradeStage?)base.Fields["UpgradeStage"];
			}
			set
			{
				base.Fields["UpgradeStage"] = value;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x00045C11 File Offset: 0x00043E11
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x00045C28 File Offset: 0x00043E28
		[Parameter(Mandatory = false)]
		public DateTime? UpgradeStageTimeStamp
		{
			get
			{
				return (DateTime?)base.Fields["UpgradeStageTimeStamp"];
			}
			set
			{
				base.Fields["UpgradeStageTimeStamp"] = value;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00045C40 File Offset: 0x00043E40
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x00045C6F File Offset: 0x00043E6F
		[Parameter(Mandatory = false)]
		public MailboxRelease MailboxRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields["MailboxRelease"], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields["MailboxRelease"] = value.ToString();
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00045C8C File Offset: 0x00043E8C
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x00045CBB File Offset: 0x00043EBB
		[Parameter(Mandatory = false)]
		public MailboxRelease ArchiveRelease
		{
			get
			{
				MailboxRelease result;
				if (!Enum.TryParse<MailboxRelease>((string)base.Fields["ArchiveRelease"], true, out result))
				{
					return MailboxRelease.None;
				}
				return result;
			}
			set
			{
				base.Fields["ArchiveRelease"] = value.ToString();
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00045CD8 File Offset: 0x00043ED8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.LinkedMasterAccount != null)
			{
				this.ValidateLinkedMasterAccount();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00045CF8 File Offset: 0x00043EF8
		protected override IConfigurable ResolveDataObject()
		{
			ADUser aduser = (ADUser)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(aduser, this.Arbitration) || MailboxTaskHelper.ExcludePublicFolderMailbox(aduser, this.PublicFolder) || MailboxTaskHelper.ExcludeMailboxPlan(aduser, false))
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.Identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), ErrorCategory.InvalidData, this.Identity);
			}
			if (base.Fields.IsModified(UserSchema.LinkedMasterAccount))
			{
				aduser.MasterAccountSid = this.linkedUserSid;
				if (aduser.MasterAccountSid != null)
				{
					this.ResolveLinkedUser(aduser);
				}
				else
				{
					this.ResolveUnlinkedUser(aduser);
				}
			}
			return aduser;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00045DC8 File Offset: 0x00043FC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IRecipientSession recipientSession = (IRecipientSession)base.DataSession;
			if (this.GenerateExternalDirectoryObjectId && string.IsNullOrEmpty(this.DataObject.ExternalDirectoryObjectId))
			{
				this.DataObject.ExternalDirectoryObjectId = Guid.NewGuid().ToString();
			}
			if (this.BusinessNetID != null)
			{
				this.DataObject.ConsumerNetID = this.DataObject.NetID;
				this.DataObject.NetID = this.BusinessNetID;
			}
			if (this.CopyShadowAttributes)
			{
				foreach (PropertyDefinition propertyDefinition in this.DataObject.Schema.AllProperties)
				{
					ADPropertyDefinition adpropertyDefinition = propertyDefinition as ADPropertyDefinition;
					if (adpropertyDefinition != null)
					{
						object value = null;
						if (adpropertyDefinition.ShadowProperty != null && this.DataObject.propertyBag.TryGetField(adpropertyDefinition, ref value))
						{
							this.DataObject.propertyBag[adpropertyDefinition.ShadowProperty] = value;
						}
					}
				}
			}
			if (this.EnableAccount.IsPresent && this.DataObject.UserAccountControl == (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.PasswordNotRequired | UserAccountControlFlags.NormalAccount))
			{
				this.DataObject.UserAccountControl = UserAccountControlFlags.NormalAccount;
				using (SecureString randomPassword = MailboxTaskUtilities.GetRandomPassword(this.DataObject.Name, this.DataObject.SamAccountName))
				{
					recipientSession.SetPassword(this.DataObject, randomPassword);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.LEOEnabled))
			{
				this.DataObject.LEOEnabled = this.LEOEnabled;
			}
			if (base.Fields.IsModified("UpgradeMessage"))
			{
				this.DataObject.UpgradeMessage = this.UpgradeMessage;
			}
			if (base.Fields.IsModified("UpgradeDetails"))
			{
				this.DataObject.UpgradeDetails = this.UpgradeDetails;
			}
			if (base.Fields.IsModified("UpgradeStage"))
			{
				this.DataObject.UpgradeStage = this.UpgradeStage;
			}
			if (base.Fields.IsModified("UpgradeStageTimeStamp"))
			{
				this.DataObject.UpgradeStageTimeStamp = this.UpgradeStageTimeStamp;
			}
			if (base.Fields.IsModified("MailboxRelease"))
			{
				this.DataObject.MailboxRelease = this.MailboxRelease;
			}
			if (base.Fields.IsModified("ArchiveRelease"))
			{
				this.DataObject.ArchiveRelease = this.ArchiveRelease;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00046068 File Offset: 0x00044268
		protected override void InternalValidate()
		{
			bool flag = base.CurrentTaskContext.ExchangeRunspaceConfig != null && base.CurrentTaskContext.ExchangeRunspaceConfig.IsAppPasswordUsed;
			if (base.UserSpecifiedParameters.IsModified(UserSchema.ResetPasswordOnNextLogon) && flag)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorChangePasswordForAppPasswordAccount), ExchangeErrorCategory.Client, this.Identity);
			}
			base.InternalValidate();
			if (this.GenerateExternalDirectoryObjectId && (RecipientTaskHelper.GetAcceptedRecipientTypes() & this.DataObject.RecipientTypeDetails) == RecipientTypeDetails.None)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorCannotGenerateExternalDirectoryObjectIdOnInternalRecipientType(this.Identity.ToString(), this.DataObject.RecipientTypeDetails.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0004612C File Offset: 0x0004432C
		private void ValidateLinkedMasterAccount()
		{
			if (string.IsNullOrEmpty(this.LinkedDomainController))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMissLinkedDomainController), ErrorCategory.InvalidArgument, this.Identity);
			}
			try
			{
				NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
				this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			}
			catch (PSArgumentException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000461D0 File Offset: 0x000443D0
		private void ResolveLinkedUser(ADUser user)
		{
			if (user.RecipientTypeDetails == RecipientTypeDetails.UserMailbox)
			{
				user.RecipientTypeDetails = RecipientTypeDetails.LinkedMailbox;
			}
			else if (user.RecipientTypeDetails == RecipientTypeDetails.RoomMailbox)
			{
				user.RecipientTypeDetails = RecipientTypeDetails.LinkedRoomMailbox;
			}
			this.GrantPermissionToLinkedUser(user);
			if (user.RecipientTypeDetails != RecipientTypeDetails.LinkedRoomMailbox)
			{
				user.RecipientDisplayType = new RecipientDisplayType?(SetUser.TryToSetACLableFlag(user.RecipientDisplayType.Value));
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00046244 File Offset: 0x00044444
		private void ResolveUnlinkedUser(ADUser user)
		{
			if (user.RecipientTypeDetails == RecipientTypeDetails.LinkedMailbox)
			{
				user.RecipientTypeDetails = RecipientTypeDetails.UserMailbox;
			}
			if (this.IsAccountDisabled(user))
			{
				user.RecipientDisplayType = new RecipientDisplayType?(SetUser.TryToClearACLableFlag(user.RecipientDisplayType.Value));
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0004628A File Offset: 0x0004448A
		private bool IsAccountDisabled(ADUser user)
		{
			return (user.UserAccountControl & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.AccountDisabled;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00046298 File Offset: 0x00044498
		private void GrantPermissionToLinkedUser(ADUser user)
		{
			if (this.IsAccountDisabled(user))
			{
				return;
			}
			user.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
			MailboxTaskHelper.GrantPermissionToLinkedUserAccount(user, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.VerboseSaveADSecurityDescriptor(user.Id.ToString()));
			}
			user.SaveSecurityDescriptor(((SecurityDescriptor)user[ADObjectSchema.NTSecurityDescriptor]).ToRawSecurityDescriptor());
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00046308 File Offset: 0x00044508
		private static RecipientDisplayType TryToSetACLableFlag(RecipientDisplayType displayType)
		{
			int num = 1073741824;
			if ((displayType & (RecipientDisplayType)num) == (RecipientDisplayType)num)
			{
				return displayType;
			}
			if (displayType == RecipientDisplayType.SyncedUSGasUDG)
			{
				return RecipientDisplayType.SyncedUSGasUSG;
			}
			if (displayType == RecipientDisplayType.DistributionGroup)
			{
				return RecipientDisplayType.SecurityDistributionGroup;
			}
			int num2 = (int)(displayType | (RecipientDisplayType)num);
			if (!Enum.IsDefined(typeof(RecipientDisplayType), num2))
			{
				return displayType;
			}
			return (RecipientDisplayType)num2;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004635C File Offset: 0x0004455C
		private static RecipientDisplayType TryToClearACLableFlag(RecipientDisplayType displayType)
		{
			int num = 1073741824;
			if ((displayType & (RecipientDisplayType)num) != (RecipientDisplayType)num)
			{
				return displayType;
			}
			if (displayType == RecipientDisplayType.SyncedUSGasUSG)
			{
				return RecipientDisplayType.SyncedUSGasUDG;
			}
			if (displayType == RecipientDisplayType.SecurityDistributionGroup)
			{
				return RecipientDisplayType.DistributionGroup;
			}
			int num2 = (int)(displayType & (RecipientDisplayType)(~(RecipientDisplayType)num));
			if (!Enum.IsDefined(typeof(RecipientDisplayType), num2))
			{
				return displayType;
			}
			return (RecipientDisplayType)num2;
		}

		// Token: 0x040003AE RID: 942
		public const string UpgradeMessageParameter = "UpgradeMessage";

		// Token: 0x040003AF RID: 943
		public const string UpgradeDetailsParameter = "UpgradeDetails";

		// Token: 0x040003B0 RID: 944
		public const string UpgradeStageParameter = "UpgradeStage";

		// Token: 0x040003B1 RID: 945
		public const string UpgradeStageTimeStampParameter = "UpgradeStageTimeStamp";

		// Token: 0x040003B2 RID: 946
		public const string MailboxReleaseParameter = "MailboxRelease";

		// Token: 0x040003B3 RID: 947
		public const string ArchiveReleaseParameter = "ArchiveRelease";

		// Token: 0x040003B4 RID: 948
		private SecurityIdentifier linkedUserSid;
	}
}
