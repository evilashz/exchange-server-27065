using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000902 RID: 2306
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ModernGroupMemberComparerByDisplayName : IComparer<ModernGroupMemberType>
	{
		// Token: 0x060042FD RID: 17149 RVA: 0x000DF66B File Offset: 0x000DD86B
		private ModernGroupMemberComparerByDisplayName()
		{
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000DF674 File Offset: 0x000DD874
		public int Compare(ModernGroupMemberType member1, ModernGroupMemberType member2)
		{
			bool flag = false;
			bool flag2 = false;
			if (member1 == null || member1.Persona == null)
			{
				flag = true;
			}
			if (member2 == null || member2.Persona == null)
			{
				flag2 = true;
			}
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
			return member1.Persona.DisplayName.CompareTo(member2.Persona.DisplayName);
		}

		// Token: 0x0400270C RID: 9996
		internal static readonly ModernGroupMemberComparerByDisplayName Singleton = new ModernGroupMemberComparerByDisplayName();
	}
}
