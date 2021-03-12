using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D6 RID: 470
	[Serializable]
	public sealed class EsentColumnDoesNotFitException : EsentUsageException
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x000133FA File Offset: 0x000115FA
		public EsentColumnDoesNotFitException() : base("Field will not fit in record", JET_err.ColumnDoesNotFit)
		{
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0001340C File Offset: 0x0001160C
		private EsentColumnDoesNotFitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
