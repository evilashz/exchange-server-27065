using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C4 RID: 708
	internal sealed class SuccessfulGetContentsTableExtendedResult : RopResult
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x0002E0A5 File Offset: 0x0002C2A5
		internal SuccessfulGetContentsTableExtendedResult(IServerObject table, int rowCount) : base(RopId.GetContentsTableExtended, ErrorCode.None, table)
		{
			Util.ThrowOnNullArgument(table, "table");
			this.rowCount = rowCount;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002E0C6 File Offset: 0x0002C2C6
		internal SuccessfulGetContentsTableExtendedResult(Reader reader) : base(reader)
		{
			this.rowCount = reader.ReadInt32();
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0002E0DB File Offset: 0x0002C2DB
		internal int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002E0E3 File Offset: 0x0002C2E3
		internal static SuccessfulGetContentsTableExtendedResult Parse(Reader reader)
		{
			return new SuccessfulGetContentsTableExtendedResult(reader);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002E0EB File Offset: 0x0002C2EB
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.rowCount);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002E100 File Offset: 0x0002C300
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" RowCount=").Append(this.rowCount);
		}

		// Token: 0x0400080F RID: 2063
		private readonly int rowCount;
	}
}
