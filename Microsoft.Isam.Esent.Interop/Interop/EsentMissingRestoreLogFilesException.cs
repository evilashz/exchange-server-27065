using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	public sealed class EsentMissingRestoreLogFilesException : EsentInconsistentException
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x00011D3A File Offset: 0x0000FF3A
		public EsentMissingRestoreLogFilesException() : base("Some restore log files are missing", JET_err.MissingRestoreLogFiles)
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00011D4C File Offset: 0x0000FF4C
		private EsentMissingRestoreLogFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
