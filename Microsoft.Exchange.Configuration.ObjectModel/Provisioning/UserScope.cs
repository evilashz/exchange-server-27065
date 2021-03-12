using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001FF RID: 511
	public class UserScope
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00037833 File Offset: 0x00035A33
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0003783B File Offset: 0x00035A3B
		internal ScopeSet CurrentScopeSet { get; private set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00037844 File Offset: 0x00035A44
		public string UserId
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0003784C File Offset: 0x00035A4C
		public OrganizationId ExecutingUserOrganizationId
		{
			get
			{
				return this.executingUserOrganizationId;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00037854 File Offset: 0x00035A54
		public OrganizationId CurrentOrganizationId
		{
			get
			{
				return this.currentOrganizationId;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x0003785C File Offset: 0x00035A5C
		public UserScopeFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00037864 File Offset: 0x00035A64
		internal UserScope(string userId, OrganizationId executingUserOrganizationId, OrganizationId currentOrganizationId, UserScopeFlags flags, ScopeSet currentScopeSet)
		{
			this.userId = userId;
			this.executingUserOrganizationId = executingUserOrganizationId;
			this.currentOrganizationId = currentOrganizationId;
			this.CurrentScopeSet = currentScopeSet;
			this.flags = flags;
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00037891 File Offset: 0x00035A91
		public UserScope(string userId, OrganizationId executingUserOrganizationId, OrganizationId currentOrganizationId, UserScopeFlags flags) : this(userId, executingUserOrganizationId, currentOrganizationId, flags, null)
		{
		}

		// Token: 0x0400043D RID: 1085
		private string userId;

		// Token: 0x0400043E RID: 1086
		private OrganizationId executingUserOrganizationId;

		// Token: 0x0400043F RID: 1087
		private OrganizationId currentOrganizationId;

		// Token: 0x04000440 RID: 1088
		private UserScopeFlags flags;
	}
}
