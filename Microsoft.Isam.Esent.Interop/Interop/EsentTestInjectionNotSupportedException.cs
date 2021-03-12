using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000217 RID: 535
	[Serializable]
	public sealed class EsentTestInjectionNotSupportedException : EsentStateException
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00013B16 File Offset: 0x00011D16
		public EsentTestInjectionNotSupportedException() : base("Test injection not supported", JET_err.TestInjectionNotSupported)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00013B28 File Offset: 0x00011D28
		private EsentTestInjectionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
