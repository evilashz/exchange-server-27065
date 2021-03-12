using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	public sealed class EsentDirtyShutdownException : EsentStateException
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x00012A5A File Offset: 0x00010C5A
		public EsentDirtyShutdownException() : base("The instance was shutdown successfully but all the attached databases were left in a dirty state by request via JET_bitTermDirty", JET_err.DirtyShutdown)
		{
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00012A6C File Offset: 0x00010C6C
		private EsentDirtyShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
