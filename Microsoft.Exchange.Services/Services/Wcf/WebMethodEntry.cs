using System;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DAB RID: 3499
	internal class WebMethodEntry
	{
		// Token: 0x060058C8 RID: 22728 RVA: 0x001145DA File Offset: 0x001127DA
		public WebMethodEntry(string name, string category, string alternateSoapRequest, bool isPublic, bool requireUserAdminRole, bool isPartnerAppOnly, string defaultUserRoleType)
		{
			this.Name = name;
			this.Category = category;
			this.AlternateSoapRequestName = alternateSoapRequest;
			this.IsPublic = isPublic;
			this.RequireUserAdminRole = requireUserAdminRole;
			this.IsPartnerAppOnly = isPartnerAppOnly;
			this.DefaultUserRoleType = defaultUserRoleType;
		}

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060058C9 RID: 22729 RVA: 0x00114617 File Offset: 0x00112817
		// (set) Token: 0x060058CA RID: 22730 RVA: 0x0011461F File Offset: 0x0011281F
		public string Name { get; private set; }

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060058CB RID: 22731 RVA: 0x00114628 File Offset: 0x00112828
		// (set) Token: 0x060058CC RID: 22732 RVA: 0x00114630 File Offset: 0x00112830
		public string Category { get; private set; }

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060058CD RID: 22733 RVA: 0x00114639 File Offset: 0x00112839
		// (set) Token: 0x060058CE RID: 22734 RVA: 0x00114641 File Offset: 0x00112841
		public string AlternateSoapRequestName { get; private set; }

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x060058CF RID: 22735 RVA: 0x0011464A File Offset: 0x0011284A
		// (set) Token: 0x060058D0 RID: 22736 RVA: 0x00114652 File Offset: 0x00112852
		public bool IsPublic { get; private set; }

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x0011465B File Offset: 0x0011285B
		// (set) Token: 0x060058D2 RID: 22738 RVA: 0x00114663 File Offset: 0x00112863
		public bool RequireUserAdminRole { get; private set; }

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x0011466C File Offset: 0x0011286C
		// (set) Token: 0x060058D4 RID: 22740 RVA: 0x00114674 File Offset: 0x00112874
		public bool IsPartnerAppOnly { get; private set; }

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x0011467D File Offset: 0x0011287D
		// (set) Token: 0x060058D6 RID: 22742 RVA: 0x00114685 File Offset: 0x00112885
		public string DefaultUserRoleType { get; private set; }

		// Token: 0x04003142 RID: 12610
		internal static readonly WebMethodEntry JsonWebMethodEntry = new WebMethodEntry("json", "JSON category", "json", true, false, false, null);
	}
}
