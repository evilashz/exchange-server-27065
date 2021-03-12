using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AD RID: 429
	[Serializable]
	public sealed class EsentTableNotEmptyException : EsentUsageException
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00012F7E File Offset: 0x0001117E
		public EsentTableNotEmptyException() : base("Table is not empty", JET_err.TableNotEmpty)
		{
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00012F90 File Offset: 0x00011190
		private EsentTableNotEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
