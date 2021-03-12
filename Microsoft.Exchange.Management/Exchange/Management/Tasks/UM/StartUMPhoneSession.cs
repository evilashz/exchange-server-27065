using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMPhoneSession;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D46 RID: 3398
	[Cmdlet("Start", "UMPhoneSession", SupportsShouldProcess = true, DefaultParameterSetName = "DefaultVoicemailGreeting")]
	public sealed class StartUMPhoneSession : NewTenantADTaskBase<UMPhoneSession>
	{
		// Token: 0x1700287C RID: 10364
		// (get) Token: 0x06008249 RID: 33353 RVA: 0x00214FDA File Offset: 0x002131DA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageStartUMPhoneSession;
			}
		}

		// Token: 0x1700287D RID: 10365
		// (get) Token: 0x0600824A RID: 33354 RVA: 0x00214FE1 File Offset: 0x002131E1
		// (set) Token: 0x0600824B RID: 33355 RVA: 0x00214FF8 File Offset: 0x002131F8
		[Parameter]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x1700287E RID: 10366
		// (get) Token: 0x0600824C RID: 33356 RVA: 0x0021500B File Offset: 0x0021320B
		// (set) Token: 0x0600824D RID: 33357 RVA: 0x00215022 File Offset: 0x00213222
		[Parameter(Mandatory = true, ParameterSetName = "DefaultVoicemailGreeting")]
		[Parameter(Mandatory = true, ParameterSetName = "AwayVoicemailGreeting")]
		[ValidateNotNullOrEmpty]
		public MailboxIdParameter UMMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["UMMailbox"];
			}
			set
			{
				base.Fields["UMMailbox"] = value;
			}
		}

		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x0600824E RID: 33358 RVA: 0x00215035 File Offset: 0x00213235
		// (set) Token: 0x0600824F RID: 33359 RVA: 0x0021504C File Offset: 0x0021324C
		[Parameter(Mandatory = true, ParameterSetName = "AwayVoicemailGreeting")]
		[Parameter(Mandatory = true, ParameterSetName = "DefaultVoicemailGreeting")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "PlayOnPhoneGreeting")]
		public string PhoneNumber
		{
			get
			{
				return (string)base.Fields["PhoneNumber"];
			}
			set
			{
				base.Fields["PhoneNumber"] = value;
			}
		}

		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x06008250 RID: 33360 RVA: 0x0021505F File Offset: 0x0021325F
		// (set) Token: 0x06008251 RID: 33361 RVA: 0x00215085 File Offset: 0x00213285
		[Parameter(Mandatory = true, ParameterSetName = "DefaultVoicemailGreeting")]
		public SwitchParameter DefaultVoicemailGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["DefaultVoicemailGreeting"] ?? false);
			}
			set
			{
				base.Fields["DefaultVoicemailGreeting"] = value;
			}
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x06008252 RID: 33362 RVA: 0x0021509D File Offset: 0x0021329D
		// (set) Token: 0x06008253 RID: 33363 RVA: 0x002150C3 File Offset: 0x002132C3
		[Parameter(Mandatory = true, ParameterSetName = "AwayVoicemailGreeting")]
		public SwitchParameter AwayVoicemailGreeting
		{
			get
			{
				return (SwitchParameter)(base.Fields["AwayVoicemailGreeting"] ?? false);
			}
			set
			{
				base.Fields["AwayVoicemailGreeting"] = value;
			}
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x06008254 RID: 33364 RVA: 0x002150DB File Offset: 0x002132DB
		// (set) Token: 0x06008255 RID: 33365 RVA: 0x002150F2 File Offset: 0x002132F2
		[Parameter(Mandatory = true, ParameterSetName = "PlayOnPhoneGreeting")]
		[ValidateNotNullOrEmpty]
		public UMCallAnsweringRuleIdParameter CallAnsweringRuleId
		{
			get
			{
				return (UMCallAnsweringRuleIdParameter)base.Fields["CallAnsweringRuleId"];
			}
			set
			{
				base.Fields["CallAnsweringRuleId"] = value;
			}
		}

		// Token: 0x06008256 RID: 33366 RVA: 0x00215108 File Offset: 0x00213308
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 133, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\StartUMPhoneSession.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06008257 RID: 33367 RVA: 0x002151BA File Offset: 0x002133BA
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.ValidateParameters();
			if (!base.HasErrors)
			{
				this.DataObject.PhoneNumber = this.PhoneNumber;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008258 RID: 33368 RVA: 0x002151EC File Offset: 0x002133EC
		protected override IConfigDataProvider CreateSession()
		{
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (parameterSetName == "AwayVoicemailGreeting")
				{
					return this.CreateProviderObjectForMailbox(TypeOfPlayOnPhoneGreetingCall.AwayGreetingRecording);
				}
				if (parameterSetName == "DefaultVoicemailGreeting")
				{
					return this.CreateProviderObjectForMailbox(TypeOfPlayOnPhoneGreetingCall.VoicemailGreetingRecording);
				}
				if (parameterSetName == "PlayOnPhoneGreeting")
				{
					return this.CreateProviderObjectForPlayOnPhone();
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06008259 RID: 33369 RVA: 0x00215248 File Offset: 0x00213448
		private void ResolveADUser(MailboxIdParameter mailbox)
		{
			this.adUser = (ADUser)base.GetDataObject<ADUser>(mailbox, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorUserNotFound(mailbox.ToString())), new LocalizedString?(Strings.ErrorUserNotUnique(mailbox.ToString())));
			base.VerifyIsWithinScopes(TaskHelper.UnderscopeSessionToOrganization(base.TenantGlobalCatalogSession, this.adUser.OrganizationId, true), this.adUser, true, new DataAccessTask<UMPhoneSession>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
		}

		// Token: 0x0600825A RID: 33370 RVA: 0x002152BE File Offset: 0x002134BE
		private UMPlayOnPhoneDataProvider CreateProviderObjectForMailbox(TypeOfPlayOnPhoneGreetingCall callType)
		{
			this.ResolveADUser(this.UMMailbox);
			return new UMPlayOnPhoneDataProvider(this.adUser, callType);
		}

		// Token: 0x0600825B RID: 33371 RVA: 0x002152D8 File Offset: 0x002134D8
		private UMPlayOnPhoneDataProvider CreateProviderObjectForPlayOnPhone()
		{
			MailboxIdParameter mailboxIdParameter = this.CallAnsweringRuleId.RawMailbox;
			if (mailboxIdParameter == null)
			{
				ADObjectId adObjectId;
				if (!base.TryGetExecutingUserId(out adObjectId))
				{
					base.WriteError(new MailboxMustBeSpecifiedException("CallAnsweringRuleId"), ErrorCategory.InvalidArgument, null);
				}
				mailboxIdParameter = new MailboxIdParameter(adObjectId);
			}
			this.ResolveADUser(mailboxIdParameter);
			return new UMPlayOnPhoneDataProvider(this.adUser, new Guid?(this.CallAnsweringRuleId.RawRuleGuid.Value));
		}

		// Token: 0x0600825C RID: 33372 RVA: 0x00215344 File Offset: 0x00213544
		private void ValidateParameters()
		{
			if (this.CallAnsweringRuleId != null)
			{
				using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(this.adUser))
				{
					if (umsubscriber != null)
					{
						using (IPAAStore ipaastore = PAAStore.Create(umsubscriber))
						{
							if (ipaastore.GetAutoAttendant(this.CallAnsweringRuleId.RawRuleGuid.Value, PAAValidationMode.None) == null)
							{
								base.WriteError(new CallAnsweringRuleNotFoundException(this.CallAnsweringRuleId.RawRuleGuid.Value.ToString()), ErrorCategory.InvalidArgument, null);
							}
							goto IL_99;
						}
					}
					base.WriteError(new UserNotUmEnabledException(this.adUser.Id.ToString()), (ErrorCategory)1000, null);
					IL_99:;
				}
			}
		}

		// Token: 0x04003F3B RID: 16187
		private ADUser adUser;

		// Token: 0x02000D47 RID: 3399
		internal abstract class ParameterSet
		{
			// Token: 0x04003F3C RID: 16188
			internal const string DefaultVoicemailGreeting = "DefaultVoicemailGreeting";

			// Token: 0x04003F3D RID: 16189
			internal const string AwayVoicemailGreeting = "AwayVoicemailGreeting";

			// Token: 0x04003F3E RID: 16190
			internal const string PlayOnPhoneGreeting = "PlayOnPhoneGreeting";
		}
	}
}
