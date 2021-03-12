using System;
using Microsoft.Exchange.Collections.TimeoutCache;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006E0 RID: 1760
	internal class BadTokenHashCache : TimeoutCache<string, bool>
	{
		// Token: 0x060035F9 RID: 13817 RVA: 0x000C1BBF File Offset: 0x000BFDBF
		private BadTokenHashCache() : base(20, 5000, false)
		{
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x000C1BCF File Offset: 0x000BFDCF
		internal static BadTokenHashCache Singleton
		{
			get
			{
				return BadTokenHashCache.singleton;
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000C1BD8 File Offset: 0x000BFDD8
		internal static string BuildKey(string wsSecurityToken)
		{
			int length = 33 * wsSecurityToken.Length / 100;
			int startIndex = BadTokenHashCache.RandomStartPointPercentage * wsSecurityToken.Length / 100;
			string text = wsSecurityToken.Substring(startIndex, length);
			return string.Format("F:{0:X8};P:{1:X8}", wsSecurityToken.GetHashCode(), text.GetHashCode());
		}

		// Token: 0x04001E29 RID: 7721
		private const int PartialTokenPercentageToHash = 33;

		// Token: 0x04001E2A RID: 7722
		private static readonly BadTokenHashCache singleton = new BadTokenHashCache();

		// Token: 0x04001E2B RID: 7723
		private static readonly int RandomStartPointPercentage = new Random().Next(5, 66);

		// Token: 0x04001E2C RID: 7724
		internal static readonly TimeSpan CacheTimeout = new TimeSpan(0, 5, 0);
	}
}
