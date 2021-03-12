using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000729 RID: 1833
	[Cmdlet("Set", "RMSTrustedPublishingDomain", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRmsTrustedPublishingDomain : SetSystemConfigurationObjectTask<RmsTrustedPublishingDomainIdParameter, RMSTrustedPublishingDomain>
	{
		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x0010B0A7 File Offset: 0x001092A7
		// (set) Token: 0x06004118 RID: 16664 RVA: 0x0010B0BE File Offset: 0x001092BE
		[Parameter(Mandatory = false)]
		public Uri IntranetLicensingUrl
		{
			get
			{
				return (Uri)base.Fields["IntranetLicensingUrl"];
			}
			set
			{
				base.Fields["IntranetLicensingUrl"] = value;
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x0010B0D1 File Offset: 0x001092D1
		// (set) Token: 0x0600411A RID: 16666 RVA: 0x0010B0E8 File Offset: 0x001092E8
		[Parameter(Mandatory = false)]
		public Uri ExtranetLicensingUrl
		{
			get
			{
				return (Uri)base.Fields["ExtranetLicensingUrl"];
			}
			set
			{
				base.Fields["ExtranetLicensingUrl"] = value;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x0010B0FB File Offset: 0x001092FB
		// (set) Token: 0x0600411C RID: 16668 RVA: 0x0010B112 File Offset: 0x00109312
		[Parameter(Mandatory = false)]
		public Uri IntranetCertificationUrl
		{
			get
			{
				return (Uri)base.Fields["IntranetCertificationUrl"];
			}
			set
			{
				base.Fields["IntranetCertificationUrl"] = value;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x0010B125 File Offset: 0x00109325
		// (set) Token: 0x0600411E RID: 16670 RVA: 0x0010B13C File Offset: 0x0010933C
		[Parameter(Mandatory = false)]
		public Uri ExtranetCertificationUrl
		{
			get
			{
				return (Uri)base.Fields["ExtranetCertificationUrl"];
			}
			set
			{
				base.Fields["ExtranetCertificationUrl"] = value;
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x0010B14F File Offset: 0x0010934F
		// (set) Token: 0x06004120 RID: 16672 RVA: 0x0010B175 File Offset: 0x00109375
		[Parameter(Mandatory = false)]
		public SwitchParameter Default
		{
			get
			{
				return (SwitchParameter)(base.Fields["Default"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Default"] = value;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x0010B18D File Offset: 0x0010938D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRMSTPD(this.Identity.ToString());
			}
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0010B1A0 File Offset: 0x001093A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			bool flag = false;
			if (base.Fields.IsModified("IntranetCertificationUrl") && !RMUtil.IsWellFormedRmServiceUrl(this.IntranetCertificationUrl))
			{
				base.WriteError(new RmsUrlIsInvalidException(this.IntranetCertificationUrl), (ErrorCategory)1000, this.IntranetCertificationUrl);
			}
			if (base.Fields.IsModified("ExtranetCertificationUrl") && !RMUtil.IsWellFormedRmServiceUrl(this.ExtranetCertificationUrl))
			{
				base.WriteError(new RmsUrlIsInvalidException(this.ExtranetCertificationUrl), (ErrorCategory)1000, this.ExtranetCertificationUrl);
			}
			Uri intranetLicensingUrl;
			if (base.Fields.IsModified("IntranetLicensingUrl"))
			{
				if (!RMUtil.IsWellFormedRmServiceUrl(this.IntranetLicensingUrl))
				{
					base.WriteError(new RmsUrlIsInvalidException(this.IntranetLicensingUrl), (ErrorCategory)1000, this.IntranetLicensingUrl);
				}
				intranetLicensingUrl = this.IntranetLicensingUrl;
				flag = true;
			}
			else
			{
				intranetLicensingUrl = this.DataObject.IntranetLicensingUrl;
			}
			Uri extranetLicensingUrl;
			if (base.Fields.IsModified("ExtranetLicensingUrl"))
			{
				if (!RMUtil.IsWellFormedRmServiceUrl(this.ExtranetLicensingUrl))
				{
					base.WriteError(new RmsUrlIsInvalidException(this.ExtranetLicensingUrl), (ErrorCategory)1000, this.ExtranetLicensingUrl);
				}
				extranetLicensingUrl = this.ExtranetLicensingUrl;
				flag = true;
			}
			else
			{
				extranetLicensingUrl = this.DataObject.ExtranetLicensingUrl;
			}
			if (flag)
			{
				foreach (string encodedTemplate in this.DataObject.RMSTemplates)
				{
					RmsTemplateType rmsTemplateType;
					string templateXrml = RMUtil.DecompressTemplate(encodedTemplate, out rmsTemplateType);
					this.ValidateTemplate(templateXrml, intranetLicensingUrl, extranetLicensingUrl);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004123 RID: 16675 RVA: 0x0010B348 File Offset: 0x00109548
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			bool flag = false;
			IRMConfiguration irmconfiguration = IRMConfiguration.Read((IConfigurationSession)base.DataSession, out flag);
			RMSTrustedPublishingDomain rmstrustedPublishingDomain = null;
			if (irmconfiguration == null)
			{
				base.WriteError(new FailedToAccessIrmConfigurationException(), (ErrorCategory)1002, this.Identity);
			}
			if (this.IntranetCertificationUrl != null)
			{
				this.DataObject.IntranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(this.IntranetCertificationUrl);
			}
			if (this.ExtranetCertificationUrl != null)
			{
				this.DataObject.ExtranetCertificationUrl = RMUtil.ConvertUriToCertificateLocationDistributionPoint(this.ExtranetCertificationUrl);
			}
			if (this.IntranetLicensingUrl != null)
			{
				if (irmconfiguration.LicensingLocation.Contains(this.DataObject.IntranetLicensingUrl))
				{
					irmconfiguration.LicensingLocation.Remove(this.DataObject.IntranetLicensingUrl);
				}
				Uri uri = RMUtil.ConvertUriToLicenseLocationDistributionPoint(this.IntranetLicensingUrl);
				if (!irmconfiguration.LicensingLocation.Contains(uri))
				{
					irmconfiguration.LicensingLocation.Add(uri);
				}
				this.DataObject.IntranetLicensingUrl = uri;
			}
			if (this.ExtranetLicensingUrl != null)
			{
				if (irmconfiguration.LicensingLocation.Contains(this.DataObject.ExtranetLicensingUrl))
				{
					irmconfiguration.LicensingLocation.Remove(this.DataObject.ExtranetLicensingUrl);
				}
				Uri uri2 = RMUtil.ConvertUriToLicenseLocationDistributionPoint(this.ExtranetLicensingUrl);
				if (!irmconfiguration.LicensingLocation.Contains(uri2))
				{
					irmconfiguration.LicensingLocation.Add(uri2);
				}
				this.DataObject.ExtranetLicensingUrl = uri2;
			}
			if (this.Default && !this.DataObject.Default)
			{
				this.DataObject.Default = true;
				try
				{
					ImportRmsTrustedPublishingDomain.ChangeDefaultTPDAndUpdateIrmConfigData((IConfigurationSession)base.DataSession, irmconfiguration, this.DataObject, out rmstrustedPublishingDomain);
					irmconfiguration.ServerCertificatesVersion++;
				}
				catch (RightsManagementServerException ex)
				{
					base.WriteError(new FailedToGenerateSharedKeyException(ex), (ErrorCategory)1000, this.Identity);
				}
			}
			if (rmstrustedPublishingDomain != null)
			{
				this.WriteWarning(Strings.WarningChangeDefaultTPD(rmstrustedPublishingDomain.Name, this.DataObject.Name));
				base.DataSession.Save(rmstrustedPublishingDomain);
			}
			base.DataSession.Save(irmconfiguration);
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x0010B588 File Offset: 0x00109788
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception) || typeof(InvalidRpmsgFormatException).IsInstanceOfType(exception) || typeof(FormatException).IsInstanceOfType(exception);
		}

		// Token: 0x06004125 RID: 16677 RVA: 0x0010B5C0 File Offset: 0x001097C0
		private void ValidateTemplate(string templateXrml, Uri intranetLicensingUrl, Uri extranetLicensingUrl)
		{
			Uri uri = null;
			Uri uri2 = null;
			Guid templateGuid;
			DrmClientUtils.ParseTemplate(templateXrml, out uri, out uri2, out templateGuid);
			if (uri != null && Uri.Compare(uri, intranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0 && Uri.Compare(uri, extranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0)
			{
				base.WriteError(new FailedToMatchTemplateDistributionPointToLicensingUriException(templateGuid, uri, intranetLicensingUrl), (ErrorCategory)1000, intranetLicensingUrl);
			}
			if (uri2 != null && Uri.Compare(uri2, intranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0 && Uri.Compare(uri2, extranetLicensingUrl, UriComponents.SchemeAndServer, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0)
			{
				base.WriteError(new FailedToMatchTemplateDistributionPointToLicensingUriException(templateGuid, uri2, extranetLicensingUrl), (ErrorCategory)1000, extranetLicensingUrl);
			}
		}
	}
}
