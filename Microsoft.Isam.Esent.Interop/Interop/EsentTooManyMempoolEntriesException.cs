using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	public sealed class EsentTooManyMempoolEntriesException : EsentMemoryException
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x00012712 File Offset: 0x00010912
		public EsentTooManyMempoolEntriesException() : base("Too many mempool entries requested", JET_err.TooManyMempoolEntries)
		{
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00012724 File Offset: 0x00010924
		private EsentTooManyMempoolEntriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
