using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D4 RID: 212
	[Serializable]
	public sealed class EsentLogFileCorruptException : EsentCorruptionException
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x000117C2 File Offset: 0x0000F9C2
		public EsentLogFileCorruptException() : base("Log file is corrupt", JET_err.LogFileCorrupt)
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000117D4 File Offset: 0x0000F9D4
		private EsentLogFileCorruptException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
