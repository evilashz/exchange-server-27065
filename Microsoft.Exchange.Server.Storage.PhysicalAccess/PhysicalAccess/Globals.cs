using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000026 RID: 38
	public static class Globals
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000EB44 File Offset: 0x0000CD44
		public static void Initialize(DatabaseType databaseTypeToUse, Factory.JetHADatabaseCreator haCreator)
		{
			Connection.Initialize();
			Factory.Initialize(databaseTypeToUse, haCreator);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000EB52 File Offset: 0x0000CD52
		public static void Terminate()
		{
		}
	}
}
