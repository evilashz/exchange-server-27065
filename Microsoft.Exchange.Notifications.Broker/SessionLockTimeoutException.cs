using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000043 RID: 67
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SessionLockTimeoutException : NotificationsBrokerTransientException
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		public SessionLockTimeoutException(Guid mailboxGuid, string cultureName) : base(ServiceStrings.SessionLockTimeoutException(mailboxGuid, cultureName))
		{
			this.mailboxGuid = mailboxGuid;
			this.cultureName = cultureName;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public SessionLockTimeoutException(Guid mailboxGuid, string cultureName, Exception innerException) : base(ServiceStrings.SessionLockTimeoutException(mailboxGuid, cultureName), innerException)
		{
			this.mailboxGuid = mailboxGuid;
			this.cultureName = cultureName;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D014 File Offset: 0x0000B214
		protected SessionLockTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mailboxGuid = (Guid)info.GetValue("mailboxGuid", typeof(Guid));
			this.cultureName = (string)info.GetValue("cultureName", typeof(string));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D069 File Offset: 0x0000B269
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mailboxGuid", this.mailboxGuid);
			info.AddValue("cultureName", this.cultureName);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000D09A File Offset: 0x0000B29A
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000D0A2 File Offset: 0x0000B2A2
		public string CultureName
		{
			get
			{
				return this.cultureName;
			}
		}

		// Token: 0x04000123 RID: 291
		private readonly Guid mailboxGuid;

		// Token: 0x04000124 RID: 292
		private readonly string cultureName;
	}
}
