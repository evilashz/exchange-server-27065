using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000368 RID: 872
	internal sealed class SuccessfulSetCollapseStateResult : RopResult
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x00037600 File Offset: 0x00035800
		internal SuccessfulSetCollapseStateResult(byte[] bookmark) : base(RopId.SetCollapseState, ErrorCode.None, null)
		{
			this.bookmark = bookmark;
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00037613 File Offset: 0x00035813
		internal SuccessfulSetCollapseStateResult(Reader reader) : base(reader)
		{
			this.bookmark = reader.ReadSizeAndByteArray();
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00037628 File Offset: 0x00035828
		internal static SuccessfulSetCollapseStateResult Parse(Reader reader)
		{
			return new SuccessfulSetCollapseStateResult(reader);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00037630 File Offset: 0x00035830
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteSizedBytes(this.bookmark);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00037645 File Offset: 0x00035845
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bookmark=[");
			Util.AppendToString(stringBuilder, this.bookmark);
			stringBuilder.Append("]");
		}

		// Token: 0x04000B2E RID: 2862
		private readonly byte[] bookmark;
	}
}
