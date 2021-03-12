using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000334 RID: 820
	internal sealed class RopSeekStream : InputRop
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00034706 File Offset: 0x00032906
		internal override RopId RopId
		{
			get
			{
				return RopId.SeekStream;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003470A File Offset: 0x0003290A
		internal static Rop CreateRop()
		{
			return new RopSeekStream();
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00034711 File Offset: 0x00032911
		internal void SetInput(byte logonIndex, byte handleTableIndex, StreamSeekOrigin streamSeekOrigin, long offset)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.streamSeekOrigin = streamSeekOrigin;
			this.offset = offset;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003472A File Offset: 0x0003292A
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.streamSeekOrigin);
			writer.WriteInt64(this.offset);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0003474C File Offset: 0x0003294C
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulSeekStreamResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003477A File Offset: 0x0003297A
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSeekStream.resultFactory;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00034781 File Offset: 0x00032981
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.streamSeekOrigin = (StreamSeekOrigin)reader.ReadByte();
			this.offset = reader.ReadInt64();
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000347A3 File Offset: 0x000329A3
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000347B8 File Offset: 0x000329B8
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SeekStream(serverObject, this.streamSeekOrigin, this.offset, RopSeekStream.resultFactory);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000347D8 File Offset: 0x000329D8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Origin=").Append(this.streamSeekOrigin);
			stringBuilder.Append(" Offset=0x").Append(this.offset.ToString("X16"));
		}

		// Token: 0x04000A6C RID: 2668
		private const RopId RopType = RopId.SeekStream;

		// Token: 0x04000A6D RID: 2669
		private static SeekStreamResultFactory resultFactory = new SeekStreamResultFactory();

		// Token: 0x04000A6E RID: 2670
		private StreamSeekOrigin streamSeekOrigin;

		// Token: 0x04000A6F RID: 2671
		private long offset;
	}
}
