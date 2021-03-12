using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000982 RID: 2434
	[Serializable]
	internal sealed class PathUniqueForRcrTargetCondition
	{
		// Token: 0x060056C8 RID: 22216 RVA: 0x00165D0C File Offset: 0x00163F0C
		public static bool Verify(string pathToCheck, List<string> paths)
		{
			if (pathToCheck == null)
			{
				return true;
			}
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			foreach (string strB in paths)
			{
				if (string.Compare(pathToCheck, strB, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return false;
				}
			}
			return true;
		}
	}
}
