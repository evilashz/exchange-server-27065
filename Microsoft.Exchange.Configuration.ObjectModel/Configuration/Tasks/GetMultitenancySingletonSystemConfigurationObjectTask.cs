using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005F RID: 95
	public abstract class GetMultitenancySingletonSystemConfigurationObjectTask<TDataObject> : GetSingletonSystemConfigurationObjectTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000ED81 File Offset: 0x0000CF81
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000ED94 File Offset: 0x0000CF94
		protected override ObjectId RootId
		{
			get
			{
				if (this.SharedConfiguration != null)
				{
					return this.SharedConfiguration.SharedConfigurationCU.Id;
				}
				if (this.organization != null)
				{
					return this.organization.ConfigurationUnit;
				}
				return base.RootId;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000EDC9 File Offset: 0x0000CFC9
		internal SharedConfiguration SharedConfiguration
		{
			get
			{
				return this.sharedConfig;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000EDD1 File Offset: 0x0000CFD1
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.SharedTenantConfigurationMode == SharedTenantConfigurationMode.Static || (this.SharedTenantConfigurationMode == SharedTenantConfigurationMode.Dehydrateable && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId)))
			{
				this.sharedConfig = SharedConfiguration.GetSharedConfiguration(base.CurrentOrganizationId);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000EE24 File Offset: 0x0000D024
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Identity != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 961, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				this.organization = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Identity, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Identity.ToString())));
				return this.organization.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		protected override IConfigDataProvider CreateSession()
		{
			if (this.SharedConfiguration != null)
			{
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, this.SharedConfiguration.GetSharedConfigurationSessionSettings(), 998, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
			}
			return base.CreateSession();
		}

		// Token: 0x040000FA RID: 250
		private ADOrganizationalUnit organization;

		// Token: 0x040000FB RID: 251
		private SharedConfiguration sharedConfig;
	}
}
