using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000260 RID: 608
	internal sealed class SuccessfulGetStatusResult : RopResult
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x0002887C File Offset: 0x00026A7C
		internal SuccessfulGetStatusResult(TableStatus tableStatus) : base(RopId.GetStatus, ErrorCode.None, null)
		{
			this.tableStatus = tableStatus;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0002888F File Offset: 0x00026A8F
		internal SuccessfulGetStatusResult(Reader reader) : base(reader)
		{
			this.tableStatus = (TableStatus)reader.ReadByte();
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000288A4 File Offset: 0x00026AA4
		internal static SuccessfulGetStatusResult Parse(Reader reader)
		{
			return new SuccessfulGetStatusResult(reader);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x000288AC File Offset: 0x00026AAC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.tableStatus);
		}

		// Token: 0x04000702 RID: 1794
		private readonly TableStatus tableStatus;
	}
}
