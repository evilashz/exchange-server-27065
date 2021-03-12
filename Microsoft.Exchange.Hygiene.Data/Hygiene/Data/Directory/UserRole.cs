using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000116 RID: 278
	internal class UserRole : Role
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x000214A0 File Offset: 0x0001F6A0
		public UserRole()
		{
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000214A8 File Offset: 0x0001F6A8
		public UserRole(ADObjectId tenantId, ADObjectId userId, string roleGroupName)
		{
			this.TenantId = tenantId;
			this.UserId = userId;
			base.RoleGroupName = roleGroupName;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x000214C5 File Offset: 0x0001F6C5
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x000214D7 File Offset: 0x0001F6D7
		public ADObjectId TenantId
		{
			get
			{
				return this[ADObjectSchema.OrganizationalUnitRoot] as ADObjectId;
			}
			internal set
			{
				this[ADObjectSchema.OrganizationalUnitRoot] = value;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000214E5 File Offset: 0x0001F6E5
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x000214F7 File Offset: 0x0001F6F7
		public ADObjectId UserId
		{
			get
			{
				return (ADObjectId)this[UserRole.UserIdDef];
			}
			internal set
			{
				this[UserRole.UserIdDef] = value;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00021505 File Offset: 0x0001F705
		public bool IsCannedRole
		{
			get
			{
				return (bool)this[UserRole.IsCannedRoleDef];
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00021517 File Offset: 0x0001F717
		public bool IsUserRole
		{
			get
			{
				return (bool)this[UserRole.IsUserRoleDef];
			}
		}

		// Token: 0x0400057B RID: 1403
		internal static readonly HygienePropertyDefinition UserIdDef = new HygienePropertyDefinition("userId", typeof(ADObjectId));

		// Token: 0x0400057C RID: 1404
		internal static readonly HygienePropertyDefinition IsCannedRoleDef = new HygienePropertyDefinition("isCannedRole", typeof(bool));

		// Token: 0x0400057D RID: 1405
		internal static readonly HygienePropertyDefinition IsUserRoleDef = new HygienePropertyDefinition("isUserRole", typeof(bool));
	}
}
