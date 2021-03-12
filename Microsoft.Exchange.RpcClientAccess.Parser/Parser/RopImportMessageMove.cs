using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002EB RID: 747
	internal sealed class RopImportMessageMove : InputRop
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0002FFAD File Offset: 0x0002E1AD
		internal override RopId RopId
		{
			get
			{
				return RopId.ImportMessageMove;
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0002FFB1 File Offset: 0x0002E1B1
		internal static Rop CreateRop()
		{
			return new RopImportMessageMove();
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] sourceFolder, byte[] sourceMessage, byte[] predecessorChangeList, byte[] destinationMessage, byte[] destinationChangeNumber)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.sourceFolder = sourceFolder;
			this.sourceMessage = sourceMessage;
			this.predecessorChangeList = predecessorChangeList;
			this.destinationMessage = destinationMessage;
			this.destinationChangeNumber = destinationChangeNumber;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.sourceFolder, FieldLength.DWordSize);
			writer.WriteSizedBytes(this.sourceMessage, FieldLength.DWordSize);
			writer.WriteSizedBytes(this.predecessorChangeList, FieldLength.DWordSize);
			writer.WriteSizedBytes(this.destinationMessage, FieldLength.DWordSize);
			writer.WriteSizedBytes(this.destinationChangeNumber, FieldLength.DWordSize);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00030042 File Offset: 0x0002E242
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulImportMessageMoveResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00030070 File Offset: 0x0002E270
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopImportMessageMove.resultFactory;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00030078 File Offset: 0x0002E278
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.sourceFolder = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
			this.sourceMessage = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
			this.predecessorChangeList = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
			this.destinationMessage = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
			this.destinationChangeNumber = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000300CE File Offset: 0x0002E2CE
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000300E4 File Offset: 0x0002E2E4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.ImportMessageMove(serverObject, this.sourceFolder, this.sourceMessage, this.predecessorChangeList, this.destinationMessage, this.destinationChangeNumber, RopImportMessageMove.resultFactory);
		}

		// Token: 0x04000935 RID: 2357
		private const RopId RopType = RopId.ImportMessageMove;

		// Token: 0x04000936 RID: 2358
		private static ImportMessageMoveResultFactory resultFactory = new ImportMessageMoveResultFactory();

		// Token: 0x04000937 RID: 2359
		private byte[] sourceFolder;

		// Token: 0x04000938 RID: 2360
		private byte[] sourceMessage;

		// Token: 0x04000939 RID: 2361
		private byte[] predecessorChangeList;

		// Token: 0x0400093A RID: 2362
		private byte[] destinationMessage;

		// Token: 0x0400093B RID: 2363
		private byte[] destinationChangeNumber;
	}
}
