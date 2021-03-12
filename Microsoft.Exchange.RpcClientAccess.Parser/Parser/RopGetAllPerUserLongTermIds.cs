using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002BD RID: 701
	internal sealed class RopGetAllPerUserLongTermIds : InputRop
	{
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0002D9AD File Offset: 0x0002BBAD
		internal override RopId RopId
		{
			get
			{
				return RopId.GetAllPerUserLongTermIds;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0002D9B1 File Offset: 0x0002BBB1
		internal static Rop CreateRop()
		{
			return new RopGetAllPerUserLongTermIds();
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreLongTermId startId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.startId = startId;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002D9C9 File Offset: 0x0002BBC9
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.startId.Serialize(writer);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0002D9DF File Offset: 0x0002BBDF
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetAllPerUserLongTermIdsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0002DA15 File Offset: 0x0002BC15
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new GetAllPerUserLongTermIdsResultFactory(outputBuffer.Count);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0002DA23 File Offset: 0x0002BC23
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.startId = StoreLongTermId.Parse(reader);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002DA39 File Offset: 0x0002BC39
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0002DA50 File Offset: 0x0002BC50
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			GetAllPerUserLongTermIdsResultFactory resultFactory = new GetAllPerUserLongTermIdsResultFactory(outputBuffer.Count);
			this.result = ropHandler.GetAllPerUserLongTermIds(serverObject, this.startId, resultFactory);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0002DA7E File Offset: 0x0002BC7E
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" StartId=").Append(this.startId);
		}

		// Token: 0x04000800 RID: 2048
		private const RopId RopType = RopId.GetAllPerUserLongTermIds;

		// Token: 0x04000801 RID: 2049
		private StoreLongTermId startId;
	}
}
