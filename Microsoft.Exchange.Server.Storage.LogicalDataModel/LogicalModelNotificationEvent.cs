using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000076 RID: 118
	public abstract class LogicalModelNotificationEvent : NotificationEvent
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0004BA18 File Offset: 0x00049C18
		public LogicalModelNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, Guid? userIdentityContext) : base(database, mailboxNumber, (int)eventType, userIdentityContext)
		{
			this.userIdentity = userIdentity;
			this.clientType = clientType;
			this.eventFlags = eventFlags;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0004BA3D File Offset: 0x00049C3D
		public EventType EventType
		{
			get
			{
				return (EventType)base.EventTypeValue;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0004BA45 File Offset: 0x00049C45
		public WindowsIdentity UserIdentity
		{
			get
			{
				return this.userIdentity;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0004BA4D File Offset: 0x00049C4D
		public ClientType ClientType
		{
			get
			{
				return this.clientType;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0004BA55 File Offset: 0x00049C55
		public EventFlags EventFlags
		{
			get
			{
				return this.eventFlags;
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0004BA60 File Offset: 0x00049C60
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" EventType:[");
			sb.Append(this.EventType);
			sb.Append("] UserIdentity:[");
			sb.Append(this.userIdentity);
			sb.Append("] clientType:[");
			sb.Append(this.clientType);
			sb.Append("] EventFlags:[");
			sb.Append(this.eventFlags);
			sb.Append("]");
		}

		// Token: 0x04000465 RID: 1125
		private WindowsIdentity userIdentity;

		// Token: 0x04000466 RID: 1126
		private ClientType clientType;

		// Token: 0x04000467 RID: 1127
		private EventFlags eventFlags;
	}
}
