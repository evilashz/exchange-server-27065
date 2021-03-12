using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public sealed class EsentLogSectorSizeMismatchException : EsentFragmentationException
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00011C06 File Offset: 0x0000FE06
		public EsentLogSectorSizeMismatchException() : base("the log file sector size does not match the current volume's sector size", JET_err.LogSectorSizeMismatch)
		{
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00011C18 File Offset: 0x0000FE18
		private EsentLogSectorSizeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
