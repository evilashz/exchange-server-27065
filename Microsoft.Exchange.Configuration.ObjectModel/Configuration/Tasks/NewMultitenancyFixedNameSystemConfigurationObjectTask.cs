using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000077 RID: 119
	public abstract class NewMultitenancyFixedNameSystemConfigurationObjectTask<TDataObject> : NewFixedNameSystemConfigurationObjectTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00011120 File Offset: 0x0000F320
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00011137 File Offset: 0x0000F337
		[Parameter]
		[ValidateNotNullOrEmpty]
		public OrganizationIdParameter Organization
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

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001114A File Offset: 0x0000F34A
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.SharedTenantConfigurationMode != SharedTenantConfigurationMode.NotShared)
			{
				base.OrgWideSessionSettings.IsSharedConfigChecked = true;
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00011166 File Offset: 0x0000F366
		protected override void InternalValidate()
		{
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.Validate(this, this.SharedTenantConfigurationMode, base.CurrentOrgState, typeof(TDataObject).ToString());
			}
			base.InternalValidate();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001119C File Offset: 0x0000F39C
		protected override IConfigDataProvider CreateSession()
		{
			base.SessionSettings.IsSharedConfigChecked = true;
			return base.CreateSession();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000111B0 File Offset: 0x0000F3B0
		protected override void InternalProcessRecord()
		{
			if (!this.IgnoreDehydratedFlag && SharedConfigurationTaskHelper.ShouldPrompt(this, this.SharedTenantConfigurationMode, base.CurrentOrgState) && !base.InternalForce)
			{
				TDataObject dataObject = this.DataObject;
				if (!base.ShouldContinue(Strings.ConfirmSharedConfiguration(dataObject.OrganizationId.OrganizationalUnit.Name)))
				{
					return;
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00011220 File Offset: 0x0000F420
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 1082, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000112D2 File Offset: 0x0000F4D2
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000112D5 File Offset: 0x0000F4D5
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000112DD File Offset: 0x0000F4DD
		public virtual SwitchParameter IgnoreDehydratedFlag
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
