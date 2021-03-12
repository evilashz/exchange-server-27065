using System;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200014A RID: 330
	internal class InstantMessagingConfiguration
	{
		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002DE70 File Offset: 0x0002C070
		private InstantMessagingConfiguration(VdirConfiguration vdirConfiguration)
		{
			this.vdirConfiguration = vdirConfiguration;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002DE7F File Offset: 0x0002C07F
		public string ServerName
		{
			get
			{
				if (this.vdirConfiguration == null)
				{
					return BaseApplication.GetAppSetting<string>("IMServerName", string.Empty);
				}
				return this.vdirConfiguration.InstantMessagingServerName;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
		public string CertificateThumbprint
		{
			get
			{
				if (this.vdirConfiguration == null)
				{
					return BaseApplication.GetAppSetting<string>("IMCertificateThumbprint", string.Empty);
				}
				return this.vdirConfiguration.InstantMessagingCertificateThumbprint;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002DEC9 File Offset: 0x0002C0C9
		public int PortNumber
		{
			get
			{
				return BaseApplication.GetAppSetting<int>("IMPortNumber", -1);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002DED8 File Offset: 0x0002C0D8
		public static InstantMessagingConfiguration GetInstance(VdirConfiguration vdirConfiguration)
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseVdirConfigForInstantMessaging.Enabled)
			{
				return new InstantMessagingConfiguration(vdirConfiguration);
			}
			return new InstantMessagingConfiguration(null);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002DF14 File Offset: 0x0002C114
		public bool CheckConfiguration()
		{
			bool result = true;
			string arg = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.UseVdirConfigForInstantMessaging.Enabled ? "OWA Virtual Directory object" : "web.config";
			if (string.IsNullOrWhiteSpace(this.ServerName))
			{
				result = false;
				OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), string.Format("Instant Messaging Server name is null or empty on {0}.", arg), ResultSeverityLevel.Error);
			}
			if (string.IsNullOrWhiteSpace(this.CertificateThumbprint))
			{
				result = false;
				OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, Feature.InstantMessage.ToString(), string.Format("Instant Messaging Certificate Thumbprint is null or empty on {0}.", arg), ResultSeverityLevel.Error);
			}
			return result;
		}

		// Token: 0x040007A2 RID: 1954
		public const string ServerNameKey = "IMServerName";

		// Token: 0x040007A3 RID: 1955
		public const string PortNumberKey = "IMPortNumber";

		// Token: 0x040007A4 RID: 1956
		public const string CertificateThumbprintKey = "IMCertificateThumbprint";

		// Token: 0x040007A5 RID: 1957
		private VdirConfiguration vdirConfiguration;
	}
}
