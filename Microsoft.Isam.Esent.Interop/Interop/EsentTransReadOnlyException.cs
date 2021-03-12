using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public sealed class EsentTransReadOnlyException : EsentUsageException
	{
		// Token: 0x06000903 RID: 2307 RVA: 0x000129B2 File Offset: 0x00010BB2
		public EsentTransReadOnlyException() : base("Read-only transaction tried to modify the database", JET_err.TransReadOnly)
		{
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000129C4 File Offset: 0x00010BC4
		private EsentTransReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
