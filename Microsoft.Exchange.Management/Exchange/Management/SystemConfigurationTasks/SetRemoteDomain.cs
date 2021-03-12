using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD3 RID: 2771
	[Cmdlet("Set", "RemoteDomain", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public class SetRemoteDomain : SetSystemConfigurationObjectTask<RemoteDomainIdParameter, DomainContentConfig>
	{
		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06006271 RID: 25201 RVA: 0x0019ADC6 File Offset: 0x00198FC6
		// (set) Token: 0x06006272 RID: 25202 RVA: 0x0019ADE7 File Offset: 0x00198FE7
		[Parameter]
		public bool TargetDeliveryDomain
		{
			get
			{
				return (bool)(base.Fields[DomainContentConfigSchema.TargetDeliveryDomain] ?? false);
			}
			set
			{
				base.Fields[DomainContentConfigSchema.TargetDeliveryDomain] = value;
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06006273 RID: 25203 RVA: 0x0019ADFF File Offset: 0x00198FFF
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRemoteDomain(this.Identity.ToString());
			}
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0019AE14 File Offset: 0x00199014
		protected override void InternalValidate()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (Server.IsSubscribedGateway(base.GlobalConfigSession))
			{
				base.WriteError(new CannotRunOnSubscribedEdgeException(), ErrorCategory.InvalidOperation, null);
			}
			base.InternalValidate();
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, this.DataObject.OrganizationId, base.ExecutingUserOrganizationId, false);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.ConfigurationSession.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 82, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\RemoteDomain\\SetRemoteDomain.cs");
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.ConfigurationSession.DomainController, true, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 88, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\MessageSecurity\\RemoteDomain\\SetRemoteDomain.cs");
			NewRemoteDomain.ValidateNoDuplicates(this.DataObject, tenantOrTopologyConfigurationSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			Server server = null;
			try
			{
				server = topologyConfigurationSession.ReadLocalServer();
			}
			catch (TransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ResourceUnavailable, null);
			}
			if (server != null && server.IsEdgeServer && this.DataObject.IsModified(DomainContentConfigSchema.AcceptMessageTypes))
			{
				base.WriteError(new ArgumentException(Strings.ParameterNotApplicableToInstalledServerRoles("AllowedOOFType, AutoForwardEnabled, AutoReplyEnabled, DeliveryReportEnabled, IsInternal, NDREnabled, MFNEnabled, UseSimpleDisplayName, NDRDiagnosticInfoEnabled"), string.Empty), ErrorCategory.InvalidArgument, null);
			}
			if (this.TargetDeliveryDomain && (this.DataObject.DomainName.IncludeSubDomains || this.DataObject.DomainName.IsStar))
			{
				base.WriteError(new CannotSetTargetDeliveryDomainOnWildCardDomainsException(this.DataObject.DomainName.ToString()), ErrorCategory.InvalidArgument, this.Identity);
			}
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0019AF88 File Offset: 0x00199188
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADObject adobject = dataObject as ADObject;
			if (adobject != null)
			{
				this.dualWriter = new FfoDualWriter(adobject.Name);
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x0019AFB8 File Offset: 0x001991B8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.TargetDeliveryDomain && !this.DataObject.TargetDeliveryDomain)
			{
				this.DataObject.TargetDeliveryDomain = true;
				ADPagedReader<DomainContentConfig> adpagedReader = ((IConfigurationSession)base.DataSession).FindPaged<DomainContentConfig>(this.DataObject.Id.Parent, QueryScope.OneLevel, null, null, 0);
				foreach (DomainContentConfig domainContentConfig in adpagedReader)
				{
					if (domainContentConfig.TargetDeliveryDomain)
					{
						domainContentConfig.TargetDeliveryDomain = false;
						base.DataSession.Save(domainContentConfig);
					}
				}
			}
			base.InternalProcessRecord();
			this.dualWriter.Save<DomainContentConfig>(this, this.DataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x040035D5 RID: 13781
		private FfoDualWriter dualWriter;
	}
}
