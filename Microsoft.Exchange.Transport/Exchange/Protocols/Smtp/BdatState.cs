using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E1 RID: 1249
	internal class BdatState
	{
		// Token: 0x060039C1 RID: 14785 RVA: 0x000EECF4 File Offset: 0x000ECEF4
		public BdatState(string messageId, Stream bdatStream, long chunkSize, long originalMessageSize, long messageSizeLimit, bool isEohSeen)
		{
			ArgumentValidator.ThrowIfNull("bdatStream", bdatStream);
			this.MessageId = messageId;
			this.BdatStream = bdatStream;
			this.AccumulatedChunkSize = chunkSize;
			this.OriginalMessageSize = originalMessageSize;
			this.MessageSizeLimit = messageSizeLimit;
			this.IsEohSeen = isEohSeen;
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060039C2 RID: 14786 RVA: 0x000EED34 File Offset: 0x000ECF34
		// (set) Token: 0x060039C3 RID: 14787 RVA: 0x000EED3C File Offset: 0x000ECF3C
		public string MessageId { get; private set; }

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000EED45 File Offset: 0x000ECF45
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x000EED4D File Offset: 0x000ECF4D
		public Stream BdatStream { get; private set; }

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000EED56 File Offset: 0x000ECF56
		// (set) Token: 0x060039C7 RID: 14791 RVA: 0x000EED5E File Offset: 0x000ECF5E
		public long AccumulatedChunkSize { get; private set; }

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000EED67 File Offset: 0x000ECF67
		// (set) Token: 0x060039C9 RID: 14793 RVA: 0x000EED6F File Offset: 0x000ECF6F
		public long OriginalMessageSize { get; private set; }

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000EED78 File Offset: 0x000ECF78
		// (set) Token: 0x060039CB RID: 14795 RVA: 0x000EED80 File Offset: 0x000ECF80
		public long MessageSizeLimit { get; private set; }

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000EED89 File Offset: 0x000ECF89
		// (set) Token: 0x060039CD RID: 14797 RVA: 0x000EED91 File Offset: 0x000ECF91
		public bool IsEohSeen { get; private set; }

		// Token: 0x060039CE RID: 14798 RVA: 0x000EED9A File Offset: 0x000ECF9A
		public void IncrementAccumulatedChunkSize(long chunkSize)
		{
			this.AccumulatedChunkSize += chunkSize;
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000EEDAA File Offset: 0x000ECFAA
		public void UpdateState(string messageId, long originalMessageSize, long messageSizeLimit, bool isEohSeen)
		{
			this.MessageId = messageId;
			this.OriginalMessageSize = originalMessageSize;
			this.MessageSizeLimit = messageSizeLimit;
			this.IsEohSeen = isEohSeen;
		}
	}
}
