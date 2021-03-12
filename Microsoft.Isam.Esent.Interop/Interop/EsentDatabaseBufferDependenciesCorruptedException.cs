using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	public sealed class EsentDatabaseBufferDependenciesCorruptedException : EsentCorruptionException
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001155A File Offset: 0x0000F75A
		public EsentDatabaseBufferDependenciesCorruptedException() : base("Buffer dependencies improperly set. Recovery failure", JET_err.DatabaseBufferDependenciesCorrupted)
		{
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001156C File Offset: 0x0000F76C
		private EsentDatabaseBufferDependenciesCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
