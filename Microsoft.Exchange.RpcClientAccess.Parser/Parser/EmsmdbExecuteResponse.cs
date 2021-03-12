using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C3 RID: 451
	internal sealed class EmsmdbExecuteResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000990 RID: 2448 RVA: 0x0001E596 File Offset: 0x0001C796
		public EmsmdbExecuteResponse(uint returnCode, uint reserved, ArraySegment<byte> ropBuffer, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.reserved = reserved;
			this.ropBuffer = ropBuffer;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001E5AF File Offset: 0x0001C7AF
		public EmsmdbExecuteResponse(Reader reader) : base(reader)
		{
			this.reserved = reader.ReadUInt32();
			this.ropBuffer = reader.ReadSizeAndByteArraySegment(FieldLength.DWordSize);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0001E5D8 File Offset: 0x0001C7D8
		public ArraySegment<byte> RopBuffer
		{
			get
			{
				return this.ropBuffer;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.reserved);
			writer.WriteSizedBytesSegment(this.ropBuffer, FieldLength.DWordSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000428 RID: 1064
		private readonly uint reserved;

		// Token: 0x04000429 RID: 1065
		private readonly ArraySegment<byte> ropBuffer;
	}
}
