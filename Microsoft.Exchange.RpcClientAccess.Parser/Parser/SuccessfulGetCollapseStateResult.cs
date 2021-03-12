using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000250 RID: 592
	internal sealed class SuccessfulGetCollapseStateResult : RopResult
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00027EF9 File Offset: 0x000260F9
		internal SuccessfulGetCollapseStateResult(byte[] collapseState) : base(RopId.GetCollapseState, ErrorCode.None, null)
		{
			this.collapseState = collapseState;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00027F0C File Offset: 0x0002610C
		internal SuccessfulGetCollapseStateResult(Reader reader) : base(reader)
		{
			this.collapseState = reader.ReadSizeAndByteArray();
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00027F21 File Offset: 0x00026121
		internal byte[] CollapseState
		{
			get
			{
				return this.collapseState;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00027F29 File Offset: 0x00026129
		internal static SuccessfulGetCollapseStateResult Parse(Reader reader)
		{
			return new SuccessfulGetCollapseStateResult(reader);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00027F31 File Offset: 0x00026131
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteSizedBytes(this.collapseState);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00027F46 File Offset: 0x00026146
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" State=[");
			Util.AppendToString(stringBuilder, this.collapseState);
			stringBuilder.Append("]");
		}

		// Token: 0x040006F1 RID: 1777
		private readonly byte[] collapseState;
	}
}
