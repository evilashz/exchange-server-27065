using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000183 RID: 387
	[Serializable]
	public sealed class EsentFilteredMoveNotSupportedException : EsentUsageException
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x00012AE6 File Offset: 0x00010CE6
		public EsentFilteredMoveNotSupportedException() : base("Attempted to provide a filter to JetSetCursorFilter() in an unsupported scenario.", JET_err.FilteredMoveNotSupported)
		{
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00012AF8 File Offset: 0x00010CF8
		private EsentFilteredMoveNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
