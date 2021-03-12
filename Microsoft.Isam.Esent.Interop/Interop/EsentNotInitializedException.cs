using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000142 RID: 322
	[Serializable]
	public sealed class EsentNotInitializedException : EsentUsageException
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x000123CA File Offset: 0x000105CA
		public EsentNotInitializedException() : base("Database engine not initialized", JET_err.NotInitialized)
		{
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000123DC File Offset: 0x000105DC
		private EsentNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
