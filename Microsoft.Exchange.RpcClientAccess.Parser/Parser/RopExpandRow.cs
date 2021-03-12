using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A7 RID: 679
	internal sealed class RopExpandRow : InputRop
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0002BF09 File Offset: 0x0002A109
		internal override RopId RopId
		{
			get
			{
				return RopId.ExpandRow;
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002BF0D File Offset: 0x0002A10D
		internal static Rop CreateRop()
		{
			return new RopExpandRow();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002BF14 File Offset: 0x0002A114
		internal void SetInput(byte logonIndex, byte handleTableIndex, short maxRows, StoreId categoryId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.maxRows = maxRows;
			this.categoryId = categoryId;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002BF2D File Offset: 0x0002A12D
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteInt16(this.maxRows);
			this.categoryId.Serialize(writer);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002BF4F File Offset: 0x0002A14F
		internal void SetParseOutputData(PropertyTag[] columns)
		{
			Util.ThrowOnNullArgument(columns, "columns");
			this.columns = columns;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002BF84 File Offset: 0x0002A184
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			if (this.columns == null)
			{
				throw new InvalidOperationException("SetParseOutputData must be called before ParseOutput.");
			}
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => new SuccessfulExpandRowResult(readerParameter, this.columns, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002BFE9 File Offset: 0x0002A1E9
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new ExpandRowResultFactory(outputBuffer.Count, connection.String8Encoding);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002BFFD File Offset: 0x0002A1FD
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.maxRows = reader.ReadInt16();
			this.categoryId = StoreId.Parse(reader);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002C01F File Offset: 0x0002A21F
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002C034 File Offset: 0x0002A234
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			ExpandRowResultFactory resultFactory = new ExpandRowResultFactory(outputBuffer.Count, serverObject.String8Encoding);
			this.result = ropHandler.ExpandRow(serverObject, this.maxRows, this.categoryId, resultFactory);
		}

		// Token: 0x040007A5 RID: 1957
		private const RopId RopType = RopId.ExpandRow;

		// Token: 0x040007A6 RID: 1958
		private short maxRows;

		// Token: 0x040007A7 RID: 1959
		private StoreId categoryId;

		// Token: 0x040007A8 RID: 1960
		private PropertyTag[] columns;
	}
}
