using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	public sealed class EsentDatabaseDirtyShutdownException : EsentInconsistentException
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00011C76 File Offset: 0x0000FE76
		public EsentDatabaseDirtyShutdownException() : base("Database was not shutdown cleanly. Recovery must first be run to properly complete database operations for the previous shutdown.", JET_err.DatabaseDirtyShutdown)
		{
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00011C88 File Offset: 0x0000FE88
		private EsentDatabaseDirtyShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
