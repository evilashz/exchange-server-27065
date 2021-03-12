﻿using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200021E RID: 542
	public class FfoToExoInboundProxyProbe : SmtpConnectionProbe
	{
		// Token: 0x0600119E RID: 4510 RVA: 0x00033BCB File Offset: 0x00031DCB
		protected override void BeforeConnect()
		{
			if (!base.Broker.IsLocal())
			{
				throw new SmtpConnectionProbeException("FfoToExoInboundProxyProbe is a local-only probe and should not be used outside in");
			}
			this.SetCertificateCriteria();
			base.WorkDefinition.UseSsl = true;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00033BF8 File Offset: 0x00031DF8
		private void SetCertificateCriteria()
		{
			base.WorkDefinition.IgnoreCertificateNameMismatchPolicyError = true;
			this.Client.IgnoreCertificateNameMismatchPolicyError = true;
			base.WorkDefinition.ClientCertificate = new ClientCertificateCriteria();
			base.WorkDefinition.ClientCertificate.StoreLocation = StoreLocation.LocalMachine;
			base.WorkDefinition.ClientCertificate.StoreName = StoreName.My;
			base.WorkDefinition.ClientCertificate.FindType = X509FindType.FindBySubjectName;
			string findValue;
			if (base.Definition.Attributes.TryGetValue("CertificateName", out findValue))
			{
				base.WorkDefinition.ClientCertificate.FindValue = findValue;
			}
			if (string.IsNullOrEmpty(base.WorkDefinition.ClientCertificate.FindValue))
			{
				if (FfoToExoInboundProxyProbe.certificateFqdn == string.Empty)
				{
					string exeConfigFilename = Path.Combine(ExchangeSetupContext.BinPath, "MSExchangeFrontendTransport.exe.config");
					Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
					{
						ExeConfigFilename = exeConfigFilename
					}, ConfigurationUserLevel.None);
					KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings["FfoToExoInboundProxyProbeCertificateFqdn"];
					FfoToExoInboundProxyProbe.certificateFqdn = ((keyValueConfigurationElement != null) ? keyValueConfigurationElement.Value : null);
				}
				base.WorkDefinition.ClientCertificate.TransportCertificateFqdn = FfoToExoInboundProxyProbe.certificateFqdn;
				base.WorkDefinition.ClientCertificate.TransportWildcardMatchType = WildcardMatchType.OneLevel;
			}
		}

		// Token: 0x04000835 RID: 2101
		private static string certificateFqdn = string.Empty;
	}
}
