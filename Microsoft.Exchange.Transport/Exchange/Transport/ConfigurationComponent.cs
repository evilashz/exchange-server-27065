using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002D0 RID: 720
	internal class ConfigurationComponent : ITransportComponent, ITransportConfiguration, IDiagnosable
	{
		// Token: 0x06001FE2 RID: 8162 RVA: 0x0007A198 File Offset: 0x00078398
		public ConfigurationComponent(ProcessTransportRole processTransportRole) : this()
		{
			this.isFrontendTransportRoleProcess = (processTransportRole == ProcessTransportRole.FrontEnd);
			this.isMailboxTransportSubmissionRoleProcess = (processTransportRole == ProcessTransportRole.MailboxSubmission);
			this.isMailboxTransportDeliveryRoleProcess = (processTransportRole == ProcessTransportRole.MailboxDelivery);
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0007A1EC File Offset: 0x000783EC
		public ConfigurationComponent()
		{
			this.parallelComponent = new ParallelTransportComponent("Transport Configuration Component");
			this.CreateAndRegister<RemoteDomainTable, RemoteDomainTable.Builder>(null, true, out this.remoteDomainTable);
			this.CreateAndRegister<AcceptedDomainTable, AcceptedDomainTable.Builder>(delegate(AcceptedDomainTable.Builder builder)
			{
				builder.IsBridgehead = this.localServer.Cache.IsBridgehead;
			}, true, out this.firstOrgAcceptedDomainTable);
			this.CreateAndRegister<X400AuthoritativeDomainTable, X400AuthoritativeDomainTable.Builder>(null, true, out this.x400DomainTable);
			this.CreateAndRegister<ReceiveConnectorConfiguration, ReceiveConnectorConfiguration.Builder>(delegate(ReceiveConnectorConfiguration.Builder builder)
			{
				builder.Server = this.localServer.Cache;
			}, true, out this.receiveConnectors);
			this.CreateAndRegister<TransportServerConfiguration, TransportServerConfiguration.Builder>(null, false, out this.localServer);
			this.CreateAndRegister<FrontendTransportServer, TransportServerConfiguration.FrontendBuilder>(null, false, out this.frontendServer);
			this.CreateAndRegister<MailboxTransportServer, TransportServerConfiguration.MailboxBuilder>(null, false, out this.mailboxServer);
			this.CreateAndRegister<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder>(null, true, out this.transportSettings);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0007A2A4 File Offset: 0x000784A4
		private static PerTenantPerimeterSettings CreateDefaultTenantPerimeterSettings()
		{
			bool routeOutboundViaEhfEnabled = false;
			bool routeOutboundViaFfoFrontendEnabled = false;
			RoutingDomain empty = RoutingDomain.Empty;
			return new PerTenantPerimeterSettings(routeOutboundViaEhfEnabled, routeOutboundViaFfoFrontendEnabled, RoutingDomain.Empty, empty);
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06001FE5 RID: 8165 RVA: 0x0007A2C4 File Offset: 0x000784C4
		// (remove) Token: 0x06001FE6 RID: 8166 RVA: 0x0007A2D2 File Offset: 0x000784D2
		public event ConfigurationUpdateHandler<AcceptedDomainTable> FirstOrgAcceptedDomainTableChanged
		{
			add
			{
				this.firstOrgAcceptedDomainTable.Changed += value;
			}
			remove
			{
				this.firstOrgAcceptedDomainTable.Changed -= value;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06001FE7 RID: 8167 RVA: 0x0007A2E0 File Offset: 0x000784E0
		// (remove) Token: 0x06001FE8 RID: 8168 RVA: 0x0007A2EE File Offset: 0x000784EE
		public event ConfigurationUpdateHandler<ReceiveConnectorConfiguration> LocalReceiveConnectorsChanged
		{
			add
			{
				this.receiveConnectors.Changed += value;
			}
			remove
			{
				this.receiveConnectors.Changed -= value;
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001FE9 RID: 8169 RVA: 0x0007A2FC File Offset: 0x000784FC
		// (remove) Token: 0x06001FEA RID: 8170 RVA: 0x0007A334 File Offset: 0x00078534
		public event ConfigurationUpdateHandler<TransportServerConfiguration> LocalServerChanged;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001FEB RID: 8171 RVA: 0x0007A369 File Offset: 0x00078569
		// (remove) Token: 0x06001FEC RID: 8172 RVA: 0x0007A377 File Offset: 0x00078577
		public event ConfigurationUpdateHandler<TransportSettingsConfiguration> TransportSettingsChanged
		{
			add
			{
				this.transportSettings.Changed += value;
			}
			remove
			{
				this.transportSettings.Changed -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001FED RID: 8173 RVA: 0x0007A385 File Offset: 0x00078585
		// (remove) Token: 0x06001FEE RID: 8174 RVA: 0x0007A393 File Offset: 0x00078593
		public event ConfigurationUpdateHandler<RemoteDomainTable> RemoteDomainTableChanged
		{
			add
			{
				this.remoteDomainTable.Changed += value;
			}
			remove
			{
				this.remoteDomainTable.Changed -= value;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x0007A3A1 File Offset: 0x000785A1
		public AcceptedDomainTable FirstOrgAcceptedDomainTable
		{
			get
			{
				return this.firstOrgAcceptedDomainTable.Cache;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x0007A3AE File Offset: 0x000785AE
		public RemoteDomainTable RemoteDomainTable
		{
			get
			{
				return this.remoteDomainTable.Cache;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0007A3BB File Offset: 0x000785BB
		public X400AuthoritativeDomainTable X400AuthoritativeDomainTable
		{
			get
			{
				return this.x400DomainTable.Cache;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x0007A3C8 File Offset: 0x000785C8
		public ReceiveConnectorConfiguration LocalReceiveConnectors
		{
			get
			{
				return this.receiveConnectors.Cache;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x0007A3D5 File Offset: 0x000785D5
		public TransportServerConfiguration LocalServer
		{
			get
			{
				return this.reconciledServer;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x0007A3DD File Offset: 0x000785DD
		public TransportSettingsConfiguration TransportSettings
		{
			get
			{
				return this.transportSettings.Cache;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x0007A3EA File Offset: 0x000785EA
		public MicrosoftExchangeRecipientPerTenantSettings MicrosoftExchangeRecipient
		{
			get
			{
				return this.GetMicrosoftExchangeRecipient(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001FF6 RID: 8182 RVA: 0x0007A3F7 File Offset: 0x000785F7
		public TransportAppConfig AppConfig
		{
			get
			{
				return Components.TransportAppConfig;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0007A3FE File Offset: 0x000785FE
		public ProcessTransportRole ProcessTransportRole
		{
			get
			{
				if (!this.processTransportRoleComputed)
				{
					throw new InvalidOperationException("ProcessTransportRole has not been computed yet.");
				}
				return this.processTransportRole;
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0007A419 File Offset: 0x00078619
		public static bool IsFrontEndTransportProcess(ITransportConfiguration transportConfiguration)
		{
			return transportConfiguration.ProcessTransportRole == ProcessTransportRole.FrontEnd;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0007A424 File Offset: 0x00078624
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "TransportConfiguration";
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0007A42C File Offset: 0x0007862C
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			string diagnosticComponentName = ((IDiagnosable)this).GetDiagnosticComponentName();
			XElement xelement = new XElement(diagnosticComponentName);
			bool flag = parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1 || parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1 || flag;
			bool flag3 = parameters.Argument.IndexOf("AppConfig", StringComparison.OrdinalIgnoreCase) != -1 || flag;
			if (flag2)
			{
				xelement.Add(this.receiveConnectors.GetDiagnosticInfo(parameters));
			}
			if (flag3)
			{
				xelement.Add(this.AppConfig.GetDiagnosticInfo());
			}
			if (!flag2 && !flag3)
			{
				xelement.Add(new XElement("help", "Supported arguments: appconfig, config, basic, verbose, help."));
			}
			return xelement;
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0007A500 File Offset: 0x00078700
		public void Load()
		{
			this.localServer.Load();
			this.ComputeProcessTransportRole();
			if (this.isFrontendTransportRoleProcess)
			{
				this.frontendServer.Load();
			}
			else if (this.isMailboxTransportSubmissionRoleProcess || this.isMailboxTransportDeliveryRoleProcess)
			{
				this.mailboxServer.Load();
			}
			this.ReconcileTransportServerObjects(false);
			this.localServer.Changed += this.LocalServer_Changed;
			if (this.isFrontendTransportRoleProcess)
			{
				this.frontendServer.Changed += this.FrontendServer_Changed;
			}
			else if (this.isMailboxTransportSubmissionRoleProcess || this.isMailboxTransportDeliveryRoleProcess)
			{
				this.mailboxServer.Changed += this.MailboxServer_Changed;
			}
			this.parallelComponent.Load();
			this.InitializeCaches();
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x0007A5C8 File Offset: 0x000787C8
		public void Unload()
		{
			this.parallelComponent.Unload();
			if (this.isFrontendTransportRoleProcess)
			{
				this.frontendServer.Changed -= this.FrontendServer_Changed;
				this.frontendServer.Unload();
			}
			else if (this.isMailboxTransportSubmissionRoleProcess || this.isMailboxTransportDeliveryRoleProcess)
			{
				this.mailboxServer.Changed -= this.MailboxServer_Changed;
				this.mailboxServer.Unload();
			}
			this.localServer.Changed -= this.LocalServer_Changed;
			this.localServer.Unload();
			this.ClearCaches();
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0007A666 File Offset: 0x00078866
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0007A66C File Offset: 0x0007886C
		public void ClearCaches()
		{
			this.transportSettingsCache.Clear();
			this.perimeterSettingsCache.Clear();
			this.remoteDomainsCache.Clear();
			this.microsoftExchangeRecipientCache.Clear();
			this.journalingRulesCache.Clear();
			this.reconciliationAccountsCache.Clear();
			this.acceptedDomainsCache.Clear();
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0007A6C6 File Offset: 0x000788C6
		public void ReloadFirstOrgAcceptedDomainTable()
		{
			this.firstOrgAcceptedDomainTable.Reload(null, EventArgs.Empty);
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0007A6D9 File Offset: 0x000788D9
		public bool TryGetTransportSettings(OrganizationId orgId, out PerTenantTransportSettings perTenantTransportSettings)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				perTenantTransportSettings = this.TransportSettings.PerTenantTransportSettings;
				return true;
			}
			return this.transportSettingsCache.TryGetValue(orgId, out perTenantTransportSettings);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0007A704 File Offset: 0x00078904
		public PerTenantTransportSettings GetTransportSettings(OrganizationId orgId)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return this.TransportSettings.PerTenantTransportSettings;
			}
			return this.transportSettingsCache.GetValue(orgId);
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x0007A72B File Offset: 0x0007892B
		public bool TryGetPerimeterSettings(OrganizationId orgId, out PerTenantPerimeterSettings perTenantPerimeterSettings)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				perTenantPerimeterSettings = ConfigurationComponent.CreateDefaultTenantPerimeterSettings();
				return true;
			}
			return this.perimeterSettingsCache.TryGetValue(orgId, out perTenantPerimeterSettings);
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0007A750 File Offset: 0x00078950
		public PerTenantPerimeterSettings GetPerimeterSettings(OrganizationId orgId)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return ConfigurationComponent.CreateDefaultTenantPerimeterSettings();
			}
			return this.perimeterSettingsCache.GetValue(orgId);
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0007A771 File Offset: 0x00078971
		public bool TryGetMicrosoftExchangeRecipient(OrganizationId orgId, out MicrosoftExchangeRecipientPerTenantSettings perTenantMicrosoftExchangeRecipient)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				perTenantMicrosoftExchangeRecipient = new MicrosoftExchangeRecipientPerTenantSettings(GlobalConfigurationBase<Microsoft.Exchange.Data.Directory.SystemConfiguration.MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance.ConfigObject, orgId);
				return true;
			}
			return this.microsoftExchangeRecipientCache.TryGetValue(orgId, out perTenantMicrosoftExchangeRecipient);
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0007A7A1 File Offset: 0x000789A1
		public MicrosoftExchangeRecipientPerTenantSettings GetMicrosoftExchangeRecipient(OrganizationId orgId)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return new MicrosoftExchangeRecipientPerTenantSettings(GlobalConfigurationBase<Microsoft.Exchange.Data.Directory.SystemConfiguration.MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Instance.ConfigObject, orgId);
			}
			return this.microsoftExchangeRecipientCache.GetValue(orgId);
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0007A7CD File Offset: 0x000789CD
		public bool TryGetRemoteDomainTable(OrganizationId orgId, out PerTenantRemoteDomainTable perTenantRemoteDomains)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				perTenantRemoteDomains = new PerTenantRemoteDomainTable(this.RemoteDomainTable);
				return true;
			}
			if (!this.remoteDomainsCache.TryGetValue(orgId, out perTenantRemoteDomains))
			{
				return false;
			}
			if (perTenantRemoteDomains == null)
			{
				throw new InvalidOperationException("remoteDomainsCache.GetValue() returned null accepted domain table. Null is not expected by the code base in this case.");
			}
			return true;
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x0007A80C File Offset: 0x00078A0C
		public PerTenantRemoteDomainTable GetRemoteDomainTable(OrganizationId orgId)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return new PerTenantRemoteDomainTable(this.RemoteDomainTable);
			}
			PerTenantRemoteDomainTable value = this.remoteDomainsCache.GetValue(orgId);
			if (value == null)
			{
				throw new InvalidOperationException("remoteDomainsCache.GetValue() returned null accepted domain table. Null is not expected by the code base in this case.");
			}
			return value;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x0007A84E File Offset: 0x00078A4E
		public bool TryGetAcceptedDomainTable(OrganizationId orgId, out PerTenantAcceptedDomainTable perTenantAcceptedDomains)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				perTenantAcceptedDomains = new PerTenantAcceptedDomainTable(this.FirstOrgAcceptedDomainTable);
				return true;
			}
			if (!this.acceptedDomainsCache.TryGetValue(orgId, out perTenantAcceptedDomains))
			{
				return false;
			}
			if (perTenantAcceptedDomains == null)
			{
				throw new InvalidOperationException("acceptedDomainsCache.TryGetValue() returned true and null accepted domain table. Null is not expected by the code base in this case.");
			}
			return true;
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0007A88D File Offset: 0x00078A8D
		public bool TryGetTenantOutboundConnectors(OrganizationId orgId, out PerTenantOutboundConnectors perTenantOutboundConnectors)
		{
			if (!this.outboundConnectorsCache.TryGetValue(orgId, out perTenantOutboundConnectors))
			{
				return false;
			}
			if (perTenantOutboundConnectors == null)
			{
				throw new InvalidOperationException("outboundConnectorsCache.TryGetValue() returned true with null tenant outbound connectors. Null is not expected by the code base in this case.");
			}
			return true;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0007A8B0 File Offset: 0x00078AB0
		public PerTenantAcceptedDomainTable GetAcceptedDomainTable(OrganizationId orgId)
		{
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return new PerTenantAcceptedDomainTable(this.FirstOrgAcceptedDomainTable);
			}
			PerTenantAcceptedDomainTable value = this.acceptedDomainsCache.GetValue(orgId);
			if (value == null)
			{
				throw new InvalidOperationException("acceptedDomainsCache.GetValue() returned null accepted domain table. Null is not expected by the code base in this case.");
			}
			return value;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x0007A8F2 File Offset: 0x00078AF2
		public bool TryGetJournalingRules(OrganizationId orgId, out JournalingRulesPerTenantSettings perTenantJournalingRules)
		{
			orgId == OrganizationId.ForestWideOrgId;
			return this.journalingRulesCache.TryGetValue(orgId, out perTenantJournalingRules);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0007A90D File Offset: 0x00078B0D
		public JournalingRulesPerTenantSettings GetJournalingRules(OrganizationId orgId)
		{
			orgId == OrganizationId.ForestWideOrgId;
			return this.journalingRulesCache.GetValue(orgId);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0007A927 File Offset: 0x00078B27
		public bool TryGetReconciliationAccounts(OrganizationId orgId, out ReconciliationAccountPerTenantSettings perTenantReconciliationSettings)
		{
			orgId == OrganizationId.ForestWideOrgId;
			return this.reconciliationAccountsCache.TryGetValue(orgId, out perTenantReconciliationSettings);
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0007A942 File Offset: 0x00078B42
		public ReconciliationAccountPerTenantSettings GetReconciliationAccounts(OrganizationId orgId)
		{
			orgId == OrganizationId.ForestWideOrgId;
			return this.reconciliationAccountsCache.GetValue(orgId);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0007A95C File Offset: 0x00078B5C
		private void LocalServer_Changed(TransportServerConfiguration update)
		{
			this.ReconcileTransportServerObjects(false);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0007A965 File Offset: 0x00078B65
		private void FrontendServer_Changed(FrontendTransportServer update)
		{
			this.ReconcileTransportServerObjects(true);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0007A96E File Offset: 0x00078B6E
		private void MailboxServer_Changed(MailboxTransportServer update)
		{
			this.ReconcileTransportServerObjects(true);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0007A978 File Offset: 0x00078B78
		private void ReconcileTransportServerObjects(bool cloneAndUpdate)
		{
			if (this.isFrontendTransportRoleProcess)
			{
				TransportServerConfiguration cache = this.localServer.Cache;
				cache.UpdateFrontEndConfiguration(this.frontendServer.Cache, cloneAndUpdate);
				this.reconciledServer = cache;
			}
			else if (this.isMailboxTransportDeliveryRoleProcess || this.isMailboxTransportSubmissionRoleProcess)
			{
				string pathSuffix = this.isMailboxTransportDeliveryRoleProcess ? "Delivery" : "Submission";
				TransportServerConfiguration cache2 = this.localServer.Cache;
				cache2.UpdateMailboxConfiguration(this.mailboxServer.Cache, pathSuffix, cloneAndUpdate);
				this.reconciledServer = cache2;
			}
			else
			{
				this.reconciledServer = this.localServer.Cache;
			}
			ConfigurationUpdateHandler<TransportServerConfiguration> localServerChanged = this.LocalServerChanged;
			if (localServerChanged != null)
			{
				localServerChanged(this.reconciledServer);
			}
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0007AA28 File Offset: 0x00078C28
		private void CreateAndRegister<TCache, TBuilder>(ConfigurationLoader<TCache, TBuilder>.ExternalConfigurationSetter extraConfiguration, bool addToParallelComponent, out ConfigurationLoader<TCache, TBuilder> cache) where TCache : class where TBuilder : ConfigurationLoader<TCache, TBuilder>.Builder, new()
		{
			this.CreateAndRegister<TCache, TBuilder>(extraConfiguration, this.AppConfig.ADPolling.ConfigurationComponentReloadInterval, addToParallelComponent, out cache);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0007AA43 File Offset: 0x00078C43
		private void CreateAndRegister<TCache, TBuilder>(ConfigurationLoader<TCache, TBuilder>.ExternalConfigurationSetter extraConfiguration, TimeSpan reloadInterval, bool addToParallelComponent, out ConfigurationLoader<TCache, TBuilder> cache) where TCache : class where TBuilder : ConfigurationLoader<TCache, TBuilder>.Builder, new()
		{
			cache = new ConfigurationLoader<TCache, TBuilder>(extraConfiguration, reloadInterval);
			if (addToParallelComponent)
			{
				this.parallelComponent.TransportComponents.Add(cache);
			}
			Components.ConfigChanged += cache.Reload;
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0007AA78 File Offset: 0x00078C78
		private void InitializeCaches()
		{
			this.transportSettingsCache = new TenantConfigurationCache<PerTenantTransportSettings>((long)this.AppConfig.PerTenantCache.TransportSettingsCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.TransportSettingsCacheExpiryInterval, this.AppConfig.PerTenantCache.TransportSettingsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.TransportSettingsCacheTracer, "TransportSettings"), new PerTenantCachePerformanceCounters(this.processTransportRole, "TransportSettings"));
			this.perimeterSettingsCache = new TenantConfigurationCache<PerTenantPerimeterSettings>((long)this.AppConfig.PerTenantCache.PerimeterSettingsCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.PerimeterSettingsCacheExpiryInterval, this.AppConfig.PerTenantCache.PerimeterSettingsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.PerimeterSettingsCacheTracer, "PerimeterSettings"), new PerTenantCachePerformanceCounters(this.processTransportRole, "PerimeterSettings"));
			this.microsoftExchangeRecipientCache = new TenantConfigurationCache<MicrosoftExchangeRecipientPerTenantSettings>((long)this.AppConfig.PerTenantCache.MicrosoftExchangeRecipientCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.MicrosoftExchangeRecipientCacheExpiryInterval, this.AppConfig.PerTenantCache.MicrosoftExchangeRecipientCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.MicrosoftExchangeRecipientCacheTracer, "MicrosoftExchangeRecipient"), new PerTenantCachePerformanceCounters(this.processTransportRole, "MicrosoftExchangeRecipient"));
			this.remoteDomainsCache = new TenantConfigurationCache<PerTenantRemoteDomainTable>((long)this.AppConfig.PerTenantCache.RemoteDomainsCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.RemoteDomainsCacheExpiryInterval, this.AppConfig.PerTenantCache.RemoteDomainsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.RemoteDomainsCacheTracer, "RemoteDomains"), new PerTenantCachePerformanceCounters(this.processTransportRole, "RemoteDomains"));
			this.acceptedDomainsCache = new TenantConfigurationCache<PerTenantAcceptedDomainTable>((long)this.AppConfig.PerTenantCache.AcceptedDomainsCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.AcceptedDomainsCacheExpiryInterval, this.AppConfig.PerTenantCache.AcceptedDomainsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.AcceptedDomainsCacheTracer, "AcceptedDomains"), new PerTenantCachePerformanceCounters(this.processTransportRole, "AcceptedDomains"));
			this.journalingRulesCache = new TenantConfigurationCache<JournalingRulesPerTenantSettings>((long)this.AppConfig.PerTenantCache.JournalingRulesCacheMaxSize.ToBytes(), this.AppConfig.PerTenantCache.JournalingCacheExpiryInterval, this.AppConfig.PerTenantCache.JournalingCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.JournalingRulesCacheTracer, "JournalingRules"), new PerTenantCachePerformanceCounters(this.processTransportRole, "JournalingRules"));
			this.reconciliationAccountsCache = new TenantConfigurationCache<ReconciliationAccountPerTenantSettings>((long)this.AppConfig.PerTenantCache.ReconciliationCacheConfigMaxSize.ToBytes(), this.AppConfig.PerTenantCache.JournalingCacheExpiryInterval, this.AppConfig.PerTenantCache.JournalingCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.JournalingRulesCacheTracer, "JournalingRules"), new PerTenantCachePerformanceCounters(this.processTransportRole, "ReconciliationAccounts"));
			this.outboundConnectorsCache = new TenantConfigurationCache<PerTenantOutboundConnectors>((long)this.AppConfig.PerTenantCache.OutboundConnectorsCacheSize.ToBytes(), this.AppConfig.PerTenantCache.OutboundConnectorsCacheExpirationInterval, this.AppConfig.PerTenantCache.OutboundConnectorsCacheCleanupInterval, new PerTenantCacheTracer(ExTraceGlobals.OutboundConnectorsCacheTracer, "OutboundConnectorsCache"), new PerTenantCachePerformanceCounters(this.processTransportRole, "OutboundConnectorsCache"));
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0007AD9C File Offset: 0x00078F9C
		private void ComputeProcessTransportRole()
		{
			if (!this.localServer.IsInitialized)
			{
				throw new InvalidOperationException("ProcessTransportRole can be computed only after initializing localserver.");
			}
			if (this.isFrontendTransportRoleProcess)
			{
				this.processTransportRole = ProcessTransportRole.FrontEnd;
			}
			else if (this.isMailboxTransportSubmissionRoleProcess)
			{
				this.processTransportRole = ProcessTransportRole.MailboxSubmission;
			}
			else if (this.isMailboxTransportDeliveryRoleProcess)
			{
				this.processTransportRole = ProcessTransportRole.MailboxDelivery;
			}
			else if (this.localServer.Cache.TransportServer.IsHubTransportServer)
			{
				this.processTransportRole = ProcessTransportRole.Hub;
			}
			else
			{
				if (!this.localServer.Cache.TransportServer.IsEdgeServer)
				{
					throw new TransportComponentLoadFailedException("Unexpected server role: " + this.localServer.Cache.TransportServer.CurrentServerRole);
				}
				this.processTransportRole = ProcessTransportRole.Edge;
			}
			this.processTransportRoleComputed = true;
		}

		// Token: 0x040010AC RID: 4268
		private readonly bool isFrontendTransportRoleProcess;

		// Token: 0x040010AD RID: 4269
		private readonly bool isMailboxTransportSubmissionRoleProcess;

		// Token: 0x040010AE RID: 4270
		private readonly bool isMailboxTransportDeliveryRoleProcess;

		// Token: 0x040010AF RID: 4271
		private ProcessTransportRole processTransportRole;

		// Token: 0x040010B0 RID: 4272
		private bool processTransportRoleComputed;

		// Token: 0x040010B1 RID: 4273
		private ParallelTransportComponent parallelComponent;

		// Token: 0x040010B2 RID: 4274
		private ConfigurationLoader<AcceptedDomainTable, AcceptedDomainTable.Builder> firstOrgAcceptedDomainTable;

		// Token: 0x040010B3 RID: 4275
		private ConfigurationLoader<RemoteDomainTable, RemoteDomainTable.Builder> remoteDomainTable;

		// Token: 0x040010B4 RID: 4276
		private ConfigurationLoader<X400AuthoritativeDomainTable, X400AuthoritativeDomainTable.Builder> x400DomainTable;

		// Token: 0x040010B5 RID: 4277
		private ConfigurationLoader<ReceiveConnectorConfiguration, ReceiveConnectorConfiguration.Builder> receiveConnectors;

		// Token: 0x040010B6 RID: 4278
		private ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder> localServer;

		// Token: 0x040010B7 RID: 4279
		private ConfigurationLoader<FrontendTransportServer, TransportServerConfiguration.FrontendBuilder> frontendServer;

		// Token: 0x040010B8 RID: 4280
		private ConfigurationLoader<MailboxTransportServer, TransportServerConfiguration.MailboxBuilder> mailboxServer;

		// Token: 0x040010B9 RID: 4281
		private ConfigurationLoader<TransportSettingsConfiguration, TransportSettingsConfiguration.Builder> transportSettings;

		// Token: 0x040010BA RID: 4282
		private TenantConfigurationCache<PerTenantTransportSettings> transportSettingsCache;

		// Token: 0x040010BB RID: 4283
		private TenantConfigurationCache<PerTenantPerimeterSettings> perimeterSettingsCache;

		// Token: 0x040010BC RID: 4284
		private TenantConfigurationCache<MicrosoftExchangeRecipientPerTenantSettings> microsoftExchangeRecipientCache;

		// Token: 0x040010BD RID: 4285
		private TenantConfigurationCache<PerTenantRemoteDomainTable> remoteDomainsCache;

		// Token: 0x040010BE RID: 4286
		private TenantConfigurationCache<PerTenantAcceptedDomainTable> acceptedDomainsCache;

		// Token: 0x040010BF RID: 4287
		private TenantConfigurationCache<JournalingRulesPerTenantSettings> journalingRulesCache;

		// Token: 0x040010C0 RID: 4288
		private TenantConfigurationCache<ReconciliationAccountPerTenantSettings> reconciliationAccountsCache;

		// Token: 0x040010C1 RID: 4289
		private TenantConfigurationCache<PerTenantOutboundConnectors> outboundConnectorsCache;

		// Token: 0x040010C2 RID: 4290
		private TransportServerConfiguration reconciledServer;

		// Token: 0x020002D1 RID: 721
		private sealed class PerTenantCacheConstants
		{
			// Token: 0x040010C4 RID: 4292
			internal const string TransportSettingsString = "TransportSettings";

			// Token: 0x040010C5 RID: 4293
			internal const string PerimeterSettingsString = "PerimeterSettings";

			// Token: 0x040010C6 RID: 4294
			internal const string JournalingRulesString = "JournalingRules";

			// Token: 0x040010C7 RID: 4295
			internal const string ReconciliationAccountsString = "ReconciliationAccounts";

			// Token: 0x040010C8 RID: 4296
			internal const string MicrosoftExchangeRecipientString = "MicrosoftExchangeRecipient";

			// Token: 0x040010C9 RID: 4297
			internal const string RemoteDomainsString = "RemoteDomains";

			// Token: 0x040010CA RID: 4298
			internal const string AcceptedDomainsString = "AcceptedDomains";

			// Token: 0x040010CB RID: 4299
			internal const string OrganizationSettingsString = "OrganizationSettings";

			// Token: 0x040010CC RID: 4300
			internal const string OutboundConnectorsCacheString = "OutboundConnectorsCache";
		}
	}
}
