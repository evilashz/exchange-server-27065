using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000746 RID: 1862
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RMSOnlineValidator
	{
		// Token: 0x060041F4 RID: 16884 RVA: 0x0010D0B8 File Offset: 0x0010B2B8
		public RMSOnlineValidator(IConfigurationSession configurationSession, IConfigurationSession dataSession, OrganizationId organizationId, Guid rmsOnlineGuidOverride, string authenticationCertificateSubjectNameOverride = null)
		{
			RmsUtil.ThrowIfParameterNull(dataSession, "configurationSession");
			RmsUtil.ThrowIfParameterNull(dataSession, "dataSession");
			RmsUtil.ThrowIfParameterNull(organizationId, "organizationId");
			this.configurationSession = configurationSession;
			this.dataSession = dataSession;
			this.organizationId = organizationId;
			this.rmsOnlineGuidOverride = rmsOnlineGuidOverride;
			if (authenticationCertificateSubjectNameOverride != null)
			{
				this.authenticationCertificateSubjectName = authenticationCertificateSubjectNameOverride;
			}
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x0010D140 File Offset: 0x0010B340
		public IRMConfigurationValidationResult Validate()
		{
			try
			{
				if (!this.ValidateIsTenantContext())
				{
					return this.result;
				}
				IRMConfiguration irmconfiguration;
				if (!this.ValidateLoadTenantsIRMConfiguration(out irmconfiguration))
				{
					return this.result;
				}
				if (!this.ValidateRmsOnlinePrerequisites(irmconfiguration))
				{
					return this.result;
				}
				RmsOnlineTpdImporter tpdImporter = new RmsOnlineTpdImporter(irmconfiguration.RMSOnlineKeySharingLocation, this.authenticationCertificateSubjectName);
				if (!this.ValidateRmsOnlineAuthenticationCertificate(tpdImporter))
				{
					return this.result;
				}
				TrustedDocDomain tpd;
				if (!this.ValidateTPDCanBeObtainedFromRMSOnline(tpdImporter, out tpd))
				{
					return this.result;
				}
				if (!this.ValidateTpdSuitableForImport(irmconfiguration, tpd, tpdImporter))
				{
					return this.result;
				}
			}
			finally
			{
				this.result.SetOverallResult();
				this.result.PrepareFinalOutput(this.organizationId);
			}
			return this.result;
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x0010D20C File Offset: 0x0010B40C
		private bool ValidateIsTenantContext()
		{
			this.result.SetTask(Strings.InfoCheckingOrganizationContext);
			if (OrganizationId.ForestWideOrgId == this.organizationId)
			{
				return this.result.SetFailureResult(Strings.ErrorNotRunningAsTenantAdmin, null, true);
			}
			return this.result.SetSuccessResult(Strings.InfoOrganizationContextChecked);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x0010D25E File Offset: 0x0010B45E
		private bool ValidateLoadTenantsIRMConfiguration(out IRMConfiguration irmConfiguration)
		{
			irmConfiguration = null;
			this.result.SetTask(Strings.InfoLoadIRMConfig);
			irmConfiguration = IRMConfiguration.Read(this.dataSession);
			return this.result.SetSuccessResult(Strings.InfoIRMConfigLoaded);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x0010D290 File Offset: 0x0010B490
		private bool ValidateRmsOnlinePrerequisites(IRMConfiguration irmConfiguration)
		{
			this.result.SetTask(Strings.InfoCheckingRmsOnlinePrerequisites);
			if (!RmsUtil.AreRmsOnlinePreRequisitesMet(irmConfiguration))
			{
				return this.result.SetFailureResult(Strings.ErrorRmsOnlinePrerequisites, null, true);
			}
			return this.result.SetSuccessResult(Strings.InfoRmsOnlinePrerequisitesChecked);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x0010D2D0 File Offset: 0x0010B4D0
		private bool ValidateRmsOnlineAuthenticationCertificate(RmsOnlineTpdImporter tpdImporter)
		{
			this.result.SetTask(Strings.InfoCheckingRmsOnlineAuthenticationCertificate);
			try
			{
				X509Certificate2 x509Certificate = tpdImporter.LoadAuthenticationCertificate();
				DateTime t = (DateTime)ExDateTime.UtcNow;
				if (x509Certificate.NotBefore > t)
				{
					return this.result.SetFailureResult(Strings.ErrorRmsOnlineAuthenticationCertificateNotYetValid, null, true);
				}
				if (x509Certificate.NotAfter < t)
				{
					return this.result.SetFailureResult(Strings.ErrorRmsOnlineAuthenticationCertificateExpired, null, true);
				}
				if (x509Certificate.NotAfter - this.certificateWarningPeriod < t)
				{
					return this.result.SetFailureResult(Strings.WarningRmsOnlineAuthenticationCertificateExpiryApproaching(x509Certificate.NotAfter), null, false);
				}
			}
			catch (ImportTpdException ex)
			{
				return this.result.SetFailureResult(Strings.ErrorRmsOnlineAuthenticationCertificateNotFound, ex, true);
			}
			return this.result.SetSuccessResult(Strings.InfoRmsOnlineAuthenticationCertificateChecked);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x0010D3B8 File Offset: 0x0010B5B8
		private bool ValidateTPDCanBeObtainedFromRMSOnline(RmsOnlineTpdImporter tpdImporter, out TrustedDocDomain tpd)
		{
			tpd = null;
			this.result.SetTask(Strings.InfoImportingTpdFromRmsOnline);
			bool flag;
			try
			{
				Guid externalDirectoryOrgIdThrowOnFailure = this.rmsOnlineGuidOverride;
				if (Guid.Empty == externalDirectoryOrgIdThrowOnFailure)
				{
					externalDirectoryOrgIdThrowOnFailure = RmsUtil.GetExternalDirectoryOrgIdThrowOnFailure(this.configurationSession, this.organizationId);
				}
				tpd = tpdImporter.Import(externalDirectoryOrgIdThrowOnFailure);
				if (tpd.m_astrRightsTemplates.Length == 0)
				{
					flag = this.result.SetSuccessResult(Strings.InfoImportingTpdFromRmsOnlineCheckedNoTemplates);
				}
				else
				{
					flag = this.result.SetSuccessResult(Strings.InfoImportingTpdFromRmsOnlineCheckedWithTemplates(RmsUtil.TemplateNamesFromTemplateArray(tpd.m_astrRightsTemplates)));
				}
			}
			catch (ImportTpdException ex)
			{
				flag = this.result.SetFailureResult(Strings.ErrorImportingTpdFromRmsOnline, ex, true);
			}
			return flag;
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x0010D46C File Offset: 0x0010B66C
		private bool ValidateTpdSuitableForImport(IRMConfiguration irmConfiguration, TrustedDocDomain tpd, RmsOnlineTpdImporter tpdImporter)
		{
			this.result.SetTask(Strings.InfoCheckingTpdFromRmsOnline);
			TpdValidator tpdValidator = new TpdValidator(irmConfiguration.InternalLicensingEnabled, tpdImporter.IntranetLicensingUrl, tpdImporter.ExtranetLicensingUrl, tpdImporter.IntranetCertificationUrl, tpdImporter.ExtranetCertificationUrl, true, true, false);
			try
			{
				object obj;
				tpdValidator.ValidateTpdSuitableForImport(tpd, Strings.RmsOnline, out obj, null, null, null, null, null, null);
			}
			catch (LocalizedException ex)
			{
				return this.result.SetFailureResult(Strings.ErrorTpdCheckingFailed, ex, true);
			}
			return this.result.SetSuccessResult(Strings.InfoTpdFromRmsOnlineChecked);
		}

		// Token: 0x0400298B RID: 10635
		private readonly TimeSpan certificateWarningPeriod = TimeSpan.FromDays(30.0);

		// Token: 0x0400298C RID: 10636
		private readonly IConfigurationSession configurationSession;

		// Token: 0x0400298D RID: 10637
		private readonly IConfigurationSession dataSession;

		// Token: 0x0400298E RID: 10638
		private readonly OrganizationId organizationId;

		// Token: 0x0400298F RID: 10639
		private readonly Guid rmsOnlineGuidOverride;

		// Token: 0x04002990 RID: 10640
		private readonly string authenticationCertificateSubjectName = RmsOnlineConstants.AuthenticationCertificateSubjectDistinguishedName;

		// Token: 0x04002991 RID: 10641
		private readonly IRMConfigurationValidationResult result = new IRMConfigurationValidationResult();
	}
}
