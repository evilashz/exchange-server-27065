using System;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000374 RID: 884
	internal class QueuedBlockMsg
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x000A6AF1 File Offset: 0x000A4CF1
		// (set) Token: 0x0600237E RID: 9086 RVA: 0x000A6AF9 File Offset: 0x000A4CF9
		public JET_EMITDATACTX EmitContext { get; private set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x000A6B02 File Offset: 0x000A4D02
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x000A6B0A File Offset: 0x000A4D0A
		public byte[] LogDataBuf { get; private set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x000A6B13 File Offset: 0x000A4D13
		// (set) Token: 0x06002382 RID: 9090 RVA: 0x000A6B1B File Offset: 0x000A4D1B
		public int LogDataStartOffset { get; private set; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000A6B24 File Offset: 0x000A4D24
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x000A6B2C File Offset: 0x000A4D2C
		public int LogDataLength { get; private set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000A6B35 File Offset: 0x000A4D35
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x000A6B3D File Offset: 0x000A4D3D
		public int CompressedLogDataLength { get; private set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000A6B46 File Offset: 0x000A4D46
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x000A6B4E File Offset: 0x000A4D4E
		public QueuedBlockMsg NextMsg { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000A6B57 File Offset: 0x000A4D57
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x000A6B5F File Offset: 0x000A4D5F
		public bool WasProcessed { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000A6B68 File Offset: 0x000A4D68
		// (set) Token: 0x0600238C RID: 9100 RVA: 0x000A6B70 File Offset: 0x000A4D70
		public long ReadDurationInTics { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000A6B79 File Offset: 0x000A4D79
		// (set) Token: 0x0600238E RID: 9102 RVA: 0x000A6B81 File Offset: 0x000A4D81
		public long RequestAckCounter { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x000A6B8A File Offset: 0x000A4D8A
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x000A6B92 File Offset: 0x000A4D92
		public DateTime MessageUtc { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x000A6B9B File Offset: 0x000A4D9B
		// (set) Token: 0x06002392 RID: 9106 RVA: 0x000A6BA3 File Offset: 0x000A4DA3
		public IOBuffer IOBuffer { get; set; }

		// Token: 0x06002393 RID: 9107 RVA: 0x000A6BAC File Offset: 0x000A4DAC
		public QueuedBlockMsg(JET_EMITDATACTX emitCtx, byte[] logDataBuf, int logDataStartOffset, int compressedLogDataLength)
		{
			this.EmitContext = emitCtx;
			this.LogDataBuf = logDataBuf;
			this.LogDataStartOffset = logDataStartOffset;
			this.LogDataLength = (int)emitCtx.cbLogData;
			this.CompressedLogDataLength = compressedLogDataLength;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x000A6BDE File Offset: 0x000A4DDE
		public int GetMessageSize()
		{
			return this.LogDataLength + 50;
		}
	}
}
