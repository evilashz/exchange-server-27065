using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000110 RID: 272
	[Serializable]
	public sealed class EsentLogTornWriteDuringHardRecoveryException : EsentCorruptionException
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x00011E52 File Offset: 0x00010052
		public EsentLogTornWriteDuringHardRecoveryException() : base("torn-write was detected during hard recovery (log was not part of a backup set)", JET_err.LogTornWriteDuringHardRecovery)
		{
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00011E64 File Offset: 0x00010064
		private EsentLogTornWriteDuringHardRecoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
