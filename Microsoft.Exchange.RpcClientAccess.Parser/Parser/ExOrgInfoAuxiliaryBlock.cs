using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200001A RID: 26
	internal sealed class ExOrgInfoAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000393C File Offset: 0x00001B3C
		public ExOrgInfoAuxiliaryBlock(ExOrgInfoFlags blockOrganizationFlags) : base(1, AuxiliaryBlockTypes.ExOrgInfo)
		{
			this.organizationFlags = blockOrganizationFlags;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000394E File Offset: 0x00001B4E
		internal ExOrgInfoAuxiliaryBlock(Reader reader) : base(reader)
		{
			this.organizationFlags = (ExOrgInfoFlags)reader.ReadUInt32();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003963 File Offset: 0x00001B63
		public ExOrgInfoFlags OrganizationFlags
		{
			get
			{
				return this.organizationFlags;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000396B File Offset: 0x00001B6B
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.organizationFlags);
		}

		// Token: 0x0400007D RID: 125
		private readonly ExOrgInfoFlags organizationFlags;
	}
}
