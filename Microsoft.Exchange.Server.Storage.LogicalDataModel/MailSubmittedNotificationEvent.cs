using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000080 RID: 128
	public class MailSubmittedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0004C3BC File Offset: 0x0004A5BC
		public MailSubmittedNotificationEvent(StoreDatabase database, int mailboxNumber, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, int documentId, int? conversationDocumentId, string messageClass) : base(database, mailboxNumber, EventType.MailSubmitted, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, fid, new int?(documentId), conversationDocumentId, messageClass)
		{
			Statistics.NotificationTypes.MailSubmitted.Bump();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0004C3F8 File Offset: 0x0004A5F8
		protected override void AppendClassName(StringBuilder sb)
		{
			sb.Append("MailSubmittedNotificationEvent");
		}
	}
}
