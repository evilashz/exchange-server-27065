using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BA RID: 442
	[Serializable]
	public sealed class EsentCannotNestDDLException : EsentUsageException
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x000130EA File Offset: 0x000112EA
		public EsentCannotNestDDLException() : base("Nesting of hierarchical DDL is not currently supported.", JET_err.CannotNestDDL)
		{
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000130FC File Offset: 0x000112FC
		private EsentCannotNestDDLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
