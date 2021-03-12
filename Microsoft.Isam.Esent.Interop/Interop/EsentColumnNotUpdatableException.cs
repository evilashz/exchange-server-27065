using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014F RID: 335
	[Serializable]
	public sealed class EsentColumnNotUpdatableException : EsentUsageException
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x00012536 File Offset: 0x00010736
		public EsentColumnNotUpdatableException() : base("Cannot set column value", JET_err.ColumnNotUpdatable)
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00012548 File Offset: 0x00010748
		private EsentColumnNotUpdatableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
