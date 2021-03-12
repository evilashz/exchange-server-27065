using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C2 RID: 706
	internal sealed class SuccessfulGetContentsTableResult : RopResult
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x0002DF35 File Offset: 0x0002C135
		internal SuccessfulGetContentsTableResult(IServerObject table, int rowCount) : base(RopId.GetContentsTable, ErrorCode.None, table)
		{
			Util.ThrowOnNullArgument(table, "table");
			this.rowCount = rowCount;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002DF52 File Offset: 0x0002C152
		internal SuccessfulGetContentsTableResult(Reader reader) : base(reader)
		{
			this.rowCount = reader.ReadInt32();
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0002DF67 File Offset: 0x0002C167
		internal int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0002DF6F File Offset: 0x0002C16F
		internal static SuccessfulGetContentsTableResult Parse(Reader reader)
		{
			return new SuccessfulGetContentsTableResult(reader);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002DF77 File Offset: 0x0002C177
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.rowCount);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0002DF8C File Offset: 0x0002C18C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" RowCount=").Append(this.rowCount);
		}

		// Token: 0x0400080C RID: 2060
		private readonly int rowCount;
	}
}
