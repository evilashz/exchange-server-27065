using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000469 RID: 1129
	public enum WacRequestHandlerMetadata
	{
		// Token: 0x0400160B RID: 5643
		[DisplayName("WRH", "RT")]
		RequestType,
		// Token: 0x0400160C RID: 5644
		[DisplayName("WRH", "ST")]
		SessionElapsedTime,
		// Token: 0x0400160D RID: 5645
		[DisplayName("WRH", "LT")]
		LockWaitTime,
		// Token: 0x0400160E RID: 5646
		[DisplayName("WRH", "CT")]
		CobaltTime,
		// Token: 0x0400160F RID: 5647
		[DisplayName("WRH", "RQL")]
		CobaltRequestLength,
		// Token: 0x04001610 RID: 5648
		[DisplayName("WRH", "RSL")]
		CobaltResponseLength,
		// Token: 0x04001611 RID: 5649
		[DisplayName("WRH", "CH")]
		CacheHit,
		// Token: 0x04001612 RID: 5650
		[DisplayName("WRH", "U")]
		Updated,
		// Token: 0x04001613 RID: 5651
		[DisplayName("WRH", "CO")]
		CobaltOperations,
		// Token: 0x04001614 RID: 5652
		[DisplayName("WRH", "CR")]
		CobaltReads,
		// Token: 0x04001615 RID: 5653
		[DisplayName("WRH", "CBR")]
		CobaltBytesRead,
		// Token: 0x04001616 RID: 5654
		[DisplayName("WRH", "CW")]
		CobaltWrites,
		// Token: 0x04001617 RID: 5655
		[DisplayName("WRH", "CBW")]
		CobaltBytesWritten,
		// Token: 0x04001618 RID: 5656
		[DisplayName("WRH", "DBC")]
		DiskBlobCount,
		// Token: 0x04001619 RID: 5657
		[DisplayName("WRH", "DBS")]
		DiskBlobSize,
		// Token: 0x0400161A RID: 5658
		[DisplayName("WRH", "WSN")]
		WopiServerName,
		// Token: 0x0400161B RID: 5659
		[DisplayName("WRH", "WCV")]
		WopiClientVersion,
		// Token: 0x0400161C RID: 5660
		[DisplayName("WRH", "WCID")]
		WopiCorrelationId,
		// Token: 0x0400161D RID: 5661
		[DisplayName("WRH", "ESID")]
		ExchangeSessionId,
		// Token: 0x0400161E RID: 5662
		[DisplayName("WRH", "UA")]
		UserAgent,
		// Token: 0x0400161F RID: 5663
		[DisplayName("WRH", "URL")]
		RequestUrl,
		// Token: 0x04001620 RID: 5664
		[DisplayName("WRH", "MCT")]
		MdbCacheReloadTime,
		// Token: 0x04001621 RID: 5665
		[DisplayName("WRH", "MCS")]
		MdbCacheSize,
		// Token: 0x04001622 RID: 5666
		[DisplayName("WRH", "ED")]
		ErrorDetails
	}
}
