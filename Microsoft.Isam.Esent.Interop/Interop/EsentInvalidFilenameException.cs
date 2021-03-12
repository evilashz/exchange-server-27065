using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014B RID: 331
	[Serializable]
	public sealed class EsentInvalidFilenameException : EsentObsoleteException
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x000124C6 File Offset: 0x000106C6
		public EsentInvalidFilenameException() : base("Filename is invalid", JET_err.InvalidFilename)
		{
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000124D8 File Offset: 0x000106D8
		private EsentInvalidFilenameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
