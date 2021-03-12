using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000222 RID: 546
	internal sealed class SuccessfulAbortResult : RopResult
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x00026624 File Offset: 0x00024824
		internal SuccessfulAbortResult(TableStatus status) : base(RopId.Abort, ErrorCode.None, null)
		{
			this.status = status;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00026637 File Offset: 0x00024837
		internal SuccessfulAbortResult(Reader reader) : base(reader)
		{
			this.status = (TableStatus)reader.ReadByte();
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002664C File Offset: 0x0002484C
		internal TableStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00026654 File Offset: 0x00024854
		internal static SuccessfulAbortResult Parse(Reader reader)
		{
			return new SuccessfulAbortResult(reader);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002665C File Offset: 0x0002485C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.status);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00026671 File Offset: 0x00024871
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" TableStatus=").Append(this.status);
		}

		// Token: 0x040006B0 RID: 1712
		private readonly TableStatus status;
	}
}
