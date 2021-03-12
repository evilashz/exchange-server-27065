using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001E5 RID: 485
	public sealed class MailboxRelocationRequest : RequestBase
	{
		// Token: 0x0600143D RID: 5181 RVA: 0x0002E47A File Offset: 0x0002C67A
		public MailboxRelocationRequest()
		{
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0002E482 File Offset: 0x0002C682
		internal MailboxRelocationRequest(IRequestIndexEntry index) : base(index)
		{
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0002E48B File Offset: 0x0002C68B
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[RequestJobSchema.ExchangeGuid];
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0002E49D File Offset: 0x0002C69D
		private new bool Protect
		{
			get
			{
				return base.Protect;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0002E4A5 File Offset: 0x0002C6A5
		private new RequestStyle RequestStyle
		{
			get
			{
				return base.RequestStyle;
			}
		}
	}
}
