using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200023B RID: 571
	internal class ProxyProbeCommon
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00038570 File Offset: 0x00036770
		internal static bool TryGetSmtpServer(out string smtpServer)
		{
			string exeConfigFilename = Path.Combine(ExchangeSetupContext.BinPath, "EdgeTransport.exe.config");
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = exeConfigFilename
			}, ConfigurationUserLevel.None);
			string value = configuration.AppSettings.Settings["OutboundFrontendServers"].Value;
			List<RoutingHost> configListFromValue = TransportAppConfig.GetConfigListFromValue<RoutingHost>(value, ',', new TransportAppConfig.TryParse<RoutingHost>(RoutingHost.TryParse));
			if (configListFromValue.Count <= 0)
			{
				smtpServer = null;
				return false;
			}
			smtpServer = configListFromValue[0].ToString();
			return true;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000385F4 File Offset: 0x000367F4
		internal static void ApplyCertificateCriteria(SmtpConnectionProbeWorkDefinition workDefinition, Dictionary<string, string> attributes)
		{
			workDefinition.IgnoreCertificateNameMismatchPolicyError = true;
			workDefinition.ClientCertificate = new ClientCertificateCriteria();
			workDefinition.ClientCertificate.StoreLocation = StoreLocation.LocalMachine;
			workDefinition.ClientCertificate.StoreName = StoreName.My;
			workDefinition.ClientCertificate.FindType = X509FindType.FindBySubjectName;
			string exeConfigFilename = Path.Combine(ExchangeSetupContext.BinPath, "EdgeTransport.exe.config");
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = exeConfigFilename
			}, ConfigurationUserLevel.None);
			KeyValueConfigurationElement keyValueConfigurationElement = configuration.AppSettings.Settings["OutboundProxyExternalCertificateSubject"];
			workDefinition.ClientCertificate.TransportCertificateFqdn = ((keyValueConfigurationElement != null) ? keyValueConfigurationElement.Value : null);
			string findValue;
			if (string.IsNullOrEmpty(workDefinition.ClientCertificate.TransportCertificateFqdn) && attributes.TryGetValue("CertificateName", out findValue))
			{
				workDefinition.ClientCertificate.FindValue = findValue;
			}
			if (string.IsNullOrEmpty(workDefinition.ClientCertificate.FindValue) && string.IsNullOrEmpty(workDefinition.ClientCertificate.TransportCertificateFqdn))
			{
				workDefinition.ClientCertificate.FindValue = Utils.TryGetStringValueFromRegistry("SOFTWARE\\Microsoft\\ExchangeLabs", "ExoDatacenterCertificateName", "*.outlook.com");
			}
			if (string.IsNullOrEmpty(workDefinition.ClientCertificate.FindValue) && string.IsNullOrEmpty(workDefinition.ClientCertificate.TransportCertificateFqdn))
			{
				throw new SmtpConnectionProbeException("Unable to assign required proxy certificate lookup value.");
			}
		}

		// Token: 0x040008F5 RID: 2293
		private const string ExchangeLabsRegKey = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x040008F6 RID: 2294
		private const string DefaultCertificateNameRegFieldName = "ExoDatacenterCertificateName";
	}
}
