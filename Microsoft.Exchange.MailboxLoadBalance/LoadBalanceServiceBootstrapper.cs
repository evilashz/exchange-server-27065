using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Injector;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Providers;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceServiceBootstrapper : DisposeTrackableBase, IAnchorService, IDisposable
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000028EB File Offset: 0x00000AEB
		public LoadBalanceServiceBootstrapper()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028F3 File Offset: 0x00000AF3
		public LoadBalanceServiceBootstrapper(LoadBalanceAnchorContext anchorContext)
		{
			AnchorUtil.ThrowOnNullArgument(anchorContext, "anchorContext");
			((IAnchorService)this).Initialize(anchorContext);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000290E File Offset: 0x00000B0E
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002916 File Offset: 0x00000B16
		public MailboxLoadBalanceService Service { get; private set; }

		// Token: 0x0600002E RID: 46 RVA: 0x00002A74 File Offset: 0x00000C74
		public IEnumerable<IDiagnosable> GetDiagnosableComponents()
		{
			yield return new LoadBalanceServiceDiagnosable(this.anchorContext);
			yield return new LoadBalanceBandSettingsStorageDiagnosable(this.anchorContext);
			yield return new LoadBalanceTopologyDiagnosable(this.anchorContext);
			yield return this.anchorContext.Config;
			yield break;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002A94 File Offset: 0x00000C94
		public void Start()
		{
			MoveInjector moveInjector = this.anchorContext.MoveInjector;
			this.Service = new MailboxLoadBalanceService(this.anchorContext);
			this.logger.Log(MigrationEventType.Verbose, "Starting load balancer service.", new object[0]);
			LoadBalanceService serviceInstance = new LoadBalanceService(this.Service, this.anchorContext);
			this.EnableProvisioningCache();
			this.balancerHost = this.StartServiceEndpoint(serviceInstance, LoadBalanceService.EndpointAddress);
			this.logger.Log(MigrationEventType.Verbose, "Starting injector service.", new object[0]);
			this.injectorHost = this.StartServiceEndpoint(new InjectorService(this.directoryProvider, this.logger, moveInjector), InjectorService.EndpointAddress);
			this.logger.Log(MigrationEventType.Verbose, "Service started.", new object[0]);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B50 File Offset: 0x00000D50
		bool IAnchorService.Initialize(AnchorContext anchorContext)
		{
			LoadBalanceAnchorContext loadBalanceAnchorContext = (LoadBalanceAnchorContext)anchorContext;
			this.anchorContext = loadBalanceAnchorContext;
			this.directoryProvider = loadBalanceAnchorContext.Directory;
			this.logger = loadBalanceAnchorContext.Logger;
			return loadBalanceAnchorContext.Settings.IsEnabled;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B90 File Offset: 0x00000D90
		protected virtual void EnableProvisioningCache()
		{
			if (this.anchorContext.Settings.UseHeatMapProvisioning)
			{
				this.logger.Log(MigrationEventType.Verbose, "Initializing local forest heat map cache.", new object[0]);
				this.anchorContext.InitializeForestHeatMap();
			}
			else
			{
				this.logger.Log(MigrationEventType.Verbose, "Enabling provisioning cache.", new object[0]);
				ProvisioningCache.InitializeAppRegistrySettings("Powershell");
			}
			if (this.anchorContext.Settings.BuildLocalCacheOnStartup)
			{
				this.anchorContext.InitializeLocalServerHeatMap();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C11 File Offset: 0x00000E11
		protected override void InternalDispose(bool disposing)
		{
			this.DisposeServiceHost(ref this.injectorHost);
			this.DisposeServiceHost(ref this.balancerHost);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002C2B File Offset: 0x00000E2B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LoadBalanceServiceBootstrapper>(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C33 File Offset: 0x00000E33
		private void DisposeServiceHost(ref ServiceHost serviceHost)
		{
			if (serviceHost != null)
			{
				serviceHost.Abort();
				serviceHost.Close();
				serviceHost = null;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002C4C File Offset: 0x00000E4C
		private ServiceHost StartServiceEndpoint(object serviceInstance, ServiceEndpointAddress serviceAddress)
		{
			AnchorUtil.ThrowOnNullArgument(serviceInstance, "serviceInstance");
			ServiceHost result;
			try
			{
				ServiceHost serviceHost = new ServiceHost(serviceInstance, serviceAddress.GetBaseUris());
				serviceHost.AddDefaultEndpoints();
				this.logger.Log(MigrationEventType.Verbose, "Opening service host for {0}, with service type {1} and namespace {2}.", new object[]
				{
					serviceHost.Description.Name,
					serviceHost.Description.ServiceType.FullName,
					serviceHost.Description.Namespace
				});
				ServiceDebugBehavior serviceDebugBehavior = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
				if (serviceDebugBehavior == null)
				{
					serviceDebugBehavior = new ServiceDebugBehavior();
					serviceHost.Description.Behaviors.Add(serviceDebugBehavior);
				}
				serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
				foreach (System.ServiceModel.Description.ServiceEndpoint serviceEndpoint in serviceHost.Description.Endpoints)
				{
					NetTcpBinding netTcpBinding = serviceEndpoint.Binding as NetTcpBinding;
					if (netTcpBinding != null)
					{
						netTcpBinding.MaxReceivedMessageSize = 10485760L;
						netTcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(10.0);
						netTcpBinding.SendTimeout = TimeSpan.FromMinutes(10.0);
					}
					this.logger.LogVerbose("Using binging: {0} ({1})", new object[]
					{
						serviceEndpoint.Binding.Name,
						serviceEndpoint.Binding.MessageVersion
					});
					LoadBalanceUtils.UpdateAndLogServiceEndpoint(this.logger, serviceEndpoint);
				}
				serviceHost.Open();
				result = serviceHost;
			}
			catch (Exception exception)
			{
				this.logger.LogError(exception, "Failed to register endpoint for service {0}", new object[]
				{
					serviceInstance.GetType().Name
				});
				throw;
			}
			return result;
		}

		// Token: 0x04000011 RID: 17
		private LoadBalanceAnchorContext anchorContext;

		// Token: 0x04000012 RID: 18
		private IDirectoryProvider directoryProvider;

		// Token: 0x04000013 RID: 19
		private ILogger logger;

		// Token: 0x04000014 RID: 20
		private ServiceHost balancerHost;

		// Token: 0x04000015 RID: 21
		private ServiceHost injectorHost;
	}
}
