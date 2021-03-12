using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000375 RID: 885
	internal sealed class WriteStreamExtendedResult : RopResult
	{
		// Token: 0x06001593 RID: 5523 RVA: 0x00037BF7 File Offset: 0x00035DF7
		internal WriteStreamExtendedResult(ErrorCode errorCode, uint byteCount) : base(RopId.WriteStreamExtended, errorCode, null)
		{
			this.byteCount = byteCount;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00037C0D File Offset: 0x00035E0D
		internal WriteStreamExtendedResult(Reader reader) : base(reader)
		{
			this.byteCount = reader.ReadUInt32();
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00037C22 File Offset: 0x00035E22
		internal uint ByteCount
		{
			get
			{
				return this.byteCount;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00037C2A File Offset: 0x00035E2A
		internal static RopResult Parse(Reader reader)
		{
			return new WriteStreamExtendedResult(reader);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00037C32 File Offset: 0x00035E32
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.byteCount);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00037C48 File Offset: 0x00035E48
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Bytes Written=0x").Append(this.byteCount.ToString("X"));
		}

		// Token: 0x04000B3A RID: 2874
		private readonly uint byteCount;
	}
}
