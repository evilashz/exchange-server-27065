using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DB RID: 731
	internal sealed class RopGetSearchCriteria : InputRop
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0002F45A File Offset: 0x0002D65A
		internal override RopId RopId
		{
			get
			{
				return RopId.GetSearchCriteria;
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0002F45E File Offset: 0x0002D65E
		internal static Rop CreateRop()
		{
			return new RopGetSearchCriteria();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0002F465 File Offset: 0x0002D665
		internal void SetInput(byte logonIndex, byte handleTableIndex, GetSearchCriteriaFlags flags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.flags = flags;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0002F478 File Offset: 0x0002D678
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool((byte)(this.flags & GetSearchCriteriaFlags.Unicode) != 0, 1);
			writer.WriteBool((byte)(this.flags & GetSearchCriteriaFlags.Restriction) != 0, 1);
			writer.WriteBool((byte)(this.flags & GetSearchCriteriaFlags.FolderIds) != 0, 1);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, (Reader readerParameter) => SuccessfulGetSearchCriteriaResult.Parse(readerParameter, string8Encoding), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0002F533 File Offset: 0x0002D733
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetSearchCriteriaResultFactory(base.LogonIndex);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0002F540 File Offset: 0x0002D740
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = GetSearchCriteriaFlags.None;
			if (reader.ReadBool())
			{
				this.flags |= GetSearchCriteriaFlags.Unicode;
			}
			if (reader.ReadBool())
			{
				this.flags |= GetSearchCriteriaFlags.Restriction;
			}
			if (reader.ReadBool())
			{
				this.flags |= GetSearchCriteriaFlags.FolderIds;
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0002F5A1 File Offset: 0x0002D7A1
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetSearchCriteriaResultFactory resultFactory = new GetSearchCriteriaResultFactory(base.LogonIndex);
			this.result = ropHandler.GetSearchCriteria(serverObject, this.flags, resultFactory);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0002F5E5 File Offset: 0x0002D7E5
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.flags);
		}

		// Token: 0x04000862 RID: 2146
		private const RopId RopType = RopId.GetSearchCriteria;

		// Token: 0x04000863 RID: 2147
		private GetSearchCriteriaFlags flags;
	}
}
