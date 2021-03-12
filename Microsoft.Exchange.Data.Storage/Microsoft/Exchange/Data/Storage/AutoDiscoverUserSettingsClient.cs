using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200035D RID: 861
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutoDiscoverUserSettingsClient : DisposableObject
	{
		// Token: 0x06002664 RID: 9828 RVA: 0x0009A1F4 File Offset: 0x000983F4
		public static AutoDiscoverUserSettingsClient CreateInstance(ITopologyConfigurationSession topologyConfigurationSession, FedOrgCredentials credentials, SmtpAddress identity, Uri autoDiscoveryEndpoint, string[] requestedSettings)
		{
			Util.ThrowOnNullArgument(credentials, "credentials");
			RequestedToken token;
			try
			{
				token = credentials.GetToken();
			}
			catch (WSTrustException ex)
			{
				string text = identity.ToString();
				string text2 = ex.ToString();
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_AutoDiscoverFailedToAquireSecurityToken, text, new object[]
				{
					text,
					text2
				});
				ExTraceGlobals.XtcTracer.TraceError<string, string>(0L, "AutoDiscover request failed for {0}, failed to aquire security token. Exception: {1}.", text, text2);
				throw new AutoDAccessException(ServerStrings.AutoDFailedToGetToken, ex);
			}
			return new AutoDiscoverUserSettingsClient(topologyConfigurationSession, SoapHttpClientAuthenticator.Create(token), EwsWsSecurityUrl.Fix(autoDiscoveryEndpoint), identity, requestedSettings);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0009A294 File Offset: 0x00098494
		public static AutoDiscoverUserSettingsClient CreateInstance(ITopologyConfigurationSession topologyConfigurationSession, NetworkCredential credentials, SmtpAddress identity, string[] requestedSettings)
		{
			return new AutoDiscoverUserSettingsClient(topologyConfigurationSession, SoapHttpClientAuthenticator.CreateForSoap(credentials), null, identity, requestedSettings);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0009A2A8 File Offset: 0x000984A8
		private AutoDiscoverUserSettingsClient(ITopologyConfigurationSession topologyConfigurationSession, SoapHttpClientAuthenticator authentificator, Uri autoDiscoveryEndpoint, SmtpAddress identity, string[] requestedSettings)
		{
			Util.ThrowOnNullArgument(topologyConfigurationSession, "topologyConfigurationSession");
			Util.ThrowOnNullArgument(identity, "identity");
			Util.ThrowOnNullArgument(requestedSettings, "requestedSettings");
			if (requestedSettings.Length == 0)
			{
				throw new ArgumentException("Requested settings array must be not empty.");
			}
			foreach (string value in requestedSettings)
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot use null or empty string as a requested setting.");
				}
			}
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.requestedSettings = requestedSettings;
				this.autoDiscoveryUri = autoDiscoveryEndpoint;
				this.identity = identity;
				this.client = new AutodiscoverClient();
				this.client.RequestedServerVersion = DefaultBinding_Autodiscover.Exchange2010RequestedServerVersion;
				this.client.UserAgent = "ExchangeMiddleTierStorage";
				this.client.Authenticator = authentificator;
				string[] autodiscoverTrustedHosters = topologyConfigurationSession.GetAutodiscoverTrustedHosters();
				if (autodiscoverTrustedHosters != null)
				{
					this.client.AllowedHostnames.AddRange(autodiscoverTrustedHosters);
				}
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					this.client.Proxy = new WebProxy(localServer.InternetWebProxy);
				}
				else
				{
					this.client.Proxy = new WebProxy();
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x0009A3FC File Offset: 0x000985FC
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x0009A409 File Offset: 0x00098609
		public string AnchorMailbox
		{
			get
			{
				return this.client.AnchorMailbox;
			}
			set
			{
				this.client.AnchorMailbox = value;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x0009A418 File Offset: 0x00098618
		public UserSettings Discover()
		{
			this.CheckDisposed(null);
			SingleGetUserSettings singleGetUserSettings = new SingleGetUserSettings(this.requestedSettings);
			UserSettings userSettings;
			try
			{
				userSettings = singleGetUserSettings.Discover(this.client, this.identity.ToString(), this.autoDiscoveryUri);
			}
			catch (GetUserSettingsException ex)
			{
				string text = this.identity.ToString();
				string arg = ex.ToString();
				StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_AutoDiscoverFailed, text, new object[]
				{
					text,
					ex.ToString()
				});
				ExTraceGlobals.XtcTracer.TraceError<string, string>(0L, "The Autodiscover request failed for {0}. Error: {1}.", text, arg);
				throw new AutoDAccessException(ServerStrings.AutoDRequestFailed, ex);
			}
			bool flag = false;
			foreach (string text2 in this.requestedSettings)
			{
				if (userSettings.IsSettingError(text2))
				{
					string text3 = this.identity.ToString();
					StorageGlobals.EventLogger.LogEvent(StorageEventLogConstants.Tuple_AutoDiscoverFailedForSetting, text3, new object[]
					{
						text3,
						text2
					});
					ExTraceGlobals.XtcTracer.TraceError<string, string>(0L, "The Autodiscover request failed for {0}. Setting: {1}.", text3, text2);
					flag = true;
				}
			}
			if (flag)
			{
				throw new AutoDAccessException(ServerStrings.AutoDRequestFailed);
			}
			return userSettings;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x0009A568 File Offset: 0x00098768
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.client != null)
			{
				this.client.Dispose();
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x0009A587 File Offset: 0x00098787
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AutoDiscoverUserSettingsClient>(this);
		}

		// Token: 0x040016F5 RID: 5877
		private const string ComponentId = "ExchangeMiddleTierStorage";

		// Token: 0x040016F6 RID: 5878
		private readonly string[] requestedSettings;

		// Token: 0x040016F7 RID: 5879
		private AutodiscoverClient client;

		// Token: 0x040016F8 RID: 5880
		private SmtpAddress identity;

		// Token: 0x040016F9 RID: 5881
		private Uri autoDiscoveryUri;
	}
}
