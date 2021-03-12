using System;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000077 RID: 119
	public abstract class ObjectNotificationEvent : LogicalModelNotificationEvent
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0004BAF4 File Offset: 0x00049CF4
		public ObjectNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int? documentId, int? conversationDocumentId, string objectClass) : this(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, Microsoft.Exchange.Server.Storage.LogicalDataModel.ExtendedEventFlags.None, fid, mid, parentFid, documentId, conversationDocumentId, objectClass)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0004BB20 File Offset: 0x00049D20
		public ObjectNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int? documentId, int? conversationDocumentId, string objectClass) : this(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, extendedEventFlags, fid, mid, parentFid, documentId, conversationDocumentId, objectClass, null)
		{
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0004BB54 File Offset: 0x00049D54
		public ObjectNotificationEvent(StoreDatabase database, int mailboxNumber, EventType eventType, WindowsIdentity userIdentity, ClientType clientType, EventFlags eventFlags, ExtendedEventFlags extendedEventFlags, ExchangeId fid, ExchangeId mid, ExchangeId parentFid, int? documentId, int? conversationDocumentId, string objectClass, Guid? userIdentityContext) : base(database, mailboxNumber, eventType, userIdentity, clientType, eventFlags, userIdentityContext)
		{
			this.fid = fid;
			this.mid = mid;
			this.parentFid = parentFid;
			this.documentId = documentId;
			this.conversationDocumentId = conversationDocumentId;
			this.objectClass = objectClass;
			this.extendedEventFlags = new ExtendedEventFlags?(extendedEventFlags);
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0004BBB0 File Offset: 0x00049DB0
		public bool IsMessageEvent
		{
			get
			{
				return this.mid.IsValid;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0004BBCB File Offset: 0x00049DCB
		public bool IsFolderEvent
		{
			get
			{
				return !this.IsMessageEvent;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0004BBD6 File Offset: 0x00049DD6
		public ExchangeId Fid
		{
			get
			{
				return this.fid;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0004BBDE File Offset: 0x00049DDE
		public ExchangeId Mid
		{
			get
			{
				return this.mid;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0004BBE6 File Offset: 0x00049DE6
		public ExchangeId ParentFid
		{
			get
			{
				return this.parentFid;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0004BBEE File Offset: 0x00049DEE
		public int? DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0004BBF6 File Offset: 0x00049DF6
		public int? ConversationDocumentId
		{
			get
			{
				return this.conversationDocumentId;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0004BBFE File Offset: 0x00049DFE
		public string ObjectClass
		{
			get
			{
				return this.objectClass;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0004BC06 File Offset: 0x00049E06
		public ExtendedEventFlags? ExtendedEventFlags
		{
			get
			{
				return this.extendedEventFlags;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0004BC0E File Offset: 0x00049E0E
		protected bool IsSameObject(ObjectNotificationEvent otherEvent)
		{
			return this.Mid == otherEvent.Mid && this.Fid == otherEvent.Fid;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0004BC38 File Offset: 0x00049E38
		protected override void AppendFields(StringBuilder sb)
		{
			base.AppendFields(sb);
			sb.Append(" Fid:[");
			sb.Append(this.fid);
			sb.Append("] Mid:[");
			sb.Append(this.mid);
			sb.Append("] ParentFid:[");
			sb.Append(this.parentFid);
			sb.Append("] DocumentId:[");
			sb.Append(this.documentId);
			sb.Append("] ConversationDocumentId:[");
			sb.Append(this.conversationDocumentId);
			sb.Append("] ObjectClass:[");
			sb.Append(this.objectClass);
			sb.Append("] ExtendedEventFlags:[");
			sb.Append(this.extendedEventFlags);
			sb.Append("]");
		}

		// Token: 0x04000468 RID: 1128
		private readonly ExchangeId fid;

		// Token: 0x04000469 RID: 1129
		private readonly ExchangeId mid;

		// Token: 0x0400046A RID: 1130
		private readonly ExchangeId parentFid;

		// Token: 0x0400046B RID: 1131
		private readonly int? documentId;

		// Token: 0x0400046C RID: 1132
		private readonly int? conversationDocumentId;

		// Token: 0x0400046D RID: 1133
		private readonly ExtendedEventFlags? extendedEventFlags;

		// Token: 0x0400046E RID: 1134
		private readonly string objectClass;
	}
}
