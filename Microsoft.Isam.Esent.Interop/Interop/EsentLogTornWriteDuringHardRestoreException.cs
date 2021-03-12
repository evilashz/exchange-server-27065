using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010F RID: 271
	[Serializable]
	public sealed class EsentLogTornWriteDuringHardRestoreException : EsentCorruptionException
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00011E36 File Offset: 0x00010036
		public EsentLogTornWriteDuringHardRestoreException() : base("torn-write was detected in a backup set during hard restore", JET_err.LogTornWriteDuringHardRestore)
		{
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00011E48 File Offset: 0x00010048
		private EsentLogTornWriteDuringHardRestoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
