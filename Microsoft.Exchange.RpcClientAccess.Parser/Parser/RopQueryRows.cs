using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200031B RID: 795
	internal sealed class RopQueryRows : InputRop
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00032C98 File Offset: 0x00030E98
		internal override RopId RopId
		{
			get
			{
				return RopId.QueryRows;
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00032C9C File Offset: 0x00030E9C
		internal static Rop CreateRop()
		{
			return new RopQueryRows();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00032CA3 File Offset: 0x00030EA3
		internal void SetInput(byte logonIndex, byte handleTableIndex, QueryRowsFlags flags, bool useForwardDirection, ushort rowCount)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
			this.useForwardDirection = useForwardDirection;
			this.rowCount = rowCount;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00032CC4 File Offset: 0x00030EC4
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteBool(this.useForwardDirection, 1);
			writer.WriteUInt16(this.rowCount);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00032CF3 File Offset: 0x00030EF3
		internal void SetParseOutputData(PropertyTag[] columns)
		{
			Util.ThrowOnNullArgument(columns, "columns");
			this.columns = columns;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00032D28 File Offset: 0x00030F28
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			if (this.columns == null)
			{
				throw new InvalidOperationException("SetParseOutputData must be called before ParseOutput.");
			}
			base.InternalParseOutput(reader, string8Encoding);
			RopResult ropResult = RopResult.Parse(reader, (Reader readerParameter) => new SuccessfulQueryRowsResult(readerParameter, this.columns, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
			if (this.result == null)
			{
				this.result = ropResult;
			}
			if (base.ChainedResults == null)
			{
				base.ChainedResults = new List<RopResult>(10);
			}
			base.ChainedResults.Add(ropResult);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00032DB8 File Offset: 0x00030FB8
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new QueryRowsResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00032DCC File Offset: 0x00030FCC
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (QueryRowsFlags)reader.ReadByte();
			this.useForwardDirection = reader.ReadBool();
			this.rowCount = reader.ReadUInt16();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00032DFA File Offset: 0x00030FFA
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00032E10 File Offset: 0x00031010
		internal override byte[] CreateFakeRopRequest(RopResult result, ServerObjectHandleTable serverObjectHandleTable)
		{
			SuccessfulQueryRowsResult successfulQueryRowsResult = result as SuccessfulQueryRowsResult;
			if ((byte)(this.flags & QueryRowsFlags.DoNotAdvance) == 0 && (byte)(this.flags & QueryRowsFlags.SendMax) != 0 && successfulQueryRowsResult.BookmarkOrigin != BookmarkOrigin.End && successfulQueryRowsResult.BookmarkOrigin != BookmarkOrigin.Beginning && successfulQueryRowsResult.Rows.Length > 0 && (successfulQueryRowsResult.Rows.Length < (int)this.rowCount || (byte)(this.flags & QueryRowsFlags.ChainAlways) != 0))
			{
				RopQueryRows ropQueryRows = (RopQueryRows)RopQueryRows.CreateRop();
				ropQueryRows.SetInput(base.LogonIndex, base.HandleTableIndex, this.flags, this.useForwardDirection, (ushort)((int)this.rowCount - successfulQueryRowsResult.Rows.Length));
				return RopDriver.CreateFakeRopRequest(ropQueryRows, serverObjectHandleTable);
			}
			return null;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00032EB8 File Offset: 0x000310B8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			QueryRowsResultFactory resultFactory = new QueryRowsResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.QueryRows(serverObject, this.flags, this.useForwardDirection, this.rowCount, resultFactory);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00032EF8 File Offset: 0x000310F8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Forward=").Append(this.useForwardDirection);
			stringBuilder.Append(" Count=").Append(this.rowCount);
		}

		// Token: 0x04000A0A RID: 2570
		private const RopId RopType = RopId.QueryRows;

		// Token: 0x04000A0B RID: 2571
		private QueryRowsFlags flags;

		// Token: 0x04000A0C RID: 2572
		private bool useForwardDirection;

		// Token: 0x04000A0D RID: 2573
		private ushort rowCount;

		// Token: 0x04000A0E RID: 2574
		private PropertyTag[] columns;
	}
}
