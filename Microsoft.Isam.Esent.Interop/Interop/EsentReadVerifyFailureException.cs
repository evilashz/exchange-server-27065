using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000137 RID: 311
	[Serializable]
	public sealed class EsentReadVerifyFailureException : EsentCorruptionException
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00012296 File Offset: 0x00010496
		public EsentReadVerifyFailureException() : base("Checksum error on a database page", JET_err.ReadVerifyFailure)
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000122A8 File Offset: 0x000104A8
		private EsentReadVerifyFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
