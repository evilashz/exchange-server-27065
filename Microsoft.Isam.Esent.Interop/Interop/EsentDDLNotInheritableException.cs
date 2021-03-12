using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public sealed class EsentDDLNotInheritableException : EsentUsageException
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00013106 File Offset: 0x00011306
		public EsentDDLNotInheritableException() : base("Tried to inherit DDL from a table not marked as a template table.", JET_err.DDLNotInheritable)
		{
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00013118 File Offset: 0x00011318
		private EsentDDLNotInheritableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
