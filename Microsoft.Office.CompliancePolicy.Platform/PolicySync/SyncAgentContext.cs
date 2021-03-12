using System;
using Microsoft.Office.CompliancePolicy.Monitor;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200011B RID: 283
	public sealed class SyncAgentContext
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00017DC4 File Offset: 0x00015FC4
		public SyncAgentContext(SyncAgentConfiguration config, ICredentialsFactory credentialFactory, ITenantInfoProviderFactory tenantInfoProviderFactory, IPolicyConfigProviderManager policyConfigProviderFactory, HostStateProvider hostStateProvider, ExecutionLog logProvider, IMonitoringNotification monitorProvider, PerfCounterProvider perfCounterProvider) : this(config, new PolicySyncWebserviceClientFactory(logProvider), credentialFactory, tenantInfoProviderFactory, policyConfigProviderFactory, hostStateProvider, logProvider, new JobFactory(), monitorProvider, perfCounterProvider)
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00017DF0 File Offset: 0x00015FF0
		internal SyncAgentContext(SyncAgentConfiguration config, IPolicySyncWebserviceClientFactory syncSvcClientFactory, ICredentialsFactory credentialFactory, ITenantInfoProviderFactory tenantInfoProviderFactory, IPolicyConfigProviderManager policyConfigProviderFactory, HostStateProvider hostStateProvider, ExecutionLog logProvider, IJobFactory jobFactory, IMonitoringNotification monitorProvider, PerfCounterProvider perfCounterProvider)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			ArgumentValidator.ThrowIfNull("hostStateProvider", hostStateProvider);
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			ArgumentValidator.ThrowIfNull("jobFactory", jobFactory);
			ArgumentValidator.ThrowIfNull("syncSvcClientFactory", syncSvcClientFactory);
			ArgumentValidator.ThrowIfNull("credentialFactory", credentialFactory);
			ArgumentValidator.ThrowIfNull("tenantInfoProviderFactory", tenantInfoProviderFactory);
			ArgumentValidator.ThrowIfNull("policyConfigProviderFactory", policyConfigProviderFactory);
			ArgumentValidator.ThrowIfNull("monitorProvider", monitorProvider);
			ArgumentValidator.ThrowIfNull("perfCounterProvider", perfCounterProvider);
			this.SyncAgentConfig = config;
			this.SyncSvcClientFactory = syncSvcClientFactory;
			this.CredentialFactory = credentialFactory;
			this.TenantInfoProviderFactory = tenantInfoProviderFactory;
			this.PolicyConfigProviderFactory = policyConfigProviderFactory;
			this.HostStateProvider = hostStateProvider;
			this.LogProvider = logProvider;
			this.JobFactory = jobFactory;
			this.MonitorProvider = monitorProvider;
			this.PerfCounterProvider = perfCounterProvider;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00017EC5 File Offset: 0x000160C5
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00017ECD File Offset: 0x000160CD
		public SyncAgentConfiguration SyncAgentConfig { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00017ED6 File Offset: 0x000160D6
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00017EDE File Offset: 0x000160DE
		public ICredentialsFactory CredentialFactory { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00017EE7 File Offset: 0x000160E7
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00017EEF File Offset: 0x000160EF
		public ITenantInfoProviderFactory TenantInfoProviderFactory { get; private set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00017EF8 File Offset: 0x000160F8
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00017F00 File Offset: 0x00016100
		public IPolicyConfigProviderManager PolicyConfigProviderFactory { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00017F09 File Offset: 0x00016109
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00017F11 File Offset: 0x00016111
		public HostStateProvider HostStateProvider { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00017F1A File Offset: 0x0001611A
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00017F22 File Offset: 0x00016122
		public ExecutionLog LogProvider { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00017F2B File Offset: 0x0001612B
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00017F33 File Offset: 0x00016133
		public IMonitoringNotification MonitorProvider { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00017F3C File Offset: 0x0001613C
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00017F44 File Offset: 0x00016144
		internal IPolicySyncWebserviceClientFactory SyncSvcClientFactory { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00017F4D File Offset: 0x0001614D
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00017F55 File Offset: 0x00016155
		internal IJobFactory JobFactory { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00017F5E File Offset: 0x0001615E
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00017F66 File Offset: 0x00016166
		internal PerfCounterProvider PerfCounterProvider { get; private set; }
	}
}
