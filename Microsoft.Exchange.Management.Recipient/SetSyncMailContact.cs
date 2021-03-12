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
	// Token: 0x020000C9 RID: 201
	[Cmdlet("Set", "SyncMailContact", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetSyncMailContact : SetMailContactBase<SyncMailContact>
	{
		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0003738D File Offset: 0x0003558D
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x000373A4 File Offset: 0x000355A4
		[Parameter(Mandatory = false)]
		public UserContactIdParameter Manager
		{
			get
			{
				return (UserContactIdParameter)base.Fields[SyncMailContactSchema.Manager];
			}
			set
			{
				base.Fields[SyncMailContactSchema.Manager] = value;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000373B7 File Offset: 0x000355B7
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x000373CE File Offset: 0x000355CE
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>)base.Fields[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x000373E1 File Offset: 0x000355E1
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x000373F8 File Offset: 0x000355F8
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

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003740B File Offset: 0x0003560B
		// (set) Token: 0x06000E9D RID: 3741 RVA: 0x00037422 File Offset: 0x00035622
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

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00037435 File Offset: 0x00035635
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0003744C File Offset: 0x0003564C
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

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0003745F File Offset: 0x0003565F
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00037476 File Offset: 0x00035676
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

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00037489 File Offset: 0x00035689
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x000374A0 File Offset: 0x000356A0
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

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x000374B3 File Offset: 0x000356B3
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x000374CA File Offset: 0x000356CA
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

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x000374DD File Offset: 0x000356DD
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x000374EF File Offset: 0x000356EF
		[Parameter(Mandatory = false)]
		public string C
		{
			get
			{
				return ((SyncMailContact)this.GetDynamicParameters()).C;
			}
			set
			{
				((SyncMailContact)this.GetDynamicParameters()).C = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00037502 File Offset: 0x00035702
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00037514 File Offset: 0x00035714
		[Parameter(Mandatory = false)]
		public string Co
		{
			get
			{
				return ((SyncMailContact)this.GetDynamicParameters()).Co;
			}
			set
			{
				((SyncMailContact)this.GetDynamicParameters()).Co = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00037527 File Offset: 0x00035727
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x0003754D File Offset: 0x0003574D
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

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00037565 File Offset: 0x00035765
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0003757C File Offset: 0x0003577C
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

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003758F File Offset: 0x0003578F
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x000375A6 File Offset: 0x000357A6
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

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000375B9 File Offset: 0x000357B9
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000375C9 File Offset: 0x000357C9
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

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000375E4 File Offset: 0x000357E4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			MultiLinkSyncHelper.ValidateIncompatibleParameters(base.Fields, this.GetIncompatibleParametersDictionary(), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			SyncMailContact syncMailContact = (SyncMailContact)this.GetDynamicParameters();
			if (syncMailContact.IsModified(SyncMailContactSchema.CountryOrRegion) && (syncMailContact.IsModified(SyncMailContactSchema.C) || syncMailContact.IsModified(SyncMailContactSchema.Co) || syncMailContact.IsModified(SyncMailContactSchema.CountryCode)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00037674 File Offset: 0x00035874
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			SyncMailContact dataObject = (SyncMailContact)this.GetDynamicParameters();
			base.SetReferenceParameter<UserContactIdParameter>(SyncMailContactSchema.Manager, this.Manager, dataObject, new GetRecipientDelegate<UserContactIdParameter>(this.GetRecipient));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(MailEnabledRecipientSchema.BypassModerationFrom, this.BypassModerationFrom, dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual));
			base.SetMultiReferenceParameter<DeliveryRecipientIdParameter>(MailEnabledRecipientSchema.BypassModerationFromDLMembers, this.BypassModerationFromDLMembers, dataObject, new GetRecipientDelegate<DeliveryRecipientIdParameter>(this.GetRecipient), new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionGroup));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawAcceptMessagesOnlyFrom", MailEnabledRecipientSchema.AcceptMessagesOnlyFrom, this.RawAcceptMessagesOnlyFrom, "RawAcceptMessagesOnlyFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawBypassModerationFrom", MailEnabledRecipientSchema.BypassModerationFrom, this.RawBypassModerationFrom, "RawBypassModerationFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>("RawRejectMessagesFrom", MailEnabledRecipientSchema.RejectMessagesFrom, this.RawRejectMessagesFrom, "RawRejectMessagesFrom", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<DeliveryRecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateMessageDeliveryRestrictionIndividual)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<RecipientIdParameter>>("RawGrantSendOnBehalfTo", MailEnabledRecipientSchema.GrantSendOnBehalfTo, this.RawGrantSendOnBehalfTo, "RawGrantSendOnBehalfTo", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<RecipientIdParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(base.ValidateGrantSendOnBehalfTo)));
			base.SetMultiReferenceParameter<RecipientWithAdUserIdParameter<ModeratorIDParameter>>("RawModeratedBy", MailEnabledRecipientSchema.ModeratedBy, this.RawModeratedBy, "RawModeratedBy", dataObject, new GetRecipientDelegate<RecipientWithAdUserIdParameter<ModeratorIDParameter>>(this.GetRecipient), SyncTaskHelper.ValidateBypassADUser(new ValidateRecipientDelegate(RecipientTaskHelper.ValidateModeratedBy)));
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003782A File Offset: 0x00035A2A
		private Dictionary<object, ArrayList> GetIncompatibleParametersDictionary()
		{
			return MultiLinkSyncHelper.GetIncompatibleParametersDictionaryForCommonMultiLink();
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00037831 File Offset: 0x00035A31
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00037844 File Offset: 0x00035A44
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADContact adcontact = (ADContact)base.PrepareDataObject();
			adcontact.BypassModerationCheck = true;
			if (this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0)
			{
				adcontact.EmailAddresses = SyncTaskHelper.ReplaceSmtpAndX500Addresses(this.SmtpAndX500Addresses, adcontact.EmailAddresses);
			}
			if (base.Fields.IsModified("SipAddresses"))
			{
				adcontact.EmailAddresses = SyncTaskHelper.ReplaceSipAddresses(this.SipAddresses, adcontact.EmailAddresses);
			}
			if (adcontact.IsModified(MailEnabledRecipientSchema.EmailAddresses))
			{
				adcontact.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, adcontact.EmailAddresses, adcontact, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			TaskLogger.LogExit();
			return adcontact;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00037904 File Offset: 0x00035B04
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ADContact dataObject2 = (ADContact)dataObject;
			return new SyncMailContact(dataObject2);
		}

		// Token: 0x040002C8 RID: 712
		private BatchReferenceErrorReporter batchReferenceErrorReporter;
	}
}
