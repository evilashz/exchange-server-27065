using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000227 RID: 551
	internal sealed class SuccessfulCollapseRowResult : RopResult
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x000269C2 File Offset: 0x00024BC2
		internal SuccessfulCollapseRowResult(int collapsedRowCount) : base(RopId.CollapseRow, ErrorCode.None, null)
		{
			this.collapsedRowCount = collapsedRowCount;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000269D5 File Offset: 0x00024BD5
		internal SuccessfulCollapseRowResult(Reader reader) : base(reader)
		{
			this.collapsedRowCount = reader.ReadInt32();
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000269EA File Offset: 0x00024BEA
		internal static SuccessfulCollapseRowResult Parse(Reader reader)
		{
			return new SuccessfulCollapseRowResult(reader);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000269F2 File Offset: 0x00024BF2
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.collapsedRowCount);
		}

		// Token: 0x040006B8 RID: 1720
		private readonly int collapsedRowCount;
	}
}
