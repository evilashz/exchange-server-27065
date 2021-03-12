using System;
using System.Text;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000DB RID: 219
	public abstract class NotificationEvent
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x00029C91 File Offset: 0x00027E91
		protected NotificationEvent(StoreDatabase database, int mailboxNumber, int eventTypeValue, Guid? userIdentityContext)
		{
			this.database = database;
			this.mailboxNumber = mailboxNumber;
			this.eventTypeValue = eventTypeValue;
			this.userIdentityContext = userIdentityContext;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00029CB6 File Offset: 0x00027EB6
		public StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00029CBE File Offset: 0x00027EBE
		public Guid MdbGuid
		{
			get
			{
				return this.database.MdbGuid;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00029CCB File Offset: 0x00027ECB
		public int MailboxNumber
		{
			get
			{
				return this.mailboxNumber;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00029CD3 File Offset: 0x00027ED3
		public int EventTypeValue
		{
			get
			{
				return this.eventTypeValue;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00029CDB File Offset: 0x00027EDB
		public Guid? UserIdentityContext
		{
			get
			{
				return this.userIdentityContext;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00029CE3 File Offset: 0x00027EE3
		public virtual NotificationEvent.RedundancyStatus GetRedundancyStatus(NotificationEvent oldNev)
		{
			return NotificationEvent.RedundancyStatus.FlagStopSearch;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00029CEA File Offset: 0x00027EEA
		public virtual NotificationEvent MergeWithOldEvent(NotificationEvent oldNev)
		{
			return this;
		}

		// Token: 0x060008B6 RID: 2230
		protected abstract void AppendClassName(StringBuilder sb);

		// Token: 0x060008B7 RID: 2231 RVA: 0x00029CF0 File Offset: 0x00027EF0
		protected virtual void AppendFields(StringBuilder sb)
		{
			sb.Append("MdbGuid:[");
			sb.Append(this.Database.MdbGuid);
			sb.Append("] MailboxNumber:[");
			sb.Append(this.MailboxNumber);
			sb.Append("] EventTypeValue:[");
			sb.Append(this.EventTypeValue);
			sb.Append("] UserIdentityContext:[");
			sb.Append((this.userIdentityContext != null) ? this.userIdentityContext.ToString() : "none");
			sb.Append("]");
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00029D96 File Offset: 0x00027F96
		public virtual void AppendToString(StringBuilder sb)
		{
			this.AppendClassName(sb);
			sb.Append(":[");
			this.AppendFields(sb);
			sb.Append("]");
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00029DC0 File Offset: 0x00027FC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(250);
			this.AppendToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x040004FC RID: 1276
		private StoreDatabase database;

		// Token: 0x040004FD RID: 1277
		private int mailboxNumber;

		// Token: 0x040004FE RID: 1278
		private int eventTypeValue;

		// Token: 0x040004FF RID: 1279
		private Guid? userIdentityContext;

		// Token: 0x020000DC RID: 220
		[Flags]
		public enum RedundancyStatus
		{
			// Token: 0x04000501 RID: 1281
			FlagDropNew = 1,
			// Token: 0x04000502 RID: 1282
			FlagDropOld = 2,
			// Token: 0x04000503 RID: 1283
			FlagReplaceOld = 4,
			// Token: 0x04000504 RID: 1284
			FlagMerge = 8,
			// Token: 0x04000505 RID: 1285
			FlagStopSearch = -2147483648,
			// Token: 0x04000506 RID: 1286
			Continue = 0,
			// Token: 0x04000507 RID: 1287
			Stop = -2147483648,
			// Token: 0x04000508 RID: 1288
			DropNewAndStop = -2147483647,
			// Token: 0x04000509 RID: 1289
			DropOld = 2,
			// Token: 0x0400050A RID: 1290
			DropOldAndStop = -2147483646,
			// Token: 0x0400050B RID: 1291
			DropBothAndStop = -2147483645,
			// Token: 0x0400050C RID: 1292
			ReplaceOldAndStop = -2147483644,
			// Token: 0x0400050D RID: 1293
			MergeReplaceOldAndStop = -2147483636,
			// Token: 0x0400050E RID: 1294
			Merge = 10
		}
	}
}
