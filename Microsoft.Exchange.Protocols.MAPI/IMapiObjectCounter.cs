using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000073 RID: 115
	public interface IMapiObjectCounter
	{
		// Token: 0x0600036B RID: 875
		long GetCount();

		// Token: 0x0600036C RID: 876
		void IncrementCount();

		// Token: 0x0600036D RID: 877
		void DecrementCount();

		// Token: 0x0600036E RID: 878
		void CheckObjectQuota(bool mustBeStrictlyUnderQuota);
	}
}
