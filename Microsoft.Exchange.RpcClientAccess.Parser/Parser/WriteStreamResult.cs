using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000376 RID: 886
	internal sealed class WriteStreamResult : RopResult
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x00037C80 File Offset: 0x00035E80
		internal WriteStreamResult(ErrorCode errorCode, ushort byteCount) : base(RopId.WriteStream, errorCode, null)
		{
			this.byteCount = byteCount;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00037C93 File Offset: 0x00035E93
		internal WriteStreamResult(Reader reader) : base(reader)
		{
			this.byteCount = reader.ReadUInt16();
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00037CA8 File Offset: 0x00035EA8
		internal ushort ByteCount
		{
			get
			{
				return this.byteCount;
			}
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00037CB0 File Offset: 0x00035EB0
		internal static RopResult Parse(Reader reader)
		{
			return new WriteStreamResult(reader);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00037CB8 File Offset: 0x00035EB8
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt16(this.byteCount);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00037CD0 File Offset: 0x00035ED0
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bytes Written=0x").Append(this.byteCount.ToString("X"));
		}

		// Token: 0x04000B3B RID: 2875
		private readonly ushort byteCount;
	}
}
