using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006A3 RID: 1699
	internal abstract class EcpServiceHostBase : ServiceHost
	{
		// Token: 0x060048AD RID: 18605 RVA: 0x000DE538 File Offset: 0x000DC738
		public EcpServiceHostBase(Type serviceType, params Uri[] baseAddresses) : base(serviceType, baseAddresses)
		{
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x000DE544 File Offset: 0x000DC744
		protected override void ApplyConfiguration()
		{
			base.ApplyConfiguration();
			ServiceDebugBehavior serviceDebugBehavior = base.Description.Behaviors.Find<ServiceDebugBehavior>();
			serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
			ServiceAuthorizationBehavior serviceAuthorizationBehavior = base.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
			serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
			serviceAuthorizationBehavior.ExternalAuthorizationPolicies = EcpServiceHostBase.customAuthorizationPolicies;
			serviceAuthorizationBehavior.ServiceAuthorizationManager = EcpServiceHostBase.rbacAuthorizationManager;
			ContractDescription contract = ContractDescription.GetContract(base.Description.ServiceType);
			foreach (Uri uri in base.BaseAddresses)
			{
				Binding binding = this.CreateBinding(uri);
				ServiceEndpoint serviceEndpoint = new ServiceEndpoint(contract, binding, new EndpointAddress(uri, new AddressHeader[0]));
				serviceEndpoint.Behaviors.Add(EcpServiceHostBase.diagnosticsBehavior);
				this.ApplyServiceEndPointConfiguration(serviceEndpoint);
				base.Description.Endpoints.Add(serviceEndpoint);
			}
		}

		// Token: 0x060048AF RID: 18607
		protected abstract Binding CreateBinding(Uri address);

		// Token: 0x060048B0 RID: 18608 RVA: 0x000DE638 File Offset: 0x000DC838
		protected virtual void ApplyServiceEndPointConfiguration(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x0400311F RID: 12575
		private static readonly DiagnosticsBehavior diagnosticsBehavior = new DiagnosticsBehavior();

		// Token: 0x04003120 RID: 12576
		private static readonly RbacAuthorizationManager rbacAuthorizationManager = new RbacAuthorizationManager();

		// Token: 0x04003121 RID: 12577
		private static readonly ReadOnlyCollection<IAuthorizationPolicy> customAuthorizationPolicies = Array.AsReadOnly<IAuthorizationPolicy>(new IAuthorizationPolicy[]
		{
			new RbacAuthorizationPolicy()
		});
	}
}
