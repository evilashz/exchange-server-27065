using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200007F RID: 127
	public class NewMailNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x0004C330 File Offset: 0x0004A530
		public NewMailNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, int documentId, int? conversationDocumentId, string messageClass, int messageFlags) : base(database, mailboxNumber, EventType.NewMail, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, fid, new int?(documentId), conversationDocumentId, messageClass)
		{
			Statistics.NotificationTypes.NewMail.Bump();
			this.messageFlags = messageFlags;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0004C370 File Offset: 0x0004A570
		public int MessageFlags
		{
			get
			{
				return this.messageFlags;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0004C378 File Offset: 0x0004A578
		public string MessageClass
		{
			get
			{
				return base.ObjectClass;
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0004C380 File Offset: 0x0004A580
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("NewMailNotificationEvent");
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0004C38E File Offset: 0x0004A58E
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" MessageFlags:[");
			sb.Append(this.messageFlags);
			sb.Append("]");
		}

		// Token: 0x04000476 RID: 1142
		private int messageFlags;
	}
}
