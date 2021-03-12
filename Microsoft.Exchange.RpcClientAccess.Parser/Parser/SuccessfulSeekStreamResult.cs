using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000367 RID: 871
	internal sealed class SuccessfulSeekStreamResult : RopResult
	{
		// Token: 0x06001550 RID: 5456 RVA: 0x00037579 File Offset: 0x00035779
		internal SuccessfulSeekStreamResult(ulong resultOffset) : base(RopId.SeekStream, ErrorCode.None, null)
		{
			this.resultOffset = resultOffset;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0003758C File Offset: 0x0003578C
		internal SuccessfulSeekStreamResult(Reader reader) : base(reader)
		{
			this.resultOffset = reader.ReadUInt64();
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000375A1 File Offset: 0x000357A1
		internal ulong ResultOffset
		{
			get
			{
				return this.resultOffset;
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000375A9 File Offset: 0x000357A9
		internal static SuccessfulSeekStreamResult Parse(Reader reader)
		{
			return new SuccessfulSeekStreamResult(reader);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x000375B1 File Offset: 0x000357B1
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt64(this.resultOffset);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x000375C8 File Offset: 0x000357C8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Offset=0x").Append(this.resultOffset.ToString("X16"));
		}

		// Token: 0x04000B2D RID: 2861
		private readonly ulong resultOffset;
	}
}
