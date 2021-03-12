using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000C4 RID: 196
	[Cmdlet("Set", "SyncMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncMailbox : SetMailboxBase<MailboxIdParameter, SyncMailbox>
	{
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00034E14 File Offset: 0x00033014
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00034E1C File Offset: 0x0003301C
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return base.AddressBookPolicy;
			}
			set
			{
				base.AddressBookPolicy = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00034E25 File Offset: 0x00033025
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00034E4A File Offset: 0x0003304A
		[Parameter(Mandatory = false)]
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)(base.Fields[MailboxSchema.ArchiveGuid] ?? Guid.Empty);
			}
			set
			{
				base.Fields[MailboxSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00034E62 File Offset: 0x00033062
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00034E79 File Offset: 0x00033079
		[Parameter(Mandatory = false)]
		public UserContactIdParameter Manager
		{
			get
			{
				return (UserContactIdParameter)base.Fields[SyncMailboxSchema.Manager];
			}
			set
			{
				base.Fields[SyncMailboxSchema.Manager] = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00034E8C File Offset: 0x0003308C
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00034E94 File Offset: 0x00033094
		[Parameter(Mandatory = false)]
		public MailboxPlanIdParameter MailboxPlanName
		{
			get
			{
				return base.MailboxPlan;
			}
			set
			{
				base.MailboxPlan = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00034E9D File Offset: 0x0003309D
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x00034EB4 File Offset: 0x000330B4
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

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00034EC7 File Offset: 0x000330C7
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00034EDE File Offset: 0x000330DE
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

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00034EF1 File Offset: 0x000330F1
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x00034F08 File Offset: 0x00033108
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x00034F1B File Offset: 0x0003311B
		// (set) Token: 0x06000D68 RID: 3432 RVA: 0x00034F32 File Offset: 0x00033132
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x00034F45 File Offset: 0x00033145
		// (set) Token: 0x06000D6A RID: 3434 RVA: 0x00034F5C File Offset: 0x0003315C
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00034F6F File Offset: 0x0003316F
		// (set) Token: 0x06000D6C RID: 3436 RVA: 0x00034F86 File Offset: 0x00033186
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00034F99 File Offset: 0x00033199
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00034FB0 File Offset: 0x000331B0
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

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00034FC3 File Offset: 0x000331C3
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00034FDA File Offset: 0x000331DA
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

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00034FED File Offset: 0x000331ED
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00034FFF File Offset: 0x000331FF
		[Parameter(Mandatory = false)]
		public string C
		{
			get
			{
				return ((SyncMailbox)this.GetDynamicParameters()).C;
			}
			set
			{
				((SyncMailbox)this.GetDynamicParameters()).C = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00035012 File Offset: 0x00033212
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x00035024 File Offset: 0x00033224
		[Parameter(Mandatory = false)]
		public string Co
		{
			get
			{
				return ((SyncMailbox)this.GetDynamicParameters()).Co;
			}
			set
			{
				((SyncMailbox)this.GetDynamicParameters()).Co = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00035037 File Offset: 0x00033237
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0003504E File Offset: 0x0003324E
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)base.Fields[SyncMailboxSchema.ReleaseTrack];
			}
			set
			{
				base.Fields[SyncMailboxSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00035066 File Offset: 0x00033266
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0003508C File Offset: 0x0003328C
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x000350A4 File Offset: 0x000332A4
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x000350BB File Offset: 0x000332BB
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x000350CE File Offset: 0x000332CE
		// (set) Token: 0x06000D7C RID: 3452 RVA: 0x000350E5 File Offset: 0x000332E5
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000350F8 File Offset: 0x000332F8
		// (set) Token: 0x06000D7E RID: 3454 RVA: 0x0003511E File Offset: 0x0003331E
		[Parameter(Mandatory = false)]
		public SwitchParameter SoftDeletedMailbox
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

		// Token: 0x06000D7F RID: 3455 RVA: 0x00035138 File Offset: 0x00033338
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.SoftDeletedMailbox.IsPresent)
			{
				recipientSession = SoftDeletedTaskHelper.GetSessionForSoftDeletedObjects(recipientSession, null);
			}
			return recipientSession;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003516A File Offset: 0x0003336A
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0003517A File Offset: 0x0003337A
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

		// Token: 0x06000D82 RID: 3458 RVA: 0x00035198 File Offset: 0x00033398
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			MultiLinkSyncHelper.ValidateIncompatibleParameters(base.Fields, this.GetIncompatibleParametersDictionary(), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			SyncMailbox syncMailbox = (SyncMailbox)this.GetDynamicParameters();
			if (syncMailbox.IsModified(SyncMailboxSchema.CountryOrRegion) && (syncMailbox.IsModified(SyncMailboxSchema.C) || syncMailbox.IsModified(SyncMailboxSchema.Co) || syncMailbox.IsModified(SyncMailboxSchema.CountryCode)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00035228 File Offset: 0x00033428
		protected override void ResolveLocalSecondaryIdentities()
		{
			bool includeSoftDeletedObjects = base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects;
			try
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = this.SoftDeletedMailbox;
				base.ResolveLocalSecondaryIdentities();
				this.InternalResolveLocalSecondaryIdentities();
			}
			finally
			{
				base.TenantGlobalCatalogSession.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00035294 File Offset: 0x00033494
		private void InternalResolveLocalSecondaryIdentities()
		{
			SyncMailbox dataObject = (SyncMailbox)this.GetDynamicParameters();
			base.SetReferenceParameter<UserContactIdParameter>(SyncMailboxSchema.Manager, this.Manager, dataObject, new GetRecipientDelegate<UserContactIdParameter>(this.GetRecipient));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFrom, this.BypassModerationFrom, dataObject, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFromDLMembers, this.BypassModerationFromDLMembers, dataObject, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionGroup));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawAcceptMessagesOnlyFrom", MailEnabledRecipientSchema.AcceptMessagesOnlyFrom, this.RawAcceptMessagesOnlyFrom, "RawAcceptMessagesOnlyFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawBypassModerationFrom", MailEnabledRecipientSchema.BypassModerationFrom, this.RawBypassModerationFrom, "RawBypassModerationFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawRejectMessagesFrom", MailEnabledRecipientSchema.RejectMessagesFrom, this.RawRejectMessagesFrom, "RawRejectMessagesFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawGrantSendOnBehalfTo", MailEnabledRecipientSchema.GrantSendOnBehalfTo, this.RawGrantSendOnBehalfTo, "RawGrantSendOnBehalfTo", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateGrantSendOnBehalfTo)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<ModeratorIDParameter>>("RawModeratedBy", MailEnabledRecipientSchema.ModeratedBy, this.RawModeratedBy, "RawModeratedBy", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<ModeratorIDParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(RecipientTaskHelper.ValidateModeratedBy)));
			base.SetReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawForwardingAddress", MailboxSchema.ForwardingAddress, this.RawForwardingAddress, "RawForwardingAddress", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), null);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0003546E File Offset: 0x0003366E
		private Dictionary<object, ArrayList> GetIncompatibleParametersDictionary()
		{
			return MultiLinkSyncHelper.GetIncompatibleParametersDictionaryForCommonMultiLink();
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00035478 File Offset: 0x00033678
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.latencyContext = ProvisioningPerformanceHelper.StartLatencyDetection(this);
			base.InternalValidate();
			if (this.DataObject.IsChanged(MailboxSchema.MasterAccountSid) && this.DataObject.IsChanged(MailboxSchema.LinkedMasterAccount))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorSyncMailboxWithMasterAccountSid(this.DataObject.MasterAccountSid.ToString(), this.DataObject.LinkedMasterAccount)), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (!base.NeedChangeMailboxSubtype && this.DataObject.IsChanged(MailboxSchema.MasterAccountSid) && this.DataObject.MasterAccountSid == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorSyncMailboxWithMasterAccountSidNull), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.ArchiveGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000355A8 File Offset: 0x000337A8
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			finally
			{
				ProvisioningPerformanceHelper.StopLatencyDetection(this.latencyContext);
			}
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000355DC File Offset: 0x000337DC
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADUser aduser = (ADUser)base.PrepareDataObject();
			aduser.BypassModerationCheck = true;
			if (this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0)
			{
				aduser.EmailAddresses = SyncTaskHelper.ReplaceSmtpAndX500Addresses(this.SmtpAndX500Addresses, aduser.EmailAddresses);
			}
			if (base.Fields.IsModified("SipAddresses"))
			{
				aduser.EmailAddresses = SyncTaskHelper.ReplaceSipAddresses(this.SipAddresses, aduser.EmailAddresses);
			}
			if (this.DataObject != null && this.DataObject.IsModified(MailEnabledRecipientSchema.EmailAddresses))
			{
				aduser.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.DataObject.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				aduser.EmailAddresses = SyncTaskHelper.UpdateSipNameEumProxyAddress(aduser.EmailAddresses);
			}
			if (base.Fields.IsModified(SyncMailboxSchema.ReleaseTrack))
			{
				aduser.ReleaseTrack = this.ReleaseTrack;
			}
			if (base.Fields.IsModified(MailboxSchema.ArchiveGuid))
			{
				aduser.ArchiveGuid = this.ArchiveGuid;
			}
			if (this.DataObject.IsModified(SyncMailboxSchema.AccountDisabled))
			{
				SyncTaskHelper.SetExchangeAccountDisabledWithADLogon(aduser, this.DataObject.AccountDisabled);
			}
			TaskLogger.LogExit();
			return aduser;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00035724 File Offset: 0x00033924
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADUser dataObject2 = (ADUser)dataObject;
			return new SyncMailbox(dataObject2);
		}

		// Token: 0x040002B4 RID: 692
		private BatchReferenceErrorReporter batchReferenceErrorReporter;

		// Token: 0x040002B5 RID: 693
		private LatencyDetectionContext latencyContext;
	}
}
