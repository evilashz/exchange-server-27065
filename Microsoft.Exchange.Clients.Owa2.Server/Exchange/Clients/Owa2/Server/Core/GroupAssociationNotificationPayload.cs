using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000174 RID: 372
	[DataContract]
	internal class GroupAssociationNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00033955 File Offset: 0x00031B55
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0003395D File Offset: 0x00031B5D
		[DataMember]
		public ModernGroupType Group { get; set; }
	}
}
