using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000182 RID: 386
	[Serializable]
	public sealed class EsentRecoveryVerifyFailureException : EsentCorruptionException
	{
		// Token: 0x06000917 RID: 2327 RVA: 0x00012ACA File Offset: 0x00010CCA
		public EsentRecoveryVerifyFailureException() : base("One or more database pages read from disk during recovery do not match the expected state.", JET_err.RecoveryVerifyFailure)
		{
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00012ADC File Offset: 0x00010CDC
		private EsentRecoveryVerifyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
