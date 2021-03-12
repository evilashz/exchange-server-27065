using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000BD RID: 189
	[Cmdlet("Set", "SyncDistributionGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncDistributionGroup : SetDistributionGroupBase<SyncDistributionGroup>
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00031F4A File Offset: 0x0003014A
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00031F61 File Offset: 0x00030161
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<DeliveryRecipientIdParameter>)base.Fields[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00031F74 File Offset: 0x00030174
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00031F8B File Offset: 0x0003018B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<DeliveryRecipientIdParameter> BypassModerationFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<DeliveryRecipientIdParameter>)base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00031F9E File Offset: 0x0003019E
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00031FB5 File Offset: 0x000301B5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawAcceptMessagesOnlyFrom
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>)base.Fields["RawAcceptMessagesOnlyFrom"];
			}
			set
			{
				base.Fields["RawAcceptMessagesOnlyFrom"] = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00031FC8 File Offset: 0x000301C8
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00031FDF File Offset: 0x000301DF
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawBypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>)base.Fields["RawBypassModerationFrom"];
			}
			set
			{
				base.Fields["RawBypassModerationFrom"] = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00031FF2 File Offset: 0x000301F2
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00032009 File Offset: 0x00030209
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> RawRejectMessagesFrom
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>)base.Fields["RawRejectMessagesFrom"];
			}
			set
			{
				base.Fields["RawRejectMessagesFrom"] = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0003201C File Offset: 0x0003021C
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00032033 File Offset: 0x00030233
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawGrantSendOnBehalfTo
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["RawGrantSendOnBehalfTo"];
			}
			set
			{
				base.Fields["RawGrantSendOnBehalfTo"] = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00032046 File Offset: 0x00030246
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x0003205D File Offset: 0x0003025D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>> RawModeratedBy
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<ModeratorIDParameter>>)base.Fields["RawModeratedBy"];
			}
			set
			{
				base.Fields["RawModeratedBy"] = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00032070 File Offset: 0x00030270
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00032087 File Offset: 0x00030287
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>> RawMembers
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>)base.Fields["RawMembers"];
			}
			set
			{
				base.Fields["RawMembers"] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0003209A File Offset: 0x0003029A
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x000320B1 File Offset: 0x000302B1
		[Parameter(Mandatory = false)]
		public RecipientWithAdUserIdParameter<RecipientIdParameter> RawManagedBy
		{
			get
			{
				return (RecipientWithAdUserIdParameter<RecipientIdParameter>)base.Fields[ADGroupSchema.RawManagedBy];
			}
			set
			{
				base.Fields[ADGroupSchema.RawManagedBy] = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000320C4 File Offset: 0x000302C4
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000320DB File Offset: 0x000302DB
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawCoManagedBy
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["RawCoManagedBy"];
			}
			set
			{
				base.Fields["RawCoManagedBy"] = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000320EE File Offset: 0x000302EE
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00032114 File Offset: 0x00030314
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotCheckAcceptedDomains
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotCheckAcceptedDomains"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DoNotCheckAcceptedDomains"] = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0003212C File Offset: 0x0003032C
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00032143 File Offset: 0x00030343
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SmtpAndX500Addresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SmtpAndX500Addresses"];
			}
			set
			{
				base.Fields["SmtpAndX500Addresses"] = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00032156 File Offset: 0x00030356
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0003216D File Offset: 0x0003036D
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SipAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SipAddresses"];
			}
			set
			{
				base.Fields["SipAddresses"] = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00032180 File Offset: 0x00030380
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x000321A1 File Offset: 0x000303A1
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000321B9 File Offset: 0x000303B9
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000321DF File Offset: 0x000303DF
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedObject"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000321F7 File Offset: 0x000303F7
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00032208 File Offset: 0x00030408
		internal override ADRecipient GetRecipient(RecipientIdParameter recipientIdParameter, Task.ErrorLoggerDelegate writeError)
		{
			bool includeSoftDeletedObjects = base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects;
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			ADRecipient recipient = base.GetRecipient(recipientIdParameter, writeError);
			base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			return recipient;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00032262 File Offset: 0x00030462
		internal override IReferenceErrorReporter ReferenceErrorReporter
		{
			get
			{
				if (this.batchReferenceErrorReporter == null)
				{
					this.batchReferenceErrorReporter = new BatchReferenceErrorReporter();
				}
				return this.batchReferenceErrorReporter;
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0003227D File Offset: 0x0003047D
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			MultiLinkSyncHelper.ValidateIncompatibleParameters(base.Fields, this.GetIncompatibleParametersDictionary(), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			TaskLogger.LogExit();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000322AC File Offset: 0x000304AC
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			SyncDistributionGroup dataObject = (SyncDistributionGroup)this.GetDynamicParameters();
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFrom, this.BypassModerationFrom, dataObject, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFromDLMembers, this.BypassModerationFromDLMembers, dataObject, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionGroup));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawAcceptMessagesOnlyFrom", MailEnabledRecipientSchema.AcceptMessagesOnlyFrom, this.RawAcceptMessagesOnlyFrom, "RawAcceptMessagesOnlyFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawBypassModerationFrom", MailEnabledRecipientSchema.BypassModerationFrom, this.RawBypassModerationFrom, "RawBypassModerationFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawRejectMessagesFrom", MailEnabledRecipientSchema.RejectMessagesFrom, this.RawRejectMessagesFrom, "RawRejectMessagesFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawGrantSendOnBehalfTo", MailEnabledRecipientSchema.GrantSendOnBehalfTo, this.RawGrantSendOnBehalfTo, "RawGrantSendOnBehalfTo", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateGrantSendOnBehalfTo)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<ModeratorIDParameter>>("RawModeratedBy", MailEnabledRecipientSchema.ModeratedBy, this.RawModeratedBy, "RawModeratedBy", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<ModeratorIDParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(RecipientTaskHelper.ValidateModeratedBy)));
			base.SetReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>(ADGroupSchema.RawManagedBy, this.RawManagedBy, dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawCoManagedBy", SyncDistributionGroupSchema.CoManagedBy, this.RawCoManagedBy, "RawCoManagedBy", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), null);
			base.SetMultiReferenceParameter<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>("RawMembers", SyncDistributionGroupSchema.Members, this.RawMembers, "RawMembers", dataObject, new GetRecipientDelegate<RecipientWithAdUserGroupIdParameter<RecipientIdParameter>>(this.GetRecipient), null);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000324B8 File Offset: 0x000306B8
		private Dictionary<object, ArrayList> GetIncompatibleParametersDictionary()
		{
			Dictionary<object, ArrayList> incompatibleParametersDictionaryForCommonMultiLink = MultiLinkSyncHelper.GetIncompatibleParametersDictionaryForCommonMultiLink();
			incompatibleParametersDictionaryForCommonMultiLink[DistributionGroupSchema.ManagedBy] = new ArrayList(new object[]
			{
				ADGroupSchema.RawManagedBy,
				"RawCoManagedBy",
				"RawCoManagedBy"
			});
			return incompatibleParametersDictionaryForCommonMultiLink;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000324FC File Offset: 0x000306FC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			base.ValidateReferenceParameter(SyncDistributionGroupSchema.RawManagedBy, SyncTaskHelper.ValidateWithBaseObjectBypassADUser<ADGroup>(new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupManagedBy)));
			base.ValidateMultiReferenceParameter("RawCoManagedBy", SyncTaskHelper.ValidateWithBaseObjectBypassADUser<ADGroup>(new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupManagedBy)));
			base.ValidateMultiReferenceParameter("RawMembers", SyncTaskHelper.ValidateWithBaseObjectBypassADUser<ADGroup>(new ValidateRecipientWithBaseObjectDelegate<ADGroup>(MailboxTaskHelper.ValidateGroupMember)));
			TaskLogger.LogExit();
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00032570 File Offset: 0x00030770
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADGroup adgroup = (ADGroup)base.PrepareDataObject();
			adgroup.BypassModerationCheck = true;
			if (this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0)
			{
				adgroup.EmailAddresses = SyncTaskHelper.ReplaceSmtpAndX500Addresses(this.SmtpAndX500Addresses, adgroup.EmailAddresses);
			}
			if (base.Fields.IsModified("SipAddresses"))
			{
				adgroup.EmailAddresses = SyncTaskHelper.ReplaceSipAddresses(this.SipAddresses, adgroup.EmailAddresses);
			}
			if (adgroup.IsModified(MailEnabledRecipientSchema.EmailAddresses))
			{
				adgroup.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, adgroup.EmailAddresses, adgroup, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (base.Fields.IsModified(ADGroupSchema.GroupType))
			{
				if (this.Type != GroupType.Distribution && this.Type != GroupType.Security)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorGroupTypeInvalid), ExchangeErrorCategory.Client, null);
				}
				bool flag = (adgroup.GroupType & GroupTypeFlags.SecurityEnabled) == GroupTypeFlags.SecurityEnabled;
				if (this.Type == GroupType.Distribution && flag)
				{
					adgroup.GroupType &= (GroupTypeFlags)2147483647;
					if ((adgroup.GroupType & GroupTypeFlags.Universal) == GroupTypeFlags.Universal)
					{
						adgroup.RecipientTypeDetails = RecipientTypeDetails.MailUniversalDistributionGroup;
					}
					else
					{
						adgroup.RecipientTypeDetails = RecipientTypeDetails.MailNonUniversalGroup;
					}
					base.DesiredRecipientType = adgroup.RecipientType;
					if (!adgroup.IsChanged(ADRecipientSchema.RecipientDisplayType))
					{
						adgroup.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DistributionGroup);
					}
				}
				else if (this.Type == GroupType.Security && !flag)
				{
					if (adgroup.RecipientTypeDetails == RecipientTypeDetails.RoomList)
					{
						base.WriteError(new TaskInvalidOperationException(Strings.ErrorConvertNonUniversalDistributionGroup(adgroup.Identity.ToString())), ExchangeErrorCategory.Client, adgroup.Identity);
					}
					adgroup.GroupType |= GroupTypeFlags.SecurityEnabled;
					if ((adgroup.GroupType & GroupTypeFlags.Universal) == GroupTypeFlags.Universal)
					{
						adgroup.RecipientTypeDetails = RecipientTypeDetails.MailUniversalSecurityGroup;
					}
					else
					{
						adgroup.RecipientTypeDetails = RecipientTypeDetails.MailNonUniversalGroup;
					}
					base.DesiredRecipientType = adgroup.RecipientType;
					if (!adgroup.IsChanged(ADRecipientSchema.RecipientDisplayType))
					{
						adgroup.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.SecurityDistributionGroup);
					}
				}
			}
			TaskLogger.LogExit();
			return adgroup;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0003279C File Offset: 0x0003099C
		protected override void UpdateRecipientDisplayType(ADGroup group)
		{
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0003279E File Offset: 0x0003099E
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncDistributionGroup.FromDataObject((ADGroup)dataObject);
		}

		// Token: 0x04000298 RID: 664
		private BatchReferenceErrorReporter batchReferenceErrorReporter;
	}
}
