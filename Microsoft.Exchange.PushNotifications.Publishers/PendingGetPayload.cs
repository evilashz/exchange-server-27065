using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B5 RID: 181
	internal sealed class PendingGetPayload
	{
		// Token: 0x06000612 RID: 1554 RVA: 0x00013B3B File Offset: 0x00011D3B
		public PendingGetPayload(int? emailCount, bool hasVoiceMail = false)
		{
			this.EmailCount = emailCount;
			this.HasVoiceMail = hasVoiceMail;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00013B51 File Offset: 0x00011D51
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x00013B59 File Offset: 0x00011D59
		public int? EmailCount { get; private set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00013B62 File Offset: 0x00011D62
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00013B6A File Offset: 0x00011D6A
		public bool HasVoiceMail { get; private set; }

		// Token: 0x06000617 RID: 1559 RVA: 0x00013B73 File Offset: 0x00011D73
		public override string ToString()
		{
			if (this.toStringCache == null)
			{
				this.toStringCache = string.Format("{{emailCount:{0}; hasVoicemail:{1}}}", this.EmailCount, this.HasVoiceMail);
			}
			return this.toStringCache;
		}

		// Token: 0x04000306 RID: 774
		private string toStringCache;
	}
}
