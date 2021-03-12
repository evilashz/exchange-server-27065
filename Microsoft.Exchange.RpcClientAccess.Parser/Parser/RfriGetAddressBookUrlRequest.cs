using System;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020001EB RID: 491
	internal sealed class RfriGetAddressBookUrlRequest : MapiHttpRequest
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x00020201 File Offset: 0x0001E401
		public RfriGetAddressBookUrlRequest(RfriGetAddressBookUrlFlags flags, string userDn, ArraySegment<byte> auxiliaryBuffer) : base(auxiliaryBuffer)
		{
			this.flags = flags;
			this.userDn = userDn;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00020218 File Offset: 0x0001E418
		public RfriGetAddressBookUrlRequest(Reader reader) : base(reader)
		{
			this.flags = (RfriGetAddressBookUrlFlags)reader.ReadUInt32();
			this.userDn = reader.ReadUnicodeString(StringFlags.IncludeNull);
			base.ParseAuxiliaryBuffer(reader);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00020241 File Offset: 0x0001E441
		public RfriGetAddressBookUrlFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00020249 File Offset: 0x0001E449
		public string UserDn
		{
			get
			{
				return this.userDn;
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00020251 File Offset: 0x0001E451
		public override void Serialize(Writer writer)
		{
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUnicodeString(this.userDn, StringFlags.IncludeNull);
			base.SerializeAuxiliaryBuffer(writer);
		}

		// Token: 0x0400048C RID: 1164
		private readonly RfriGetAddressBookUrlFlags flags;

		// Token: 0x0400048D RID: 1165
		private readonly string userDn;
	}
}
