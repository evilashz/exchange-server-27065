using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public sealed class EsentFixedDDLException : EsentUsageException
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x000130B2 File Offset: 0x000112B2
		public EsentFixedDDLException() : base("DDL operations prohibited on this table", JET_err.FixedDDL)
		{
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000130C4 File Offset: 0x000112C4
		private EsentFixedDDLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
