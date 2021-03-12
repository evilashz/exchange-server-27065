using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Common
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[Serializable]
	public class LocalizedDisplayNameAttribute : DisplayNameAttribute, ILocalizedString
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003627 File Offset: 0x00001827
		public LocalizedDisplayNameAttribute()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000362F File Offset: 0x0000182F
		public LocalizedDisplayNameAttribute(LocalizedString displayname)
		{
			this.displayname = displayname;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x0000363E File Offset: 0x0000183E
		public sealed override string DisplayName
		{
			[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
			get
			{
				return this.displayname;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000364B File Offset: 0x0000184B
		public LocalizedString LocalizedString
		{
			get
			{
				return this.displayname;
			}
		}

		// Token: 0x0400002A RID: 42
		private LocalizedString displayname;
	}
}
