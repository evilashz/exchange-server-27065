using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034C RID: 844
	internal sealed class SuccessfulSortTableResult : RopResult
	{
		// Token: 0x0600145B RID: 5211 RVA: 0x00035D06 File Offset: 0x00033F06
		internal SuccessfulSortTableResult(TableStatus tableStatus) : base(RopId.SortTable, ErrorCode.None, null)
		{
			this.tableStatus = tableStatus;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00035D19 File Offset: 0x00033F19
		internal SuccessfulSortTableResult(Reader reader) : base(reader)
		{
			this.tableStatus = (TableStatus)reader.ReadByte();
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00035D2E File Offset: 0x00033F2E
		internal TableStatus TableStatus
		{
			get
			{
				return this.tableStatus;
			}
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00035D36 File Offset: 0x00033F36
		public override string ToString()
		{
			return string.Format("SuccessfulSortTableResult: [Status: {0}]", this.tableStatus);
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00035D4D File Offset: 0x00033F4D
		internal static SuccessfulSortTableResult Parse(Reader reader)
		{
			return new SuccessfulSortTableResult(reader);
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x00035D55 File Offset: 0x00033F55
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.tableStatus);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00035D6A File Offset: 0x00033F6A
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Status=").Append(this.tableStatus);
		}

		// Token: 0x04000AC3 RID: 2755
		private readonly TableStatus tableStatus;
	}
}
