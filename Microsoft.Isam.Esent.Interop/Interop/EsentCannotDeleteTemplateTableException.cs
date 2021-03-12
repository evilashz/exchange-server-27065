using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public sealed class EsentCannotDeleteTemplateTableException : EsentUsageException
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x0001307A File Offset: 0x0001127A
		public EsentCannotDeleteTemplateTableException() : base("Illegal attempt to delete a template table", JET_err.CannotDeleteTemplateTable)
		{
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001308C File Offset: 0x0001128C
		private EsentCannotDeleteTemplateTableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
