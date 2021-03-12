using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000078 RID: 120
	public abstract class ObjectMovedCopiedNotificationEvent : ObjectNotificationEvent
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x0004BD28 File Offset: 0x00049F28
		public ObjectMovedCopiedNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int? documentId, int? conversationDocumentId, ExchangeId oldFid, ExchangeId oldMid, ExchangeId oldParentFid, int? oldConversationDocumentId, string objectClass) : base(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, documentId, conversationDocumentId, objectClass)
		{
			this.oldFid = oldFid;
			this.oldMid = oldMid;
			this.oldParentFid = oldParentFid;
			this.oldConversationDocumentId = oldConversationDocumentId;
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0004BD72 File Offset: 0x00049F72
		public ExchangeId OldFid
		{
			get
			{
				return this.oldFid;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0004BD7A File Offset: 0x00049F7A
		public ExchangeId OldMid
		{
			get
			{
				return this.oldMid;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0004BD82 File Offset: 0x00049F82
		public ExchangeId OldParentFid
		{
			get
			{
				return this.oldParentFid;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0004BD8A File Offset: 0x00049F8A
		public int? OldConversationDocumentId
		{
			get
			{
				return this.oldConversationDocumentId;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0004BD94 File Offset: 0x00049F94
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" OldFid:[");
			sb.Append(this.oldFid);
			sb.Append("] OldMid:[");
			sb.Append(this.oldMid);
			sb.Append("] OldParentFid:[");
			sb.Append(this.oldParentFid);
			sb.Append("]");
			sb.Append("] OldConversationDocumentId:[");
			sb.Append(this.oldConversationDocumentId);
			sb.Append("]");
		}

		// Token: 0x0400046F RID: 1135
		private readonly ExchangeId oldFid;

		// Token: 0x04000470 RID: 1136
		private readonly ExchangeId oldMid;

		// Token: 0x04000471 RID: 1137
		private readonly ExchangeId oldParentFid;

		// Token: 0x04000472 RID: 1138
		private readonly int? oldConversationDocumentId;
	}
}
