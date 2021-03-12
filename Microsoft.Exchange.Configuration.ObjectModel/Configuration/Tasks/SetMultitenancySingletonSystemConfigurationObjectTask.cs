using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000097 RID: 151
	public abstract class SetMultitenancySingletonSystemConfigurationObjectTask<TDataObject> : SetSingletonSystemConfigurationObjectTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00016638 File Offset: 0x00014838
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00016640 File Offset: 0x00014840
		internal LazilyInitialized<SharedTenantConfigurationState> CurrentOrgState { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00016649 File Offset: 0x00014849
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00016660 File Offset: 0x00014860
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00016680 File Offset: 0x00014880
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			this.CurrentOrgState = new LazilyInitialized<SharedTenantConfigurationState>(() => SharedConfiguration.GetSharedConfigurationState(base.CurrentOrganizationId));
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001669F File Offset: 0x0001489F
		protected override void InternalStateReset()
		{
			this.ResolveCurrentOrgIdBasedOnIdentity(this.Identity);
			base.InternalStateReset();
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000166B4 File Offset: 0x000148B4
		protected override IConfigDataProvider CreateSession()
		{
			if (this.SharedTenantConfigurationMode != SharedTenantConfigurationMode.NotShared)
			{
				base.SessionSettings.IsSharedConfigChecked = true;
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 1000, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00016701 File Offset: 0x00014901
		protected override void InternalValidate()
		{
			SharedConfigurationTaskHelper.Validate(this, this.SharedTenantConfigurationMode, this.CurrentOrgState, typeof(TDataObject).Name);
			base.InternalValidate();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001672C File Offset: 0x0001492C
		protected override void InternalProcessRecord()
		{
			if (SharedConfigurationTaskHelper.ShouldPrompt(this, this.SharedTenantConfigurationMode, this.CurrentOrgState) && !base.InternalForce)
			{
				TDataObject dataObject = this.DataObject;
				if (!base.ShouldContinue(Strings.ConfirmSharedConfiguration(dataObject.OrganizationId.OrganizationalUnit.Name)))
				{
					TaskLogger.LogExit();
					return;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00016791 File Offset: 0x00014991
		protected override OrganizationId ResolveCurrentOrganization()
		{
			return base.CurrentOrganizationId;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001679C File Offset: 0x0001499C
		protected override void ResolveCurrentOrgIdBasedOnIdentity(IIdentityParameter identity)
		{
			if (this.Identity == null)
			{
				base.SetCurrentOrganizationWithScopeSet(base.ExecutingUserOrganizationId);
				return;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 1060, "ResolveCurrentOrgIdBasedOnIdentity", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\SetAdObjectTask.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Identity, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Identity.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Identity.ToString())));
			base.SetCurrentOrganizationWithScopeSet(adorganizationalUnit.OrganizationId);
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00016853 File Offset: 0x00014A53
		protected override bool DeepSearch
		{
			get
			{
				return this.Instance != null || base.DeepSearch;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001686C File Offset: 0x00014A6C
		protected override ObjectId RootId
		{
			get
			{
				if (this.Instance != null)
				{
					TDataObject instance = this.Instance;
					return instance.Identity;
				}
				return base.RootId;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000168A1 File Offset: 0x00014AA1
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
