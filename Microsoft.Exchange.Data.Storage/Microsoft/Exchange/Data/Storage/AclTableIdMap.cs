using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000646 RID: 1606
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AclTableIdMap
	{
		// Token: 0x06004235 RID: 16949 RVA: 0x0011A99C File Offset: 0x00118B9C
		public long GetIdForSecurityIdentifier(SecurityIdentifier securityIdentifier, List<SecurityIdentifier> sidHistory)
		{
			long num;
			if (this.securityIdentifierToIdMap.TryGetValue(securityIdentifier, out num))
			{
				return num;
			}
			if (sidHistory != null)
			{
				foreach (SecurityIdentifier key in sidHistory)
				{
					if (this.securityIdentifierToIdMap.TryGetValue(key, out num))
					{
						return num;
					}
				}
			}
			long num2;
			this.nextId = (num2 = this.nextId) + 1L;
			num = num2;
			this.securityIdentifierToIdMap[securityIdentifier] = num;
			if (sidHistory != null)
			{
				foreach (SecurityIdentifier key2 in sidHistory)
				{
					this.securityIdentifierToIdMap[key2] = num;
				}
			}
			return num;
		}

		// Token: 0x0400247F RID: 9343
		private readonly Dictionary<SecurityIdentifier, long> securityIdentifierToIdMap = new Dictionary<SecurityIdentifier, long>();

		// Token: 0x04002480 RID: 9344
		private long nextId = 1000L;
	}
}
