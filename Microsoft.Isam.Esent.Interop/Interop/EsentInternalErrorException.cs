using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000BB RID: 187
	[Serializable]
	public sealed class EsentInternalErrorException : EsentOperationException
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001150F File Offset: 0x0000F70F
		public EsentInternalErrorException() : base("Fatal internal error", JET_err.InternalError)
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001151E File Offset: 0x0000F71E
		private EsentInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
