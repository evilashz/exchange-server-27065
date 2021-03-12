using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.ServiceHost
{
	// Token: 0x02000003 RID: 3
	internal sealed class ServiceHost : ServiceHostBase
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002AD4 File Offset: 0x00000CD4
		static ServiceHost()
		{
			ExWatson.Register();
			ServiceHostBase.ComponentGuid = new Guid("0ac170ab-ae08-46c1-9265-d72a9ca84df6");
			ServiceHostBase.Log = new ExEventLog(ServiceHostBase.ComponentGuid, "MSExchangeServiceHost");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002D48 File Offset: 0x00000F48
		private ServiceHost() : base("MSExchangeServiceHost", ServiceHost.Modules)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002D5A File Offset: 0x00000F5A
		protected override void InitializeThrottling()
		{
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.ServiceHost);
			SettingOverrideSync.Instance.Start(true);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D70 File Offset: 0x00000F70
		public static void Main(string[] args)
		{
			using (ServiceHost serviceHost = new ServiceHost())
			{
				ServiceHostBase.MainEntry(serviceHost, args);
			}
		}

		// Token: 0x04000007 RID: 7
		private static readonly BoolAppSettingsEntry EnableCertificateDeployment = new BoolAppSettingsEntry("EnableCertificateDeployment", false, null);

		// Token: 0x04000008 RID: 8
		private static readonly ModuleMap[] Modules = new ModuleMap[]
		{
			new ModuleMap("Microsoft.Exchange.RPCOverHTTPAutoconfig.dll", "Microsoft.Exchange.Servicelets.RPCHTTP.RPCHTTPServicelet", ServerRole.Cafe | ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.RPCOverHTTPAutoconfig.dll", "Microsoft.Exchange.Servicelets.ServiceAccount.Servicelet", ServerRole.Cafe, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.ExchangeCertificateServicelet.dll", "Microsoft.Exchange.Servicelets.ExchangeCertificate.Servicelet", ServerRole.All, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.JobQueueServicelet.dll", "Microsoft.Exchange.Servicelets.JobQueue.Servicelet", ServerRole.All, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.UnifiedPolicySyncServicelet.dll", "Microsoft.Exchange.Servicelets.UnifiedPolicySync.Servicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Office.CompliancePolicy.Exchange.Dar.dll", "Microsoft.Office.CompliancePolicy.Exchange.Dar.Service.Servicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.CertificateDeploymentServicelet.dll", "Microsoft.Exchange.Servicelets.CertificateDeployment.Servicelet", ServerRole.Cafe | ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.HubTransport, ServerRole.None, ServiceHost.EnableCertificateDeployment.Value ? (RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated) : RunInExchangeMode.Enterprise),
			new ModuleMap("Microsoft.Exchange.ProvisioningServicelet.dll", "Microsoft.Exchange.Servicelets.Provisioning.ProvisioningServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.SyncMigrationServicelet.dll", "Microsoft.Exchange.Servicelets.SyncMigration.SyncMigrationServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.UpgradeHandler.dll", "Microsoft.Exchange.Servicelets.Upgrade.UpgradeHandler", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.UpgradeInjector.dll", "Microsoft.Exchange.Servicelets.Upgrade.UpgradeInjector", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.UpgradeBatchCreator.dll", "Microsoft.Exchange.Servicelets.Upgrade.UpgradeBatchCreator", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.TenantDataCollector.dll", "Microsoft.Exchange.Servicelets.Upgrade.TenantDataCollector", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.MigrationMonitor.dll", "Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationMonitor", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.AuthAdminServicelet.dll", "Microsoft.Exchange.Servicelets.AuthAdmin.AuthAdmin", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.SACLWatcherServicelet.dll", "Microsoft.Exchange.Servicelets.SACLWatcher.Servicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.AuditLogSearchServicelet.dll", "Microsoft.Exchange.Servicelets.AuditLogSearch.AuditLogSearchServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.CertificateNotificationServicelet.dll", "Microsoft.Exchange.Servicelets.CertificateNotificationServicelet", ServerRole.Cafe | ServerRole.ClientAccess, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.TenantRelocationServicelet.dll", "Microsoft.Exchange.Servicelets.TenantRelocationServicelet", ServerRole.ClientAccess, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.DiagnosticsAggregationServicelet.dll", "Microsoft.Exchange.Servicelets.DiagnosticsAggregation.DiagnosticsAggregationServicelet", ServerRole.HubTransport, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.PhotoGarbageCollectionServicelet.dll", "Microsoft.Exchange.Servicelets.PhotoGarbageCollection.PhotoGarbageCollectionServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.Enterprise | RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.MRS.SlowMRSDetector.dll", "Microsoft.Exchange.Servicelets.MRS.SlowMRSDetector", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter | RunInExchangeMode.DatacenterDedicated),
			new ModuleMap("Microsoft.Exchange.TenantUpgradeServicelet.dll", "Microsoft.Exchange.Servicelets.TenantUpgradeServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter),
			new ModuleMap("Microsoft.Exchange.DirectoryTasksServicelet.dll", "Microsoft.Exchange.Servicelets.DirectoryTasksServicelet", ServerRole.Mailbox, ServerRole.None, RunInExchangeMode.ExchangeDatacenter)
		};
	}
}
