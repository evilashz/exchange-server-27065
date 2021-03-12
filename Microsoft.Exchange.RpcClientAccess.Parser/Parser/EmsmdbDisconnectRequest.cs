using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001BE RID: 446
	internal sealed class EmsmdbDisconnectRequest : MapiHttpRequest
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0001E485 File Offset: 0x0001C685
		public EmsmdbDisconnectRequest(ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001E48E File Offset: 0x0001C68E
		public EmsmdbDisconnectRequest(Reader reader) : base(reader)
		{
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001E49E File Offset: 0x0001C69E
		public override void Serialize(Writer writer)
		{
			base.SerializeAuxiliaryBuffer(writer);
		}
	}
}
