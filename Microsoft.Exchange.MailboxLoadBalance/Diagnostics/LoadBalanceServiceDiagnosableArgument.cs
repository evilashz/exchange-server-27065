using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceServiceDiagnosableArgument : LoadBalanceDiagnosableArgumentBase
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000A210 File Offset: 0x00008410
		public bool CleanQueues
		{
			get
			{
				return base.HasArgument("cleanqueues");
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A21D File Offset: 0x0000841D
		public bool ShowLoadBalancerResults
		{
			get
			{
				return base.HasArgument("loadbalanceresults");
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000A22A File Offset: 0x0000842A
		public bool StartLoadBalance
		{
			get
			{
				return base.HasArgument("startloadbalance");
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000A237 File Offset: 0x00008437
		public bool ShowQueues
		{
			get
			{
				return base.HasArgument("showqueues");
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000A244 File Offset: 0x00008444
		public bool ShowQueuedRequests
		{
			get
			{
				return base.HasArgument("showqueuedrequests");
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000A251 File Offset: 0x00008451
		public bool RemoveSoftDeletedMailbox
		{
			get
			{
				return base.HasArgument("removesoftdeletedmailbox");
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000A25E File Offset: 0x0000845E
		public bool GetMoveHistory
		{
			get
			{
				return base.HasArgument("getmovehistory");
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000A26B File Offset: 0x0000846B
		public Guid MailboxGuid
		{
			get
			{
				return base.GetArgument<Guid>("mailboxguid");
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000A278 File Offset: 0x00008478
		public Guid DatabaseGuid
		{
			get
			{
				return base.GetArgument<Guid>("databaseguid");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000A285 File Offset: 0x00008485
		public Guid TargetDatabaseGuid
		{
			get
			{
				return base.GetArgument<Guid>("targetdatabaseguid");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000A292 File Offset: 0x00008492
		public Guid SourceDatabaseGuid
		{
			get
			{
				return base.GetArgument<Guid>("sourcedatabaseguid");
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000A29F File Offset: 0x0000849F
		public bool IsDrainingDatabase
		{
			get
			{
				return base.HasArgument("draindatabase");
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000A2AC File Offset: 0x000084AC
		public Guid DatabaseToDrainGuid
		{
			get
			{
				return base.GetArgument<Guid>("draindatabase");
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A2BC File Offset: 0x000084BC
		protected override void ExtendSchema(Dictionary<string, Type> schema)
		{
			schema["startloadbalance"] = typeof(bool);
			schema["loadbalanceresults"] = typeof(bool);
			schema["showqueues"] = typeof(bool);
			schema["showqueuedrequests"] = typeof(bool);
			schema["removesoftdeletedmailbox"] = typeof(bool);
			schema["mailboxguid"] = typeof(Guid);
			schema["databaseguid"] = typeof(Guid);
			schema["getmovehistory"] = typeof(bool);
			schema["sourcedatabaseguid"] = typeof(Guid);
			schema["targetdatabaseguid"] = typeof(Guid);
			schema["draindag"] = typeof(Guid);
			schema["draindatabase"] = typeof(Guid);
			schema["cleanqueues"] = typeof(bool);
		}

		// Token: 0x04000109 RID: 265
		internal const string LoadBalancerResultsArgument = "loadbalanceresults";

		// Token: 0x0400010A RID: 266
		internal const string ShowQueuesArgument = "showqueues";

		// Token: 0x0400010B RID: 267
		private const string StartLoadBalanceArgument = "startloadbalance";

		// Token: 0x0400010C RID: 268
		private const string ShowQueuedRequestsArgument = "showqueuedrequests";

		// Token: 0x0400010D RID: 269
		private const string RemoveSoftDeletedMailboxArgument = "removesoftdeletedmailbox";

		// Token: 0x0400010E RID: 270
		private const string GetMoveHistoryArgument = "getmovehistory";

		// Token: 0x0400010F RID: 271
		private const string MailboxGuidArgument = "mailboxguid";

		// Token: 0x04000110 RID: 272
		private const string DatabaseGuidArgument = "databaseguid";

		// Token: 0x04000111 RID: 273
		private const string TargetDatabaseGuidArgument = "targetdatabaseguid";

		// Token: 0x04000112 RID: 274
		private const string SourceDatabaseGuidArgument = "sourcedatabaseguid";

		// Token: 0x04000113 RID: 275
		private const string DatabaseToDrainArgument = "draindatabase";

		// Token: 0x04000114 RID: 276
		private const string DagToDrainArgument = "draindag";

		// Token: 0x04000115 RID: 277
		private const string CleanQueuesArgument = "cleanqueues";
	}
}
