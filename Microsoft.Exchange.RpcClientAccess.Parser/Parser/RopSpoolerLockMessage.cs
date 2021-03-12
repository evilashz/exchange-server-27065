using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034D RID: 845
	internal sealed class RopSpoolerLockMessage : InputRop
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00035D8F File Offset: 0x00033F8F
		internal override RopId RopId
		{
			get
			{
				return RopId.SpoolerLockMessage;
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00035D93 File Offset: 0x00033F93
		internal static Rop CreateRop()
		{
			return new RopSpoolerLockMessage();
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00035D9A File Offset: 0x00033F9A
		internal void SetInput(byte logonIndex, byte handleTableIndex, StoreId messageId, LockState lockState)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.messageId = messageId;
			this.lockState = lockState;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00035DB3 File Offset: 0x00033FB3
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			this.messageId.Serialize(writer);
			writer.WriteByte((byte)this.lockState);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00035DD5 File Offset: 0x00033FD5
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00035E03 File Offset: 0x00034003
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSpoolerLockMessage.resultFactory;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00035E0A File Offset: 0x0003400A
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.messageId = StoreId.Parse(reader);
			this.lockState = (LockState)reader.ReadByte();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00035E2C File Offset: 0x0003402C
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00035E41 File Offset: 0x00034041
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SpoolerLockMessage(serverObject, this.messageId, this.lockState, RopSpoolerLockMessage.resultFactory);
		}

		// Token: 0x04000AC4 RID: 2756
		private const RopId RopType = RopId.SpoolerLockMessage;

		// Token: 0x04000AC5 RID: 2757
		private static SpoolerLockMessageResultFactory resultFactory = new SpoolerLockMessageResultFactory();

		// Token: 0x04000AC6 RID: 2758
		private StoreId messageId;

		// Token: 0x04000AC7 RID: 2759
		private LockState lockState;
	}
}
