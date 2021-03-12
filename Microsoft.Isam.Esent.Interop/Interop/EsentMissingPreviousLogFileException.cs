using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D9 RID: 217
	[Serializable]
	public sealed class EsentMissingPreviousLogFileException : EsentCorruptionException
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x0001184E File Offset: 0x0000FA4E
		public EsentMissingPreviousLogFileException() : base("Missing the log file for check point", JET_err.MissingPreviousLogFile)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00011860 File Offset: 0x0000FA60
		private EsentMissingPreviousLogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
