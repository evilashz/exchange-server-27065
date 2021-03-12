using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000EB RID: 235
	[Serializable]
	public sealed class EsentLogDiskFullException : EsentDiskException
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x00011A46 File Offset: 0x0000FC46
		public EsentLogDiskFullException() : base("Log disk full", JET_err.LogDiskFull)
		{
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00011A58 File Offset: 0x0000FC58
		private EsentLogDiskFullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
