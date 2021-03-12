using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200005E RID: 94
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct LongTermIdRange
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000A493 File Offset: 0x00008693
		public StoreLongTermId MinLongTermId
		{
			get
			{
				return this.minLongTermId;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A49B File Offset: 0x0000869B
		public StoreLongTermId MaxLongTermId
		{
			get
			{
				return this.maxLongTermId;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public static implicit operator GuidGlobCountSet(LongTermIdRange longTermIdRange)
		{
			GuidGlobCount guidGlobCount = longTermIdRange.MinLongTermId;
			GuidGlobCount guidGlobCount2 = longTermIdRange.MaxLongTermId;
			GlobCountRange range = new GlobCountRange(guidGlobCount.GlobCount, guidGlobCount2.GlobCount);
			GlobCountSet globCountSet = new GlobCountSet();
			globCountSet.Insert(range);
			return new GuidGlobCountSet(guidGlobCount.Guid, globCountSet);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A4FB File Offset: 0x000086FB
		public override string ToString()
		{
			return string.Format("{0}: [{1}]-[{2}]", this.IsValid() ? "Valid" : "Invalid", this.minLongTermId, this.maxLongTermId);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A534 File Offset: 0x00008734
		public bool IsValid()
		{
			return this.MinLongTermId.Guid == this.MaxLongTermId.Guid && this.MinLongTermId <= this.MaxLongTermId && !ArrayComparer<byte>.Comparer.Equals(this.MinLongTermId.GlobCount, StoreLongTermId.Null.GlobCount);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public LongTermIdRange(StoreLongTermId minLongTermId, StoreLongTermId maxLongTermId)
		{
			this.minLongTermId = minLongTermId;
			this.maxLongTermId = maxLongTermId;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A5B1 File Offset: 0x000087B1
		internal static LongTermIdRange Parse(Reader reader)
		{
			return new LongTermIdRange(StoreLongTermId.Parse(reader), StoreLongTermId.Parse(reader));
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A5C4 File Offset: 0x000087C4
		internal void Serialize(Writer writer)
		{
			this.minLongTermId.Serialize(writer);
			this.maxLongTermId.Serialize(writer);
		}

		// Token: 0x0400012B RID: 299
		internal const int Size = 48;

		// Token: 0x0400012C RID: 300
		private readonly StoreLongTermId minLongTermId;

		// Token: 0x0400012D RID: 301
		private readonly StoreLongTermId maxLongTermId;
	}
}
