using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200018A RID: 394
	[Serializable]
	public sealed class EsentDTCMissingCallbackOnRecoveryException : EsentObsoleteException
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00012BAA File Offset: 0x00010DAA
		public EsentDTCMissingCallbackOnRecoveryException() : base("Attempted to recover a distributed transaction but no callback for DTC coordination was specified on initialisation", JET_err.DTCMissingCallbackOnRecovery)
		{
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00012BBC File Offset: 0x00010DBC
		private EsentDTCMissingCallbackOnRecoveryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
