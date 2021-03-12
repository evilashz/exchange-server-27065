using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001E6 RID: 486
	internal sealed class NspiUnbindRequest : MapiHttpRequest
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0001FEC2 File Offset: 0x0001E0C2
		public NspiUnbindRequest(NspiUnbindFlags flags, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001FED2 File Offset: 0x0001E0D2
		public NspiUnbindRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiUnbindFlags)reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001FEEE File Offset: 0x0001E0EE
		public NspiUnbindFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001FEF6 File Offset: 0x0001E0F6
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000486 RID: 1158
		private readonly NspiUnbindFlags flags;
	}
}
