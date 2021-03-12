using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000229 RID: 553
	internal class TenantCookieFilter : BaseCookieFilter
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00045A04 File Offset: 0x00043C04
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00045A16 File Offset: 0x00043C16
		public ADObjectId TenantId
		{
			get
			{
				return this[TenantCookieSchema.TenantIdProp] as ADObjectId;
			}
			set
			{
				this[TenantCookieSchema.TenantIdProp] = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00045A24 File Offset: 0x00043C24
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x00045A36 File Offset: 0x00043C36
		public bool Completed
		{
			get
			{
				return (bool)this[BaseCookieSchema.CompleteProp];
			}
			set
			{
				this[BaseCookieSchema.CompleteProp] = value;
			}
		}

		// Token: 0x04000B5C RID: 2908
		public static readonly TenantCookieFilter Default = new TenantCookieFilter();
	}
}
