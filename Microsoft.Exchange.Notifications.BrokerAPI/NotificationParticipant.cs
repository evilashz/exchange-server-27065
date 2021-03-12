using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000028 RID: 40
	[KnownType(typeof(NotificationParticipantLocationKind))]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class NotificationParticipant
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000394B File Offset: 0x00001B4B
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003953 File Offset: 0x00001B53
		[DataMember(EmitDefaultValue = false)]
		public NotificationParticipantLocationKind LocationKind { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000395C File Offset: 0x00001B5C
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003964 File Offset: 0x00001B64
		[DataMember(EmitDefaultValue = false)]
		public OrganizationId OrganizationId { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000396D File Offset: 0x00001B6D
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003975 File Offset: 0x00001B75
		[DataMember(EmitDefaultValue = false)]
		public Guid ExternalDirectoryOrganizationId { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000397E File Offset: 0x00001B7E
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003986 File Offset: 0x00001B86
		[DataMember(EmitDefaultValue = false)]
		public Guid DatabaseGuid { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000398F File Offset: 0x00001B8F
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003997 File Offset: 0x00001B97
		[DataMember(EmitDefaultValue = false)]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000039A0 File Offset: 0x00001BA0
		// (set) Token: 0x060000EC RID: 236 RVA: 0x000039A8 File Offset: 0x00001BA8
		[DataMember(EmitDefaultValue = false)]
		public string MailboxSmtp { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000039B1 File Offset: 0x00001BB1
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000039B9 File Offset: 0x00001BB9
		[DataMember(EmitDefaultValue = false)]
		public string FrontEndUrl { get; set; }

		// Token: 0x060000EF RID: 239 RVA: 0x000039C4 File Offset: 0x00001BC4
		internal NotificationParticipant AsNotificationSender()
		{
			NotificationParticipant notificationParticipant = new NotificationParticipant
			{
				DatabaseGuid = this.DatabaseGuid,
				MailboxGuid = this.MailboxGuid,
				MailboxSmtp = this.MailboxSmtp
			};
			if (this.OrganizationId != OrganizationId.ForestWideOrgId)
			{
				notificationParticipant.ExternalDirectoryOrganizationId = new Guid(this.OrganizationId.ToExternalDirectoryOrganizationId());
			}
			return notificationParticipant;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003A28 File Offset: 0x00001C28
		internal NotificationParticipant AsNotificationReceiver()
		{
			return new NotificationParticipant
			{
				MailboxGuid = this.MailboxGuid,
				MailboxSmtp = this.MailboxSmtp,
				FrontEndUrl = this.FrontEndUrl
			};
		}
	}
}
