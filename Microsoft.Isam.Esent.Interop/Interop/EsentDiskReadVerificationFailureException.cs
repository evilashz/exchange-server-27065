using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200013A RID: 314
	[Serializable]
	public sealed class EsentDiskReadVerificationFailureException : EsentCorruptionException
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x000122EA File Offset: 0x000104EA
		public EsentDiskReadVerificationFailureException() : base("The OS returned ERROR_CRC from file IO", JET_err.DiskReadVerificationFailure)
		{
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000122FC File Offset: 0x000104FC
		private EsentDiskReadVerificationFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
