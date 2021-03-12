using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000903 RID: 2307
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ModernGroupMemberComparerByOwnerAndDisplayName : IComparer<ModernGroupMemberType>
	{
		// Token: 0x06004300 RID: 17152 RVA: 0x000DF6D8 File Offset: 0x000DD8D8
		private ModernGroupMemberComparerByOwnerAndDisplayName()
		{
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000DF6E0 File Offset: 0x000DD8E0
		public int Compare(ModernGroupMemberType member1, ModernGroupMemberType member2)
		{
			bool flag = member1 == null;
			bool flag2 = member2 == null;
			if (flag && flag2)
			{
				return 0;
			}
			if (flag)
			{
				return 1;
			}
			if (flag2)
			{
				return -1;
			}
			if (member1.IsOwner == member2.IsOwner)
			{
				return ModernGroupMemberComparerByDisplayName.Singleton.Compare(member1, member2);
			}
			if (member1.IsOwner)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x0400270D RID: 9997
		internal static readonly ModernGroupMemberComparerByOwnerAndDisplayName Singleton = new ModernGroupMemberComparerByOwnerAndDisplayName();
	}
}
