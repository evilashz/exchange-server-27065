using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200033D RID: 829
	internal sealed class RopSetProperties : RopSetPropertiesBase
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x000350AA File Offset: 0x000332AA
		internal override RopId RopId
		{
			get
			{
				return RopId.SetProperties;
			}
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x000350AE File Offset: 0x000332AE
		internal static Rop CreateRop()
		{
			return new RopSetProperties();
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x000350B5 File Offset: 0x000332B5
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetProperties.resultFactory;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000350BC File Offset: 0x000332BC
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetProperties(serverObject, base.Properties, RopSetProperties.resultFactory);
		}

		// Token: 0x04000A89 RID: 2697
		private const RopId RopType = RopId.SetProperties;

		// Token: 0x04000A8A RID: 2698
		private static SetPropertiesResultFactory resultFactory = new SetPropertiesResultFactory();
	}
}
