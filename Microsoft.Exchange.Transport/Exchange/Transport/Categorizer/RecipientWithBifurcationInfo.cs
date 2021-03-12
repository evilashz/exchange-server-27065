using System;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000286 RID: 646
	internal struct RecipientWithBifurcationInfo<T> where T : IEquatable<T>, IComparable<T>, new()
	{
		// Token: 0x06001BD3 RID: 7123 RVA: 0x00072585 File Offset: 0x00070785
		public RecipientWithBifurcationInfo(MailRecipient recipient, T bifurcationInfo)
		{
			this.recipient = recipient;
			this.bifurcationInfo = bifurcationInfo;
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00072595 File Offset: 0x00070795
		public MailRecipient Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001BD5 RID: 7125 RVA: 0x0007259D File Offset: 0x0007079D
		// (set) Token: 0x06001BD6 RID: 7126 RVA: 0x000725A5 File Offset: 0x000707A5
		public T BifurcationInfo
		{
			get
			{
				return this.bifurcationInfo;
			}
			set
			{
				this.bifurcationInfo = value;
			}
		}

		// Token: 0x04000D24 RID: 3364
		private MailRecipient recipient;

		// Token: 0x04000D25 RID: 3365
		private T bifurcationInfo;
	}
}
