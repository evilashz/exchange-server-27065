using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E0 RID: 224
	[Serializable]
	public sealed class EsentLoggingDisabledException : EsentUsageException
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x00011912 File Offset: 0x0000FB12
		public EsentLoggingDisabledException() : base("Log is not active", JET_err.LoggingDisabled)
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00011924 File Offset: 0x0000FB24
		private EsentLoggingDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
