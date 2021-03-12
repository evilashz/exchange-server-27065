using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200006A RID: 106
	[Cmdlet("Get", "RemovedMailbox")]
	public sealed class GetRemovedMailbox : GetTenantADObjectWithIdentityTaskBase<RemovedMailboxIdParameter, RemovedMailbox>
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001F967 File Offset: 0x0001DB67
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001F96F File Offset: 0x0001DB6F
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001F986 File Offset: 0x0001DB86
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

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001F999 File Offset: 0x0001DB99
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001F9A1 File Offset: 0x0001DBA1
		[Parameter]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return base.InternalResultSize;
			}
			set
			{
				base.InternalResultSize = value;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		protected override IConfigDataProvider CreateSession()
		{
			return MailboxTaskHelper.GetSessionForDeletedObjects(base.DomainController, base.CurrentOrganizationId, base.ExecutingUserOrganizationId);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001F9C3 File Offset: 0x0001DBC3
		protected override bool ShouldSupportPreResolveOrgIdBasedOnIdentity()
		{
			return true;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.ExecutingUserOrganizationId, null, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, sessionSettings, ConfigScopes.TenantSubTree, 99, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\mailbox\\GetRemovedMailbox.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				this.rootId = adorganizationalUnit.OrganizationId.OrganizationalUnit;
				return adorganizationalUnit.OrganizationId;
			}
			OrganizationId organizationId = base.ResolveCurrentOrganization();
			this.rootId = organizationId.OrganizationalUnit;
			return organizationId;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001FA94 File Offset: 0x0001DC94
		protected override IEnumerable<RemovedMailbox> GetPagedData()
		{
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(RemovedMailbox), this.InternalFilter, null, this.DeepSearch));
			RemovedMailboxIdParameter removedMailboxIdParameter = new RemovedMailboxIdParameter("*");
			return removedMailboxIdParameter.GetObjects<RemovedMailbox>(this.RootId, base.DataSession);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return (RemovedMailbox)dataObject;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001FB02 File Offset: 0x0001DD02
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(this.ConvertDataObjectToPresentationObject(dataObject));
		}

		// Token: 0x040001A3 RID: 419
		private ADObjectId rootId;
	}
}
