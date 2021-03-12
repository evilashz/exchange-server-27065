using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000073 RID: 115
	public abstract class NewRecipientObjectTaskBase<TDataObject> : NewADTaskBase<TDataObject> where TDataObject : ADRecipient, new()
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001047E File Offset: 0x0000E67E
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00010495 File Offset: 0x0000E695
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

		// Token: 0x0600048C RID: 1164 RVA: 0x000104A8 File Offset: 0x0000E6A8
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 355, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
			tenantOrRootOrgRecipientSession.LinkResolutionServer = ADSession.GetCurrentConfigDC(base.SessionSettings.GetAccountOrResourceForestFqdn());
			tenantOrRootOrgRecipientSession.UseGlobalCatalog = false;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00010578 File Offset: 0x0000E778
		protected override OrganizationId ResolveCurrentOrganization()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
			IConfigurationSession session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, sessionSettings, 387, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Configuration\\src\\ObjectModel\\BaseTasks\\NewAdObjectTask.cs");
			session.UseConfigNC = false;
			session.UseGlobalCatalog = (base.ServerSettings.ViewEntireForest && null == base.DomainController);
			if (this.Organization != null)
			{
				base.CurrentOrganizationId = base.ProvisioningCache.TryAddAndGetGlobalDictionaryValue<OrganizationId, string>(CannedProvisioningCacheKeys.OrganizationIdDictionary, this.Organization.RawIdentity, delegate()
				{
					ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)this.GetDataObject<ADOrganizationalUnit>(this.Organization, session, null, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())), ExchangeErrorCategory.Client);
					return adorganizationalUnit.OrganizationId;
				});
			}
			return base.CurrentOrganizationId;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001064C File Offset: 0x0000E84C
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			TDataObject tdataObject = (TDataObject)((object)base.PrepareDataObject());
			if (tdataObject.ObjectCategory == null && this.ConfigurationSession.SchemaNamingContext != null)
			{
				tdataObject.ObjectCategory = this.ConfigurationSession.SchemaNamingContext.GetChildId(tdataObject.ObjectCategoryCN);
			}
			this.PrepareRecipientObject(tdataObject);
			RecipientTaskHelper.RemoveEmptyValueFromEmailAddresses(tdataObject);
			TaskLogger.LogExit();
			return tdataObject;
		}

		// Token: 0x0600048F RID: 1167
		protected abstract void PrepareRecipientObject(TDataObject dataObject);

		// Token: 0x06000490 RID: 1168 RVA: 0x000106CD File Offset: 0x0000E8CD
		protected override void WriteResult(IConfigurable result)
		{
			this.WriteResult((ADObject)result);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000106DB File Offset: 0x0000E8DB
		protected virtual void WriteResult(ADObject result)
		{
			base.WriteResult(result);
		}
	}
}
