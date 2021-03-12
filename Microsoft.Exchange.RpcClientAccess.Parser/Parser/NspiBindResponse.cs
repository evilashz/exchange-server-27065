using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001C9 RID: 457
	internal sealed class NspiBindResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x0001E782 File Offset: 0x0001C982
		public NspiBindResponse(uint returnCode, Guid serverGuid, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.serverGuid = serverGuid;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001E793 File Offset: 0x0001C993
		public NspiBindResponse(Reader reader) : base(reader)
		{
			this.serverGuid = reader.ReadGuid();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001E7AF File Offset: 0x0001C9AF
		public Guid ServerGuid
		{
			get
			{
				return this.serverGuid;
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001E7B7 File Offset: 0x0001C9B7
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteGuid(this.serverGuid);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400042F RID: 1071
		private readonly Guid serverGuid;
	}
}
