using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000040 RID: 64
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BrokerMailboxNotFoundException : NotificationsBrokerTransientException
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000CE0C File Offset: 0x0000B00C
		public BrokerMailboxNotFoundException(Guid mailboxGuid) : base(ServiceStrings.BrokerMailboxNotFoundException(mailboxGuid))
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000CE21 File Offset: 0x0000B021
		public BrokerMailboxNotFoundException(Guid mailboxGuid, Exception innerException) : base(ServiceStrings.BrokerMailboxNotFoundException(mailboxGuid), innerException)
		{
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000CE37 File Offset: 0x0000B037
		protected BrokerMailboxNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxGuid = (Guid)info.GetValue("mailboxGuid", typeof(Guid));
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000CE61 File Offset: 0x0000B061
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxGuid", this.mailboxGuid);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000CE81 File Offset: 0x0000B081
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x0400011F RID: 287
		private readonly Guid mailboxGuid;
	}
}
