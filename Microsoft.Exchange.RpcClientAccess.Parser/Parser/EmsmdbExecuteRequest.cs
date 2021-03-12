using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C2 RID: 450
	internal sealed class EmsmdbExecuteRequest : MapiHttpRequest
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public EmsmdbExecuteRequest(uint flags, ArraySegment<byte> ropBuffer, uint maxOutputBufferSize, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.ropBuffer = ropBuffer;
			this.maxOutputBufferSize = maxOutputBufferSize;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001E51B File Offset: 0x0001C71B
		public EmsmdbExecuteRequest(Reader reader) : base(reader)
		{
			this.flags = reader.ReadUInt32();
			this.ropBuffer = reader.ReadSizeAndByteArraySegment(FieldLength.DWordSize);
			this.maxOutputBufferSize = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x0001E550 File Offset: 0x0001C750
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x0001E558 File Offset: 0x0001C758
		public ArraySegment<byte> RopBuffer
		{
			get
			{
				return this.ropBuffer;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x0001E560 File Offset: 0x0001C760
		public uint MaxOutputBufferSize
		{
			get
			{
				return this.maxOutputBufferSize;
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001E568 File Offset: 0x0001C768
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.flags);
			writer.WriteSizedBytesSegment(this.ropBuffer, FieldLength.DWordSize);
			writer.WriteUInt32(this.maxOutputBufferSize);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000425 RID: 1061
		private readonly uint flags;

		// Token: 0x04000426 RID: 1062
		private readonly ArraySegment<byte> ropBuffer;

		// Token: 0x04000427 RID: 1063
		private readonly uint maxOutputBufferSize;
	}
}
