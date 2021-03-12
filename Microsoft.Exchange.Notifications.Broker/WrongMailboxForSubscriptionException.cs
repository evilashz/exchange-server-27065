using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000042 RID: 66
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WrongMailboxForSubscriptionException : NotificationsBrokerPermanentException
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0000CF01 File Offset: 0x0000B101
		public WrongMailboxForSubscriptionException(Guid subscriptionMailboxGuid, Guid mailboxDataGuid) : base(ServiceStrings.WrongMailboxForSubscriptionException(subscriptionMailboxGuid, mailboxDataGuid))
		{
			this.subscriptionMailboxGuid = subscriptionMailboxGuid;
			this.mailboxDataGuid = mailboxDataGuid;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000CF1E File Offset: 0x0000B11E
		public WrongMailboxForSubscriptionException(Guid subscriptionMailboxGuid, Guid mailboxDataGuid, Exception innerException) : base(ServiceStrings.WrongMailboxForSubscriptionException(subscriptionMailboxGuid, mailboxDataGuid), innerException)
		{
			this.subscriptionMailboxGuid = subscriptionMailboxGuid;
			this.mailboxDataGuid = mailboxDataGuid;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000CF3C File Offset: 0x0000B13C
		protected WrongMailboxForSubscriptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.subscriptionMailboxGuid = (Guid)info.GetValue("subscriptionMailboxGuid", typeof(Guid));
			this.mailboxDataGuid = (Guid)info.GetValue("mailboxDataGuid", typeof(Guid));
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000CF91 File Offset: 0x0000B191
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("subscriptionMailboxGuid", this.subscriptionMailboxGuid);
			info.AddValue("mailboxDataGuid", this.mailboxDataGuid);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000CFC7 File Offset: 0x0000B1C7
		public Guid SubscriptionMailboxGuid
		{
			get
			{
				return this.subscriptionMailboxGuid;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000CFCF File Offset: 0x0000B1CF
		public Guid MailboxDataGuid
		{
			get
			{
				return this.mailboxDataGuid;
			}
		}

		// Token: 0x04000121 RID: 289
		private readonly Guid subscriptionMailboxGuid;

		// Token: 0x04000122 RID: 290
		private readonly Guid mailboxDataGuid;
	}
}
