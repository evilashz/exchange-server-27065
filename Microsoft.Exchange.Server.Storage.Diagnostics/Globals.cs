using System;
using System.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200003C RID: 60
	public static class Globals
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000D5A8 File Offset: 0x0000B7A8
		internal static DiagnosticQueryHandler QueryHandler
		{
			get
			{
				if (Microsoft.Exchange.Server.Storage.Diagnostics.Globals.queryHandler == null)
				{
					DiagnosticQueryFactory factory = StoreQueryFactory.CreateFactory();
					Microsoft.Exchange.Server.Storage.Diagnostics.Globals.queryHandler = DiagnosticQueryHandler.Create(factory);
				}
				return Microsoft.Exchange.Server.Storage.Diagnostics.Globals.queryHandler;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000D5D2 File Offset: 0x0000B7D2
		public static void Initialize()
		{
			DatabaseSchema.Initialize();
			SimpleQueryTargets.Initialize();
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.QueryHandler.Register();
			StoreQueryTargets.Register<ThreadManager.ThreadDiagnosticInfo>(ThreadManager.Instance, Visibility.Public);
			StoreQueryTargets.Register<ThreadManager.ProcessThreadInfo>(ThreadManager.Instance, Visibility.Public);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000D5FE File Offset: 0x0000B7FE
		public static void Terminate()
		{
			Microsoft.Exchange.Server.Storage.Diagnostics.Globals.QueryHandler.Deregister();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000D60A File Offset: 0x0000B80A
		public static void InitInMemoryDatabaseSchema(Context context, StoreDatabase database)
		{
			DatabaseSchema.Initialize(database);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000D612 File Offset: 0x0000B812
		public static void DatabaseMounting(Context context, StoreDatabase database)
		{
			DatabaseSchema.PostMountInitialize(context, database);
			SimpleQueryTargets.MountEventHandler(database);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000D621 File Offset: 0x0000B821
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D623 File Offset: 0x0000B823
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000D625 File Offset: 0x0000B825
		[Conditional("DEBUG")]
		internal static void Assert(bool assertCondition, string message)
		{
		}

		// Token: 0x04000117 RID: 279
		private static DiagnosticQueryHandler queryHandler;
	}
}
