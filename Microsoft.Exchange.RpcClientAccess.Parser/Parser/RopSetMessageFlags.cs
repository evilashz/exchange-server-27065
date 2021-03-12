using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033A RID: 826
	internal sealed class RopSetMessageFlags : InputRop
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00034BED File Offset: 0x00032DED
		internal override RopId RopId
		{
			get
			{
				return RopId.SetMessageFlags;
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00034BF4 File Offset: 0x00032DF4
		internal static Rop CreateRop()
		{
			return new RopSetMessageFlags();
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00034BFB File Offset: 0x00032DFB
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId messageId, MessageFlags flags, MessageFlags flagsMask)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageId = messageId;
			this.flags = flags;
			this.flagsMask = flagsMask;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00034C1C File Offset: 0x00032E1C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.messageId.Serialize(writer);
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUInt32((uint)this.flagsMask);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00034C4A File Offset: 0x00032E4A
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00034C78 File Offset: 0x00032E78
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetMessageFlags.resultFactory;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00034C7F File Offset: 0x00032E7F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageId = StoreId.Parse(reader);
			this.flags = (MessageFlags)reader.ReadUInt32();
			this.flagsMask = (MessageFlags)reader.ReadUInt32();
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00034CAD File Offset: 0x00032EAD
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00034CC2 File Offset: 0x00032EC2
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetMessageFlags(serverObject, this.messageId, this.flags, this.flagsMask, RopSetMessageFlags.resultFactory);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x00034CE8 File Offset: 0x00032EE8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
			stringBuilder.Append(" Flags=").Append(this.flags);
			stringBuilder.Append(" Mask=").Append(this.flagsMask);
		}

		// Token: 0x04000A7E RID: 2686
		private const RopId RopType = RopId.SetMessageFlags;

		// Token: 0x04000A7F RID: 2687
		private static SetMessageFlagsResultFactory resultFactory = new SetMessageFlagsResultFactory();

		// Token: 0x04000A80 RID: 2688
		private StoreId messageId;

		// Token: 0x04000A81 RID: 2689
		private MessageFlags flags;

		// Token: 0x04000A82 RID: 2690
		private MessageFlags flagsMask;
	}
}
