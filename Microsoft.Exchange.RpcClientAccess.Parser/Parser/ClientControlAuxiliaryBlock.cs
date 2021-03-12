using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000010 RID: 16
	internal sealed class ClientControlAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000363E File Offset: 0x0000183E
		public ClientControlAuxiliaryBlock(ClientControlFlags flags, TimeSpan expiryTime) : base(1, AuxiliaryBlockTypes.ClientControl)
		{
			this.flags = flags;
			this.expiryTime = (uint)expiryTime.TotalMilliseconds;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000365E File Offset: 0x0000185E
		internal ClientControlAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.flags = (ClientControlFlags)reader.ReadUInt32();
			this.expiryTime = reader.ReadUInt32();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000367F File Offset: 0x0000187F
		internal ClientControlFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003687 File Offset: 0x00001887
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.flags);
			writer.WriteUInt32(this.expiryTime);
		}

		// Token: 0x0400006C RID: 108
		private readonly ClientControlFlags flags;

		// Token: 0x0400006D RID: 109
		private readonly uint expiryTime;
	}
}
