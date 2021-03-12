using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000058 RID: 88
	internal struct GuidGlobCountSet : IEquatable<GuidGlobCountSet>
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000958A File Offset: 0x0000778A
		public GuidGlobCountSet(Guid guid, GlobCountSet globCountSet)
		{
			this.guid = guid;
			this.globCountSet = globCountSet;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000959A File Offset: 0x0000779A
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000095A2 File Offset: 0x000077A2
		public GlobCountSet GlobCountSet
		{
			get
			{
				return this.globCountSet;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000095AA File Offset: 0x000077AA
		public bool IsEmpty
		{
			get
			{
				return this.globCountSet == null || this.globCountSet.IsEmpty;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000095C1 File Offset: 0x000077C1
		public int CountRanges
		{
			get
			{
				if (this.globCountSet != null)
				{
					return this.globCountSet.CountRanges;
				}
				return 0;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000095D8 File Offset: 0x000077D8
		public ulong CountIds
		{
			get
			{
				if (this.globCountSet != null)
				{
					return this.globCountSet.CountIds;
				}
				return 0UL;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000095F0 File Offset: 0x000077F0
		public GuidGlobCountSet Clone()
		{
			return new GuidGlobCountSet(this.guid, this.globCountSet.Clone());
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009608 File Offset: 0x00007808
		public override string ToString()
		{
			return string.Format("{0}:{1}", this.guid, this.globCountSet);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009628 File Offset: 0x00007828
		public bool Equals(GuidGlobCountSet other)
		{
			return this.guid.Equals(other.guid) && this.globCountSet.Equals(other.globCountSet);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009660 File Offset: 0x00007860
		public override bool Equals(object obj)
		{
			return obj is GuidGlobCountSet && this.Equals((GuidGlobCountSet)obj);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009678 File Offset: 0x00007878
		public override int GetHashCode()
		{
			return this.guid.GetHashCode() ^ this.globCountSet.GetHashCode();
		}

		// Token: 0x04000117 RID: 279
		private readonly Guid guid;

		// Token: 0x04000118 RID: 280
		private readonly GlobCountSet globCountSet;
	}
}
