using System;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA8 RID: 2728
	[Cmdlet("New", "AcceptedDomain", SupportsShouldProcess = true)]
	public sealed class NewAcceptedDomain : NewMultitenancySystemConfigurationObjectTask<AcceptedDomain>
	{
		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x0600607B RID: 24699 RVA: 0x00191E75 File Offset: 0x00190075
		// (set) Token: 0x0600607C RID: 24700 RVA: 0x00191E7D File Offset: 0x0019007D
		[Parameter(Mandatory = true, Position = 0)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = MailboxTaskHelper.GetNameOfAcceptableLengthForMultiTenantMode(value, out this.nameWarning);
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x0600607D RID: 24701 RVA: 0x00191E91 File Offset: 0x00190091
		// (set) Token: 0x0600607E RID: 24702 RVA: 0x00191E9E File Offset: 0x0019009E
		[Parameter(Mandatory = true)]
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return this.DataObject.DomainName;
			}
			set
			{
				this.DataObject.DomainName = value;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x0600607F RID: 24703 RVA: 0x00191EAC File Offset: 0x001900AC
		// (set) Token: 0x06006080 RID: 24704 RVA: 0x00191EB9 File Offset: 0x001900B9
		[Parameter]
		public AcceptedDomainType DomainType
		{
			get
			{
				return this.DataObject.DomainType;
			}
			set
			{
				this.DataObject.DomainType = value;
			}
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x00191EC7 File Offset: 0x001900C7
		// (set) Token: 0x06006082 RID: 24706 RVA: 0x00191ED4 File Offset: 0x001900D4
		[Parameter]
		public AuthenticationType AuthenticationType
		{
			get
			{
				return this.DataObject.RawAuthenticationType;
			}
			set
			{
				this.DataObject.RawAuthenticationType = value;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06006083 RID: 24707 RVA: 0x00191EE2 File Offset: 0x001900E2
		// (set) Token: 0x06006084 RID: 24708 RVA: 0x00191EEF File Offset: 0x001900EF
		[Parameter]
		public LiveIdInstanceType LiveIdInstanceType
		{
			get
			{
				return this.DataObject.RawLiveIdInstanceType;
			}
			set
			{
				this.DataObject.RawLiveIdInstanceType = value;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06006085 RID: 24709 RVA: 0x00191EFD File Offset: 0x001900FD
		// (set) Token: 0x06006086 RID: 24710 RVA: 0x00191F14 File Offset: 0x00190114
		[Parameter(Mandatory = false)]
		public RecipientIdParameter CatchAllRecipient
		{
			get
			{
				return (RecipientIdParameter)base.Fields[AcceptedDomainSchema.CatchAllRecipient];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.CatchAllRecipient] = value;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06006087 RID: 24711 RVA: 0x00191F27 File Offset: 0x00190127
		// (set) Token: 0x06006088 RID: 24712 RVA: 0x00191F34 File Offset: 0x00190134
		[Parameter(Mandatory = false)]
		public bool MatchSubDomains
		{
			get
			{
				return this.DataObject.MatchSubDomains;
			}
			set
			{
				this.DataObject.MatchSubDomains = value;
			}
		}

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00191F42 File Offset: 0x00190142
		// (set) Token: 0x0600608A RID: 24714 RVA: 0x00191F59 File Offset: 0x00190159
		[Parameter]
		public MailFlowPartnerIdParameter MailFlowPartner
		{
			get
			{
				return (MailFlowPartnerIdParameter)base.Fields[AcceptedDomainSchema.MailFlowPartner];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.MailFlowPartner] = value;
			}
		}

		// Token: 0x17001D3D RID: 7485
		// (get) Token: 0x0600608B RID: 24715 RVA: 0x00191F6C File Offset: 0x0019016C
		// (set) Token: 0x0600608C RID: 24716 RVA: 0x00191F79 File Offset: 0x00190179
		[Parameter]
		public bool OutboundOnly
		{
			get
			{
				return this.DataObject.OutboundOnly;
			}
			set
			{
				this.DataObject.OutboundOnly = value;
			}
		}

		// Token: 0x17001D3E RID: 7486
		// (get) Token: 0x0600608D RID: 24717 RVA: 0x00191F87 File Offset: 0x00190187
		// (set) Token: 0x0600608E RID: 24718 RVA: 0x00191F94 File Offset: 0x00190194
		[Parameter]
		public bool InitialDomain
		{
			get
			{
				return this.DataObject.InitialDomain;
			}
			set
			{
				this.DataObject.InitialDomain = value;
			}
		}

		// Token: 0x17001D3F RID: 7487
		// (get) Token: 0x0600608F RID: 24719 RVA: 0x00191FA2 File Offset: 0x001901A2
		// (set) Token: 0x06006090 RID: 24720 RVA: 0x00191FC8 File Offset: 0x001901C8
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipDnsProvisioning
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipDnsProvisioning"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipDnsProvisioning"] = value;
			}
		}

		// Token: 0x17001D40 RID: 7488
		// (get) Token: 0x06006091 RID: 24721 RVA: 0x00191FE0 File Offset: 0x001901E0
		// (set) Token: 0x06006092 RID: 24722 RVA: 0x00192006 File Offset: 0x00190206
		[Parameter(Mandatory = false)]
		public SwitchParameter SkipDomainNameValidation
		{
			get
			{
				return (SwitchParameter)(base.Fields["SkipDomainNameValidation"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SkipDomainNameValidation"] = value;
			}
		}

		// Token: 0x17001D41 RID: 7489
		// (get) Token: 0x06006093 RID: 24723 RVA: 0x0019201E File Offset: 0x0019021E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAcceptedDomain(this.Name, this.DomainName.ToString());
			}
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x00192038 File Offset: 0x00190238
		protected override void InternalBeginProcessing()
		{
			if (this.nameWarning != LocalizedString.Empty)
			{
				this.WriteWarning(this.nameWarning);
			}
			base.InternalBeginProcessing();
			MailFlowPartnerIdParameter mailFlowPartner = this.MailFlowPartner;
			if (mailFlowPartner != null)
			{
				MailFlowPartner mailFlowPartner2 = (MailFlowPartner)base.GetDataObject<MailFlowPartner>(mailFlowPartner, base.GlobalConfigSession, this.RootId, new LocalizedString?(Strings.MailFlowPartnerNotExists(mailFlowPartner)), new LocalizedString?(Strings.MailFlowPartnerNotUnique(mailFlowPartner)), ExchangeErrorCategory.Client);
				this.mailFlowPartnerId = (ADObjectId)mailFlowPartner2.Identity;
			}
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001920B8 File Offset: 0x001902B8
		internal static void ValidateDomainName(AcceptedDomain domain, Task.TaskErrorLoggingDelegate errorWriter)
		{
			string domain2 = domain.DomainName.Domain;
			DuplicateAcceptedDomainException ex = new DuplicateAcceptedDomainException(domain2);
			ConflictingAcceptedDomainException conflictingAcceptedDomainException = new ConflictingAcceptedDomainException(domain2);
			Exception ex2;
			if (!ADAccountPartitionLocator.ValidateDomainName(domain, ex, conflictingAcceptedDomainException, out ex2))
			{
				ErrorCategory category = ErrorCategory.InvalidOperation;
				if (ex2 == ex)
				{
					category = ErrorCategory.ResourceExists;
				}
				errorWriter(ex2, category, domain);
			}
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x00192100 File Offset: 0x00190300
		protected override IConfigurable PrepareDataObject()
		{
			AcceptedDomain acceptedDomain = (AcceptedDomain)base.PrepareDataObject();
			acceptedDomain.SetId(this.ConfigurationSession, this.Name);
			if (base.Fields.IsModified(AcceptedDomainSchema.MailFlowPartner))
			{
				acceptedDomain.MailFlowPartner = this.mailFlowPartnerId;
			}
			else
			{
				IConfigurable[] array = base.DataSession.Find<PerimeterConfig>(null, null, true, null);
				if (array != null && array.Length == 1 && ((PerimeterConfig)array[0]).MailFlowPartner != null)
				{
					acceptedDomain.MailFlowPartner = ((PerimeterConfig)array[0]).MailFlowPartner;
				}
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && !this.DataObject.InitialDomain)
			{
				acceptedDomain.PendingCompletion = true;
			}
			return acceptedDomain;
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001921BC File Offset: 0x001903BC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			if (this.SkipDomainNameValidation)
			{
				if (!TemplateTenantConfiguration.IsTemplateTenant(base.OrganizationId))
				{
					base.WriteError(new CannotBypassDomainNameValidationException(), ErrorCategory.InvalidOperation, null);
				}
			}
			else
			{
				NewAcceptedDomain.ValidateDomainName(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.DataObject.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain))
			{
				this.WriteWarning(Strings.WarnAboutStarAcceptedDomain);
			}
			NewAcceptedDomain.DomainAdditionValidator domainAdditionValidator = new NewAcceptedDomain.DomainAdditionValidator(this);
			domainAdditionValidator.ValidateAllPolicies();
			if (this.DataObject.InitialDomain)
			{
				NewAcceptedDomain.ValidateInitialDomain(this.DataObject, this.ConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.DataObject.DomainType == AcceptedDomainType.ExternalRelay && (Datacenter.IsMicrosoftHostedOnly(true) || Datacenter.IsForefrontForOfficeDatacenter()))
			{
				base.WriteError(new ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException(), ErrorCategory.InvalidOperation, null);
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.CatchAllRecipient) && this.CatchAllRecipient != null)
			{
				this.resolvedCatchAllRecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.CatchAllRecipient, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.CatchAllRecipientNotExists(this.CatchAllRecipient)), new LocalizedString?(Strings.CatchAllRecipientNotUnique(this.CatchAllRecipient)), ExchangeErrorCategory.Client);
			}
			AcceptedDomainUtility.ValidateCatchAllRecipient(this.resolvedCatchAllRecipient, this.DataObject, base.Fields.IsModified(AcceptedDomainSchema.CatchAllRecipient), new Task.TaskErrorLoggingDelegate(base.WriteError));
			AcceptedDomainUtility.ValidateIfOutboundConnectorToRouteDomainIsPresent(base.DataSession, this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			AcceptedDomainUtility.ValidateMatchSubDomains(this.DataObject.MatchSubDomains, this.DataObject.DomainType, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x00192388 File Offset: 0x00190588
		protected override void InternalProcessRecord()
		{
			this.DataObject.Default = !NewAcceptedDomain.DomainsExist(this.ConfigurationSession, null);
			this.DataObject.AddressBookEnabled = (this.DataObject.DomainType == AcceptedDomainType.Authoritative);
			bool flag = AcceptedDomainUtility.IsCoexistenceDomain(this.DataObject.DomainName.Domain);
			if (flag && !this.SkipDnsProvisioning)
			{
				this.DataObject.IsCoexistenceDomain = true;
				try
				{
					AcceptedDomainUtility.RegisterCoexistenceDomain(this.DataObject.DomainName.Domain);
				}
				catch (TimeoutException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				}
				catch (InvalidOperationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				}
				catch (SecurityAccessDeniedException exception3)
				{
					base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
				}
				catch (CommunicationException exception4)
				{
					base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
				}
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.CatchAllRecipient))
			{
				if (this.resolvedCatchAllRecipient != null)
				{
					this.DataObject.CatchAllRecipientID = this.resolvedCatchAllRecipient.OriginalId;
				}
				else
				{
					this.DataObject.CatchAllRecipientID = null;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x001924BC File Offset: 0x001906BC
		private static bool DomainsExist(IConfigurationSession session, QueryFilter filter)
		{
			AcceptedDomain[] array = session.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 1);
			return array.Length != 0;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x001924E0 File Offset: 0x001906E0
		private static void ValidateInitialDomain(AcceptedDomain domain, IConfigurationSession session, Task.TaskErrorLoggingDelegate errorWriter)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.InitialDomain, true);
			AcceptedDomain[] array = session.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 0);
			if (array.Length != 0)
			{
				errorWriter(new DuplicateInitialDomainException(), ErrorCategory.ResourceExists, domain);
			}
		}

		// Token: 0x04003547 RID: 13639
		private LocalizedString nameWarning = LocalizedString.Empty;

		// Token: 0x04003548 RID: 13640
		private ADObjectId mailFlowPartnerId;

		// Token: 0x04003549 RID: 13641
		private ADRecipient resolvedCatchAllRecipient;

		// Token: 0x02000AB0 RID: 2736
		private class DomainAdditionValidator : SetAcceptedDomain.DomainEditValidator
		{
			// Token: 0x060060E0 RID: 24800 RVA: 0x00194711 File Offset: 0x00192911
			public DomainAdditionValidator(NewAcceptedDomain task) : base(new Task.TaskErrorLoggingDelegate(task.WriteError), (IConfigurationSession)task.DataSession, null, task.DataObject)
			{
			}
		}
	}
}
