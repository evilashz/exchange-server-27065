using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000393 RID: 915
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CalendarFolderPermission : Permission
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x000A1DD1 File Offset: 0x0009FFD1
		internal CalendarFolderPermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights, long memberId) : base(securityPrincipal, memberRights, memberId)
		{
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000A1DDC File Offset: 0x0009FFDC
		internal CalendarFolderPermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights) : base(securityPrincipal, memberRights)
		{
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x000A1DE6 File Offset: 0x0009FFE6
		// (set) Token: 0x06002886 RID: 10374 RVA: 0x000A1E14 File Offset: 0x000A0014
		public FreeBusyAccess FreeBusyAccess
		{
			get
			{
				if ((base.MemberRights & MemberRights.FreeBusyDetailed) == MemberRights.FreeBusyDetailed)
				{
					return FreeBusyAccess.Details;
				}
				if ((base.MemberRights & MemberRights.FreeBusySimple) == MemberRights.FreeBusySimple)
				{
					return FreeBusyAccess.Basic;
				}
				return FreeBusyAccess.None;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<FreeBusyAccess>(value, "value");
				switch (value)
				{
				case FreeBusyAccess.None:
					base.MemberRights &= ~(MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed);
					return;
				case FreeBusyAccess.Basic:
					base.MemberRights |= MemberRights.FreeBusySimple;
					base.MemberRights &= ~MemberRights.FreeBusyDetailed;
					return;
				case FreeBusyAccess.Details:
					base.MemberRights |= (MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000A1E8B File Offset: 0x000A008B
		protected override void ValidateMemberRights(MemberRights memberRights)
		{
			EnumValidator.ThrowIfInvalid<MemberRights>(memberRights, "memberRights");
		}
	}
}
