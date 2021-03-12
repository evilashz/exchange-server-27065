using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ManagementEndpoint;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AAE RID: 2734
	[Cmdlet("Set", "AcceptedDomain", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAcceptedDomain : SetSystemConfigurationObjectTask<AcceptedDomainIdParameter, AcceptedDomain>
	{
		// Token: 0x17001D4A RID: 7498
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x001939A3 File Offset: 0x00191BA3
		// (set) Token: 0x060060BD RID: 24765 RVA: 0x001939AB File Offset: 0x00191BAB
		[Parameter]
		public bool MakeDefault
		{
			get
			{
				return this.makeDefault;
			}
			set
			{
				this.makeDefault = value;
			}
		}

		// Token: 0x17001D4B RID: 7499
		// (get) Token: 0x060060BE RID: 24766 RVA: 0x001939B4 File Offset: 0x00191BB4
		// (set) Token: 0x060060BF RID: 24767 RVA: 0x001939CB File Offset: 0x00191BCB
		[Parameter]
		public bool IsCoexistenceDomain
		{
			get
			{
				return (bool)base.Fields[AcceptedDomainSchema.IsCoexistenceDomain];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.IsCoexistenceDomain] = value;
			}
		}

		// Token: 0x17001D4C RID: 7500
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x001939E3 File Offset: 0x00191BE3
		// (set) Token: 0x060060C1 RID: 24769 RVA: 0x001939FA File Offset: 0x00191BFA
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

		// Token: 0x17001D4D RID: 7501
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00193A0D File Offset: 0x00191C0D
		// (set) Token: 0x060060C3 RID: 24771 RVA: 0x00193A24 File Offset: 0x00191C24
		[Parameter(Mandatory = false)]
		public bool MatchSubDomains
		{
			get
			{
				return (bool)base.Fields[AcceptedDomainSchema.MatchSubDomains];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.MatchSubDomains] = value;
			}
		}

		// Token: 0x17001D4E RID: 7502
		// (get) Token: 0x060060C4 RID: 24772 RVA: 0x00193A3C File Offset: 0x00191C3C
		// (set) Token: 0x060060C5 RID: 24773 RVA: 0x00193A53 File Offset: 0x00191C53
		[Parameter(Mandatory = false)]
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

		// Token: 0x17001D4F RID: 7503
		// (get) Token: 0x060060C6 RID: 24774 RVA: 0x00193A66 File Offset: 0x00191C66
		// (set) Token: 0x060060C7 RID: 24775 RVA: 0x00193A7D File Offset: 0x00191C7D
		[Parameter]
		public AuthenticationType AuthenticationType
		{
			get
			{
				return (AuthenticationType)base.Fields[AcceptedDomainSchema.RawAuthenticationType];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.RawAuthenticationType] = value;
			}
		}

		// Token: 0x17001D50 RID: 7504
		// (get) Token: 0x060060C8 RID: 24776 RVA: 0x00193A95 File Offset: 0x00191C95
		// (set) Token: 0x060060C9 RID: 24777 RVA: 0x00193AAC File Offset: 0x00191CAC
		[Parameter]
		public bool InitialDomain
		{
			get
			{
				return (bool)base.Fields[AcceptedDomainSchema.InitialDomain];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.InitialDomain] = value;
			}
		}

		// Token: 0x17001D51 RID: 7505
		// (get) Token: 0x060060CA RID: 24778 RVA: 0x00193AC4 File Offset: 0x00191CC4
		// (set) Token: 0x060060CB RID: 24779 RVA: 0x00193ADB File Offset: 0x00191CDB
		[Parameter]
		public LiveIdInstanceType LiveIdInstanceType
		{
			get
			{
				return (LiveIdInstanceType)base.Fields[AcceptedDomainSchema.RawLiveIdInstanceType];
			}
			set
			{
				base.Fields[AcceptedDomainSchema.RawLiveIdInstanceType] = value;
			}
		}

		// Token: 0x17001D52 RID: 7506
		// (get) Token: 0x060060CC RID: 24780 RVA: 0x00193AF3 File Offset: 0x00191CF3
		// (set) Token: 0x060060CD RID: 24781 RVA: 0x00193B14 File Offset: 0x00191D14
		[Parameter]
		public bool EnableNego2Authentication
		{
			get
			{
				return (bool)(base.Fields[AcceptedDomainSchema.EnableNego2Authentication] ?? false);
			}
			set
			{
				base.Fields[AcceptedDomainSchema.EnableNego2Authentication] = value;
			}
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x060060CE RID: 24782 RVA: 0x00193B2C File Offset: 0x00191D2C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAcceptedDomain(this.Identity.ToString());
			}
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x00193B40 File Offset: 0x00191D40
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			AcceptedDomain acceptedDomain = (AcceptedDomain)this.GetDynamicParameters();
			if (base.Fields.IsModified(AcceptedDomainSchema.MailFlowPartner))
			{
				MailFlowPartnerIdParameter mailFlowPartner = this.MailFlowPartner;
				if (mailFlowPartner != null)
				{
					MailFlowPartner mailFlowPartner2 = (MailFlowPartner)base.GetDataObject<MailFlowPartner>(mailFlowPartner, base.GlobalConfigSession, this.RootId, new LocalizedString?(Strings.MailFlowPartnerNotExists(mailFlowPartner)), new LocalizedString?(Strings.MailFlowPartnerNotUnique(mailFlowPartner)), ExchangeErrorCategory.Client);
					acceptedDomain.MailFlowPartner = (ADObjectId)mailFlowPartner2.Identity;
					return;
				}
				acceptedDomain.MailFlowPartner = null;
			}
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x00193BC8 File Offset: 0x00191DC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.makeDefault && !this.DataObject.Default)
			{
				this.DataObject.Default = true;
				ADPagedReader<AcceptedDomain> adpagedReader = ((IConfigurationSession)base.DataSession).FindPaged<AcceptedDomain>(this.DataObject.Id.Parent, QueryScope.OneLevel, null, null, 0);
				foreach (AcceptedDomain acceptedDomain in adpagedReader)
				{
					if (acceptedDomain.Default)
					{
						acceptedDomain.Default = false;
						base.DataSession.Save(acceptedDomain);
					}
				}
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.IsCoexistenceDomain) && this.DataObject.IsCoexistenceDomain != this.IsCoexistenceDomain)
			{
				if (!this.IsCoexistenceDomain)
				{
					try
					{
						AcceptedDomainUtility.DeregisterCoexistenceDomain(this.DataObject.DomainName.Domain);
						goto IL_162;
					}
					catch (TimeoutException exception)
					{
						base.WriteError(exception, ErrorCategory.InvalidArgument, null);
						goto IL_162;
					}
					catch (InvalidOperationException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
						goto IL_162;
					}
					catch (SecurityAccessDeniedException exception3)
					{
						base.WriteError(exception3, ErrorCategory.InvalidArgument, null);
						goto IL_162;
					}
					catch (CommunicationException exception4)
					{
						base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
						goto IL_162;
					}
				}
				try
				{
					AcceptedDomainUtility.RegisterCoexistenceDomain(this.DataObject.DomainName.Domain);
				}
				catch (TimeoutException exception5)
				{
					base.WriteError(exception5, ErrorCategory.InvalidArgument, null);
				}
				catch (InvalidOperationException exception6)
				{
					base.WriteError(exception6, ErrorCategory.InvalidArgument, null);
				}
				catch (SecurityAccessDeniedException exception7)
				{
					base.WriteError(exception7, ErrorCategory.InvalidArgument, null);
				}
				catch (CommunicationException exception8)
				{
					base.WriteError(exception8, ErrorCategory.InvalidArgument, null);
				}
				IL_162:
				this.DataObject.IsCoexistenceDomain = this.IsCoexistenceDomain;
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.RawAuthenticationType))
			{
				this.DataObject.RawAuthenticationType = this.AuthenticationType;
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.InitialDomain))
			{
				this.DataObject.InitialDomain = this.InitialDomain;
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.RawLiveIdInstanceType))
			{
				this.DataObject.RawLiveIdInstanceType = this.LiveIdInstanceType;
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.EnableNego2Authentication))
			{
				this.DataObject.EnableNego2Authentication = this.EnableNego2Authentication;
				if (ManagementEndpointBase.IsGlobalDirectoryConfigured())
				{
					IGlobalDirectorySession globalSession = DirectorySessionFactory.GetGlobalSession(null);
					globalSession.SetDomainFlag(this.DataObject.Name, GlsDomainFlags.Nego2Enabled, this.EnableNego2Authentication);
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
			if (base.Fields.IsModified(AcceptedDomainSchema.MatchSubDomains))
			{
				this.DataObject.MatchSubDomains = this.MatchSubDomains;
			}
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<AcceptedDomain>(this, this.DataObject, TenantSettingSyncLogType.SYNCACCEPTEDDOM, null);
			TaskLogger.LogExit();
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x00193EE8 File Offset: 0x001920E8
		protected override void InternalValidate()
		{
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			if (this.DataObject.PendingRemoval && !this.DataObject.IsChanged(AcceptedDomainSchema.PendingRemoval))
			{
				base.WriteError(new CannotOperateOnAcceptedDomainPendingRemovalException(this.DataObject.DomainName.ToString()), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataObject.PendingRemoval && this.DataObject.IsChanged(AcceptedDomainSchema.PendingRemoval))
			{
				RemoveAcceptedDomain.CheckDomainForRemoval(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.DataObject.IsChanged(ADObjectSchema.Name))
			{
				NewAcceptedDomain.ValidateDomainName(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (this.DataObject.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain))
			{
				this.WriteWarning(Strings.WarnAboutStarAcceptedDomain);
			}
			SetAcceptedDomain.DomainEditValidator domainEditValidator = new SetAcceptedDomain.DomainEditValidator(this);
			domainEditValidator.ValidateAllPolicies();
			if (this.DataObject.IsChanged(AcceptedDomainSchema.AcceptedDomainType) && this.DataObject.DomainType == AcceptedDomainType.ExternalRelay && (Datacenter.IsMicrosoftHostedOnly(true) || Datacenter.IsForefrontForOfficeDatacenter()))
			{
				base.WriteError(new ExternalRelayDomainsAreNotAllowedInDatacenterAndFfoException(), ErrorCategory.InvalidOperation, null);
			}
			if (base.Fields.IsModified(AcceptedDomainSchema.CatchAllRecipient) && this.CatchAllRecipient != null)
			{
				this.resolvedCatchAllRecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.CatchAllRecipient, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.CatchAllRecipientNotExists(this.CatchAllRecipient)), new LocalizedString?(Strings.CatchAllRecipientNotUnique(this.CatchAllRecipient)), ExchangeErrorCategory.Client);
			}
			AcceptedDomainUtility.ValidateCatchAllRecipient(this.resolvedCatchAllRecipient, this.DataObject, base.Fields.IsModified(AcceptedDomainSchema.CatchAllRecipient), new Task.TaskErrorLoggingDelegate(base.WriteError));
			AcceptedDomainUtility.ValidateIfOutboundConnectorToRouteDomainIsPresent(base.DataSession, this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			if (this.DataObject.IsChanged(AcceptedDomainSchema.AcceptedDomainType) || base.Fields.IsModified(AcceptedDomainSchema.MatchSubDomains))
			{
				bool matchSubDomains = base.Fields.IsModified(AcceptedDomainSchema.MatchSubDomains) ? this.MatchSubDomains : this.DataObject.MatchSubDomains;
				AcceptedDomainUtility.ValidateMatchSubDomains(matchSubDomains, this.DataObject.DomainType, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x04003551 RID: 13649
		private bool makeDefault;

		// Token: 0x04003552 RID: 13650
		private ADRecipient resolvedCatchAllRecipient;

		// Token: 0x02000AAF RID: 2735
		internal class DomainEditValidator : UpdateEmailAddressPolicy.DomainValidator
		{
			// Token: 0x17001D54 RID: 7508
			// (get) Token: 0x060060D3 RID: 24787 RVA: 0x00194132 File Offset: 0x00192332
			protected Task.TaskErrorLoggingDelegate ErrorWriter
			{
				get
				{
					return this.errorWriter;
				}
			}

			// Token: 0x17001D55 RID: 7509
			// (get) Token: 0x060060D4 RID: 24788 RVA: 0x0019413A File Offset: 0x0019233A
			protected AcceptedDomain OldDomain
			{
				get
				{
					return this.oldDomain;
				}
			}

			// Token: 0x060060D5 RID: 24789 RVA: 0x00194142 File Offset: 0x00192342
			public DomainEditValidator(SetAcceptedDomain task) : this(new Task.TaskErrorLoggingDelegate(task.WriteError), (IConfigurationSession)task.DataSession, SetAcceptedDomain.DomainEditValidator.LoadOldVersion(task), task.DataObject)
			{
			}

			// Token: 0x060060D6 RID: 24790 RVA: 0x0019416D File Offset: 0x0019236D
			public DomainEditValidator(Task.TaskErrorLoggingDelegate errorWriter, IConfigurationSession session, AcceptedDomain oldDomain, AcceptedDomain newDomain) : this(session, oldDomain, newDomain)
			{
				this.errorWriter = errorWriter;
			}

			// Token: 0x060060D7 RID: 24791 RVA: 0x00194180 File Offset: 0x00192380
			private DomainEditValidator(IConfigurationSession session, AcceptedDomain oldDomain, AcceptedDomain newDomain) : base(SetAcceptedDomain.DomainEditValidator.FindConflictingDomains(session, oldDomain, newDomain))
			{
				this.oldDomain = oldDomain;
				this.newDomain = newDomain;
				this.session = session;
			}

			// Token: 0x060060D8 RID: 24792 RVA: 0x001941A8 File Offset: 0x001923A8
			public void ValidateAllPolicies()
			{
				if (this.oldDomain != null && this.oldDomain.FederatedOrganizationLink != null && (this.newDomain == null || this.oldDomain.DomainName.Domain != this.newDomain.DomainName.Domain) && !SetAcceptedDomain.DomainEditValidator.isMultiTenancyEnabled)
				{
					this.errorWriter(new CannotRemoveFederatedAcceptedDomainException(this.oldDomain.DomainName.Domain), ErrorCategory.InvalidOperation, this.oldDomain.Identity);
				}
				foreach (EmailAddressPolicy policy in this.session.FindAllPaged<EmailAddressPolicy>())
				{
					base.Validate(policy);
				}
			}

			// Token: 0x060060D9 RID: 24793 RVA: 0x00194274 File Offset: 0x00192474
			protected override void WriteInvalidTemplate(SmtpProxyAddressTemplate template)
			{
				if (this.oldDomain == null)
				{
					return;
				}
				if (!this.newDomain.IsChanged(AcceptedDomainSchema.AcceptedDomainType) || this.newDomain.DomainType != AcceptedDomainType.ExternalRelay)
				{
					return;
				}
				if (this.IsUsedBy(template))
				{
					this.ErrorWriter(new LocalizedException(Strings.CannotMakeAcceptedDomainExternalRelaySinceItIsReferencedByAddressTemplate(this.OldDomain.DomainName, template)), ErrorCategory.InvalidOperation, this.OldDomain.Identity);
				}
			}

			// Token: 0x060060DA RID: 24794 RVA: 0x001942E4 File Offset: 0x001924E4
			protected override void HandleNonAuthoritativeDomains(EmailAddressPolicy policy, HashSet<SmtpDomain> domains)
			{
				ProxyAddressTemplateCollection proxyAddressTemplateCollection = new ProxyAddressTemplateCollection();
				foreach (ProxyAddressTemplate proxyAddressTemplate in policy.NonAuthoritativeDomains)
				{
					SmtpDomain template;
					if (!UpdateEmailAddressPolicy.DomainValidator.TryGetDomain(proxyAddressTemplate, out template) || (!SetAcceptedDomain.DomainEditValidator.Conflict(this.newDomain, template) && !SetAcceptedDomain.DomainEditValidator.Conflict(this.oldDomain, template)))
					{
						proxyAddressTemplateCollection.Add(proxyAddressTemplate);
					}
				}
				foreach (SmtpDomain smtpDomain in domains)
				{
					if (SetAcceptedDomain.DomainEditValidator.Conflict(this.newDomain, smtpDomain) || SetAcceptedDomain.DomainEditValidator.Conflict(this.oldDomain, smtpDomain))
					{
						SmtpProxyAddressTemplate item = new SmtpProxyAddressTemplate("@" + smtpDomain.Domain, false);
						proxyAddressTemplateCollection.Add(item);
					}
				}
				UpdateEmailAddressPolicy.CheckEapVersion(policy);
				policy.NonAuthoritativeDomains = proxyAddressTemplateCollection;
				this.session.Save(policy);
			}

			// Token: 0x060060DB RID: 24795 RVA: 0x001943F0 File Offset: 0x001925F0
			protected static bool Conflict(AcceptedDomain accepted, SmtpDomain template)
			{
				return accepted != null && accepted.DomainName != null && accepted.DomainName.Match(template.Domain) != -1;
			}

			// Token: 0x060060DC RID: 24796 RVA: 0x00194418 File Offset: 0x00192618
			protected bool IsUsedBy(SmtpProxyAddressTemplate template)
			{
				SmtpDomain template2;
				return UpdateEmailAddressPolicy.DomainValidator.TryGetDomain(template, out template2) && SetAcceptedDomain.DomainEditValidator.Conflict(this.OldDomain, template2);
			}

			// Token: 0x060060DD RID: 24797 RVA: 0x00194440 File Offset: 0x00192640
			private static AcceptedDomain LoadOldVersion(SetAcceptedDomain task)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, task.DataObject.Guid);
				AcceptedDomain[] array = task.ConfigurationSession.Find<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 1);
				if (array.Length != 0)
				{
					return array[0];
				}
				return null;
			}

			// Token: 0x060060DE RID: 24798 RVA: 0x001946BC File Offset: 0x001928BC
			private static IEnumerable<AcceptedDomain> FindConflictingDomains(IConfigurationSession session, AcceptedDomain oldDomain, AcceptedDomain newDomain)
			{
				List<QueryFilter> filters = new List<QueryFilter>();
				filters.AddRange(AcceptedDomain.ConflictingDomainFilters(oldDomain, false));
				filters.AddRange(AcceptedDomain.ConflictingDomainFilters(newDomain, false));
				QueryFilter filter = new OrFilter(filters.ToArray());
				ADPagedReader<AcceptedDomain> results = session.FindPaged<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 0);
				foreach (AcceptedDomain result in results)
				{
					yield return result;
				}
				yield return newDomain;
				yield break;
			}

			// Token: 0x04003553 RID: 13651
			private static readonly bool isMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

			// Token: 0x04003554 RID: 13652
			private AcceptedDomain newDomain;

			// Token: 0x04003555 RID: 13653
			private AcceptedDomain oldDomain;

			// Token: 0x04003556 RID: 13654
			private IConfigurationSession session;

			// Token: 0x04003557 RID: 13655
			private Task.TaskErrorLoggingDelegate errorWriter;
		}
	}
}
