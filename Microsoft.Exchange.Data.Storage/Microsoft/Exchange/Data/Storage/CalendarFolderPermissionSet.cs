using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000396 RID: 918
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarFolderPermissionSet : PermissionSet, IEnumerable<CalendarFolderPermission>, IEnumerable<Permission>, IEnumerable
	{
		// Token: 0x06002899 RID: 10393 RVA: 0x000A207B File Offset: 0x000A027B
		public CalendarFolderPermissionSet(PermissionTable permissionTable) : base(permissionTable)
		{
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000A2084 File Offset: 0x000A0284
		public new CalendarFolderPermission AddEntry(PermissionSecurityPrincipal securityPrincipal, PermissionLevel initialPermissionLevel)
		{
			return (CalendarFolderPermission)base.AddEntry(securityPrincipal, initialPermissionLevel);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000A2093 File Offset: 0x000A0293
		public new CalendarFolderPermission GetEntry(PermissionSecurityPrincipal securityPrincipal)
		{
			return (CalendarFolderPermission)base.GetEntry(securityPrincipal);
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600289C RID: 10396 RVA: 0x000A20A1 File Offset: 0x000A02A1
		public new CalendarFolderPermission DefaultPermission
		{
			get
			{
				return (CalendarFolderPermission)base.DefaultPermission;
			}
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000A20AE File Offset: 0x000A02AE
		internal override Permission CreatePermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights)
		{
			return new CalendarFolderPermission(securityPrincipal, memberRights);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000A20B7 File Offset: 0x000A02B7
		internal override Permission CreatePermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights, long memberId)
		{
			return new CalendarFolderPermission(securityPrincipal, memberRights, memberId);
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x000A20C1 File Offset: 0x000A02C1
		internal override ModifyTableOptions ModifyTableOptions
		{
			get
			{
				return base.ModifyTableOptions | ModifyTableOptions.FreeBusyAware;
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000A20CB File Offset: 0x000A02CB
		public new IEnumerator<CalendarFolderPermission> GetEnumerator()
		{
			return new DownCastEnumerator<Permission, CalendarFolderPermission>(base.PermissionTable.GetEnumerator());
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000A20DD File Offset: 0x000A02DD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x000A20E5 File Offset: 0x000A02E5
		IEnumerator<Permission> IEnumerable<Permission>.GetEnumerator()
		{
			return base.PermissionTable.GetEnumerator();
		}
	}
}
