using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016A RID: 362
	[Serializable]
	public sealed class EsentLogFilePathInUseException : EsentUsageException
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x0001282A File Offset: 0x00010A2A
		public EsentLogFilePathInUseException() : base("Logfile path already used by another database instance", JET_err.LogFilePathInUse)
		{
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001283C File Offset: 0x00010A3C
		private EsentLogFilePathInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
