using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018F RID: 399
	[Serializable]
	public sealed class NextHopSolutionKeyId : ObjectId
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x00047655 File Offset: 0x00045855
		public NextHopSolutionKeyId(Guid uniqueIdentifier)
		{
			this.uniqueIdentifier = uniqueIdentifier;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00047664 File Offset: 0x00045864
		public override byte[] GetBytes()
		{
			return this.uniqueIdentifier.ToByteArray();
		}

		// Token: 0x04000950 RID: 2384
		public static NextHopSolutionKeyId DefaultNextHopSolutionKeyId = new NextHopSolutionKeyId(Guid.Empty);

		// Token: 0x04000951 RID: 2385
		private readonly Guid uniqueIdentifier;
	}
}
