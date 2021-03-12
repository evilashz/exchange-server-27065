using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x02000261 RID: 609
	internal class SnapInAliasMap
	{
		// Token: 0x06001540 RID: 5440 RVA: 0x0004EA00 File Offset: 0x0004CC00
		internal static void AddAliasMappingForSnapIn(string alias, string snapinName)
		{
			if (SnapInAliasMap.snapInAliasMap.ContainsKey(alias))
			{
				throw new ArgumentException("Mapping is already provided for alias :" + alias);
			}
			SnapInAliasMap.snapInAliasMap[alias] = snapinName;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		internal static string GetSnapInName(string alias)
		{
			if (SnapInAliasMap.snapInAliasMap.Count > 0 && SnapInAliasMap.snapInAliasMap.ContainsKey(alias))
			{
				return SnapInAliasMap.snapInAliasMap[alias];
			}
			return alias;
		}

		// Token: 0x04000663 RID: 1635
		private static Dictionary<string, string> snapInAliasMap = new Dictionary<string, string>();
	}
}
