using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Hybrid.Entity
{
	// Token: 0x020008F6 RID: 2294
	internal class OrganizationConfig : IOrganizationConfig
	{
		// Token: 0x1700185E RID: 6238
		// (get) Token: 0x0600514D RID: 20813 RVA: 0x001521F6 File Offset: 0x001503F6
		// (set) Token: 0x0600514E RID: 20814 RVA: 0x001521FE File Offset: 0x001503FE
		public string Name { get; set; }

		// Token: 0x1700185F RID: 6239
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x00152207 File Offset: 0x00150407
		// (set) Token: 0x06005150 RID: 20816 RVA: 0x0015220F File Offset: 0x0015040F
		public Guid Guid { get; set; }

		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06005151 RID: 20817 RVA: 0x00152218 File Offset: 0x00150418
		// (set) Token: 0x06005152 RID: 20818 RVA: 0x00152220 File Offset: 0x00150420
		public ExchangeObjectVersion AdminDisplayVersion { get; set; }

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x06005153 RID: 20819 RVA: 0x00152229 File Offset: 0x00150429
		// (set) Token: 0x06005154 RID: 20820 RVA: 0x00152231 File Offset: 0x00150431
		public bool IsUpgradingOrganization { get; set; }

		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x06005155 RID: 20821 RVA: 0x0015223A File Offset: 0x0015043A
		// (set) Token: 0x06005156 RID: 20822 RVA: 0x00152242 File Offset: 0x00150442
		public string OrganizationConfigHash { get; set; }

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06005157 RID: 20823 RVA: 0x0015224B File Offset: 0x0015044B
		// (set) Token: 0x06005158 RID: 20824 RVA: 0x00152253 File Offset: 0x00150453
		public bool IsDehydrated { get; set; }

		// Token: 0x06005159 RID: 20825 RVA: 0x0015225C File Offset: 0x0015045C
		public override string ToString()
		{
			return this.Name;
		}
	}
}
