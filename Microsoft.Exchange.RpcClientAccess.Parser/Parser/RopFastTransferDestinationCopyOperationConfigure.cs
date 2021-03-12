using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A9 RID: 681
	internal sealed class RopFastTransferDestinationCopyOperationConfigure : InputOutputRop
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002C076 File Offset: 0x0002A276
		internal override RopId RopId
		{
			get
			{
				return RopId.FastTransferDestinationCopyOperationConfigure;
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0002C07A File Offset: 0x0002A27A
		internal static Rop CreateRop()
		{
			return new RopFastTransferDestinationCopyOperationConfigure();
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002C081 File Offset: 0x0002A281
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, FastTransferCopyOperation copyOperation, FastTransferCopyPropertiesFlag flags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.copyOperation = copyOperation;
			this.flags = flags;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002C09C File Offset: 0x0002A29C
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.copyOperation);
			writer.WriteByte((byte)this.flags);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002C0BE File Offset: 0x0002A2BE
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulFastTransferDestinationCopyOperationConfigureResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002C0EC File Offset: 0x0002A2EC
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopFastTransferDestinationCopyOperationConfigure.resultFactory;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002C0F3 File Offset: 0x0002A2F3
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.copyOperation = (FastTransferCopyOperation)reader.ReadByte();
			this.flags = (FastTransferCopyPropertiesFlag)reader.ReadByte();
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002C115 File Offset: 0x0002A315
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002C12A File Offset: 0x0002A32A
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.FastTransferDestinationCopyOperationConfigure(serverObject, this.copyOperation, this.flags, RopFastTransferDestinationCopyOperationConfigure.resultFactory);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002C14C File Offset: 0x0002A34C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" FastTransferCopyOperation=").Append(this.copyOperation);
			stringBuilder.Append(" FastTransferCopyPropertiesFlag=").Append(this.flags);
		}

		// Token: 0x040007AE RID: 1966
		private const RopId RopType = RopId.FastTransferDestinationCopyOperationConfigure;

		// Token: 0x040007AF RID: 1967
		private static FastTransferDestinationCopyOperationConfigureResultFactory resultFactory = new FastTransferDestinationCopyOperationConfigureResultFactory();

		// Token: 0x040007B0 RID: 1968
		private FastTransferCopyOperation copyOperation;

		// Token: 0x040007B1 RID: 1969
		private FastTransferCopyPropertiesFlag flags;
	}
}
