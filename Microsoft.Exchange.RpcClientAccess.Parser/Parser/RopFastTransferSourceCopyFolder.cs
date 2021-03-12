using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002AE RID: 686
	internal sealed class RopFastTransferSourceCopyFolder : InputOutputRop
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0002C88B File Offset: 0x0002AA8B
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferSourceCopyFolder;
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002C88F File Offset: 0x0002AA8F
		internal static Rop CreateRop()
		{
			return new RopFastTransferSourceCopyFolder();
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002C896 File Offset: 0x0002AA96
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, FastTransferCopyFolderFlag flags, FastTransferSendOption sendOptions)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.flags = flags;
			this.sendOptions = sendOptions;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002C8B1 File Offset: 0x0002AAB1
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.flags);
			writer.WriteByte((byte)this.sendOptions);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002C8D3 File Offset: 0x0002AAD3
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferSourceCopyFolderResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002C901 File Offset: 0x0002AB01
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferSourceCopyFolder.resultFactory;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002C908 File Offset: 0x0002AB08
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.flags = (FastTransferCopyFolderFlag)reader.ReadByte();
			this.sendOptions = (FastTransferSendOption)reader.ReadByte();
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002C92A File Offset: 0x0002AB2A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002C93F File Offset: 0x0002AB3F
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferSourceCopyFolder(serverObject, this.flags, this.sendOptions, RopFastTransferSourceCopyFolder.resultFactory);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002C960 File Offset: 0x0002AB60
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" flags=").Append(this.flags.ToString());
			stringBuilder.Append(" sendOptions=").Append(this.sendOptions.ToString());
		}

		// Token: 0x040007BE RID: 1982
		private const RopId RopType = RopId.FastTransferSourceCopyFolder;

		// Token: 0x040007BF RID: 1983
		private static FastTransferSourceCopyFolderResultFactory resultFactory = new FastTransferSourceCopyFolderResultFactory();

		// Token: 0x040007C0 RID: 1984
		private FastTransferCopyFolderFlag flags;

		// Token: 0x040007C1 RID: 1985
		private FastTransferSendOption sendOptions;
	}
}
