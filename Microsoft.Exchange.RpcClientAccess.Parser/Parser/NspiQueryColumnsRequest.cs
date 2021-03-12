using System;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001DC RID: 476
	internal sealed class NspiQueryColumnsRequest : MapiHttpRequest
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001F4E5 File Offset: 0x0001D6E5
		public NspiQueryColumnsRequest(NspiQueryColumnsFlags flags, NspiQueryColumnsMapiFlags mapiFlags, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.mapiFlags = mapiFlags;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public NspiQueryColumnsRequest(Reader reader) : base(reader)
		{
			this.flags = (NspiQueryColumnsFlags)reader.ReadUInt32();
			this.mapiFlags = (NspiQueryColumnsMapiFlags)reader.ReadUInt32();
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0001F524 File Offset: 0x0001D724
		public NspiQueryColumnsFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001F52C File Offset: 0x0001D72C
		public NspiQueryColumnsMapiFlags MapiFlags
		{
			get
			{
				return this.mapiFlags;
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001F534 File Offset: 0x0001D734
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUInt32((uint)this.mapiFlags);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x04000463 RID: 1123
		private readonly NspiQueryColumnsFlags flags;

		// Token: 0x04000464 RID: 1124
		private readonly NspiQueryColumnsMapiFlags mapiFlags;
	}
}
