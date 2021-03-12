using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public sealed class EsentMissingCurrentLogFilesException : EsentInconsistentException
	{
		// Token: 0x06000829 RID: 2089 RVA: 0x00011DC6 File Offset: 0x0000FFC6
		public EsentMissingCurrentLogFilesException() : base("Some current log files are missing for continuous restore", JET_err.MissingCurrentLogFiles)
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		private EsentMissingCurrentLogFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
