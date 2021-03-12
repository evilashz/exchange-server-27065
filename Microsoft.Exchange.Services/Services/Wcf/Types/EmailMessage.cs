using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009E2 RID: 2530
	[DataContract]
	public class EmailMessage
	{
		// Token: 0x06004768 RID: 18280 RVA: 0x001003C0 File Offset: 0x000FE5C0
		public EmailMessage(bool isRead, string subject, string from)
		{
			this.IsRead = isRead;
			this.Subject = subject;
			this.From = from;
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004769 RID: 18281 RVA: 0x001003DD File Offset: 0x000FE5DD
		// (set) Token: 0x0600476A RID: 18282 RVA: 0x001003E5 File Offset: 0x000FE5E5
		[DataMember]
		public bool IsRead { get; set; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x0600476B RID: 18283 RVA: 0x001003EE File Offset: 0x000FE5EE
		// (set) Token: 0x0600476C RID: 18284 RVA: 0x001003F6 File Offset: 0x000FE5F6
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x001003FF File Offset: 0x000FE5FF
		// (set) Token: 0x0600476E RID: 18286 RVA: 0x00100407 File Offset: 0x000FE607
		[DataMember]
		public string From { get; set; }
	}
}
