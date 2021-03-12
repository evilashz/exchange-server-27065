using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000643 RID: 1603
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FlagsMan
	{
		// Token: 0x0600420A RID: 16906 RVA: 0x00119560 File Offset: 0x00117760
		public static bool IsAutoTagSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.Autotag) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x0011959C File Offset: 0x0011779C
		public static bool IsUserOverrideSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.UserOverride) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x001195D8 File Offset: 0x001177D8
		public static bool IsExplicitSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.ExplicitTag) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x00119614 File Offset: 0x00117814
		public static bool IsPersonalTagSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.PersonalTag) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x0600420E RID: 16910 RVA: 0x00119650 File Offset: 0x00117850
		public static bool IsSystemDataSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.SystemData) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x0011968C File Offset: 0x0011788C
		public static bool IsExplicitArchiveSet(object retentionFlagsObject)
		{
			if (!(retentionFlagsObject is int) && !(retentionFlagsObject is RetentionAndArchiveFlags))
			{
				return false;
			}
			RetentionAndArchiveFlags retentionAndArchiveFlags = (RetentionAndArchiveFlags)retentionFlagsObject;
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(retentionAndArchiveFlags, "retentionFlags");
			return (retentionAndArchiveFlags & RetentionAndArchiveFlags.ExplictArchiveTag) != RetentionAndArchiveFlags.None;
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x001196C7 File Offset: 0x001178C7
		public static bool DoesFolderNeedRescan(int flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>((RetentionAndArchiveFlags)flags, "flags");
			return (flags & 384) != 0;
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x001196E1 File Offset: 0x001178E1
		public static RetentionAndArchiveFlags ClearNeedRescan(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags & ~RetentionAndArchiveFlags.NeedsRescan;
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x001196F5 File Offset: 0x001178F5
		public static int ClearPendingRescan(int flags)
		{
			return flags & -257;
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x001196FE File Offset: 0x001178FE
		public static int SetNeedRescan(int flags)
		{
			return flags | 128;
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x00119707 File Offset: 0x00117907
		public static RetentionAndArchiveFlags SetPendingRescan(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags | RetentionAndArchiveFlags.PendingRescan;
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x0011971B File Offset: 0x0011791B
		public static int ClearAutoTag(int retentionFlags)
		{
			return retentionFlags & -5;
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x00119721 File Offset: 0x00117921
		public static int ClearUserOverride(int retentionFlags)
		{
			return retentionFlags & -3;
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x00119727 File Offset: 0x00117927
		public static int ClearExplicit(int retentionFlags)
		{
			return retentionFlags & -2;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x0011972D File Offset: 0x0011792D
		public static int ClearPersonalTag(int retentionFlags)
		{
			return retentionFlags & -9;
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x00119734 File Offset: 0x00117934
		public static int SetAutoTag(int? retentionFlags)
		{
			return (retentionFlags ?? 0) | 4;
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x00119758 File Offset: 0x00117958
		public static RetentionAndArchiveFlags ClearAllRetentionFlags(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags & ~(RetentionAndArchiveFlags.ExplicitTag | RetentionAndArchiveFlags.UserOverride | RetentionAndArchiveFlags.Autotag | RetentionAndArchiveFlags.PersonalTag);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x00119769 File Offset: 0x00117969
		public static int SetExplicit(int retentionFlags)
		{
			return retentionFlags | 1;
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0011976E File Offset: 0x0011796E
		public static int SetPersonalTag(int retentionFlags)
		{
			return retentionFlags | 8;
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x00119773 File Offset: 0x00117973
		public static int SetSystemData(int retentionFlags)
		{
			return retentionFlags | 64;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x00119779 File Offset: 0x00117979
		public static RetentionAndArchiveFlags ClearAllArchiveFlags(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags & ~(RetentionAndArchiveFlags.ExplictArchiveTag | RetentionAndArchiveFlags.KeepInPlace);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0011978A File Offset: 0x0011798A
		public static RetentionAndArchiveFlags SetExplicitArchiveFlag(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags | RetentionAndArchiveFlags.ExplictArchiveTag;
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0011979B File Offset: 0x0011799B
		public static RetentionAndArchiveFlags ClearExplicitArchiveFlag(RetentionAndArchiveFlags flags)
		{
			EnumValidator.ThrowIfInvalid<RetentionAndArchiveFlags>(flags, "flags");
			return flags & ~RetentionAndArchiveFlags.ExplictArchiveTag;
		}
	}
}
