using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x0200047C RID: 1148
	public abstract class ManagementEndpointBase : Task
	{
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0009EACF File Offset: 0x0009CCCF
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x0009EAD7 File Offset: 0x0009CCD7
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public GlobalDirectoryServiceType GlobalDirectoryService { get; set; }

		// Token: 0x0600285B RID: 10331 RVA: 0x0009EAE0 File Offset: 0x0009CCE0
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || DataAccessHelper.IsDataAccessKnownException(exception) || exception is RedirectionEntryManagerException || exception is MonadDataAdapterInvocationException;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0009EB08 File Offset: 0x0009CD08
		private IGlobalDirectorySession GlobalDirectorySession(string redirectFormatForMServ = null)
		{
			switch (this.GlobalDirectoryService)
			{
			case GlobalDirectoryServiceType.Default:
				return DirectorySessionFactory.GetGlobalSession(redirectFormatForMServ);
			case GlobalDirectoryServiceType.MServ:
				return new MServDirectorySession(redirectFormatForMServ);
			case GlobalDirectoryServiceType.Gls:
				return new GlsDirectorySession();
			default:
				throw new ArgumentException("GlobalDirectoryService");
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0009EB4E File Offset: 0x0009CD4E
		protected virtual string GetRedirectionTemplate()
		{
			return null;
		}

		// Token: 0x0600285E RID: 10334
		internal abstract void ProcessRedirectionEntry(IGlobalDirectorySession session);

		// Token: 0x0600285F RID: 10335 RVA: 0x0009EB54 File Offset: 0x0009CD54
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (!ManagementEndpointBase.IsGlobalDirectoryConfigured())
			{
				base.WriteWarning("Management endpoint code skipped in test environment.");
				return;
			}
			IGlobalDirectorySession session = this.GlobalDirectorySession(this.GetRedirectionTemplate());
			this.ProcessRedirectionEntry(session);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0009EB90 File Offset: 0x0009CD90
		public static bool IsGlobalDirectoryConfigured()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 111, "IsGlobalDirectoryConfigured", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ManagementEndpoint\\ManagementEndpointBase.cs");
			ADSite localSite = topologyConfigurationSession.GetLocalSite();
			return localSite.PartnerId != -1 || !localSite.DistinguishedName.EndsWith("DC=extest,DC=microsoft,DC=com");
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x0009EBDF File Offset: 0x0009CDDF
		internal static string GetSmtpNextHopDomain()
		{
			if (string.IsNullOrEmpty(ManagementEndpointBase.smtpNextHopFormat))
			{
				ManagementEndpointBase.smtpNextHopFormat = RegistrySettings.ExchangeServerCurrentVersion.SmtpNextHopDomainFormat;
			}
			return string.Format(ManagementEndpointBase.smtpNextHopFormat, ManagementEndpointBase.GetLocalSite().PartnerId);
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x0009EC10 File Offset: 0x0009CE10
		private static ADSite GetLocalSite()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 142, "GetLocalSite", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ManagementEndpoint\\ManagementEndpointBase.cs");
			return topologyConfigurationSession.GetLocalSite();
		}

		// Token: 0x04001DF8 RID: 7672
		private static string smtpNextHopFormat;
	}
}
