using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200005D RID: 93
	public abstract class GetMultitenancySystemConfigurationObjectTask<TIdentity, TDataObject> : GetSystemConfigurationObjectTask<TIdentity, TDataObject> where TIdentity : IIdentityParameter where TDataObject : ADObject, new()
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000EA2B File Offset: 0x0000CC2B
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000EA42 File Offset: 0x0000CC42
		[Parameter]
		public virtual OrganizationIdParameter Organization
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000EA55 File Offset: 0x0000CC55
		internal SharedConfiguration SharedConfiguration
		{
			get
			{
				return this.sharedConfig;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000EA5D File Offset: 0x0000CC5D
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000EA92 File Offset: 0x0000CC92
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000EA98 File Offset: 0x0000CC98
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 630, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				this.organization = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return this.organization.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000EB4F File Offset: 0x0000CD4F
		internal override IConfigurationSession CreateConfigurationSession(ADSessionSettings sessionSettings)
		{
			if (this.SharedConfiguration != null)
			{
				return base.CreateConfigurationSession(this.SharedConfiguration.GetSharedConfigurationSessionSettings());
			}
			return base.CreateConfigurationSession(sessionSettings);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000EB74 File Offset: 0x0000CD74
		protected override IConfigDataProvider CreateSession()
		{
			if (this.SharedConfiguration != null)
			{
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, this.SharedConfiguration.GetSharedConfigurationSessionSettings(), 694, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\GetAdObjectTask.cs");
			}
			return base.CreateSession();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		protected override void InternalStateReset()
		{
			base.CheckExclusiveParameters(new object[]
			{
				"AccountPartition",
				"Organization"
			});
			base.InternalStateReset();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000EBF8 File Offset: 0x0000CDF8
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

		// Token: 0x040000F6 RID: 246
		protected const string ParameterSetOrganization = "OrganizationSet";

		// Token: 0x040000F7 RID: 247
		private ADOrganizationalUnit organization;

		// Token: 0x040000F8 RID: 248
		private SharedConfiguration sharedConfig;
	}
}
