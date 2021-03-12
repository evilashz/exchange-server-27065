using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public sealed class EsentMissingLogFileException : EsentCorruptionException
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00011A2A File Offset: 0x0000FC2A
		public EsentMissingLogFileException() : base("Current log file missing", JET_err.MissingLogFile)
		{
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00011A3C File Offset: 0x0000FC3C
		private EsentMissingLogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
