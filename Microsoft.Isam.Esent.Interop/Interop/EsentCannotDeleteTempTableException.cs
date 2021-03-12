using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public sealed class EsentCannotDeleteTempTableException : EsentUsageException
	{
		// Token: 0x0600097B RID: 2427 RVA: 0x00013042 File Offset: 0x00011242
		public EsentCannotDeleteTempTableException() : base("Use CloseTable instead of DeleteTable to delete temp table", JET_err.CannotDeleteTempTable)
		{
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00013054 File Offset: 0x00011254
		private EsentCannotDeleteTempTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
