using System;

namespace Microsoft.Exchange.Rpc.MultiMailboxSearch
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	internal sealed class PagingReferenceItem : MultiMailboxSearchBase
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x00009CF8 File Offset: 0x000090F8
		internal PagingReferenceItem(int version, byte[] equalsRestriction, byte[] comparisionFilterRestriction) : base(version)
		{
			this.equalsRestriction = equalsRestriction;
			this.comparisionFilterRestriction = comparisionFilterRestriction;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00009CD0 File Offset: 0x000090D0
		internal PagingReferenceItem(byte[] equalsRestriction, byte[] comparisionFilterRestriction) : base(MultiMailboxSearchBase.CurrentVersion)
		{
			this.equalsRestriction = equalsRestriction;
			this.comparisionFilterRestriction = comparisionFilterRestriction;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00009D1C File Offset: 0x0000911C
		internal byte[] EqualsRestriction
		{
			get
			{
				return this.equalsRestriction;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00009D30 File Offset: 0x00009130
		internal byte[] ComparisionRestriction
		{
			get
			{
				return this.comparisionFilterRestriction;
			}
		}

		// Token: 0x04000B06 RID: 2822
		private readonly byte[] equalsRestriction;

		// Token: 0x04000B07 RID: 2823
		private readonly byte[] comparisionFilterRestriction;
	}
}
