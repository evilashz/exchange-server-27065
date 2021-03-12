using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public sealed class EsentColumnTooBigException : EsentUsageException
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0001344E File Offset: 0x0001164E
		public EsentColumnTooBigException() : base("Field length is greater than maximum", JET_err.ColumnTooBig)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00013460 File Offset: 0x00011660
		private EsentColumnTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
