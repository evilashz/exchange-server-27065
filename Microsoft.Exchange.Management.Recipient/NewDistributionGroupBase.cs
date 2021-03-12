using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000025 RID: 37
	public abstract class NewDistributionGroupBase : NewMailEnabledRecipientObjectTask<ADGroup>
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00009588 File Offset: 0x00007788
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewDistributionGroup(base.Name.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000095A5 File Offset: 0x000077A5
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000095C6 File Offset: 0x000077C6
		[Parameter]
		public GroupType Type
		{
			get
			{
				return (GroupType)(base.Fields[ADGroupSchema.GroupType] ?? GroupType.Distribution);
			}
			set
			{
				base.Fields[ADGroupSchema.GroupType] = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000095DE File Offset: 0x000077DE
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x000095EB File Offset: 0x000077EB
		[Parameter]
		public string SamAccountName
		{
			get
			{
				return this.DataObject.SamAccountName;
			}
			set
			{
				this.DataObject.SamAccountName = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000095F9 File Offset: 0x000077F9
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009610 File Offset: 0x00007810
		[Parameter]
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009623 File Offset: 0x00007823
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000963A File Offset: 0x0000783A
		[Parameter]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> Members
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields[ADGroupSchema.Members];
			}
			set
			{
				base.Fields[ADGroupSchema.Members] = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000964D File Offset: 0x0000784D
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000965A File Offset: 0x0000785A
		[Parameter(Mandatory = false)]
		public MemberUpdateType MemberJoinRestriction
		{
			get
			{
				return this.DataObject.MemberJoinRestriction;
			}
			set
			{
				this.DataObject.MemberJoinRestriction = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00009668 File Offset: 0x00007868
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00009675 File Offset: 0x00007875
		[Parameter(Mandatory = false)]
		public MemberUpdateType MemberDepartRestriction
		{
			get
			{
				return this.DataObject.MemberDepartRestriction;
			}
			set
			{
				this.DataObject.MemberDepartRestriction = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00009683 File Offset: 0x00007883
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00009690 File Offset: 0x00007890
		[Parameter]
		public bool BypassNestedModerationEnabled
		{
			get
			{
				return this.DataObject.BypassNestedModerationEnabled;
			}
			set
			{
				this.DataObject.BypassNestedModerationEnabled = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000969E File Offset: 0x0000789E
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000096B5 File Offset: 0x000078B5
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this.DataObject[ADRecipientSchema.Notes];
			}
			set
			{
				this.DataObject[ADRecipientSchema.Notes] = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000096C8 File Offset: 0x000078C8
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000096EE File Offset: 0x000078EE
		[Parameter]
		public SwitchParameter CopyOwnerToMember
		{
			get
			{
				return (SwitchParameter)(base.Fields["CopyOwnerToMember"] ?? false);
			}
			set
			{
				base.Fields["CopyOwnerToMember"] = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00009706 File Offset: 0x00007906
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000972C File Offset: 0x0000792C
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

		// Token: 0x060001C6 RID: 454 RVA: 0x00009744 File Offset: 0x00007944
		public NewDistributionGroupBase()
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000974C File Offset: 0x0000794C
		protected override void StampDefaultValues(ADGroup dataObject)
		{
			base.StampDefaultValues(dataObject);
			dataObject.StampDefaultValues(RecipientType.MailUniversalSecurityGroup);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000975C File Offset: 0x0000795C
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009782 File Offset: 0x00007982
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

		// Token: 0x060001CA RID: 458 RVA: 0x0000979C File Offset: 0x0000799C
		protected override void PrepareRecipientObject(ADGroup group)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(group);
			Organization organization;
			if (base.Organization == null)
			{
				organization = this.ConfigurationSession.GetOrgContainer();
			}
			else
			{
				organization = this.ConfigurationSession.Read<ExchangeConfigurationUnit>(base.CurrentOrgContainerId);
			}
			ADObjectId adobjectId = null;
			base.TryGetExecutingUserId(out adobjectId);
			if (!this.IgnoreNamingPolicy.IsPresent && adobjectId != null)
			{
				ADUser user = (ADUser)RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(null, adobjectId).Read(adobjectId);
				string groupNameWithNamingPolicy = DistributionGroupTaskHelper.GetGroupNameWithNamingPolicy(organization, user, group, base.Name, ADObjectSchema.Name, new Task.ErrorLoggerDelegate(base.WriteError));
				if (groupNameWithNamingPolicy.Length > 64)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorDistributionGroupNameTooLong), ExchangeErrorCategory.Client, null);
				}
				base.Name = groupNameWithNamingPolicy;
				if (!string.IsNullOrEmpty(base.DisplayName))
				{
					base.DisplayName = DistributionGroupTaskHelper.GetGroupNameWithNamingPolicy(organization, user, group, base.DisplayName, ADRecipientSchema.DisplayName, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (base.OrganizationalUnit == null && !ADObjectId.IsNullOrEmpty(organization.DistributionGroupDefaultOU))
			{
				group.SetId(organization.DistributionGroupDefaultOU.GetChildId(base.Name));
			}
			if (base.OrganizationalUnit == null && group[ADRecipientSchema.DefaultDistributionListOU] != null)
			{
				ADObjectId adobjectId2 = (ADObjectId)group[ADRecipientSchema.DefaultDistributionListOU];
				RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(new OrganizationalUnitIdParameter(adobjectId2), this.ConfigurationSession, base.CurrentOrganizationId, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.Client, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				group.SetId(adobjectId2.GetChildId(base.Name));
			}
			if (this.Type != GroupType.Distribution && this.Type != GroupType.Security)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorGroupTypeInvalid), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified(DistributionGroupSchema.ManagedBy))
			{
				MailboxTaskHelper.StampOnManagedBy(this.DataObject, this.managedByRecipients, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.RoomList.IsPresent)
			{
				if (this.Type != GroupType.Distribution)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorCreateRoomListSecurityGroup(base.Name)), ExchangeErrorCategory.Client, base.Name);
				}
				group.RecipientTypeDetails = RecipientTypeDetails.RoomList;
				if (group.ManagedBy != null)
				{
					group.AcceptMessagesOnlyFromSendersOrMembers = new MultiValuedProperty<ADObjectId>(group.ManagedBy);
				}
			}
			MailboxTaskHelper.ValidateGroupManagedBy(base.TenantGlobalCatalogSession, group, this.managedByRecipients, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError));
			MailboxTaskHelper.ValidateGroupManagedByRecipientRestriction(base.TenantGlobalCatalogSession, group, this.managedByRecipients, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			group.GroupType = (GroupTypeFlags)((GroupType)8 | this.Type);
			if (!group.IsChanged(ADRecipientSchema.RecipientDisplayType))
			{
				if ((group.GroupType & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled)
				{
					group.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.SecurityDistributionGroup);
				}
				else
				{
					group.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DistributionGroup);
				}
			}
			if (string.IsNullOrEmpty(group.SamAccountName))
			{
				IRecipientSession[] recipientSessions = new IRecipientSession[]
				{
					base.RootOrgGlobalCatalogSession
				};
				if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.ServiceAccountForest.Enabled && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId)
				{
					recipientSessions = new IRecipientSession[]
					{
						base.RootOrgGlobalCatalogSession,
						base.PartitionOrRootOrgGlobalCatalogSession
					};
				}
				group.SamAccountName = RecipientTaskHelper.GenerateUniqueSamAccountName(recipientSessions, group.Id.DomainId, group.Name, true, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), false);
			}
			else
			{
				RecipientTaskHelper.IsSamAccountNameUnique(group, group.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (string.IsNullOrEmpty(group.Alias))
			{
				group.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, base.CurrentOrganizationId, group.SamAccountName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (base.Fields.IsChanged(ADGroupSchema.Members) && this.Members != null)
			{
				foreach (RecipientIdParameter member in this.Members)
				{
					MailboxTaskHelper.ValidateAndAddMember(base.TenantGlobalCatalogSession, group, member, false, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
				}
			}
			if (this.CopyOwnerToMember.IsPresent && this.managedByRecipients != null)
			{
				foreach (ADRecipient adrecipient in this.managedByRecipients)
				{
					if (!group.Members.Contains(adrecipient.Id))
					{
						MailboxTaskHelper.ValidateMemberInGroup(adrecipient, group, new Task.ErrorLoggerDelegate(base.WriteError));
						group.Members.Add(adrecipient.Id);
					}
				}
			}
			if ((group.GroupType & GroupTypeFlags.Universal) == GroupTypeFlags.Universal)
			{
				MailboxTaskHelper.ValidateAddedMembers(base.TenantGlobalCatalogSession, group, new Task.ErrorLoggerDelegate(base.WriteError), new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>));
			}
			if (!this.DataObject.IsModified(ADGroupSchema.MemberDepartRestriction))
			{
				this.DataObject.MemberDepartRestriction = ((this.Type == GroupType.Security) ? MemberUpdateType.Closed : MemberUpdateType.Open);
			}
			if (group.ArbitrationMailbox == null)
			{
				group.ArbitrationMailbox = MailboxTaskHelper.GetArbitrationMailbox(base.TenantGlobalCatalogSession, base.CurrentOrgContainerId);
				if (group.ArbitrationMailbox == null)
				{
					if (group.MemberJoinRestriction == MemberUpdateType.ApprovalRequired || group.MemberDepartRestriction == MemberUpdateType.ApprovalRequired)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMbxNotSetForApproval(base.Name)), ExchangeErrorCategory.Client, group.Identity);
					}
					if (group.ModerationEnabled)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorArbitrationMbxNotSetForModeration(base.Name)), ExchangeErrorCategory.Client, group.Identity);
					}
				}
			}
			DistributionGroupTaskHelper.CheckMembershipRestriction(group, new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009DA8 File Offset: 0x00007FA8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.EmailAddressPolicy.Enabled)
			{
				this.DataObject.EmailAddressPolicyEnabled = false;
			}
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServer);
			TaskLogger.LogExit();
		}

		// Token: 0x04000045 RID: 69
		protected MultiValuedProperty<ADRecipient> managedByRecipients;

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x060001CD RID: 461
		internal delegate IConfigurable GetUniqueObject(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError);
	}
}
