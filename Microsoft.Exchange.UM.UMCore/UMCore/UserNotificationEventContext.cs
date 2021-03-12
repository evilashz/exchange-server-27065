using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000268 RID: 616
	internal class UserNotificationEventContext
	{
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00051080 File Offset: 0x0004F280
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00051088 File Offset: 0x0004F288
		public RedirectionTarget.ResultSet Backend { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00051091 File Offset: 0x0004F291
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00051099 File Offset: 0x0004F299
		public string User { get; set; }
	}
}
