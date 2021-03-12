using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public sealed class EsentLogFileSizeMismatchException : EsentUsageException
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00011B7A File Offset: 0x0000FD7A
		public EsentLogFileSizeMismatchException() : base("actual log file size does not match JET_paramLogFileSize", JET_err.LogFileSizeMismatch)
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00011B8C File Offset: 0x0000FD8C
		private EsentLogFileSizeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
