using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000728 RID: 1832
	[Cmdlet("Set", "RMSTemplate", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRMSTemplate : SetTaskBase<RmsTemplatePresentation>
	{
		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x0010ACF3 File Offset: 0x00108EF3
		// (set) Token: 0x06004108 RID: 16648 RVA: 0x0010AD0A File Offset: 0x00108F0A
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ValueFromPipeline = true)]
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

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06004109 RID: 16649 RVA: 0x0010AD1D File Offset: 0x00108F1D
		// (set) Token: 0x0600410A RID: 16650 RVA: 0x0010AD34 File Offset: 0x00108F34
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		[ValidateNotNull]
		public RmsTemplateIdParameter Identity
		{
			get
			{
				return (RmsTemplateIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600410B RID: 16651 RVA: 0x0010AD47 File Offset: 0x00108F47
		// (set) Token: 0x0600410C RID: 16652 RVA: 0x0010AD5E File Offset: 0x00108F5E
		[Parameter(Mandatory = true)]
		public RmsTemplateType Type
		{
			get
			{
				return (RmsTemplateType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x0010AD76 File Offset: 0x00108F76
		// (set) Token: 0x0600410E RID: 16654 RVA: 0x0010AD7E File Offset: 0x00108F7E
		[Parameter(Mandatory = false)]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x0010AD87 File Offset: 0x00108F87
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRMSTemplate(this.Identity.ToString());
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0010AD9C File Offset: 0x00108F9C
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (this.Organization != null)
			{
				ADObjectId rootOrgContainerId = ADSystemConfigurationSession.GetRootOrgContainerId(this.DomainController, null);
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(rootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, ConfigScopes.TenantSubTree, 105, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\rms\\SetRmsTemplate.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				base.CurrentOrganizationId = adorganizationalUnit.OrganizationId;
			}
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x0010AE58 File Offset: 0x00109058
		protected override IConfigDataProvider CreateSession()
		{
			if (OrganizationId.ForestWideOrgId.Equals(base.CurrentOrganizationId))
			{
				base.WriteError(new ArgumentException(Strings.TenantOrganizationMissing, string.Empty), (ErrorCategory)1000, null);
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			ADSessionSettings sessionSettings = ADSessionSettings.FromCustomScopeSet(base.ScopeSet, rootOrgContainerIdForLocalForest, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 149, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\rms\\SetRmsTemplate.cs");
			return new RmsTemplateDataProvider(tenantOrTopologyConfigurationSession, RmsTemplateType.All, true);
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x0010AEE8 File Offset: 0x001090E8
		protected override IConfigurable PrepareDataObject()
		{
			IConfigurable[] array = ((RmsTemplateDataProvider)base.DataSession).Find<RmsTemplatePresentation>(new RmsTemplateQueryFilter(this.Identity.TemplateId, this.Identity.TemplateName), null, false, null);
			if (array == null || array.Length == 0)
			{
				base.WriteError(new RmsTemplateNotFoundException(this.Identity.ToString()), (ErrorCategory)1000, this.Identity);
			}
			return array[0];
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x0010AF50 File Offset: 0x00109150
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				TaskLogger.LogExit();
				return;
			}
			Guid templateId = ((RmsTemplateIdentity)this.DataObject.Identity).TemplateId;
			if (templateId == RmsTemplate.DoNotForward.Id || templateId == RmsTemplate.InternetConfidential.Id)
			{
				base.WriteError(new CannotModifyOneOffTemplatesException(), (ErrorCategory)1000, this.Identity);
			}
			if (this.Type != RmsTemplateType.Archived && this.Type != RmsTemplateType.Distributed)
			{
				base.WriteError(new TemplateTypeNotValidException(this.Type.ToString()), (ErrorCategory)1000, this.Type);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x0010B008 File Offset: 0x00109208
		protected override void InternalProcessRecord()
		{
			if (this.DataObject.Type == this.Type)
			{
				this.WriteWarning(Strings.WarningTemplateNotModified(this.DataObject.Identity.ToString()));
				return;
			}
			if (this.DataObject.Type == RmsTemplateType.Distributed && this.Type == RmsTemplateType.Archived)
			{
				this.WriteWarning(Strings.WarningChangeTemplateState(this.DataObject.Identity.ToString()));
			}
			this.DataObject.Type = this.Type;
			base.InternalProcessRecord();
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x0010B08C File Offset: 0x0010928C
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception);
		}
	}
}
