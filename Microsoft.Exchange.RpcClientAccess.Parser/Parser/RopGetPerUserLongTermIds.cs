using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D0 RID: 720
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RopGetPerUserLongTermIds : InputRop
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0002EB13 File Offset: 0x0002CD13
		internal override RopId RopId
		{
			get
			{
				return RopId.GetPerUserLongTermIds;
			}
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002EB17 File Offset: 0x0002CD17
		internal static Rop CreateRop()
		{
			return new RopGetPerUserLongTermIds();
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002EB1E File Offset: 0x0002CD1E
		internal void SetInput(byte logonIndex, byte handleTableIndex, Guid databaseGuid)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0002EB2F File Offset: 0x0002CD2F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteGuid(this.databaseGuid);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002EB45 File Offset: 0x0002CD45
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetPerUserLongTermIdsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002EB73 File Offset: 0x0002CD73
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetPerUserLongTermIds.resultFactory;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002EB7A File Offset: 0x0002CD7A
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.databaseGuid = reader.ReadGuid();
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002EB90 File Offset: 0x0002CD90
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002EBA5 File Offset: 0x0002CDA5
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetPerUserLongTermIds(serverObject, this.databaseGuid, RopGetPerUserLongTermIds.resultFactory);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002EBBF File Offset: 0x0002CDBF
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" DatabaseGuid=").Append(this.databaseGuid);
		}

		// Token: 0x04000832 RID: 2098
		private const RopId RopType = RopId.GetPerUserLongTermIds;

		// Token: 0x04000833 RID: 2099
		private static readonly GetPerUserLongTermIdsResultFactory resultFactory = new GetPerUserLongTermIdsResultFactory();

		// Token: 0x04000834 RID: 2100
		private Guid databaseGuid;
	}
}
