using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000338 RID: 824
	internal sealed class SuccessfulSetColumnsResult : RopResult
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00034AB8 File Offset: 0x00032CB8
		internal TableStatus TableStatus
		{
			get
			{
				return this.tableStatus;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00034AC0 File Offset: 0x00032CC0
		internal SuccessfulSetColumnsResult(TableStatus tableStatus) : base(RopId.SetColumns, ErrorCode.None, null)
		{
			this.tableStatus = tableStatus;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00034AD3 File Offset: 0x00032CD3
		internal SuccessfulSetColumnsResult(Reader reader) : base(reader)
		{
			this.tableStatus = (TableStatus)reader.ReadByte();
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00034AE8 File Offset: 0x00032CE8
		internal static SuccessfulSetColumnsResult Parse(Reader reader)
		{
			return new SuccessfulSetColumnsResult(reader);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00034AF0 File Offset: 0x00032CF0
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte((byte)this.tableStatus);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00034B05 File Offset: 0x00032D05
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Status=").Append(this.tableStatus);
		}

		// Token: 0x04000A7A RID: 2682
		private readonly TableStatus tableStatus;
	}
}
