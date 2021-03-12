using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000125 RID: 293
	[Serializable]
	public sealed class EsentLogFileNotCopiedException : EsentUsageException
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0001209E File Offset: 0x0001029E
		public EsentLogFileNotCopiedException() : base("log truncation attempted but not all required logs were copied", JET_err.LogFileNotCopied)
		{
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000120B0 File Offset: 0x000102B0
		private EsentLogFileNotCopiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
