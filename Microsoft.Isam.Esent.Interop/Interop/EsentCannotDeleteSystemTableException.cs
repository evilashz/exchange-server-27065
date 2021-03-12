using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public sealed class EsentCannotDeleteSystemTableException : EsentUsageException
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0001305E File Offset: 0x0001125E
		public EsentCannotDeleteSystemTableException() : base("Illegal attempt to delete a system table", JET_err.CannotDeleteSystemTable)
		{
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00013070 File Offset: 0x00011270
		private EsentCannotDeleteSystemTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
