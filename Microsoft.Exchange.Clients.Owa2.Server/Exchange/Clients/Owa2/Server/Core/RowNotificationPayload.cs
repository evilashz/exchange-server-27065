using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001AB RID: 427
	[DataContract]
	public class RowNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x0003B1C8 File Offset: 0x000393C8
		// (set) Token: 0x06000F40 RID: 3904 RVA: 0x0003B1D0 File Offset: 0x000393D0
		[DataMember(EmitDefaultValue = false)]
		public ItemType Item { get; set; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x0003B1D9 File Offset: 0x000393D9
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x0003B1E1 File Offset: 0x000393E1
		[DataMember(EmitDefaultValue = false)]
		public ConversationType Conversation { get; set; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0003B1EA File Offset: 0x000393EA
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0003B1F2 File Offset: 0x000393F2
		[DataMember]
		public string Prior { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0003B1FB File Offset: 0x000393FB
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0003B203 File Offset: 0x00039403
		[DataMember(EmitDefaultValue = false)]
		public bool Reload { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0003B20C File Offset: 0x0003940C
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0003B214 File Offset: 0x00039414
		[DataMember]
		public string FolderId { get; set; }
	}
}
