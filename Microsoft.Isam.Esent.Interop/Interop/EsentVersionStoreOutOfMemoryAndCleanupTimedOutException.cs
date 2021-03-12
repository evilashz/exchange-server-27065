using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	public sealed class EsentVersionStoreOutOfMemoryAndCleanupTimedOutException : EsentUsageException
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x00012686 File Offset: 0x00010886
		public EsentVersionStoreOutOfMemoryAndCleanupTimedOutException() : base("Version store out of memory (and cleanup attempt failed to complete)", JET_err.VersionStoreOutOfMemoryAndCleanupTimedOut)
		{
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00012698 File Offset: 0x00010898
		private EsentVersionStoreOutOfMemoryAndCleanupTimedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
