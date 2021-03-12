using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033E RID: 830
	internal sealed class RopSetPropertiesNoReplicate : RopSetPropertiesBase
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000350EA File Offset: 0x000332EA
		internal override RopId RopId
		{
			get
			{
				return RopId.SetPropertiesNoReplicate;
			}
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000350EE File Offset: 0x000332EE
		internal static Rop CreateRop()
		{
			return new RopSetPropertiesNoReplicate();
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x000350F5 File Offset: 0x000332F5
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetPropertiesNoReplicate.resultFactory;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000350FC File Offset: 0x000332FC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetPropertiesNoReplicate(serverObject, base.Properties, RopSetPropertiesNoReplicate.resultFactory);
		}

		// Token: 0x04000A8B RID: 2699
		private const RopId RopType = RopId.SetPropertiesNoReplicate;

		// Token: 0x04000A8C RID: 2700
		private static SetPropertiesNoReplicateResultFactory resultFactory = new SetPropertiesNoReplicateResultFactory();
	}
}
