using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public sealed class EsentPageSizeMismatchException : EsentInconsistentException
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x00012D32 File Offset: 0x00010F32
		public EsentPageSizeMismatchException() : base("The database page size does not match the engine", JET_err.PageSizeMismatch)
		{
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00012D44 File Offset: 0x00010F44
		private EsentPageSizeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
