using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000253 RID: 595
	internal class RoutingComponent : IRoutingComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x060019C6 RID: 6598 RVA: 0x00069A2B File Offset: 0x00067C2B
		public RoutingComponent()
		{
			this.mailRouter = new MailRouter();
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x00069A3E File Offset: 0x00067C3E
		public IMailRouter MailRouter
		{
			get
			{
				return this.mailRouter;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00069A48 File Offset: 0x00067C48
		public void Load()
		{
			if (this.dependencies == null)
			{
				throw new InvalidOperationException("Load-time dependencies must be set before loading Routing Component");
			}
			RoutingPerformanceCounters perfCounters = new RoutingPerformanceCounters(this.dependencies.ProcessTransportRole);
			RoutingContextCore contextCore = new RoutingContextCore(this.dependencies.ProcessTransportRole, this.settings, this.dependencies, this.edgeDependencies, perfCounters);
			this.mailRouter.Load(contextCore);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00069AA9 File Offset: 0x00067CA9
		public void Unload()
		{
			this.mailRouter.Unload();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00069AB6 File Offset: 0x00067CB6
		public void SetLoadTimeDependencies(TransportAppConfig appConfig, ITransportConfiguration transportConfig)
		{
			this.dependencies = new RoutingDependencies(appConfig, transportConfig);
			this.edgeDependencies = new EdgeRoutingDependencies(transportConfig);
			if (appConfig != null)
			{
				this.settings = appConfig.Routing;
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00069AE0 File Offset: 0x00067CE0
		public void SetRunTimeDependencies(ShadowRedundancyComponent shadowRedundancy, UnhealthyTargetFilterComponent unhealthyTargetFilter, CategorizerComponent categorizer)
		{
			if (this.dependencies == null)
			{
				throw new InvalidOperationException("Load-time dependencies must be set before run-time dependencies");
			}
			this.dependencies.ShadowRedundancy = shadowRedundancy;
			this.dependencies.UnhealthyTargetFilter = unhealthyTargetFilter;
			this.dependencies.Categorizer = categorizer;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00069B19 File Offset: 0x00067D19
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00069B1C File Offset: 0x00067D1C
		public string GetDiagnosticComponentName()
		{
			return "Routing";
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00069B24 File Offset: 0x00067D24
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			bool verbose = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			foreach (XElement content in this.mailRouter.GetDiagnosticInfo(verbose, parameters))
			{
				xelement.Add(content);
			}
			if (flag)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, help, config, dagSelector, tenantDagQuota, tenant:{tenantID e.g.1afa2e80-0251-4521-8086-039fb2f9d8d6}."));
			}
			return xelement;
		}

		// Token: 0x04000C4F RID: 3151
		private TransportAppConfig.RoutingConfig settings;

		// Token: 0x04000C50 RID: 3152
		private RoutingDependencies dependencies;

		// Token: 0x04000C51 RID: 3153
		private EdgeRoutingDependencies edgeDependencies;

		// Token: 0x04000C52 RID: 3154
		private MailRouter mailRouter;
	}
}
