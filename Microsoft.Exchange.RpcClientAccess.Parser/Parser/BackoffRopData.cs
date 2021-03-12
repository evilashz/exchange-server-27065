using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000047 RID: 71
	internal struct BackoffRopData
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000730A File Offset: 0x0000550A
		internal BackoffRopData(RopId ropId, uint duration)
		{
			this.ropId = ropId;
			this.duration = duration;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000731A File Offset: 0x0000551A
		internal BackoffRopData(Reader reader)
		{
			this.ropId = (RopId)reader.ReadByte();
			this.duration = reader.ReadUInt32();
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00007334 File Offset: 0x00005534
		public RopId RopId
		{
			get
			{
				return this.ropId;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000733C File Offset: 0x0000553C
		public uint Duration
		{
			get
			{
				return this.duration;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007344 File Offset: 0x00005544
		internal void Serialize(Writer writer)
		{
			writer.WriteByte((byte)this.RopId);
			writer.WriteUInt32(this.Duration);
		}

		// Token: 0x040000D9 RID: 217
		private readonly RopId ropId;

		// Token: 0x040000DA RID: 218
		private readonly uint duration;
	}
}
