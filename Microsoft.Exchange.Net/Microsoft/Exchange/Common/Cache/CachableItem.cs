using System;

namespace Microsoft.Exchange.Common.Cache
{
	// Token: 0x02000671 RID: 1649
	[Serializable]
	internal abstract class CachableItem
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001DDA RID: 7642
		public abstract long ItemSize { get; }

		// Token: 0x06001DDB RID: 7643 RVA: 0x000367E3 File Offset: 0x000349E3
		public virtual bool IsExpired(DateTime currentTime)
		{
			return false;
		}
	}
}
