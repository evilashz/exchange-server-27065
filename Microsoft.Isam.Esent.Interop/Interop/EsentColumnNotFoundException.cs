using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	public sealed class EsentColumnNotFoundException : EsentUsageException
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x0001346A File Offset: 0x0001166A
		public EsentColumnNotFoundException() : base("No such column", JET_err.ColumnNotFound)
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001347C File Offset: 0x0001167C
		private EsentColumnNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
