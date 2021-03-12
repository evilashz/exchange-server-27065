using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	public sealed class EsentDatabaseAlreadyRunningMaintenanceException : EsentUsageException
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00013A1A File Offset: 0x00011C1A
		public EsentDatabaseAlreadyRunningMaintenanceException() : base("The operation did not complete successfully because the database is already running maintenance on specified database", JET_err.DatabaseAlreadyRunningMaintenance)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00013A2C File Offset: 0x00011C2C
		private EsentDatabaseAlreadyRunningMaintenanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
