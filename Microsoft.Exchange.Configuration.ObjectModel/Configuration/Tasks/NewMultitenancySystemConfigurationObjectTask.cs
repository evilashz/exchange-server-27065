using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000079 RID: 121
	public abstract class NewMultitenancySystemConfigurationObjectTask<TDataObject> : NewSystemConfigurationObjectTask<TDataObject> where TDataObject : ADObject, new()
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00011319 File Offset: 0x0000F519
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00011330 File Offset: 0x0000F530
		[ValidateNotNullOrEmpty]
		[Parameter]
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00011343 File Offset: 0x0000F543
		protected OrganizationId OrganizationId
		{
			get
			{
				if (!(base.CurrentOrganizationId != null))
				{
					return OrganizationId.ForestWideOrgId;
				}
				return base.CurrentOrganizationId;
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001135F File Offset: 0x0000F55F
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.SharedTenantConfigurationMode != SharedTenantConfigurationMode.NotShared)
			{
				base.OrgWideSessionSettings.IsSharedConfigChecked = true;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001137B File Offset: 0x0000F57B
		protected override void InternalValidate()
		{
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.Validate(this, this.SharedTenantConfigurationMode, base.CurrentOrgState, typeof(TDataObject).ToString());
			}
			base.InternalValidate();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000113B1 File Offset: 0x0000F5B1
		protected override IConfigDataProvider CreateSession()
		{
			base.SessionSettings.IsSharedConfigChecked = true;
			return base.CreateSession();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000113C8 File Offset: 0x0000F5C8
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 1211, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001147C File Offset: 0x0000F67C
		protected override void InternalProcessRecord()
		{
			if (!this.IgnoreDehydratedFlag && SharedConfigurationTaskHelper.ShouldPrompt(this, this.SharedTenantConfigurationMode, base.CurrentOrgState) && !base.InternalForce)
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

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000114EE File Offset: 0x0000F6EE
		protected virtual SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000114F1 File Offset: 0x0000F6F1
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x000114F9 File Offset: 0x0000F6F9
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
