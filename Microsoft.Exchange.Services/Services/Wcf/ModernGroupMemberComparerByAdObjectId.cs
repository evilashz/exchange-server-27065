using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000901 RID: 2305
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ModernGroupMemberComparerByAdObjectId : IEqualityComparer<ModernGroupMemberType>
	{
		// Token: 0x060042F9 RID: 17145 RVA: 0x000DF5C6 File Offset: 0x000DD7C6
		private ModernGroupMemberComparerByAdObjectId()
		{
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000DF5D0 File Offset: 0x000DD7D0
		public bool Equals(ModernGroupMemberType member1, ModernGroupMemberType member2)
		{
			bool flag = member1 == null || member1.Persona == null;
			bool flag2 = member2 == null || member2.Persona == null;
			return (flag && flag2) || (!flag && !flag2 && member1.Persona.ADObjectId == member2.Persona.ADObjectId);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000DF62C File Offset: 0x000DD82C
		public int GetHashCode(ModernGroupMemberType member)
		{
			if (member == null || member.Persona == null)
			{
				return 0;
			}
			return member.Persona.ADObjectId.GetHashCode();
		}

		// Token: 0x0400270B RID: 9995
		internal static readonly ModernGroupMemberComparerByAdObjectId Singleton = new ModernGroupMemberComparerByAdObjectId();
	}
}
