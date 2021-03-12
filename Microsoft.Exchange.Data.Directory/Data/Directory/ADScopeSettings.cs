using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000074 RID: 116
	public class ADScopeSettings
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001E801 File Offset: 0x0001CA01
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0001E809 File Offset: 0x0001CA09
		public OrganizationId Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x04000246 RID: 582
		private OrganizationId scope;
	}
}
