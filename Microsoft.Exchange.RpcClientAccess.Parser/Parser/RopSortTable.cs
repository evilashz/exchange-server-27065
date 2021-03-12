using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034B RID: 843
	internal sealed class RopSortTable : InputRop
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x00035AFA File Offset: 0x00033CFA
		internal override RopId RopId
		{
			get
			{
				return RopId.SortTable;
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00035AFE File Offset: 0x00033CFE
		internal static Rop CreateRop()
		{
			return new RopSortTable();
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00035B05 File Offset: 0x00033D05
		internal void SetInput(byte logonIndex, byte handleTableIndex, SortTableFlags flags, ushort categoryCount, ushort expandedCount, SortOrder[] sortOrders)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.categoryCount = categoryCount;
			this.expandedCount = expandedCount;
			this.sortOrders = sortOrders;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00035B30 File Offset: 0x00033D30
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteUInt16((ushort)this.sortOrders.Length);
			writer.WriteUInt16(this.categoryCount);
			writer.WriteUInt16(this.expandedCount);
			for (int i = 0; i < this.sortOrders.Length; i++)
			{
				this.sortOrders[i].Serialize(writer);
			}
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00035B99 File Offset: 0x00033D99
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSortTableResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00035BC7 File Offset: 0x00033DC7
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSortTable.resultFactory;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00035BD0 File Offset: 0x00033DD0
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (SortTableFlags)reader.ReadByte();
			ushort num = reader.ReadUInt16();
			this.categoryCount = reader.ReadUInt16();
			this.expandedCount = reader.ReadUInt16();
			reader.CheckBoundary((uint)num, 5U);
			this.sortOrders = new SortOrder[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				this.sortOrders[i] = SortOrder.Parse(reader);
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00035C3E File Offset: 0x00033E3E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00035C53 File Offset: 0x00033E53
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SortTable(serverObject, this.flags, this.categoryCount, this.expandedCount, this.sortOrders, RopSortTable.resultFactory);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00035C80 File Offset: 0x00033E80
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Categories=").Append(this.categoryCount);
			stringBuilder.Append(" Expanded=").Append(this.expandedCount);
			if (this.sortOrders != null)
			{
				Util.AppendToString<SortOrder>(stringBuilder, this.sortOrders);
			}
		}

		// Token: 0x04000ABD RID: 2749
		private const RopId RopType = RopId.SortTable;

		// Token: 0x04000ABE RID: 2750
		private static SortTableResultFactory resultFactory = new SortTableResultFactory();

		// Token: 0x04000ABF RID: 2751
		private SortTableFlags flags;

		// Token: 0x04000AC0 RID: 2752
		private ushort categoryCount;

		// Token: 0x04000AC1 RID: 2753
		private ushort expandedCount;

		// Token: 0x04000AC2 RID: 2754
		private SortOrder[] sortOrders;
	}
}
