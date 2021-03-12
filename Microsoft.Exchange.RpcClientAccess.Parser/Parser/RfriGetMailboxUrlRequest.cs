using System;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001ED RID: 493
	internal sealed class RfriGetMailboxUrlRequest : MapiHttpRequest
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x000202C6 File Offset: 0x0001E4C6
		public RfriGetMailboxUrlRequest(RfriGetMailboxUrlFlags flags, string serverDn, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.serverDn = serverDn;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000202DD File Offset: 0x0001E4DD
		public RfriGetMailboxUrlRequest(Reader reader) : base(reader)
		{
			this.flags = (RfriGetMailboxUrlFlags)reader.ReadUInt32();
			this.serverDn = reader.ReadUnicodeString(StringFlags.IncludeNull);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00020306 File Offset: 0x0001E506
		public RfriGetMailboxUrlFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002030E File Offset: 0x0001E50E
		public string ServerDn
		{
			get
			{
				return this.serverDn;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00020316 File Offset: 0x0001E516
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUnicodeString(this.serverDn, StringFlags.IncludeNull);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400048F RID: 1167
		private readonly RfriGetMailboxUrlFlags flags;

		// Token: 0x04000490 RID: 1168
		private readonly string serverDn;
	}
}
