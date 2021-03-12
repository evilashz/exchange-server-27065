using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	public sealed class EsentDatabaseFileReadOnlyException : EsentUsageException
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0001219A File Offset: 0x0001039A
		public EsentDatabaseFileReadOnlyException() : base("Tried to attach a read-only database file for read/write operations", JET_err.DatabaseFileReadOnly)
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000121AC File Offset: 0x000103AC
		private EsentDatabaseFileReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
