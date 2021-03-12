using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025B RID: 603
	internal sealed class SuccessfulGetPerUserLongTermIdsResult : RopResult
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x00028490 File Offset: 0x00026690
		internal SuccessfulGetPerUserLongTermIdsResult(StoreLongTermId[] longTermIds) : base(RopId.GetPerUserLongTermIds, ErrorCode.None, null)
		{
			if (longTermIds == null)
			{
				throw new ArgumentNullException("longTermIds");
			}
			this.longTermIds = longTermIds;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000284B1 File Offset: 0x000266B1
		internal SuccessfulGetPerUserLongTermIdsResult(Reader reader) : base(reader)
		{
			this.longTermIds = reader.ReadSizeAndStoreLongTermIdArray();
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x000284C6 File Offset: 0x000266C6
		internal StoreLongTermId[] LongTermIds
		{
			get
			{
				return this.longTermIds;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000284CE File Offset: 0x000266CE
		internal static SuccessfulGetPerUserLongTermIdsResult Parse(Reader reader)
		{
			return new SuccessfulGetPerUserLongTermIdsResult(reader);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000284D6 File Offset: 0x000266D6
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteCountedStoreLongTermIds(this.LongTermIds);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000284EB File Offset: 0x000266EB
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" LongTermIds=[");
			Util.AppendToString<StoreLongTermId>(stringBuilder, this.longTermIds);
			stringBuilder.Append("]");
		}

		// Token: 0x040006FD RID: 1789
		private readonly StoreLongTermId[] longTermIds;
	}
}
