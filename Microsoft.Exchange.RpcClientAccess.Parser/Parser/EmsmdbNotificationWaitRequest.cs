using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C4 RID: 452
	internal sealed class EmsmdbNotificationWaitRequest : MapiHttpRequest
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x0001E609 File Offset: 0x0001C809
		public EmsmdbNotificationWaitRequest(uint reserved, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.reserved = reserved;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001E619 File Offset: 0x0001C819
		public EmsmdbNotificationWaitRequest(Reader reader) : base(reader)
		{
			this.reserved = reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001E635 File Offset: 0x0001C835
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32(this.reserved);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400042A RID: 1066
		private readonly uint reserved;
	}
}
