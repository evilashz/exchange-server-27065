using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public sealed class EsentDatabaseStreamingFileMismatchException : EsentObsoleteException
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00011B5E File Offset: 0x0000FD5E
		public EsentDatabaseStreamingFileMismatchException() : base("Database and streaming file do not match each other", JET_err.DatabaseStreamingFileMismatch)
		{
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00011B70 File Offset: 0x0000FD70
		private EsentDatabaseStreamingFileMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
