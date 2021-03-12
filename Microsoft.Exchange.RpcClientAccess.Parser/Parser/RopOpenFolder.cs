using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200030F RID: 783
	internal sealed class RopOpenFolder : InputOutputRop
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0003228C File Offset: 0x0003048C
		internal override RopId RopId
		{
			get
			{
				return RopId.OpenFolder;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0003228F File Offset: 0x0003048F
		internal static Rop CreateRop()
		{
			return new RopOpenFolder();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00032296 File Offset: 0x00030496
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopOpenFolder.resultFactory;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0003229D File Offset: 0x0003049D
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.folderId = StoreId.Parse(reader);
			this.openMode = (OpenMode)reader.ReadByte();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x000322BF File Offset: 0x000304BF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000322D4 File Offset: 0x000304D4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.OpenFolder(serverObject, this.folderId, this.openMode, RopOpenFolder.resultFactory);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x000322F4 File Offset: 0x000304F4
		internal void SetInput(byte logonIndex, byte inputObjectHandleTableIndex, byte outputFolderHandleIndex, StoreId folderId, OpenMode openMode)
		{
			base.SetCommonInput(logonIndex, inputObjectHandleTableIndex, outputFolderHandleIndex);
			this.folderId = folderId;
			this.openMode = openMode;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0003230F File Offset: 0x0003050F
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.folderId.Serialize(writer);
			writer.WriteByte((byte)this.openMode);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00032331 File Offset: 0x00030531
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulOpenFolderResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00032360 File Offset: 0x00030560
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" OpenMode=").Append(this.openMode);
			stringBuilder.Append(" FID=").Append(this.folderId.ToString());
		}

		// Token: 0x040009E0 RID: 2528
		private const RopId RopType = RopId.OpenFolder;

		// Token: 0x040009E1 RID: 2529
		private static OpenFolderResultFactory resultFactory = new OpenFolderResultFactory();

		// Token: 0x040009E2 RID: 2530
		private StoreId folderId;

		// Token: 0x040009E3 RID: 2531
		private OpenMode openMode;
	}
}
