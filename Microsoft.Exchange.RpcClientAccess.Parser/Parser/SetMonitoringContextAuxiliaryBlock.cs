using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000038 RID: 56
	internal sealed class SetMonitoringContextAuxiliaryBlock : AuxiliaryBlock
	{
		// Token: 0x0600010C RID: 268 RVA: 0x000049AB File Offset: 0x00002BAB
		public SetMonitoringContextAuxiliaryBlock() : base(1, AuxiliaryBlockTypes.SetMonitoringContext)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000049B6 File Offset: 0x00002BB6
		internal SetMonitoringContextAuxiliaryBlock(Reader reader) : base(reader)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000049BF File Offset: 0x00002BBF
		protected override void Serialize(Writer writer)
		{
			base.Serialize(writer);
		}
	}
}
