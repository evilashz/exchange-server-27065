using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public sealed class EsentIndexDuplicateException : EsentUsageException
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x000131AE File Offset: 0x000113AE
		public EsentIndexDuplicateException() : base("Index is already defined", JET_err.IndexDuplicate)
		{
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000131C0 File Offset: 0x000113C0
		private EsentIndexDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
