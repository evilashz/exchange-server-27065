using System;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000371 RID: 881
	public enum ShadowBreadcrumbs
	{
		// Token: 0x04001386 RID: 4998
		Empty,
		// Token: 0x04001387 RID: 4999
		Open,
		// Token: 0x04001388 RID: 5000
		PrepareForCommand,
		// Token: 0x04001389 RID: 5001
		WriteInternal,
		// Token: 0x0400138A RID: 5002
		WriteQueuingBuffer,
		// Token: 0x0400138B RID: 5003
		WriteQueuingCommand,
		// Token: 0x0400138C RID: 5004
		WriteDequeuingBuffer,
		// Token: 0x0400138D RID: 5005
		WriteDequeuingCommand,
		// Token: 0x0400138E RID: 5006
		WriteToProxy,
		// Token: 0x0400138F RID: 5007
		WriteProxyDataComplete,
		// Token: 0x04001390 RID: 5008
		WriteAfterCloseSkipped,
		// Token: 0x04001391 RID: 5009
		WriteNewBdatCommand,
		// Token: 0x04001392 RID: 5010
		NextHopConnectionFailedOver,
		// Token: 0x04001393 RID: 5011
		NextHopConnectionAckMailItem,
		// Token: 0x04001394 RID: 5012
		NextHopAckConnection,
		// Token: 0x04001395 RID: 5013
		SessionAckConnectionFailure,
		// Token: 0x04001396 RID: 5014
		SessionAckMessage,
		// Token: 0x04001397 RID: 5015
		Complete,
		// Token: 0x04001398 RID: 5016
		Close,
		// Token: 0x04001399 RID: 5017
		MessageShadowingComplete,
		// Token: 0x0400139A RID: 5018
		ProxyFailover,
		// Token: 0x0400139B RID: 5019
		LocalMessageDiscarded,
		// Token: 0x0400139C RID: 5020
		LocalMessageRejected
	}
}
