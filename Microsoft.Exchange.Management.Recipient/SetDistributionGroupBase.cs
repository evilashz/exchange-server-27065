using System;
using System.Management.Automation;
using Microsoft.Exchange.Approval.Applications;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200002E RID: 46
	public abstract class SetDistributionGroupBase<TPublicObject> : SetMailEnabledRecipientObjectTask<DistributionGroupIdParameter, TPublicObject, ADGroup> where TPublicObject : DistributionGroup, new()
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000AA86 File Offset: 0x00008C86
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDistributionGroup(this.Identity.ToString());
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000AA98 File Offset: 0x00008C98
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000AAAF File Offset: 0x00008CAF
		[Parameter(Mandatory = false)]
		public string ExpansionServer
		{
			get
			{
				return (string)base.Fields["ExpansionServer"];
			}
			set
			{
				base.Fields["ExpansionServer"] = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000AAD9 File Offset: 0x00008CD9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<GeneralRecipientIdParameter> ManagedBy
		{
			get
			{
				return (MultiValuedProperty<GeneralRecipientIdParameter>)base.Fields[DistributionGroupSchema.ManagedBy];
			}
			set
			{
				base.Fields[DistributionGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000AAEC File Offset: 0x00008CEC
		// (set) Token: 0x06000227 RID: 551 RVA: 0x0000AB03 File Offset: 0x00008D03
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public MemberUpdateType MemberJoinRestriction
		{
			get
			{
				return (MemberUpdateType)base.Fields[ADGroupSchema.MemberJoinRestriction];
			}
			set
			{
				base.Fields[ADGroupSchema.MemberJoinRestriction] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000AB1B File Offset: 0x00008D1B
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000AB32 File Offset: 0x00008D32
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public MemberUpdateType MemberDepartRestriction
		{
			get
			{
				return (MemberUpdateType)base.Fields[ADGroupSchema.MemberDepartRestriction];
			}
			set
			{
				base.Fields[ADGroupSchema.MemberDepartRestriction] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000AB4A File Offset: 0x00008D4A
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000AB4D File Offset: 0x00008D4D
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000AB73 File Offset: 0x00008D73
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassSecurityGroupManagerCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassSecurityGroupManagerCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassSecurityGroupManagerCheck"] = value;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AB8B File Offset: 0x00008D8B
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return true;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000AB8E File Offset: 0x00008D8E
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		[Parameter(Mandatory = false)]
		public SwitchParameter RoomList
		{
			get
			{
				return (SwitchParameter)(base.Fields["RoomList"] ?? false);
			}
			set
			{
				base.Fields["RoomList"] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000ABCC File Offset: 0x00008DCC
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000ABF2 File Offset: 0x00008DF2
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreNamingPolicy
		{
			get
			{
				return (SwitchParameter)(base.Fields["IgnoreNamingPolicy"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IgnoreNamingPolicy"] = value;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AC0C File Offset: 0x00008E0C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified("ExpansionServer"))
			{
				if (string.IsNullOrEmpty(this.ExpansionServer))
				{
					this.ExpansionServer = string.Empty;
					this.homeMTA = null;
				}
				else
				{
					Server server = SetDynamicDistributionGroup.ResolveExpansionServer(this.ExpansionServer, base.GlobalConfigSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<Server>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
					base.ValidateExpansionServer(server, true);
					this.ExpansionServer = server.ExchangeLegacyDN;
					this.homeMTA = server.ResponsibleMTA;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000ACBC File Offset: 0x00008EBC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup group = (ADGroup)base.PrepareDataObject();
			this.flagCloseGroupMemberJoinForNoArbitrationMbx = false;
			this.flagCloseGroupMemberDepartForNoArbitrationMbx = false;
			this.UpdateRecipientDisplayType(group);
			ADObjectId adobjectId;
			bool flag = base.TryGetExecutingUserId(out adobjectId);
			if (!this.IgnoreNamingPolicy.IsPresent && (base.UserSpecifiedParameters.IsChanged(ADObjectSchema.Name.Name) || base.UserSpecifiedParameters.IsChanged(MailEnabledRecipientSchema.DisplayName.Name)))
			{
				Organization organization;
				if (group.OrganizationId.ConfigurationUnit == null && group.OrganizationId.OrganizationalUnit == null)
				{
					organization = this.ConfigurationSession.GetOrgContainer();
				}
				else
				{
					organization = this.ConfigurationSession.Read<ExchangeConfigurationUnit>(group.OrganizationId.ConfigurationUnit);
				}
				if (flag)
				{
					IRecipientSession recipientSession = RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(null, adobjectId);
					ADUser user = (ADUser)recipientSession.Read(adobjectId);
					if (base.UserSpecifiedParameters.IsChanged(ADObjectSchema.Name.Name))
					{
						group.Name = DistributionGroupTaskHelper.GetGroupNameWithNamingPolicy(organization, user, group, group.Name, ADObjectSchema.Name, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					if (base.UserSpecifiedParameters.IsChanged(MailEnabledRecipientSchema.DisplayName.Name))
					{
						group.DisplayName = DistributionGroupTaskHelper.GetGroupNameWithNamingPolicy(organization, user, group, group.DisplayName, ADRecipientSchema.DisplayName, new Task.ErrorLoggerDelegate(base.WriteError));
					}
				}
			}
			bool flag2 = false;
			ADScopeException ex = null;
			if (flag && adobjectId != null && !((IDirectorySession)base.DataSession).TryVerifyIsWithinScopes(group, true, out ex))
			{
				group.IsExecutingUserGroupOwner = true;
				flag2 = true;
				base.WriteVerbose(Strings.VerboseDGOwnershipDeepSearch(adobjectId.ToString(), group.Identity.ToString()));
				RecipientTaskHelper.ValidateUserIsGroupManager(adobjectId, group, delegate(LocalizedException exception, ExchangeErrorCategory category, object taget)
				{
					group.IsExecutingUserGroupOwner = false;
				}, true, base.TenantGlobalCatalogSession);
				group.propertyBag.ResetChangeTracking(ADGroupSchema.IsExecutingUserGroupOwner);
			}
			if (group.RecipientDisplayType == RecipientDisplayType.SecurityDistributionGroup && !flag2 && !this.BypassSecurityGroupManagerCheck && (base.Fields.IsChanged(DistributionGroupSchema.ManagedBy) || base.Fields.IsChanged(MailEnabledRecipientSchema.GrantSendOnBehalfTo) || base.Fields.IsChanged(ADObjectSchema.Name) || base.Fields.IsChanged(DistributionGroupSchema.SamAccountName)))
			{
				if (!flag)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorExecutingUserOutOfTargetOrg(base.MyInvocation.MyCommand.Name)), ExchangeErrorCategory.Client, group.Identity.ToString());
				}
				RecipientTaskHelper.ValidateUserIsGroupManager(adobjectId, group, new Task.ErrorLoggerDelegate(base.WriteError), true, base.TenantGlobalCatalogSession);
				group.IsExecutingUserGroupOwner = true;
				group.propertyBag.ResetChangeTracking(ADGroupSchema.IsExecutingUserGroupOwner);
			}
			base.SetMultiReferenceParameter<GeneralRecipientIdParameter>(DistributionGroupSchema.ManagedBy, this.ManagedBy, group, new GetRecipientDelegate<GeneralRecipientIdParameter>(this.GetRecipient));
			if (base.Fields.IsModified(ADGroupSchema.MemberJoinRestriction))
			{
				group.MemberJoinRestriction = this.MemberJoinRestriction;
			}
			if (base.Fields.IsModified(ADGroupSchema.MemberDepartRestriction))
			{
				group.MemberDepartRestriction = this.MemberDepartRestriction;
			}
			if (base.Fields.IsModified(ADRecipientSchema.ArbitrationMailbox))
			{
				if (base.ArbitrationMailbox == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorNullParameter(ADRecipientSchema.ArbitrationMailbox.Name)), ExchangeErrorCategory.Client, group.Identity);
				}
				ADObjectId arbitrationMailbox = group.ArbitrationMailbox;
			}
			if (!group.ExchangeVersion.IsOlderThan(ADRecipientSchema.ArbitrationMailbox.VersionAdded) && (group.ArbitrationMailbox == null || group.ArbitrationMailbox.IsDescendantOf(ADSession.GetDeletedObjectsContainer(group.ArbitrationMailbox.DomainId))))
			{
				group.ArbitrationMailbox = MailboxTaskHelper.GetArbitrationMailbox(base.TenantGlobalCatalogSession, group.ConfigurationUnit ?? base.RootOrgContainerId);
				if (group.ArbitrationMailbox == null)
				{
					if (group.MemberJoinRestriction == MemberUpdateType.ApprovalRequired)
					{
						if (base.Fields.IsModified(ADGroupSchema.MemberJoinRestriction))
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMbxNotSetForApproval(this.Identity.ToString())), ExchangeErrorCategory.Client, group.Identity);
						}
						else
						{
							group.MemberJoinRestriction = MemberUpdateType.Closed;
							this.flagCloseGroupMemberJoinForNoArbitrationMbx = true;
						}
					}
					if (group.MemberDepartRestriction == MemberUpdateType.ApprovalRequired)
					{
						if (base.Fields.IsModified(ADGroupSchema.MemberDepartRestriction))
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMbxNotSetForApproval(this.Identity.ToString())), ExchangeErrorCategory.Client, group.Identity);
						}
						else
						{
							group.MemberDepartRestriction = MemberUpdateType.Closed;
							this.flagCloseGroupMemberDepartForNoArbitrationMbx = true;
						}
					}
				}
			}
			TaskLogger.LogExit();
			return group;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B22E File Offset: 0x0000942E
		protected virtual void UpdateRecipientDisplayType(ADGroup group)
		{
			if ((group.GroupType & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled)
			{
				group.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.SecurityDistributionGroup);
				return;
			}
			group.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DistributionGroup);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B260 File Offset: 0x00009460
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			DistributionGroupTaskHelper.CheckMembershipRestriction(this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError));
			if (this.RoomList.IsPresent && this.DataObject.RecipientTypeDetails != RecipientTypeDetails.RoomList)
			{
				this.ValidateConvertToRoomList();
			}
			base.ValidateMultiReferenceParameter(DistributionGroupSchema.ManagedBy, new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupManagedBy));
			MultiValuedProperty<ADRecipient> recipients;
			this.recipientsDictionary.TryGetValue(DistributionGroupSchema.ManagedBy, out recipients);
			if (base.Fields.IsModified(DistributionGroupSchema.ManagedBy) || base.Fields.IsModified(DistributionGroupSchema.MemberJoinRestriction) || base.Fields.IsModified(DistributionGroupSchema.MemberDepartRestriction) || this.DataObject.ModerationEnabled || base.Fields.IsModified(MailEnabledRecipientSchema.ModeratedBy))
			{
				MailboxTaskHelper.ValidateGroupManagedByRecipientRestriction(base.TenantGlobalCatalogSession, this.DataObject, recipients, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.SamAccountName))
			{
				RecipientTaskHelper.IsSamAccountNameUnique(this.DataObject, this.DataObject.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (base.Fields.IsModified("ExpansionServer"))
			{
				this.DataObject.ExpansionServer = this.ExpansionServer;
				this.DataObject.HomeMTA = this.homeMTA;
			}
			else if (this.DataObject.IsChanged(DistributionGroupBaseSchema.ExpansionServer))
			{
				if (!string.IsNullOrEmpty(this.DataObject.ExpansionServer))
				{
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ExchangeLegacyDN, this.DataObject.ExpansionServer);
					base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.GlobalConfigSession, typeof(Server), filter, null, true));
					Server[] array = null;
					try
					{
						array = base.GlobalConfigSession.Find<Server>(null, QueryScope.SubTree, filter, null, 2);
					}
					finally
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.GlobalConfigSession));
					}
					switch (array.Length)
					{
					case 0:
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorServerNotFound(this.DataObject.ExpansionServer)), ExchangeErrorCategory.Client, this.Identity);
						return;
					case 1:
						base.ValidateExpansionServer(array[0], false);
						this.DataObject.ExpansionServer = array[0].ExchangeLegacyDN;
						break;
					case 2:
						base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorServerNotUnique(this.DataObject.ExpansionServer)), ExchangeErrorCategory.Client, this.Identity);
						return;
					}
					this.DataObject.HomeMTA = array[0].ResponsibleMTA;
				}
				else
				{
					this.DataObject.HomeMTA = null;
				}
			}
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServer);
			TaskLogger.LogExit();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B558 File Offset: 0x00009758
		protected override void InternalProcessRecord()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (this.DataObject.IsChanged(ADGroupSchema.MemberJoinRestriction))
			{
				switch (this.DataObject.MemberJoinRestriction)
				{
				case MemberUpdateType.Closed:
					if (!this.flagCloseGroupMemberJoinForNoArbitrationMbx)
					{
						flag = true;
					}
					break;
				case MemberUpdateType.Open:
					flag2 = true;
					break;
				}
			}
			if (this.DataObject.IsChanged(ADGroupSchema.MemberDepartRestriction))
			{
				switch (this.DataObject.MemberDepartRestriction)
				{
				case MemberUpdateType.Closed:
					if (!this.flagCloseGroupMemberDepartForNoArbitrationMbx)
					{
						flag3 = true;
					}
					break;
				case MemberUpdateType.Open:
					flag4 = true;
					break;
				}
			}
			TaskLogger.LogEnter();
			if (this.RoomList.IsPresent && this.DataObject.RecipientTypeDetails != RecipientTypeDetails.RoomList)
			{
				this.DataObject.RecipientTypeDetails = RecipientTypeDetails.RoomList;
			}
			base.InternalProcessRecord();
			if (this.flagCloseGroupMemberJoinForNoArbitrationMbx)
			{
				this.WriteWarning(Strings.WarningCloseGroupMemberJoinForNoArbitrationMbx(this.Identity.ToString()));
			}
			if (this.flagCloseGroupMemberDepartForNoArbitrationMbx)
			{
				this.WriteWarning(Strings.WarningCloseGroupMemberDepartForNoArbitrationMbx(this.Identity.ToString()));
			}
			if (flag)
			{
				this.BulkQueueOperation(true, false);
			}
			if (flag2)
			{
				this.BulkQueueOperation(true, true);
			}
			if (flag3)
			{
				this.BulkQueueOperation(false, false);
			}
			if (flag4)
			{
				this.BulkQueueOperation(false, true);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B6A0 File Offset: 0x000098A0
		private void BulkQueueOperation(bool isJoin, bool isApprove)
		{
			if (this.DataObject.ArbitrationMailbox == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMailboxNotSet(this.Identity.ToString())), ExchangeErrorCategory.Client, this.Identity);
			}
			ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(new MailboxIdParameter(this.DataObject.ArbitrationMailbox), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(this.DataObject.ArbitrationMailbox.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(this.DataObject.ArbitrationMailbox.ToString())), ExchangeErrorCategory.Client);
			if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMbxType(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (!adrecipient.IsValid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidArbitrationMailbox(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			ADObjectId identity;
			if (!base.TryGetExecutingUserId(out identity))
			{
				this.WriteWarning(Strings.WarningBulkOperationCannotDetermineRequester);
				return;
			}
			ADRecipient adrecipient2 = (ADRecipient)base.TenantGlobalCatalogSession.Read<ADRecipient>(identity);
			string text;
			if (isJoin)
			{
				text = AutoGroupApplication.BuildApprovalData("Add-DistributionGroupMember", this.DataObject.Id);
			}
			else
			{
				text = AutoGroupApplication.BuildApprovalData("Remove-DistributionGroupMember", this.DataObject.Id);
			}
			QueryFilter additionalFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.ApprovalApplicationId, 0),
				new TextFilter(MessageItemSchema.ApprovalApplicationData, text, MatchOptions.FullString, MatchFlags.IgnoreCase)
			});
			StoreObjectId[] array = ApprovalProcessor.QueryRequests(adrecipient.PrimarySmtpAddress, SmtpAddress.Empty, SmtpAddress.Empty, SmtpAddress.Empty, new ApprovalStatus?(ApprovalStatus.Unhandled), additionalFilter, null);
			if (array == null || array.Length == 0)
			{
				return;
			}
			foreach (StoreObjectId itemId in array)
			{
				if (isApprove)
				{
					ApprovalProcessor.ApproveRequest(adrecipient.PrimarySmtpAddress, itemId, adrecipient2.PrimarySmtpAddress, null);
				}
				else
				{
					ApprovalProcessor.RejectRequest(adrecipient.PrimarySmtpAddress, itemId, adrecipient2.PrimarySmtpAddress, null);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000B8BC File Offset: 0x00009ABC
		private void ValidateConvertToRoomList()
		{
			RecipientTypeDetails recipientTypeDetails = this.DataObject.RecipientTypeDetails;
			if (recipientTypeDetails != RecipientTypeDetails.MailUniversalDistributionGroup)
			{
				base.WriteError(new TaskInvalidOperationException(Strings.ErrorConvertNonUniversalDistributionGroup(this.DataObject.Identity.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			foreach (ADObjectId adobjectId in this.DataObject.Members)
			{
				IRecipientSession session = RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, adobjectId);
				ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(new RecipientIdParameter(adobjectId), session, null, new LocalizedString?(Strings.ErrorRecipientNotFound(adobjectId.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(adobjectId.ToString())), ExchangeErrorCategory.Client);
				if (adrecipient.RecipientTypeDetails != RecipientTypeDetails.RoomMailbox && adrecipient.RecipientTypeDetails != RecipientTypeDetails.RoomList && adrecipient.RecipientDisplayType != RecipientDisplayType.SyncedConferenceRoomMailbox)
				{
					base.WriteError(new TaskInvalidOperationException(Strings.ErrorConvertGroupContainsNonRoomMailbox(this.DataObject.Identity.ToString(), adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
				}
			}
		}

		// Token: 0x04000049 RID: 73
		private ADObjectId homeMTA;

		// Token: 0x0400004A RID: 74
		private bool flagCloseGroupMemberJoinForNoArbitrationMbx;

		// Token: 0x0400004B RID: 75
		private bool flagCloseGroupMemberDepartForNoArbitrationMbx;
	}
}
