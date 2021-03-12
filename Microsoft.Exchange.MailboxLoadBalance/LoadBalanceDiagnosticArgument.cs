using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceDiagnosticArgument : DiagnosableArgument
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000026B4 File Offset: 0x000008B4
		public bool Verbose
		{
			get
			{
				return base.HasArgument("verbose");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000026C1 File Offset: 0x000008C1
		public bool RefreshData
		{
			get
			{
				return base.HasArgument("refresh");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000026CE File Offset: 0x000008CE
		public bool ShowForest
		{
			get
			{
				return base.HasArgument("forest");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000026DB File Offset: 0x000008DB
		public bool ShowSite
		{
			get
			{
				return base.HasArgument("site");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000026E8 File Offset: 0x000008E8
		public Guid Site
		{
			get
			{
				return base.GetArgument<Guid>("site");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000026F5 File Offset: 0x000008F5
		public bool ShowServer
		{
			get
			{
				return base.HasArgument("server");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002702 File Offset: 0x00000902
		public Guid Server
		{
			get
			{
				return base.GetArgument<Guid>("server");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000270F File Offset: 0x0000090F
		public bool ShowLoadBalancerResults
		{
			get
			{
				return base.HasArgument("loadbalanceresults");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000271C File Offset: 0x0000091C
		public bool StartLoadBalance
		{
			get
			{
				return base.HasArgument("startloadbalance");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002729 File Offset: 0x00000929
		public bool ShowDatabase
		{
			get
			{
				return base.HasArgument("database");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002736 File Offset: 0x00000936
		public Guid DatabaseGuid
		{
			get
			{
				return base.GetArgument<Guid>("database");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002743 File Offset: 0x00000943
		public bool TraceEnabled
		{
			get
			{
				return base.HasArgument("trace");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002750 File Offset: 0x00000950
		public bool ShowQueues
		{
			get
			{
				return base.HasArgument("showqueues");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000275D File Offset: 0x0000095D
		protected override bool FailOnMissingArgument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002760 File Offset: 0x00000960
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["database"] = typeof(Guid);
			schema["server"] = typeof(Guid);
			schema["site"] = typeof(Guid);
			schema["forest"] = typeof(bool);
			schema["loadbalanceresults"] = typeof(bool);
			schema["refresh"] = typeof(bool);
			schema["startloadbalance"] = typeof(bool);
			schema["verbose"] = typeof(bool);
			schema["trace"] = typeof(bool);
			schema["showqueues"] = typeof(bool);
		}

		// Token: 0x04000005 RID: 5
		private const string DatabaseArgument = "database";

		// Token: 0x04000006 RID: 6
		private const string ServerArgument = "server";

		// Token: 0x04000007 RID: 7
		private const string SiteArgument = "site";

		// Token: 0x04000008 RID: 8
		private const string ForestArgument = "forest";

		// Token: 0x04000009 RID: 9
		private const string RefreshDataArgument = "refresh";

		// Token: 0x0400000A RID: 10
		private const string LoadBalancerResultsArgument = "loadbalanceresults";

		// Token: 0x0400000B RID: 11
		private const string StartLoadBalanceArgument = "startloadbalance";

		// Token: 0x0400000C RID: 12
		private const string VerboseArgument = "verbose";

		// Token: 0x0400000D RID: 13
		private const string TraceArgument = "trace";

		// Token: 0x0400000E RID: 14
		private const string ShowQueuesArgument = "showqueues";
	}
}
