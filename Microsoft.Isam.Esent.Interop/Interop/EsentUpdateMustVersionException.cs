using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F8 RID: 504
	[Serializable]
	public sealed class EsentUpdateMustVersionException : EsentUsageException
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x000137B2 File Offset: 0x000119B2
		public EsentUpdateMustVersionException() : base("No version updates only for uncommitted tables", JET_err.UpdateMustVersion)
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000137C4 File Offset: 0x000119C4
		private EsentUpdateMustVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
