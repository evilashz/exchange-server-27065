using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200032A RID: 810
	internal sealed class SuccessfulRestrictResult : RopResult
	{
		// Token: 0x06001346 RID: 4934 RVA: 0x000340AC File Offset: 0x000322AC
		internal SuccessfulRestrictResult(TableStatus tableStatus) : base(RopId.Restrict, ErrorCode.None, null)
		{
			this.tableStatus = tableStatus;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000340BF File Offset: 0x000322BF
		internal SuccessfulRestrictResult(Reader reader) : base(reader)
		{
			this.tableStatus = (TableStatus)reader.ReadByte();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000340D4 File Offset: 0x000322D4
		internal static SuccessfulRestrictResult Parse(Reader reader)
		{
			return new SuccessfulRestrictResult(reader);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000340DC File Offset: 0x000322DC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.tableStatus);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x000340F1 File Offset: 0x000322F1
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" TableStatus=").Append(this.tableStatus);
		}

		// Token: 0x04000A4D RID: 2637
		private readonly TableStatus tableStatus;
	}
}
