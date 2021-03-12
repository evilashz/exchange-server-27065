using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000179 RID: 377
	internal class Attachment16Data : Attachment14Data
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x0005C95E File Offset: 0x0005AB5E
		public Attachment16Data(AttachmentAction changeType)
		{
			this.ChangeType = changeType;
			this.ContentType = string.Empty;
			this.ClientId = null;
			this.Content = null;
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x0005C986 File Offset: 0x0005AB86
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x0005C98E File Offset: 0x0005AB8E
		public AttachmentAction ChangeType { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x0005C997 File Offset: 0x0005AB97
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x0005C99F File Offset: 0x0005AB9F
		public string ContentType { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0005C9A8 File Offset: 0x0005ABA8
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x0005C9B0 File Offset: 0x0005ABB0
		public string ClientId { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0005C9B9 File Offset: 0x0005ABB9
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x0005C9C1 File Offset: 0x0005ABC1
		public byte[] Content { get; set; }
	}
}
