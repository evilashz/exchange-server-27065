using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000189 RID: 393
	[Serializable]
	public sealed class EsentDTCMissingCallbackException : EsentObsoleteException
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x00012B8E File Offset: 0x00010D8E
		public EsentDTCMissingCallbackException() : base("Attempted to begin a distributed transaction but no callback for DTC coordination was specified on initialisation", JET_err.DTCMissingCallback)
		{
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00012BA0 File Offset: 0x00010DA0
		private EsentDTCMissingCallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
