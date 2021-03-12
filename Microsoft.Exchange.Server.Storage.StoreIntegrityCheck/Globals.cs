using System;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200000E RID: 14
	public static class Globals
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000460B File Offset: 0x0000280B
		public static void Initialize()
		{
			InMemoryJobStorage.Initialize();
			JobScheduler.Initialize();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004617 File Offset: 0x00002817
		public static void Terminate()
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004619 File Offset: 0x00002819
		public static void DatabaseMounting(Context context, StoreDatabase database, bool readOnly)
		{
			InMemoryJobStorage.MountEventHandler(context, database);
			JobScheduler.MountEventHandler(context, database, readOnly);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000462A File Offset: 0x0000282A
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000462C File Offset: 0x0000282C
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
			InMemoryJobStorage.DismountEventHandler(database);
			JobScheduler.DismountEventHandler(database);
		}
	}
}
