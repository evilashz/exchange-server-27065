using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public sealed class EsentDatabasePatchFileMismatchException : EsentObsoleteException
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x00011CAE File Offset: 0x0000FEAE
		public EsentDatabasePatchFileMismatchException() : base("Patch file is not generated from this backup", JET_err.DatabasePatchFileMismatch)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00011CC0 File Offset: 0x0000FEC0
		private EsentDatabasePatchFileMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
