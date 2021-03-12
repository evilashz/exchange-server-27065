using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000262 RID: 610
	internal sealed class SuccessfulGetStreamSizeResult : RopResult
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00028906 File Offset: 0x00026B06
		internal SuccessfulGetStreamSizeResult(uint streamSize) : base(RopId.GetStreamSize, ErrorCode.None, null)
		{
			this.streamSize = streamSize;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00028919 File Offset: 0x00026B19
		internal SuccessfulGetStreamSizeResult(Reader reader) : base(reader)
		{
			this.streamSize = reader.ReadUInt32();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002892E File Offset: 0x00026B2E
		internal uint StreamSize
		{
			get
			{
				return this.streamSize;
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00028936 File Offset: 0x00026B36
		internal static SuccessfulGetStreamSizeResult Parse(Reader reader)
		{
			return new SuccessfulGetStreamSizeResult(reader);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0002893E File Offset: 0x00026B3E
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.streamSize);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00028954 File Offset: 0x00026B54
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Size=0x").Append(this.StreamSize.ToString("X"));
		}

		// Token: 0x04000704 RID: 1796
		private readonly uint streamSize;
	}
}
