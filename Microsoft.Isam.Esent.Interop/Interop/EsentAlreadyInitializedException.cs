using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000143 RID: 323
	[Serializable]
	public sealed class EsentAlreadyInitializedException : EsentUsageException
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x000123E6 File Offset: 0x000105E6
		public EsentAlreadyInitializedException() : base("Database engine already initialized", JET_err.AlreadyInitialized)
		{
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x000123F8 File Offset: 0x000105F8
		private EsentAlreadyInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
