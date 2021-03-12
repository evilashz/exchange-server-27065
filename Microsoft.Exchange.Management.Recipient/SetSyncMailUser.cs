using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CD RID: 205
	[Cmdlet("Set", "SyncMailUser", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncMailUser : SetMailUserBase<MailUserIdParameter, SyncMailUser>
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00039A4C File Offset: 0x00037C4C
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x00039A63 File Offset: 0x00037C63
		[Parameter(Mandatory = false)]
		public RecipientIdParameter ForwardingAddress
		{
			get
			{
				return (RecipientIdParameter)base.Fields[MailUserSchema.ForwardingAddress];
			}
			set
			{
				base.Fields[MailUserSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00039A76 File Offset: 0x00037C76
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00039A8D File Offset: 0x00037C8D
		[Parameter(Mandatory = false)]
		public UserContactIdParameter Manager
		{
			get
			{
				return (UserContactIdParameter)base.Fields[SyncMailUserSchema.Manager];
			}
			set
			{
				base.Fields[SyncMailUserSchema.Manager] = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00039AA0 File Offset: 0x00037CA0
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00039AB7 File Offset: 0x00037CB7
		[Parameter(Mandatory = false)]
		public MailboxPlanIdParameter IntendedMailboxPlanName
		{
			get
			{
				return (MailboxPlanIdParameter)base.Fields["IntendedMailboxPlan"];
			}
			set
			{
				base.Fields["IntendedMailboxPlan"] = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00039ACA File Offset: 0x00037CCA
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00039AE1 File Offset: 0x00037CE1
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00039AF4 File Offset: 0x00037CF4
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00039B0B File Offset: 0x00037D0B
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

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00039B1E File Offset: 0x00037D1E
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00039B35 File Offset: 0x00037D35
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

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00039B48 File Offset: 0x00037D48
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00039B5F File Offset: 0x00037D5F
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

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00039B72 File Offset: 0x00037D72
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00039B89 File Offset: 0x00037D89
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

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00039B9C File Offset: 0x00037D9C
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00039BB3 File Offset: 0x00037DB3
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

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00039BC6 File Offset: 0x00037DC6
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00039BDD File Offset: 0x00037DDD
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

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00039BF0 File Offset: 0x00037DF0
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00039C07 File Offset: 0x00037E07
		[Parameter(Mandatory = false)]
		public RecipientWithAdUserIdParameter<RecipientIdParameter> RawForwardingAddress
		{
			get
			{
				return (RecipientWithAdUserIdParameter<RecipientIdParameter>)base.Fields["RawForwardingAddress"];
			}
			set
			{
				base.Fields["RawForwardingAddress"] = value;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00039C1A File Offset: 0x00037E1A
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00039C2C File Offset: 0x00037E2C
		[Parameter(Mandatory = false)]
		public string C
		{
			get
			{
				return ((SyncMailUser)this.GetDynamicParameters()).C;
			}
			set
			{
				((SyncMailUser)this.GetDynamicParameters()).C = value;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00039C3F File Offset: 0x00037E3F
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00039C51 File Offset: 0x00037E51
		[Parameter(Mandatory = false)]
		public string Co
		{
			get
			{
				return ((SyncMailUser)this.GetDynamicParameters()).Co;
			}
			set
			{
				((SyncMailUser)this.GetDynamicParameters()).Co = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00039C64 File Offset: 0x00037E64
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00039C8A File Offset: 0x00037E8A
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

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x00039CA2 File Offset: 0x00037EA2
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x00039CB9 File Offset: 0x00037EB9
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x00039CCC File Offset: 0x00037ECC
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x00039CE3 File Offset: 0x00037EE3
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00039CF6 File Offset: 0x00037EF6
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x00039D0D File Offset: 0x00037F0D
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)base.Fields["ReleaseTrack"];
			}
			set
			{
				base.Fields["ReleaseTrack"] = value;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00039D25 File Offset: 0x00037F25
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x00039D4B File Offset: 0x00037F4B
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedMailUser
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00039D63 File Offset: 0x00037F63
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00039D7A File Offset: 0x00037F7A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxOwners
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["RawSiteMailboxOwners"];
			}
			set
			{
				base.Fields["RawSiteMailboxOwners"] = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x00039D8D File Offset: 0x00037F8D
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00039DA4 File Offset: 0x00037FA4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> RawSiteMailboxUsers
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>>)base.Fields["RawSiteMailboxUsers"];
			}
			set
			{
				base.Fields["RawSiteMailboxUsers"] = value;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00039DB7 File Offset: 0x00037FB7
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x00039DCE File Offset: 0x00037FCE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientIdParameter> SiteMailboxOwners
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields[SyncMailUserSchema.SiteMailboxOwners];
			}
			set
			{
				base.Fields[SyncMailUserSchema.SiteMailboxOwners] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00039DE1 File Offset: 0x00037FE1
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00039DF8 File Offset: 0x00037FF8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientIdParameter> SiteMailboxUsers
		{
			get
			{
				return (MultiValuedProperty<RecipientIdParameter>)base.Fields[SyncMailUserSchema.SiteMailboxUsers];
			}
			set
			{
				base.Fields[SyncMailUserSchema.SiteMailboxUsers] = value;
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00039E0C File Offset: 0x0003800C
		internal static MultiValuedProperty<ADObjectId> ResolveSiteMailboxOwnersReferenceParameter(IList<RecipientIdParameter> recipientIdParameters, IRecipientSession recipientSession, DataAccessHelper.CategorizedGetDataObjectDelegate getDataObject, WriteWarningDelegate writeWarning)
		{
			if (recipientIdParameters == null || recipientIdParameters.Count == 0)
			{
				return null;
			}
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
			foreach (RecipientIdParameter recipientIdParameter in recipientIdParameters)
			{
				ADRecipient adrecipient = null;
				try
				{
					adrecipient = (ADRecipient)getDataObject(recipientIdParameter, recipientSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
				}
				catch (ManagementObjectNotFoundException ex)
				{
					writeWarning(new LocalizedString(ex.Message));
					continue;
				}
				catch (ManagementObjectAmbiguousException ex2)
				{
					writeWarning(new LocalizedString(ex2.Message));
					continue;
				}
				if (adrecipient != null && (adrecipient.RecipientType == RecipientType.User || TeamMailboxMembershipHelper.IsUserQualifiedType(adrecipient)))
				{
					multiValuedProperty.Add((ADObjectId)adrecipient.Identity);
				}
			}
			return multiValuedProperty;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00039F1C File Offset: 0x0003811C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.SoftDeletedMailUser.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00039F4E File Offset: 0x0003814E
		internal override IRecipientSession CreateTenantGlobalCatalogSession(ADSessionSettings sessionSettings)
		{
			sessionSettings.IncludeSoftDeletedObjects = this.SoftDeletedMailUser;
			return base.CreateTenantGlobalCatalogSession(sessionSettings);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00039F68 File Offset: 0x00038168
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00039F78 File Offset: 0x00038178
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

		// Token: 0x06000FEE RID: 4078 RVA: 0x00039F94 File Offset: 0x00038194
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			MultiLinkSyncHelper.ValidateIncompatibleParameters(base.Fields, this.GetIncompatibleParametersDictionary(), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			SyncMailUser syncMailUser = (SyncMailUser)this.GetDynamicParameters();
			if (syncMailUser.IsModified(SyncMailUserSchema.CountryOrRegion) && (syncMailUser.IsModified(SyncMailUserSchema.C) || syncMailUser.IsModified(SyncMailUserSchema.Co) || syncMailUser.IsModified(SyncMailUserSchema.CountryCode)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
			}
			if (syncMailUser.IsModified(SyncMailUserSchema.ResourceCustom) && (syncMailUser.IsModified(SyncMailUserSchema.ResourcePropertiesDisplay) || syncMailUser.IsModified(SyncMailUserSchema.ResourceSearchProperties)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictResourceCustom), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003A064 File Offset: 0x00038264
		protected override void ResolveLocalSecondaryIdentities()
		{
			bool includeSoftDeletedObjects = base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects;
			try
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = this.SoftDeletedMailUser;
				base.ResolveLocalSecondaryIdentities();
				this.InternalResolveLocalSecondaryIdentities();
			}
			finally
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003A124 File Offset: 0x00038324
		private void InternalResolveLocalSecondaryIdentities()
		{
			SyncMailUser syncMailUser = (SyncMailUser)this.GetDynamicParameters();
			base.SetReferenceParameter<UserContactIdParameter>(SyncMailUserSchema.Manager, this.Manager, syncMailUser, new GetRecipientDelegate<UserContactIdParameter>(this.GetRecipient));
			base.SetReferenceParameter<RecipientIdParameter>(MailUserSchema.ForwardingAddress, this.ForwardingAddress, syncMailUser, new GetRecipientDelegate<RecipientIdParameter>(this.GetRecipient));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFrom, this.BypassModerationFrom, syncMailUser, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFromDLMembers, this.BypassModerationFromDLMembers, syncMailUser, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionGroup));
			if (base.Fields.IsModified("IntendedMailboxPlan"))
			{
				this.intendedMailboxPlanObject = null;
				if (this.IntendedMailboxPlanName != null)
				{
					this.intendedMailboxPlanObject = base.ProvisioningCache.TryAddAndGetOrganizationDictionaryValue<ADUser, string>(CannedProvisioningCacheKeys.CacheKeyMailboxPlanIdParameterId, base.CurrentOrganizationId, this.IntendedMailboxPlanName.RawIdentity, () => (ADUser)base.GetDataObject<ADUser>(this.IntendedMailboxPlanName, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(this.IntendedMailboxPlanName.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(this.IntendedMailboxPlanName.ToString())), ExchangeErrorCategory.Client));
				}
			}
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawAcceptMessagesOnlyFrom", MailEnabledRecipientSchema.AcceptMessagesOnlyFrom, this.RawAcceptMessagesOnlyFrom, "RawAcceptMessagesOnlyFrom", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawBypassModerationFrom", MailEnabledRecipientSchema.BypassModerationFrom, this.RawBypassModerationFrom, "RawBypassModerationFrom", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawRejectMessagesFrom", MailEnabledRecipientSchema.RejectMessagesFrom, this.RawRejectMessagesFrom, "RawRejectMessagesFrom", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawGrantSendOnBehalfTo", MailEnabledRecipientSchema.GrantSendOnBehalfTo, this.RawGrantSendOnBehalfTo, "RawGrantSendOnBehalfTo", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateGrantSendOnBehalfTo)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<ModeratorIDParameter>>("RawModeratedBy", MailEnabledRecipientSchema.ModeratedBy, this.RawModeratedBy, "RawModeratedBy", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<ModeratorIDParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(RecipientTaskHelper.ValidateModeratedBy)));
			base.SetReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawForwardingAddress", MailUserSchema.ForwardingAddress, this.RawForwardingAddress, "RawForwardingAddress", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), null);
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawSiteMailboxOwners", SyncMailUserSchema.SiteMailboxOwners, this.RawSiteMailboxOwners, "RawSiteMailboxOwners", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(SetSyncMailUser.ValidateSiteMailboxUsers)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawSiteMailboxUsers", SyncMailUserSchema.SiteMailboxUsers, this.RawSiteMailboxUsers, "RawSiteMailboxUsers", syncMailUser, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(SetSyncMailUser.ValidateSiteMailboxUsers)));
			if (base.Fields.IsModified(SyncMailUserSchema.SiteMailboxOwners))
			{
				MultiValuedProperty<ADObjectId> value = SetSyncMailUser.ResolveSiteMailboxOwnersReferenceParameter(this.SiteMailboxOwners, base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new WriteWarningDelegate(this.WriteWarning));
				syncMailUser[SyncMailUserSchema.SiteMailboxOwners] = value;
			}
			base.SetMultiReferenceParameter<RecipientIdParameter>(SyncMailUserSchema.SiteMailboxUsers, SyncMailUserSchema.SiteMailboxUsers, this.SiteMailboxUsers, "SiteMailboxUsers", syncMailUser, new GetRecipientDelegate<RecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(SetSyncMailUser.ValidateSiteMailboxUsers));
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A46A File Offset: 0x0003866A
		private static void ValidateSiteMailboxUsers(ADRecipient recipient, string recipientId, Task.ErrorLoggerDelegate writeError)
		{
			if (!TeamMailboxMembershipHelper.IsUserQualifiedType(recipient))
			{
				writeError(new TaskInvalidOperationException(Strings.ErrorTeamMailboxUserNotResolved(recipient.Identity.ToString())), ExchangeErrorCategory.Client, recipient.Identity);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003A49A File Offset: 0x0003869A
		private Dictionary<object, ArrayList> GetIncompatibleParametersDictionary()
		{
			return MultiLinkSyncHelper.GetIncompatibleParametersDictionaryForCommonMultiLink();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A4A1 File Offset: 0x000386A1
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.intendedMailboxPlanObject != null)
			{
				RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, this.intendedMailboxPlanObject, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003A4D8 File Offset: 0x000386D8
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			aduser.BypassModerationCheck = true;
			if (base.Fields.IsModified("IntendedMailboxPlan"))
			{
				aduser.IntendedMailboxPlan = ((this.intendedMailboxPlanObject == null) ? null : this.intendedMailboxPlanObject.Id);
			}
			if (this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0)
			{
				aduser.EmailAddresses = SyncTaskHelper.ReplaceSmtpAndX500Addresses(this.SmtpAndX500Addresses, aduser.EmailAddresses);
			}
			if (base.Fields.IsModified("ReleaseTrack"))
			{
				aduser.ReleaseTrack = this.ReleaseTrack;
			}
			if (base.Fields.IsModified("SipAddresses"))
			{
				aduser.EmailAddresses = SyncTaskHelper.ReplaceSipAddresses(this.SipAddresses, aduser.EmailAddresses);
			}
			if (this.DataObject.IsModified(MailEnabledRecipientSchema.EmailAddresses))
			{
				aduser.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.DataObject.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003A5F4 File Offset: 0x000387F4
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADUser dataObject2 = (ADUser)dataObject;
			return new SyncMailUser(dataObject2);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003A610 File Offset: 0x00038810
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADUser aduser = (ADUser)dataObject;
			if (this.Instance.IsModified(SyncMailUserSchema.ElcMailboxFlags) && !VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.OverWriteElcMailboxFlags.Enabled)
			{
				ElcMailboxFlags elcMailboxFlags = aduser.ElcMailboxFlags & ElcMailboxFlags.ValidArchiveDatabase;
				this.Instance.ElcMailboxFlags |= elcMailboxFlags;
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x040002DD RID: 733
		private BatchReferenceErrorReporter batchReferenceErrorReporter;

		// Token: 0x040002DE RID: 734
		private ADUser intendedMailboxPlanObject;
	}
}
