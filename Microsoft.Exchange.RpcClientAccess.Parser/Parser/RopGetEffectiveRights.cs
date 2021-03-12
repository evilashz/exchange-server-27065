using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002C5 RID: 709
	internal sealed class RopGetEffectiveRights : InputRop
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0002E120 File Offset: 0x0002C320
		internal override RopId RopId
		{
			get
			{
				return RopId.GetEffectiveRights;
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002E124 File Offset: 0x0002C324
		internal static Rop CreateRop()
		{
			return new RopGetEffectiveRights();
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002E12B File Offset: 0x0002C32B
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte[] addressBookId, StoreId folderId)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.addressBookId = addressBookId;
			this.folderId = folderId;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002E144 File Offset: 0x0002C344
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedBytes(this.addressBookId, FieldLength.DWordSize);
			this.folderId.Serialize(writer);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002E167 File Offset: 0x0002C367
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulGetEffectiveRightsResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0002E195 File Offset: 0x0002C395
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopGetEffectiveRights.resultFactory;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0002E19C File Offset: 0x0002C39C
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.addressBookId = reader.ReadSizeAndByteArray(FieldLength.DWordSize);
			this.folderId = StoreId.Parse(reader);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002E1BF File Offset: 0x0002C3BF
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.GetEffectiveRights(serverObject, this.addressBookId, this.folderId, RopGetEffectiveRights.resultFactory);
		}

		// Token: 0x04000810 RID: 2064
		private const RopId RopType = RopId.GetEffectiveRights;

		// Token: 0x04000811 RID: 2065
		private static GetEffectiveRightsResultFactory resultFactory = new GetEffectiveRightsResultFactory();

		// Token: 0x04000812 RID: 2066
		private byte[] addressBookId;

		// Token: 0x04000813 RID: 2067
		private StoreId folderId;
	}
}
