using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	public sealed class EsentCannotAddFixedVarColumnToDerivedTableException : EsentObsoleteException
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x0001315A File Offset: 0x0001135A
		public EsentCannotAddFixedVarColumnToDerivedTableException() : base("Template table was created with NoFixedVarColumnsInDerivedTables", JET_err.CannotAddFixedVarColumnToDerivedTable)
		{
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0001316C File Offset: 0x0001136C
		private EsentCannotAddFixedVarColumnToDerivedTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
