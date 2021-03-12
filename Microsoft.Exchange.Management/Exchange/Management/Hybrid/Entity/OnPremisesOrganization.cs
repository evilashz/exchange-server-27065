using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008F2 RID: 2290
	internal class OnPremisesOrganization : IOnPremisesOrganization, IEntity<IOnPremisesOrganization>
	{
		// Token: 0x0600512C RID: 20780 RVA: 0x00152064 File Offset: 0x00150264
		public OnPremisesOrganization()
		{
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x0015206C File Offset: 0x0015026C
		public OnPremisesOrganization(Guid organizationGuid, string organizationName, MultiValuedProperty<SmtpDomain> hybridDomains, ADObjectId inboundConnector, ADObjectId outboundConnector, string name, ADObjectId organizationRelationship)
		{
			this.OrganizationGuid = organizationGuid;
			this.OrganizationName = organizationName;
			this.HybridDomains = hybridDomains;
			this.InboundConnector = inboundConnector;
			this.OutboundConnector = outboundConnector;
			this.Name = name;
			this.OrganizationRelationship = organizationRelationship;
		}

		// Token: 0x1700184E RID: 6222
		// (get) Token: 0x0600512E RID: 20782 RVA: 0x001520A9 File Offset: 0x001502A9
		// (set) Token: 0x0600512F RID: 20783 RVA: 0x001520B1 File Offset: 0x001502B1
		public ADObjectId Identity { get; set; }

		// Token: 0x1700184F RID: 6223
		// (get) Token: 0x06005130 RID: 20784 RVA: 0x001520BA File Offset: 0x001502BA
		// (set) Token: 0x06005131 RID: 20785 RVA: 0x001520C2 File Offset: 0x001502C2
		public Guid OrganizationGuid { get; set; }

		// Token: 0x17001850 RID: 6224
		// (get) Token: 0x06005132 RID: 20786 RVA: 0x001520CB File Offset: 0x001502CB
		// (set) Token: 0x06005133 RID: 20787 RVA: 0x001520D3 File Offset: 0x001502D3
		public string OrganizationName { get; set; }

		// Token: 0x17001851 RID: 6225
		// (get) Token: 0x06005134 RID: 20788 RVA: 0x001520DC File Offset: 0x001502DC
		// (set) Token: 0x06005135 RID: 20789 RVA: 0x001520E4 File Offset: 0x001502E4
		public MultiValuedProperty<SmtpDomain> HybridDomains { get; set; }

		// Token: 0x17001852 RID: 6226
		// (get) Token: 0x06005136 RID: 20790 RVA: 0x001520ED File Offset: 0x001502ED
		// (set) Token: 0x06005137 RID: 20791 RVA: 0x001520F5 File Offset: 0x001502F5
		public ADObjectId InboundConnector { get; set; }

		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x001520FE File Offset: 0x001502FE
		// (set) Token: 0x06005139 RID: 20793 RVA: 0x00152106 File Offset: 0x00150306
		public ADObjectId OutboundConnector { get; set; }

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x0600513A RID: 20794 RVA: 0x0015210F File Offset: 0x0015030F
		// (set) Token: 0x0600513B RID: 20795 RVA: 0x00152117 File Offset: 0x00150317
		public string Name { get; set; }

		// Token: 0x17001855 RID: 6229
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x00152120 File Offset: 0x00150320
		// (set) Token: 0x0600513D RID: 20797 RVA: 0x00152128 File Offset: 0x00150328
		public ADObjectId OrganizationRelationship { get; set; }

		// Token: 0x0600513E RID: 20798 RVA: 0x00152131 File Offset: 0x00150331
		public override string ToString()
		{
			if (this.Identity != null)
			{
				return this.Identity.ToString();
			}
			return "<New>";
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x0015214C File Offset: 0x0015034C
		public bool Equals(IOnPremisesOrganization obj)
		{
			return false;
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x00152150 File Offset: 0x00150350
		public IOnPremisesOrganization Clone(ADObjectId identity)
		{
			OnPremisesOrganization onPremisesOrganization = new OnPremisesOrganization();
			onPremisesOrganization.UpdateFrom(this);
			onPremisesOrganization.Identity = identity;
			return onPremisesOrganization;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x00152174 File Offset: 0x00150374
		public void UpdateFrom(IOnPremisesOrganization obj)
		{
			this.OrganizationGuid = obj.OrganizationGuid;
			this.OrganizationName = obj.OrganizationName;
			this.HybridDomains = obj.HybridDomains;
			this.InboundConnector = obj.InboundConnector;
			this.OutboundConnector = obj.OutboundConnector;
			this.Name = obj.Name;
			this.OrganizationRelationship = obj.OrganizationRelationship;
		}
	}
}
