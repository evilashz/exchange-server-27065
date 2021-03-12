using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D0F RID: 3343
	public class EnableUMMailboxBase<TIdentity> : UMMailboxTask<TIdentity> where TIdentity : RecipientIdParameter, new()
	{
		// Token: 0x06008049 RID: 32841 RVA: 0x0020CB33 File Offset: 0x0020AD33
		public EnableUMMailboxBase()
		{
			this.PinExpired = true;
			this.ValidateOnly = false;
		}

		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x0600804A RID: 32842 RVA: 0x0020CB4E File Offset: 0x0020AD4E
		// (set) Token: 0x0600804B RID: 32843 RVA: 0x0020CB65 File Offset: 0x0020AD65
		[Parameter]
		public MultiValuedProperty<string> Extensions
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["Extensions"];
			}
			set
			{
				base.Fields["Extensions"] = value;
			}
		}

		// Token: 0x170027C0 RID: 10176
		// (get) Token: 0x0600804C RID: 32844 RVA: 0x0020CB78 File Offset: 0x0020AD78
		// (set) Token: 0x0600804D RID: 32845 RVA: 0x0020CB8F File Offset: 0x0020AD8F
		[Parameter(Mandatory = true)]
		public MailboxPolicyIdParameter UMMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["UMMailboxPolicy"];
			}
			set
			{
				base.Fields["UMMailboxPolicy"] = value;
			}
		}

		// Token: 0x170027C1 RID: 10177
		// (get) Token: 0x0600804E RID: 32846 RVA: 0x0020CBA2 File Offset: 0x0020ADA2
		// (set) Token: 0x0600804F RID: 32847 RVA: 0x0020CBB9 File Offset: 0x0020ADB9
		[Parameter(Mandatory = false)]
		public string SIPResourceIdentifier
		{
			get
			{
				return (string)base.Fields["SIPResourceIdentifier"];
			}
			set
			{
				base.Fields["SIPResourceIdentifier"] = value;
			}
		}

		// Token: 0x170027C2 RID: 10178
		// (get) Token: 0x06008050 RID: 32848 RVA: 0x0020CBCC File Offset: 0x0020ADCC
		// (set) Token: 0x06008051 RID: 32849 RVA: 0x0020CBE3 File Offset: 0x0020ADE3
		[Parameter(Mandatory = false)]
		public string Pin
		{
			get
			{
				return (string)base.Fields["Pin"];
			}
			set
			{
				base.Fields["Pin"] = value;
			}
		}

		// Token: 0x170027C3 RID: 10179
		// (get) Token: 0x06008052 RID: 32850 RVA: 0x0020CBF6 File Offset: 0x0020ADF6
		// (set) Token: 0x06008053 RID: 32851 RVA: 0x0020CC17 File Offset: 0x0020AE17
		[Parameter(Mandatory = false)]
		public bool PinExpired
		{
			get
			{
				return (bool)(base.Fields["PinExpired"] ?? false);
			}
			set
			{
				base.Fields["PinExpired"] = value;
			}
		}

		// Token: 0x170027C4 RID: 10180
		// (get) Token: 0x06008054 RID: 32852 RVA: 0x0020CC2F File Offset: 0x0020AE2F
		// (set) Token: 0x06008055 RID: 32853 RVA: 0x0020CC46 File Offset: 0x0020AE46
		[Parameter(Mandatory = false)]
		public string NotifyEmail
		{
			get
			{
				return (string)base.Fields["NotifyEmail"];
			}
			set
			{
				base.Fields["NotifyEmail"] = value;
			}
		}

		// Token: 0x170027C5 RID: 10181
		// (get) Token: 0x06008056 RID: 32854 RVA: 0x0020CC59 File Offset: 0x0020AE59
		// (set) Token: 0x06008057 RID: 32855 RVA: 0x0020CC70 File Offset: 0x0020AE70
		[Parameter(Mandatory = false)]
		public string PilotNumber
		{
			get
			{
				return (string)base.Fields["PilotNumber"];
			}
			set
			{
				base.Fields["PilotNumber"] = value;
			}
		}

		// Token: 0x170027C6 RID: 10182
		// (get) Token: 0x06008058 RID: 32856 RVA: 0x0020CC83 File Offset: 0x0020AE83
		// (set) Token: 0x06008059 RID: 32857 RVA: 0x0020CCA4 File Offset: 0x0020AEA4
		[Parameter(Mandatory = false)]
		public bool AutomaticSpeechRecognitionEnabled
		{
			get
			{
				return (bool)(base.Fields["ASREnabled"] ?? false);
			}
			set
			{
				base.Fields["ASREnabled"] = value;
			}
		}

		// Token: 0x170027C7 RID: 10183
		// (get) Token: 0x0600805A RID: 32858 RVA: 0x0020CCBC File Offset: 0x0020AEBC
		// (set) Token: 0x0600805B RID: 32859 RVA: 0x0020CCD3 File Offset: 0x0020AED3
		[Parameter]
		public SwitchParameter ValidateOnly
		{
			get
			{
				return (SwitchParameter)base.Fields["ValidateOnly"];
			}
			set
			{
				base.Fields["ValidateOnly"] = value;
			}
		}

		// Token: 0x170027C8 RID: 10184
		// (get) Token: 0x0600805C RID: 32860 RVA: 0x0020CCEC File Offset: 0x0020AEEC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				TIdentity identity = this.Identity;
				return Strings.ConfirmationMessageEnableUMMailbox(identity.ToString(), this.UMMailboxPolicy.ToString());
			}
		}

		// Token: 0x170027C9 RID: 10185
		// (get) Token: 0x0600805D RID: 32861 RVA: 0x0020CD1D File Offset: 0x0020AF1D
		protected virtual bool ShouldSavePin
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027CA RID: 10186
		// (get) Token: 0x0600805E RID: 32862 RVA: 0x0020CD20 File Offset: 0x0020AF20
		protected virtual bool ShouldSubmitWelcomeMessage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027CB RID: 10187
		// (get) Token: 0x0600805F RID: 32863 RVA: 0x0020CD23 File Offset: 0x0020AF23
		protected virtual bool ShouldInitUMMailbox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027CC RID: 10188
		// (get) Token: 0x06008060 RID: 32864 RVA: 0x0020CD26 File Offset: 0x0020AF26
		protected UMDialPlan DialPlan
		{
			get
			{
				return this.dialPlan;
			}
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x0020CD30 File Offset: 0x0020AF30
		private bool IsUserAllowedForUnifiedMessaging(ADUser user)
		{
			bool result = true;
			if (user != null && user.Database != null)
			{
				DatabaseLocationInfo serverForDatabase = ActiveManager.GetCachingActiveManagerInstance().GetServerForDatabase(user.Database.ObjectGuid);
				if (serverForDatabase != null && serverForDatabase.AdminDisplayVersion.Major > (int)ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major && (serverForDatabase.AdminDisplayVersion.Major != 15 || serverForDatabase.AdminDisplayVersion.Minor > 1))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x0020CDB4 File Offset: 0x0020AFB4
		protected override void DoValidate()
		{
			LocalizedException ex = null;
			if (this.ShouldInitUMMailbox && UMSubscriber.IsValidSubscriber(this.DataObject))
			{
				ex = new UserAlreadyUmEnabledException(this.DataObject.Id.Name);
			}
			else if (!this.IsUserAllowedForUnifiedMessaging(this.DataObject))
			{
				ex = new UserNotAllowedForUMEnabledException();
			}
			else
			{
				MailboxPolicyIdParameter ummailboxPolicy = this.UMMailboxPolicy;
				this.mailboxPolicy = (UMMailboxPolicy)base.GetDataObject<UMMailboxPolicy>(ummailboxPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.UMMailboxPolicyIdNotFound(ummailboxPolicy.ToString())), new LocalizedString?(Strings.MultipleUMMailboxPolicyWithSameId(ummailboxPolicy.ToString())));
				this.dialPlan = this.mailboxPolicy.GetDialPlan();
				if (this.DialPlan.SubscriberType != UMSubscriberType.Consumer || this.DialPlan.URIType != UMUriType.E164 || !string.IsNullOrEmpty(this.SIPResourceIdentifier) || (this.Extensions != null && this.Extensions.Count != 0))
				{
					if (this.Extensions == null || this.Extensions.Count == 0)
					{
						string text = null;
						PhoneNumber phoneNumber;
						if (PhoneNumber.TryParse(this.DataObject.Phone, out phoneNumber))
						{
							text = this.DialPlan.GetDefaultExtension(phoneNumber.Number);
						}
						if (!string.IsNullOrEmpty(Utils.TrimSpaces(text)))
						{
							this.Extensions = new MultiValuedProperty<string>(text);
						}
					}
					if (this.DialPlan.URIType == UMUriType.SipName)
					{
						ProxyAddress proxyAddress = this.DataObject.EmailAddresses.Find((ProxyAddress p) => string.Equals(p.PrefixString, "sip", StringComparison.OrdinalIgnoreCase));
						string text2 = (proxyAddress != null) ? proxyAddress.AddressString : null;
						if (string.IsNullOrEmpty(this.SIPResourceIdentifier))
						{
							this.SIPResourceIdentifier = ((text2 != null) ? text2 : null);
						}
						else if (text2 != null && !string.Equals(this.SIPResourceIdentifier, text2, StringComparison.OrdinalIgnoreCase))
						{
							ex = new SIPResouceIdConflictWithExistingValue(this.SIPResourceIdentifier, text2);
							base.WriteError(ex, ErrorCategory.InvalidArgument, null);
						}
					}
					IRecipientSession tenantLocalRecipientSession = RecipientTaskHelper.GetTenantLocalRecipientSession(this.DataObject.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
					LocalizedException ex2 = null;
					TelephoneNumberProcessStatus telephoneNumberProcessStatus;
					Utils.ValidateExtensionsAndSipResourceIdentifier(tenantLocalRecipientSession, this.ConfigurationSession, CommonConstants.DataCenterADPresent, this.DataObject, this.DialPlan, (this.Extensions != null) ? this.Extensions.ToArray() : null, null, this.SIPResourceIdentifier, out ex2, out telephoneNumberProcessStatus);
					if (ex2 != null)
					{
						this.DataObject.EmailAddresses.Clear();
						ex = ex2;
					}
				}
			}
			if (ex == null || this.ValidateOnly)
			{
				this.DataObject.UMEnabledFlags |= UMEnabledFlags.UMEnabled;
				if (base.Fields.IsModified("ASREnabled"))
				{
					bool flag = (bool)base.Fields["ASREnabled"];
					if (flag)
					{
						this.DataObject.UMEnabledFlags |= UMEnabledFlags.ASREnabled;
					}
					else
					{
						this.DataObject.UMEnabledFlags = (this.DataObject.UMEnabledFlags & ~UMEnabledFlags.ASREnabled);
					}
				}
				this.DataObject.PopulateDtmfMap(true);
				if (!this.ValidateOnly)
				{
					Utils.UMPopulate(this.DataObject, this.SIPResourceIdentifier, this.Extensions, this.mailboxPolicy, this.DialPlan);
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						RecipientTaskHelper.ValidateSmtpAddress(this.ConfigurationSession, this.DataObject.EmailAddresses, this.DataObject, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache);
					}
					if (this.ShouldSavePin)
					{
						base.PinInfo = base.ValidateOrGeneratePIN(this.Pin, this.mailboxPolicy.Guid);
						base.PinInfo.PinExpired = this.PinExpired;
						base.PinInfo.LockedOut = false;
					}
				}
				else if (this.Extensions != null)
				{
					this.DataObject.AddEUMProxyAddress(this.Extensions, this.DialPlan);
				}
			}
			if (ex == null)
			{
				if (this.ValidateOnly)
				{
					this.WriteResult();
				}
				return;
			}
			if (this.ValidateOnly)
			{
				this.WriteResult();
				base.WriteError(ex, ErrorCategory.InvalidArgument, null, false, null);
				return;
			}
			base.WriteError(ex, ErrorCategory.InvalidArgument, null);
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x0020D1C0 File Offset: 0x0020B3C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.ValidateOnly)
			{
				return;
			}
			if (this.ShouldSavePin)
			{
				base.SavePIN(this.mailboxPolicy.Guid);
			}
			if (this.ShouldInitUMMailbox)
			{
				base.InitUMMailbox();
			}
			base.InternalProcessRecord();
			if (!base.HasErrors)
			{
				if (this.ShouldSubmitWelcomeMessage)
				{
					base.SubmitWelcomeMessage(this.NotifyEmail, this.PilotNumber, this.Extensions, this.DialPlan);
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMUserEnabled, null, new object[]
				{
					this.DataObject.Id.Name
				});
				this.WriteResult();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06008064 RID: 32868 RVA: 0x0020D274 File Offset: 0x0020B474
		private void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			UMMailbox ummailbox = new UMMailbox(this.DataObject);
			if (this.ValidateOnly)
			{
				ummailbox.SIPResourceIdentifier = this.SIPResourceIdentifier;
			}
			base.WriteObject(ummailbox);
			TaskLogger.LogExit();
		}

		// Token: 0x04003ED8 RID: 16088
		private UMDialPlan dialPlan;

		// Token: 0x04003ED9 RID: 16089
		private UMMailboxPolicy mailboxPolicy;
	}
}
