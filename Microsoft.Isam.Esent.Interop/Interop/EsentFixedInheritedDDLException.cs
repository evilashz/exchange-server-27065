using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public sealed class EsentFixedInheritedDDLException : EsentUsageException
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x000130CE File Offset: 0x000112CE
		public EsentFixedInheritedDDLException() : base("On a derived table, DDL operations are prohibited on inherited portion of DDL", JET_err.FixedInheritedDDL)
		{
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000130E0 File Offset: 0x000112E0
		private EsentFixedInheritedDDLException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
