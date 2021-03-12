using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000395 RID: 917
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PermissionSet : IEnumerable<Permission>, IEnumerable
	{
		// Token: 0x06002888 RID: 10376 RVA: 0x000A1E98 File Offset: 0x000A0098
		public PermissionSet(PermissionTable permissionTable)
		{
			this.permissionTable = permissionTable;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000A1EA8 File Offset: 0x000A00A8
		public Permission AddEntry(PermissionSecurityPrincipal securityPrincipal, PermissionLevel initialPermissionLevel)
		{
			if (!EnumValidator<PermissionLevel>.IsMemberOf(initialPermissionLevel, PermissionSet.ValidPermissionLevelForAddEntry))
			{
				throw new EnumOutOfRangeException("initialPermissionLevel", ServerStrings.BadEnumValue(typeof(PermissionLevel)));
			}
			Permission permission = this.PermissionTable.AddEntry(securityPrincipal, MemberRights.None);
			permission.PermissionLevel = initialPermissionLevel;
			return permission;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000A1EF7 File Offset: 0x000A00F7
		public Permission AddEntry(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights)
		{
			return this.PermissionTable.AddEntry(securityPrincipal, memberRights);
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000A1F06 File Offset: 0x000A0106
		public void RemoveEntry(PermissionSecurityPrincipal securityPrincipal)
		{
			this.PermissionTable.RemoveEntry(securityPrincipal);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000A1F14 File Offset: 0x000A0114
		public void Clear()
		{
			this.PermissionTable.Clear();
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000A1F21 File Offset: 0x000A0121
		public Permission GetEntry(PermissionSecurityPrincipal securityPrincipal)
		{
			return this.PermissionTable.GetEntry(securityPrincipal);
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x000A1F2F File Offset: 0x000A012F
		public Permission DefaultPermission
		{
			get
			{
				return this.PermissionTable.DefaultPermission;
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000A1F3C File Offset: 0x000A013C
		public void SetDefaultPermission(MemberRights memberRights)
		{
			EnumValidator<MemberRights>.ThrowIfInvalid(memberRights, "memberRights");
			if (this.PermissionTable.DefaultPermission != null)
			{
				this.PermissionTable.DefaultPermission.MemberRights = memberRights;
				return;
			}
			this.PermissionTable.DefaultPermission = this.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Default), memberRights, 0L);
			this.PermissionTable.DefaultPermission.MarkAsNew();
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000A1F9D File Offset: 0x000A019D
		public Permission AnonymousPermission
		{
			get
			{
				return this.PermissionTable.AnonymousPermission;
			}
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000A1FAC File Offset: 0x000A01AC
		public void SetAnonymousPermission(MemberRights memberRights)
		{
			EnumValidator<MemberRights>.ThrowIfInvalid(memberRights, "memberRights");
			if (this.PermissionTable.AnonymousPermission != null)
			{
				this.PermissionTable.AnonymousPermission.MemberRights = memberRights;
				return;
			}
			this.PermissionTable.AnonymousPermission = this.CreatePermission(new PermissionSecurityPrincipal(PermissionSecurityPrincipal.SpecialPrincipalType.Anonymous), memberRights, -1L);
			this.PermissionTable.AnonymousPermission.MarkAsNew();
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000A200D File Offset: 0x000A020D
		internal virtual Permission CreatePermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights)
		{
			return new Permission(securityPrincipal, memberRights);
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000A2016 File Offset: 0x000A0216
		internal virtual Permission CreatePermission(PermissionSecurityPrincipal securityPrincipal, MemberRights memberRights, long memberId)
		{
			return new Permission(securityPrincipal, memberRights, memberId);
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000A2020 File Offset: 0x000A0220
		internal virtual ModifyTableOptions ModifyTableOptions
		{
			get
			{
				return ModifyTableOptions.None;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000A2023 File Offset: 0x000A0223
		protected PermissionTable PermissionTable
		{
			get
			{
				return this.permissionTable;
			}
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000A202B File Offset: 0x000A022B
		public IEnumerator<Permission> GetEnumerator()
		{
			return this.PermissionTable.GetEnumerator();
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000A2038 File Offset: 0x000A0238
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040017CD RID: 6093
		private readonly PermissionTable permissionTable;

		// Token: 0x040017CE RID: 6094
		private static readonly PermissionLevel[] ValidPermissionLevelForAddEntry = new PermissionLevel[]
		{
			PermissionLevel.None,
			PermissionLevel.Owner,
			PermissionLevel.PublishingEditor,
			PermissionLevel.Editor,
			PermissionLevel.PublishingAuthor,
			PermissionLevel.Author,
			PermissionLevel.NonEditingAuthor,
			PermissionLevel.Reviewer,
			PermissionLevel.Contributor
		};
	}
}
