using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceTopologyDiagnosableArgument : LoadBalanceDiagnosableArgumentBase
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000A68D File Offset: 0x0000888D
		public bool ShowServer
		{
			get
			{
				return base.HasArgument("server");
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000A69A File Offset: 0x0000889A
		public Guid ServerGuid
		{
			get
			{
				return base.GetArgument<Guid>("server");
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000A6A7 File Offset: 0x000088A7
		public bool ShowDatabase
		{
			get
			{
				return base.HasArgument("database");
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000A6B4 File Offset: 0x000088B4
		public bool ShowForest
		{
			get
			{
				return base.HasArgument("forest");
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000A6C1 File Offset: 0x000088C1
		public Guid DatabaseGuid
		{
			get
			{
				return base.GetArgument<Guid>("database");
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000A6CE File Offset: 0x000088CE
		public bool ShowDag
		{
			get
			{
				return base.HasArgument("dag");
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000A6DB File Offset: 0x000088DB
		public Guid DagGuid
		{
			get
			{
				return base.GetArgument<Guid>("dag");
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000A6E8 File Offset: 0x000088E8
		public bool ShowForestHeatMap
		{
			get
			{
				return base.HasArgument("forestHeatMap");
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000A6F5 File Offset: 0x000088F5
		public bool ShowLocalServerHeatMap
		{
			get
			{
				return base.HasArgument("localServerHeatMap");
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A704 File Offset: 0x00008904
		protected override void ExtendSchema(Dictionary<string, Type> schema)
		{
			schema["database"] = typeof(Guid);
			schema["server"] = typeof(Guid);
			schema["dag"] = typeof(Guid);
			schema["forest"] = typeof(bool);
			schema["forestHeatMap"] = typeof(bool);
			schema["localServerHeatMap"] = typeof(bool);
		}

		// Token: 0x04000118 RID: 280
		private const string DatabaseArgument = "database";

		// Token: 0x04000119 RID: 281
		private const string ServerArgument = "server";

		// Token: 0x0400011A RID: 282
		private const string DagArgument = "dag";

		// Token: 0x0400011B RID: 283
		private const string ForestArgument = "forest";

		// Token: 0x0400011C RID: 284
		private const string ShowForestHeatMapArgument = "forestHeatMap";

		// Token: 0x0400011D RID: 285
		private const string ShowLocalServerHeatMapArgument = "localServerHeatMap";
	}
}
