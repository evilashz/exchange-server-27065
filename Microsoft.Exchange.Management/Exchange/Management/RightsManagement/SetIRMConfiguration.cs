using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.Authentication;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Security.RightsManagement.SOAP.Server;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000727 RID: 1831
	[Cmdlet("Set", "IRMConfiguration", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetIRMConfiguration : SetMultitenancySingletonSystemConfigurationObjectTask<IRMConfiguration>
	{
		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x060040EF RID: 16623 RVA: 0x0010A56C File Offset: 0x0010876C
		// (set) Token: 0x060040F0 RID: 16624 RVA: 0x0010A592 File Offset: 0x00108792
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x060040F1 RID: 16625 RVA: 0x0010A5AA File Offset: 0x001087AA
		// (set) Token: 0x060040F2 RID: 16626 RVA: 0x0010A5D0 File Offset: 0x001087D0
		[Parameter(Mandatory = false)]
		public SwitchParameter RefreshServerCertificates
		{
			get
			{
				return (SwitchParameter)(base.Fields["RefreshServerCertificates"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RefreshServerCertificates"] = value;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060040F3 RID: 16627 RVA: 0x0010A5E8 File Offset: 0x001087E8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageIRMConfig;
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x0010A5EF File Offset: 0x001087EF
		protected override IConfigurable ResolveDataObject()
		{
			return IRMConfiguration.Read((IConfigurationSession)base.DataSession);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0010A604 File Offset: 0x00108804
		protected override IConfigurable PrepareDataObject()
		{
			IRMConfiguration irmconfiguration = (IRMConfiguration)base.PrepareDataObject();
			if (this.RefreshServerCertificates)
			{
				irmconfiguration.ServerCertificatesVersion++;
			}
			return irmconfiguration;
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x0010A63C File Offset: 0x0010883C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			IRMConfiguration dataObject = this.DataObject;
			try
			{
				this.ValidateCommon();
				this.datacenter = (Datacenter.IsMicrosoftHostedOnly(true) || Datacenter.IsPartnerHostedOnly(true));
				if (this.datacenter)
				{
					this.ValidateForDC(dataObject);
				}
				else
				{
					this.ValidateForEnterprise(dataObject);
				}
			}
			catch (CannotDetermineExchangeModeException exception)
			{
				base.WriteError(SetIRMConfiguration.GetExceptionInfo(exception), ErrorCategory.InvalidOperation, base.Identity);
			}
			catch (ExchangeConfigurationException exception2)
			{
				base.WriteError(SetIRMConfiguration.GetExceptionInfo(exception2), ErrorCategory.InvalidOperation, base.Identity);
			}
			catch (RightsManagementException exception3)
			{
				base.WriteError(SetIRMConfiguration.GetExceptionInfo(exception3), ErrorCategory.InvalidOperation, base.Identity);
			}
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x0010A6F8 File Offset: 0x001088F8
		protected override void InternalProcessRecord()
		{
			if (this.DataObject.IsChanged(IRMConfigurationSchema.InternalLicensingEnabled) && !this.DataObject.InternalLicensingEnabled && !this.Force && !base.ShouldContinue(Strings.ConfirmationOnDisablingInternalLicensing))
			{
				return;
			}
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				return;
			}
			if (this.DataObject.InternalLicensingEnabled && this.DataObject.IsChanged(IRMConfigurationSchema.TransportDecryptionMandatory) && this.DataObject.TransportDecryptionSetting == TransportDecryptionSetting.Mandatory && !this.datacenter)
			{
				this.WriteWarning(Strings.WarningTransportDecryptionMandatoryRequiresSuperUser);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x0010A7BC File Offset: 0x001089BC
		private static Exception GetExceptionInfo(Exception exception)
		{
			string text = string.Empty;
			Exception ex = exception;
			int num = 0;
			while (num < 10 && ex != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
				{
					(num == 9 || ex.InnerException == null) ? string.Empty : " ---> ",
					ex.Message,
					text
				});
				ex = ex.InnerException;
				num++;
			}
			return new Exception(text, exception);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x0010A830 File Offset: 0x00108A30
		private void ValidateCommon()
		{
			if (this.DataObject.IsChanged(IRMConfigurationSchema.InternalLicensingEnabled) && !this.DataObject.InternalLicensingEnabled)
			{
				this.ThrowIfAnyEnabledTransportRulesHaveRightsProtectMessageAction();
				this.ThrowIfAnyUmMailboxPoliciesProtectVoiceMail();
				this.ThrowIfAnyEnabledTransportRuleHaveEncryptOrDecryptAction();
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x0010A864 File Offset: 0x00108A64
		private void ThrowIfAnyEnabledTransportRulesHaveRightsProtectMessageAction()
		{
			IEnumerable<TransportRule> transportRulesMatchingDelegate = Utils.GetTransportRulesMatchingDelegate(this.ConfigurationSession, new Utils.TransportRuleSelectionDelegate(SetIRMConfiguration.IsTransportRuleEnabledAndHasRightsProtectMessageAction), null);
			TransportRule transportRule = transportRulesMatchingDelegate.FirstOrDefault<TransportRule>();
			if (transportRule != null)
			{
				base.WriteError(new EtrHasRmsActionException(transportRule.Name), ExchangeErrorCategory.Client, base.Identity);
			}
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x0010A8BB File Offset: 0x00108ABB
		private static bool IsTransportRuleEnabledAndHasRightsProtectMessageAction(Microsoft.Exchange.MessagingPolicies.Rules.Rule transportRule, object delegateContext)
		{
			if (transportRule.Enabled != RuleState.Enabled)
			{
				return false;
			}
			return transportRule.Actions.Any((Microsoft.Exchange.MessagingPolicies.Rules.Action action) => action is RightsProtectMessage);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x0010A8F0 File Offset: 0x00108AF0
		private void ThrowIfAnyEnabledTransportRuleHaveEncryptOrDecryptAction()
		{
			IEnumerable<TransportRule> transportRulesMatchingDelegate = Utils.GetTransportRulesMatchingDelegate(this.ConfigurationSession, new Utils.TransportRuleSelectionDelegate(SetIRMConfiguration.IsTransportRuleEnabledAndHasEncryptMessageOrDecryptMessageAction), null);
			TransportRule transportRule = transportRulesMatchingDelegate.FirstOrDefault<TransportRule>();
			if (transportRule != null)
			{
				base.WriteError(new EtrHasE4eActionException(transportRule.Name), ExchangeErrorCategory.Client, base.Identity);
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x0010A954 File Offset: 0x00108B54
		private static bool IsTransportRuleEnabledAndHasEncryptMessageOrDecryptMessageAction(Microsoft.Exchange.MessagingPolicies.Rules.Rule transportRule, object delegateContext)
		{
			if (transportRule.Enabled != RuleState.Enabled)
			{
				return false;
			}
			if (!transportRule.Actions.Any((Microsoft.Exchange.MessagingPolicies.Rules.Action action) => action is ApplyOME))
			{
				return transportRule.Actions.Any((Microsoft.Exchange.MessagingPolicies.Rules.Action action) => action is RemoveOME);
			}
			return true;
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x0010A9C0 File Offset: 0x00108BC0
		private void ThrowIfAnyUmMailboxPoliciesProtectVoiceMail()
		{
			IEnumerable<UMMailboxPolicy> enumerable = this.ConfigurationSession.FindAllPaged<UMMailboxPolicy>();
			foreach (UMMailboxPolicy ummailboxPolicy in enumerable)
			{
				if (ummailboxPolicy.ProtectAuthenticatedVoiceMail != DRMProtectionOptions.None)
				{
					base.WriteError(new UnifiedMessagingMailboxPolicyHasProtectAuthenticatedVoiceMailSetToException(ummailboxPolicy.Name, ummailboxPolicy.ProtectAuthenticatedVoiceMail), ExchangeErrorCategory.Client, base.Identity);
				}
				if (ummailboxPolicy.ProtectUnauthenticatedVoiceMail != DRMProtectionOptions.None)
				{
					base.WriteError(new UnifiedMessagingMailboxPolicyHasProtectUnauthenticatedVoiceMailSetToException(ummailboxPolicy.Name, ummailboxPolicy.ProtectUnauthenticatedVoiceMail), ExchangeErrorCategory.Client, base.Identity);
				}
			}
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0010AA64 File Offset: 0x00108C64
		private void ValidateForDC(IRMConfiguration config)
		{
			if (config.InternalLicensingEnabled)
			{
				IEnumerable<RMSTrustedPublishingDomain> enumerable = ((IConfigurationSession)base.DataSession).FindPaged<RMSTrustedPublishingDomain>(this.DataObject.Id, QueryScope.OneLevel, null, null, 0);
				foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in enumerable)
				{
					if (string.IsNullOrEmpty(rmstrustedPublishingDomain.PrivateKey))
					{
						base.WriteError(new TPDWithoutPrivateKeyException(rmstrustedPublishingDomain.Name), (ErrorCategory)1000, base.Identity);
					}
				}
				if (config.ServiceLocation == null)
				{
					base.WriteError(new NoTPDsImportedException(), ErrorCategory.InvalidOperation, base.Identity);
				}
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0010AB1C File Offset: 0x00108D1C
		private void ValidateForEnterprise(IRMConfiguration config)
		{
			if (config.ExternalLicensingEnabled && !ExternalAuthentication.GetCurrent().Enabled)
			{
				base.WriteError(new OrganizationNotFederatedException(), ErrorCategory.InvalidOperation, base.Identity);
			}
			if (config.InternalLicensingEnabled)
			{
				Uri rmsserviceLocation = RmsClientManager.GetRMSServiceLocation(OrganizationId.ForestWideOrgId, ServiceType.Certification);
				if (rmsserviceLocation == null)
				{
					base.WriteError(new NoRMSServersFoundException(), ErrorCategory.InvalidOperation, base.Identity);
				}
				this.ValidateRmsVersion(rmsserviceLocation, ServiceType.CertificationService);
				this.ValidateRmsVersion(rmsserviceLocation, ServiceType.LicensingService);
			}
			if (!MultiValuedPropertyBase.IsNullOrEmpty(config.LicensingLocation))
			{
				foreach (Uri uri in config.LicensingLocation)
				{
					if (string.IsNullOrEmpty(RMUtil.ConvertUriToLicenseUrl(uri)))
					{
						base.WriteError(new RmsUrlIsInvalidException(uri), ErrorCategory.InvalidOperation, base.Identity);
					}
					this.ValidateRmsVersion(uri, ServiceType.LicensingService);
				}
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0010AC04 File Offset: 0x00108E04
		private void ValidateRmsVersion(Uri uri, ServiceType serviceType)
		{
			if (serviceType == ServiceType.LicensingService)
			{
				uri = RmsoProxyUtil.GetLicenseServerRedirectUrl(uri);
			}
			if (serviceType == ServiceType.CertificationService)
			{
				uri = RmsoProxyUtil.GetCertificationServerRedirectUrl(uri);
			}
			if ((this.DataObject.IsChanged(IRMConfigurationSchema.InternalLicensingEnabled) && this.DataObject.InternalLicensingEnabled) || (this.DataObject.IsChanged(IRMConfigurationSchema.LicensingLocation) && serviceType == ServiceType.LicensingService))
			{
				using (ServerWSManager serverWSManager = new ServerWSManager(uri, serviceType, null, null, RmsClientManagerUtils.GetLocalServerProxy(this.datacenter), RmsClientManager.AppSettings.RmsSoapQueriesTimeout))
				{
					if (serviceType == ServiceType.CertificationService && !serverWSManager.ValidateCertificationServiceVersion())
					{
						base.WriteError(new RmsVersionMismatchException(uri), ErrorCategory.InvalidOperation, base.Identity);
					}
					if (serviceType == ServiceType.LicensingService && !serverWSManager.ValidateLicensingServiceVersion())
					{
						base.WriteError(new RmsVersionMismatchException(uri), ErrorCategory.InvalidOperation, base.Identity);
					}
				}
			}
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x0010ACD8 File Offset: 0x00108ED8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception);
		}

		// Token: 0x0400291E RID: 10526
		private const string Separator = " ---> ";

		// Token: 0x0400291F RID: 10527
		private bool datacenter;
	}
}
