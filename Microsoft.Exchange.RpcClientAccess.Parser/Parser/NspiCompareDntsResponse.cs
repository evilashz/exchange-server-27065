using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001CB RID: 459
	internal sealed class NspiCompareDntsResponse : MapiHttpOperationResponse
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0001E893 File Offset: 0x0001CA93
		public NspiCompareDntsResponse(uint returnCode, int result, ArraySegment<byte> auxiliaryBuffer) : base(returnCode, auxiliaryBuffer)
		{
			this.result = result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public NspiCompareDntsResponse(Reader reader) : base(reader)
		{
			this.result = reader.ReadInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		public int Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
		public override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.result);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000434 RID: 1076
		private readonly int result;
	}
}
