using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C5 RID: 453
	internal sealed class EmsmdbNotificationWaitResponse : MapiHttpOperationResponse
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x0001E64A File Offset: 0x0001C84A
		public EmsmdbNotificationWaitResponse(uint returnCode, uint flags, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.flags = flags;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001E65B File Offset: 0x0001C85B
		public EmsmdbNotificationWaitResponse(Reader reader) : base(reader)
		{
			this.flags = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001E677 File Offset: 0x0001C877
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001E67F File Offset: 0x0001C87F
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32(this.flags);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400042B RID: 1067
		private readonly uint flags;
	}
}
