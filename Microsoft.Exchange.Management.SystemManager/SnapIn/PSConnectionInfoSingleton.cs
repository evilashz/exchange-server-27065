using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Remoting;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x0200029A RID: 666
	internal class PSConnectionInfoSingleton
	{
		// Token: 0x06001C20 RID: 7200 RVA: 0x0007A338 File Offset: 0x00078538
		static PSConnectionInfoSingleton()
		{
			AppDomain.CurrentDomain.DomainUnload += PSConnectionInfoSingleton.AppDomainUnloadEventHandler;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x0007A388 File Offset: 0x00078588
		private static void AppDomainUnloadEventHandler(object sender, EventArgs args)
		{
			PSConnectionInfoSingleton.connection.Dispose();
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x0007A394 File Offset: 0x00078594
		public static void DisposeCurrentInstance()
		{
			if (PSConnectionInfoSingleton.instance != null)
			{
				PSConnectionInfoSingleton.instance = null;
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0007A3A4 File Offset: 0x000785A4
		protected PSConnectionInfoSingleton()
		{
			this.Type = OrganizationType.ToolOrEdge;
			this.LogonWithDefaultCredential = true;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0007A3F1 File Offset: 0x000785F1
		public static PSConnectionInfoSingleton GetInstance()
		{
			if (PSConnectionInfoSingleton.instance == null)
			{
				PSConnectionInfoSingleton.instance = new PSConnectionInfoSingleton();
			}
			return PSConnectionInfoSingleton.instance;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0007A409 File Offset: 0x00078609
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x0007A411 File Offset: 0x00078611
		public IReportProgress ReportProgress { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0007A41A File Offset: 0x0007861A
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x0007A422 File Offset: 0x00078622
		public bool Enabled { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x0007A42B File Offset: 0x0007862B
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x0007A433 File Offset: 0x00078633
		public string DisplayName { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0007A43C File Offset: 0x0007863C
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0007A444 File Offset: 0x00078644
		public Uri Uri { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0007A44D File Offset: 0x0007864D
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0007A455 File Offset: 0x00078655
		public bool LogonWithDefaultCredential { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x0007A45E File Offset: 0x0007865E
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x0007A466 File Offset: 0x00078666
		public string CredentialKey { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x0007A46F File Offset: 0x0007866F
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x0007A477 File Offset: 0x00078677
		public OrganizationType Type { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x0007A480 File Offset: 0x00078680
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x0007A488 File Offset: 0x00078688
		public PSCredential ProposedCredential { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x0007A494 File Offset: 0x00078694
		public string UserAccount
		{
			get
			{
				if (this.LogonWithDefaultCredential)
				{
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						return current.Name;
					}
				}
				if (this.DefaultConnectionInfo != null)
				{
					return this.DefaultConnectionInfo.Credentials.UserName;
				}
				if (this.ProposedCredential != null)
				{
					return this.ProposedCredential.UserName;
				}
				PSCredential pscredential = CredentialHelper.ReadCredential(this.CredentialKey);
				if (pscredential != null)
				{
					return pscredential.UserName;
				}
				return null;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x0007A518 File Offset: 0x00078718
		public string ServerName
		{
			get
			{
				if (this.Uri != null)
				{
					return this.Uri.Host;
				}
				return null;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x0007A535 File Offset: 0x00078735
		public Fqdn EcpServer
		{
			get
			{
				if (this.Type == OrganizationType.Cloud && this.Uri == new Uri(PSConnectionInfoSingleton.ExchangeOnlineUri))
				{
					return new Fqdn(PSConnectionInfoSingleton.ExchangeOnlineEcpServer);
				}
				return this.RemotePSServer;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x0007A568 File Offset: 0x00078768
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x0007A570 File Offset: 0x00078770
		public Fqdn RemotePSServer
		{
			get
			{
				return this.remotePSServer;
			}
			private set
			{
				if (this.remotePSServer != value)
				{
					this.remotePSServer = value;
					this.OnRemotePSServerChanged();
				}
			}
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0007A588 File Offset: 0x00078788
		private void OnRemotePSServerChanged()
		{
			EventHandler eventHandler = (EventHandler)this.eventHandlers[this.remotePSServerChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x06001C3B RID: 7227 RVA: 0x0007A5BB File Offset: 0x000787BB
		// (remove) Token: 0x06001C3C RID: 7228 RVA: 0x0007A5CF File Offset: 0x000787CF
		public event EventHandler RemotePSServerChanged
		{
			add
			{
				this.eventHandlers.AddHandler(this.remotePSServerChangedEvent, value);
			}
			remove
			{
				this.eventHandlers.RemoveHandler(this.remotePSServerChangedEvent, value);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x0007A5E3 File Offset: 0x000787E3
		// (set) Token: 0x06001C3E RID: 7230 RVA: 0x0007A5EB File Offset: 0x000787EB
		private MonadConnectionInfo DefaultConnectionInfo
		{
			get
			{
				return this.defaultConnectionInfo;
			}
			set
			{
				if (this.DefaultConnectionInfo != value)
				{
					this.defaultConnectionInfo = value;
					this.RemotePSServer = ((this.DefaultConnectionInfo == null) ? null : new Fqdn(this.DefaultConnectionInfo.ServerUri.Host));
				}
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0007A623 File Offset: 0x00078823
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0007A62B File Offset: 0x0007882B
		public SynchronizationContext UISyncContext { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0007A634 File Offset: 0x00078834
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x0007A63C File Offset: 0x0007883C
		public string SlotVersion { get; set; }

		// Token: 0x06001C43 RID: 7235 RVA: 0x0007A648 File Offset: 0x00078848
		public void UpdateRemotePSServer(Fqdn server)
		{
			if (this.Type != OrganizationType.LocalOnPremise && this.Type != OrganizationType.ToolOrEdge)
			{
				throw new NotSupportedException();
			}
			Uri uri = this.Uri;
			Uri remotePowerShellUri = PSConnectionInfoSingleton.GetRemotePowerShellUri(server);
			bool flag = true;
			try
			{
				this.Uri = remotePowerShellUri;
				this.DefaultConnectionInfo = this.DiscoverOrganizationConnectionInfo();
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.Uri = uri;
				}
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0007A6B0 File Offset: 0x000788B0
		public MonadConnectionInfo GetMonadConnectionInfo(ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel)
		{
			MonadConnectionInfo monadConnectionInfo = this.GenerateMonadConnectionInfo();
			if (serializationLevel != ExchangeRunspaceConfigurationSettings.SerializationLevel.Full && monadConnectionInfo != null)
			{
				monadConnectionInfo = new MonadConnectionInfo(PSConnectionInfoSingleton.ExtractValidUri(monadConnectionInfo), monadConnectionInfo.Credentials, monadConnectionInfo.ShellUri, monadConnectionInfo.FileTypesXml, monadConnectionInfo.AuthenticationMechanism, serializationLevel, monadConnectionInfo.ClientApplication, monadConnectionInfo.ClientVersion, monadConnectionInfo.MaximumConnectionRedirectionCount, monadConnectionInfo.SkipServerCertificateCheck);
			}
			return monadConnectionInfo;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x0007A709 File Offset: 0x00078909
		private static Uri ExtractValidUri(MonadConnectionInfo connectionInfo)
		{
			if (connectionInfo.ServerUri.ToString().StartsWith(PSConnectionInfoSingleton.GetExchangeOnlineUri().ToString()))
			{
				return PSConnectionInfoSingleton.GetExchangeOnlineUri();
			}
			return new Uri(connectionInfo.ServerUri.GetLeftPart(UriPartial.Path));
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0007A73E File Offset: 0x0007893E
		public MonadConnectionInfo GetMonadConnectionInfo()
		{
			return this.GetMonadConnectionInfo(ExchangeRunspaceConfigurationSettings.SerializationLevel.Full);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0007A748 File Offset: 0x00078948
		private MonadConnectionInfo GenerateMonadConnectionInfo()
		{
			if (this.DefaultConnectionInfo != null)
			{
				return this.DefaultConnectionInfo;
			}
			MonadConnectionInfo result;
			lock (this.syncObject)
			{
				if (this.DefaultConnectionInfo == null)
				{
					this.DefaultConnectionInfo = this.DiscoverOrganizationConnectionInfo();
				}
				result = this.DefaultConnectionInfo;
			}
			return result;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x0007A7B0 File Offset: 0x000789B0
		private void ReportConnectToSpecifiedServerProgress(int percent)
		{
			if (this.ReportProgress != null)
			{
				string host = this.Uri.Host;
				this.ReportProgress.ReportProgress(percent, 100, Strings.ProgressReportConnectToSpecifiedServer(host), Strings.ProgressReportConnectToSpecifiedServerErrorText(host));
			}
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0007A7F5 File Offset: 0x000789F5
		private void ReportDiscoverServerProgress(int percent)
		{
			if (this.ReportProgress != null)
			{
				this.ReportProgress.ReportProgress(percent, 100, Strings.ProgressReportDiscoverExchangeServer, Strings.ProgressReportDiscoverExchangeServerErrorText);
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0007A824 File Offset: 0x00078A24
		private void ReportConnectToServerProgress(int percent)
		{
			if (this.ReportProgress != null)
			{
				string host = this.Uri.Host;
				this.ReportProgress.ReportProgress(percent, 100, Strings.ProgressReportConnectToServer(host), Strings.ProgressReportConnectToServerErrorText(host));
			}
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x0007A86C File Offset: 0x00078A6C
		private MonadConnectionInfo DiscoverOrganizationConnectionInfo()
		{
			if (!WinformsHelper.IsRemoteEnabled())
			{
				return null;
			}
			if (this.Type == OrganizationType.RemoteOnPremise || this.Type == OrganizationType.Cloud)
			{
				this.ReportConnectToSpecifiedServerProgress(0);
				return this.DiscoverConnectionInfo();
			}
			if (this.Uri != null)
			{
				try
				{
					this.ReportConnectToSpecifiedServerProgress(0);
					return this.DiscoverConnectionInfo();
				}
				catch (PSRemotingDataStructureException)
				{
				}
				catch (PSRemotingTransportException)
				{
				}
			}
			this.ReportDiscoverServerProgress(15);
			this.Uri = PSConnectionInfoSingleton.GetRemotePowerShellUri(PSConnectionInfoSingleton.DiscoverExchangeServer());
			this.ReportConnectToServerProgress(40);
			return this.DiscoverConnectionInfo();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0007A90C File Offset: 0x00078B0C
		private MonadConnectionInfo DiscoverConnectionInfo()
		{
			SupportedVersionList supportedVersionList = null;
			MonadConnectionInfo result = new ConnectionRetryDiscoverer(this).DiscoverConnectionInfo(out supportedVersionList, (OrganizationType.LocalOnPremise == this.Type || OrganizationType.Cloud == this.Type || OrganizationType.RemoteOnPremise == this.Type) ? this.SlotVersion : string.Empty);
			if (this.Type == OrganizationType.Cloud)
			{
				if (supportedVersionList.Count == 0)
				{
					throw new SupportedVersionListFormatException(Strings.AtLeastOneVersionMustBeSupported);
				}
				if (supportedVersionList.IsSupported(this.SlotVersion))
				{
					throw new VersionMismatchException(Strings.MicrosoftExchangeOnPremise, supportedVersionList);
				}
			}
			else if (this.Type == OrganizationType.RemoteOnPremise)
			{
				if (supportedVersionList == null || supportedVersionList.Count == 0)
				{
					throw new InvalidOperationException(Strings.SP1ConnectToRTMServerError);
				}
				if (!supportedVersionList.IsSupported(this.SlotVersion))
				{
					throw new InvalidOperationException(Strings.IncampatibleVersionConnectionError(supportedVersionList.GetLatestVersion(), this.SlotVersion));
				}
			}
			return result;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0007A9D6 File Offset: 0x00078BD6
		private Fqdn GetAutoDiscoveredServer()
		{
			if (string.IsNullOrEmpty(this.autoDiscoveredServer))
			{
				this.autoDiscoveredServer = PSConnectionInfoSingleton.DiscoverExchangeServer();
			}
			return this.autoDiscoveredServer;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x0007A9FC File Offset: 0x00078BFC
		public MonadConnectionInfo GetMonadConnectionInfo(IUIService uiService, OrganizationSetting forestInfo)
		{
			lock (this.forestConnectionInfos)
			{
				if (this.forestConnectionInfos.ContainsKey(forestInfo.Key))
				{
					return this.forestConnectionInfos[forestInfo.Key];
				}
			}
			MonadConnectionInfo monadConnectionInfo = this.DiscoverForestConnectionInfo(uiService, forestInfo);
			MonadConnectionInfo result;
			lock (this.forestConnectionInfos)
			{
				this.forestConnectionInfos[forestInfo.Key] = monadConnectionInfo;
				result = monadConnectionInfo;
			}
			return result;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0007AAA8 File Offset: 0x00078CA8
		private MonadConnectionInfo DiscoverForestConnectionInfo(IUIService uiService, OrganizationSetting forestInfo)
		{
			SupportedVersionList supportedVersionList = null;
			switch (forestInfo.Type)
			{
			case OrganizationType.LocalOnPremise:
				return new ConnectionRetryDiscoverer(uiService, OrganizationType.LocalOnPremise, Strings.MicrosoftExchangeOnPremise, PSConnectionInfoSingleton.GetRemotePowerShellUri(this.GetAutoDiscoveredServer()), true).DiscoverConnectionInfo(out supportedVersionList, string.Empty);
			case OrganizationType.RemoteOnPremise:
			case OrganizationType.Cloud:
				return new ConnectionRetryDiscoverer(forestInfo, uiService).DiscoverConnectionInfo(out supportedVersionList, string.Empty);
			default:
				return null;
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0007AB13 File Offset: 0x00078D13
		public string GetConnectionStringForScript()
		{
			if (!WinformsHelper.IsRemoteEnabled())
			{
				return "pooled=false";
			}
			return "timeout=30";
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0007AB28 File Offset: 0x00078D28
		private static Fqdn DiscoverExchangeServer()
		{
			string fqdn = null;
			using (new OpenConnection(PSConnectionInfoSingleton.connection))
			{
				string str = Path.Combine(ConfigurationContext.Setup.BinPath, "ConnectFunctions.ps1");
				using (MonadCommand monadCommand = new LoggableMonadCommand(". '" + str + "'", PSConnectionInfoSingleton.connection))
				{
					monadCommand.CommandType = CommandType.Text;
					monadCommand.ExecuteNonQuery();
				}
				using (MonadCommand monadCommand2 = new LoggableMonadCommand("Discover-ExchangeServer", PSConnectionInfoSingleton.connection))
				{
					monadCommand2.CommandType = CommandType.StoredProcedure;
					monadCommand2.Parameters.Add(new MonadParameter("UseWIA", true));
					monadCommand2.Parameters.Add(new MonadParameter("SuppressError", true));
					monadCommand2.Parameters.Add(new MonadParameter("CurrentVersion", ServerVersion.InstalledVersion));
					object[] array = monadCommand2.Execute();
					if (array == null || array.Length <= 0)
					{
						throw new CmdletInvocationException(Strings.FailedToAutoDiscoverExchangeServer);
					}
					fqdn = (array[0] as string);
				}
			}
			return new Fqdn(fqdn);
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x0007AC70 File Offset: 0x00078E70
		public static Uri GetRemotePowerShellUri(Fqdn server)
		{
			if (server == null)
			{
				return null;
			}
			return new Uri(string.Format(PSConnectionInfoSingleton.RemotePowerShellUrlFormat, server.ToString()));
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0007AC8C File Offset: 0x00078E8C
		public static Uri GetExchangeOnlineUri()
		{
			if (PSConnectionInfoSingleton.exchangeOnlineUri != null)
			{
				return PSConnectionInfoSingleton.exchangeOnlineUri;
			}
			string name = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools";
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
				{
					if (registryKey != null)
					{
						string text = registryKey.GetValue("EMC.ExchangeOnlineUri") as string;
						if (!string.IsNullOrEmpty(text))
						{
							PSConnectionInfoSingleton.exchangeOnlineUri = new Uri(text);
						}
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			if (PSConnectionInfoSingleton.exchangeOnlineUri == null)
			{
				PSConnectionInfoSingleton.exchangeOnlineUri = new Uri(PSConnectionInfoSingleton.ExchangeOnlineUri);
			}
			return PSConnectionInfoSingleton.exchangeOnlineUri;
		}

		// Token: 0x04000A7A RID: 2682
		private static readonly string RemotePowerShellUrlFormat = "http://{0}/PowerShell";

		// Token: 0x04000A7B RID: 2683
		private static readonly string ExchangeOnlineUri = "https://ps.outlook.com/PowerShell/PowerShell.htm";

		// Token: 0x04000A7C RID: 2684
		private static readonly string ExchangeOnlineEcpServer = "www.outlook.com";

		// Token: 0x04000A7D RID: 2685
		private static PSConnectionInfoSingleton instance;

		// Token: 0x04000A7E RID: 2686
		private object syncObject = new object();

		// Token: 0x04000A7F RID: 2687
		private EventHandlerList eventHandlers = new EventHandlerList();

		// Token: 0x04000A80 RID: 2688
		private static MonadConnection connection = new MonadConnection("pooled=false");

		// Token: 0x04000A81 RID: 2689
		private Fqdn remotePSServer;

		// Token: 0x04000A82 RID: 2690
		private object remotePSServerChangedEvent = new object();

		// Token: 0x04000A83 RID: 2691
		private MonadConnectionInfo defaultConnectionInfo;

		// Token: 0x04000A84 RID: 2692
		private Fqdn autoDiscoveredServer;

		// Token: 0x04000A85 RID: 2693
		private IDictionary<string, MonadConnectionInfo> forestConnectionInfos = new Dictionary<string, MonadConnectionInfo>();

		// Token: 0x04000A86 RID: 2694
		private static Uri exchangeOnlineUri;
	}
}
