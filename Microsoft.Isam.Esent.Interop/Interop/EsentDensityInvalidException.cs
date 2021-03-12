using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AC RID: 428
	[Serializable]
	public sealed class EsentDensityInvalidException : EsentUsageException
	{
		// Token: 0x0600096B RID: 2411 RVA: 0x00012F62 File Offset: 0x00011162
		public EsentDensityInvalidException() : base("Bad file/index density", JET_err.DensityInvalid)
		{
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00012F74 File Offset: 0x00011174
		private EsentDensityInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
