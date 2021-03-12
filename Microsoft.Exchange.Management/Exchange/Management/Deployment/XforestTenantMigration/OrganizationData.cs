using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D82 RID: 3458
	[Serializable]
	public sealed class OrganizationData : IConfigurable
	{
		// Token: 0x17002945 RID: 10565
		// (get) Token: 0x060084C1 RID: 33985 RVA: 0x0021E630 File Offset: 0x0021C830
		// (set) Token: 0x060084C2 RID: 33986 RVA: 0x0021E638 File Offset: 0x0021C838
		public string Name { get; set; }

		// Token: 0x17002946 RID: 10566
		// (get) Token: 0x060084C3 RID: 33987 RVA: 0x0021E641 File Offset: 0x0021C841
		// (set) Token: 0x060084C4 RID: 33988 RVA: 0x0021E649 File Offset: 0x0021C849
		public DirectoryObjectCollection OrganizationalUnit { get; set; }

		// Token: 0x17002947 RID: 10567
		// (get) Token: 0x060084C5 RID: 33989 RVA: 0x0021E652 File Offset: 0x0021C852
		// (set) Token: 0x060084C6 RID: 33990 RVA: 0x0021E65A File Offset: 0x0021C85A
		public DirectoryObjectCollection ConfigurationUnit { get; set; }

		// Token: 0x17002948 RID: 10568
		// (get) Token: 0x060084C7 RID: 33991 RVA: 0x0021E663 File Offset: 0x0021C863
		// (set) Token: 0x060084C8 RID: 33992 RVA: 0x0021E66B File Offset: 0x0021C86B
		internal string RootOrgName { get; set; }

		// Token: 0x17002949 RID: 10569
		// (get) Token: 0x060084C9 RID: 33993 RVA: 0x0021E674 File Offset: 0x0021C874
		// (set) Token: 0x060084CA RID: 33994 RVA: 0x0021E67C File Offset: 0x0021C87C
		internal string SourceFqdn { get; set; }

		// Token: 0x1700294A RID: 10570
		// (get) Token: 0x060084CB RID: 33995 RVA: 0x0021E685 File Offset: 0x0021C885
		// (set) Token: 0x060084CC RID: 33996 RVA: 0x0021E68D File Offset: 0x0021C88D
		internal string SourceADSite { get; set; }

		// Token: 0x060084CD RID: 33997 RVA: 0x0021E696 File Offset: 0x0021C896
		public OrganizationData()
		{
			this.Name = null;
			this.OrganizationalUnit = new DirectoryObjectCollection();
			this.ConfigurationUnit = new DirectoryObjectCollection();
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x0021E6BB File Offset: 0x0021C8BB
		public OrganizationData(string name, DirectoryObjectCollection orgUnit, DirectoryObjectCollection configUnit, string rootOrgName, string sourceFqdn, string sourceADSite)
		{
			this.Name = name;
			this.OrganizationalUnit = orgUnit;
			this.ConfigurationUnit = configUnit;
			this.RootOrgName = rootOrgName;
			this.SourceFqdn = sourceFqdn;
			this.SourceADSite = sourceADSite;
		}

		// Token: 0x1700294B RID: 10571
		// (get) Token: 0x060084CF RID: 33999 RVA: 0x0021E6F0 File Offset: 0x0021C8F0
		ObjectId IConfigurable.Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x0021E6F3 File Offset: 0x0021C8F3
		ValidationError[] IConfigurable.Validate()
		{
			return new ValidationError[0];
		}

		// Token: 0x1700294C RID: 10572
		// (get) Token: 0x060084D1 RID: 34001 RVA: 0x0021E6FB File Offset: 0x0021C8FB
		bool IConfigurable.IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700294D RID: 10573
		// (get) Token: 0x060084D2 RID: 34002 RVA: 0x0021E6FE File Offset: 0x0021C8FE
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x0021E701 File Offset: 0x0021C901
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x0021E703 File Offset: 0x0021C903
		void IConfigurable.ResetChangeTracking()
		{
		}
	}
}
