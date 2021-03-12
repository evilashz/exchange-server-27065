using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C9 RID: 713
	internal sealed class RopGetLocalReplicationIds : InputRop
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0002E438 File Offset: 0x0002C638
		internal override RopId RopId
		{
			get
			{
				return RopId.GetLocalReplicationIds;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002E43C File Offset: 0x0002C63C
		internal static Rop CreateRop()
		{
			return new RopGetLocalReplicationIds();
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002E443 File Offset: 0x0002C643
		internal void SetInput(byte logonIndex, byte handleTableIndex, uint idCount)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.idCount = idCount;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002E454 File Offset: 0x0002C654
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt32(this.idCount);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002E46A File Offset: 0x0002C66A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetLocalReplicationIdsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002E498 File Offset: 0x0002C698
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetLocalReplicationIds.resultFactory;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002E49F File Offset: 0x0002C69F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.idCount = reader.ReadUInt32();
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002E4B5 File Offset: 0x0002C6B5
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002E4CA File Offset: 0x0002C6CA
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetLocalReplicationIds(serverObject, this.idCount, RopGetLocalReplicationIds.resultFactory);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Count=").Append(this.idCount);
		}

		// Token: 0x0400081D RID: 2077
		private const RopId RopType = RopId.GetLocalReplicationIds;

		// Token: 0x0400081E RID: 2078
		private static GetLocalReplicationIdsResultFactory resultFactory = new GetLocalReplicationIdsResultFactory();

		// Token: 0x0400081F RID: 2079
		private uint idCount;
	}
}
