using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000126 RID: 294
	internal class DuplicateResultCheck
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00022B83 File Offset: 0x00020D83
		internal DuplicateResultCheck()
		{
			this.resultsHash = new List<ResultWrapper>();
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00022B98 File Offset: 0x00020D98
		internal bool Contains(List<IUMRecognitionPhrase> results)
		{
			ResultWrapper resultWrapper = new ResultWrapper(results);
			if (this.resultsHash.Count == 0)
			{
				this.resultsHash.Add(resultWrapper);
				return false;
			}
			bool flag = false;
			foreach (ResultWrapper resultWrapper2 in this.resultsHash)
			{
				if (resultWrapper2.CompareTo(resultWrapper) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.resultsHash.Add(resultWrapper);
			}
			return flag;
		}

		// Token: 0x0400088C RID: 2188
		private List<ResultWrapper> resultsHash;
	}
}
