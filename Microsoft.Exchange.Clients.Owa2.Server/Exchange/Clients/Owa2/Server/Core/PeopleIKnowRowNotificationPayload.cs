using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A1 RID: 417
	[DataContract]
	public class PeopleIKnowRowNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x0003A99B File Offset: 0x00038B9B
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x0003A9A3 File Offset: 0x00038BA3
		[DataMember]
		public Persona[] Personas { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0003A9AC File Offset: 0x00038BAC
		// (set) Token: 0x06000F10 RID: 3856 RVA: 0x0003A9B4 File Offset: 0x00038BB4
		[DataMember]
		public string PersonaEmailAdress { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003A9BD File Offset: 0x00038BBD
		// (set) Token: 0x06000F12 RID: 3858 RVA: 0x0003A9C5 File Offset: 0x00038BC5
		[DataMember]
		public int PersonaUnreadCount { get; set; }
	}
}
