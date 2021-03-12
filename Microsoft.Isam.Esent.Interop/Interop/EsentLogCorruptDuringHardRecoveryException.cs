using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000112 RID: 274
	[Serializable]
	public sealed class EsentLogCorruptDuringHardRecoveryException : EsentCorruptionException
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00011E8A File Offset: 0x0001008A
		public EsentLogCorruptDuringHardRecoveryException() : base("corruption was detected during hard recovery (log was not part of a backup set)", JET_err.LogCorruptDuringHardRecovery)
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00011E9C File Offset: 0x0001009C
		private EsentLogCorruptDuringHardRecoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
