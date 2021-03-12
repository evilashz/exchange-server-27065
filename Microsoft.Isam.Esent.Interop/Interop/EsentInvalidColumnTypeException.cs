using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	public sealed class EsentInvalidColumnTypeException : EsentUsageException
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x000134DA File Offset: 0x000116DA
		public EsentInvalidColumnTypeException() : base("Invalid column data type", JET_err.InvalidColumnType)
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000134EC File Offset: 0x000116EC
		private EsentInvalidColumnTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
