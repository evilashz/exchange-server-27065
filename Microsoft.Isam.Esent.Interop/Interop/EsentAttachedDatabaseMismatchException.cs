using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public sealed class EsentAttachedDatabaseMismatchException : EsentInconsistentException
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x00012D86 File Offset: 0x00010F86
		public EsentAttachedDatabaseMismatchException() : base("An outstanding database attachment has been detected at the start or end of recovery, but database is missing or does not match attachment info", JET_err.AttachedDatabaseMismatch)
		{
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00012D98 File Offset: 0x00010F98
		private EsentAttachedDatabaseMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
