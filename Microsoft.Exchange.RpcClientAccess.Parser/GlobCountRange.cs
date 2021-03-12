using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000051 RID: 81
	public struct GlobCountRange : IEquatable<GlobCountRange>
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000753C File Offset: 0x0000573C
		public GlobCountRange(ulong lowBound, ulong highBound)
		{
			GlobCountSet.VerifyGlobCountArgument(lowBound, "lowBound");
			GlobCountSet.VerifyGlobCountArgument(highBound, "highBound");
			if (lowBound > highBound)
			{
				throw new ArgumentException("Lowbound should be less or equal to highbound");
			}
			this.lowBound = lowBound;
			this.highBound = highBound;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00007571 File Offset: 0x00005771
		public ulong LowBound
		{
			get
			{
				return this.lowBound;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007579 File Offset: 0x00005779
		public ulong HighBound
		{
			get
			{
				return this.highBound;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007581 File Offset: 0x00005781
		public override bool Equals(object other)
		{
			return other is GlobCountRange && this.Equals((GlobCountRange)other);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000759C File Offset: 0x0000579C
		public override int GetHashCode()
		{
			return this.lowBound.GetHashCode() ^ this.highBound.GetHashCode();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000075C6 File Offset: 0x000057C6
		public bool Equals(GlobCountRange other)
		{
			return this.highBound == other.highBound && this.lowBound == other.lowBound;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000075E8 File Offset: 0x000057E8
		public GlobCountRange Clone()
		{
			return new GlobCountRange(this.lowBound, this.highBound);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000075FB File Offset: 0x000057FB
		public override string ToString()
		{
			return string.Format("[0x{0:X}, 0x{1:X}]", this.lowBound, this.highBound);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000761D File Offset: 0x0000581D
		internal bool Contains(ulong element)
		{
			return element >= this.lowBound && element <= this.highBound;
		}

		// Token: 0x04000102 RID: 258
		private readonly ulong lowBound;

		// Token: 0x04000103 RID: 259
		private readonly ulong highBound;
	}
}
