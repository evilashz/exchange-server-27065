using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public sealed class EsentLogCorruptDuringHardRestoreException : EsentCorruptionException
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00011E6E File Offset: 0x0001006E
		public EsentLogCorruptDuringHardRestoreException() : base("corruption was detected in a backup set during hard restore", JET_err.LogCorruptDuringHardRestore)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00011E80 File Offset: 0x00010080
		private EsentLogCorruptDuringHardRestoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
