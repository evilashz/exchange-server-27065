using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x02000020 RID: 32
	public class DiagnosticsConfiguration
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00005D24 File Offset: 0x00003F24
		public DiagnosticsConfiguration()
		{
			string exePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Microsoft.Exchange.Diagnostics.Service.exe");
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(exePath);
			if (!configuration.HasFile)
			{
				throw new FileNotFoundException("No configuration file exists for service.");
			}
			this.serviceConfiguration = (configuration.GetSection("ServiceConfiguration") as ServiceConfiguration);
			this.jobSection = (configuration.GetSection("JobSection") as JobSection);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005D97 File Offset: 0x00003F97
		public ServiceConfiguration ServiceConfiguration
		{
			get
			{
				return this.serviceConfiguration;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005D9F File Offset: 0x00003F9F
		public JobSection JobSection
		{
			get
			{
				return this.jobSection;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public static bool GetIsDatacenterMode()
		{
			object obj = null;
			string value = null;
			if (CommonUtils.TryGetRegistryValue(DiagnosticsConfiguration.DiagnosticsRegistryKey, "LogFolderPath", null, out obj))
			{
				value = obj.ToString();
			}
			return !string.IsNullOrEmpty(value) || DiagnosticsConfiguration.GetIsInDogfoodDomain(".EXCHANGE.CORP.MICROSOFT.COM");
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005DE8 File Offset: 0x00003FE8
		private static bool GetIsInDogfoodDomain(string domainSuffix)
		{
			bool result = false;
			string hostName = Dns.GetHostName();
			if (!string.IsNullOrEmpty(hostName))
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				if (hostEntry != null && !string.IsNullOrEmpty(hostEntry.HostName))
				{
					result = hostEntry.HostName.EndsWith(domainSuffix, StringComparison.OrdinalIgnoreCase);
				}
			}
			return result;
		}

		// Token: 0x040002F8 RID: 760
		private const string DiagnosticsLogFolderRegistryValue = "LogFolderPath";

		// Token: 0x040002F9 RID: 761
		public static readonly string DiagnosticsRegistryKey = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\ExchangeServer\\v15\\Diagnostics";

		// Token: 0x040002FA RID: 762
		private readonly ServiceConfiguration serviceConfiguration;

		// Token: 0x040002FB RID: 763
		private readonly JobSection jobSection;
	}
}
