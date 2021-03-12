using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200005F RID: 95
	internal class MigrationObjectLog<T, TSchema, TConfig> : ObjectLog<T> where TSchema : ObjectLogSchema, new() where TConfig : ObjectLogConfiguration, new()
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001C647 File Offset: 0x0001A847
		internal MigrationObjectLog() : base(Activator.CreateInstance<TSchema>(), Activator.CreateInstance<TConfig>())
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001C663 File Offset: 0x0001A863
		protected static void Write(T migrationObject)
		{
			MigrationObjectLog<T, TSchema, TConfig>.instance.LogObject(migrationObject);
		}

		// Token: 0x040001FA RID: 506
		private static MigrationObjectLog<T, TSchema, TConfig> instance = new MigrationObjectLog<T, TSchema, TConfig>();
	}
}
