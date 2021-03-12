using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001BE RID: 446
	[DataContract]
	internal class UnseenItemNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0003D469 File Offset: 0x0003B669
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0003D471 File Offset: 0x0003B671
		[DataMember(Name = "UnseenData", IsRequired = true)]
		public UnseenDataType UnseenData { get; set; }
	}
}
