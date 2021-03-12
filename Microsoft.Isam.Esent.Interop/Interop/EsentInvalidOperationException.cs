using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000203 RID: 515
	[Serializable]
	public sealed class EsentInvalidOperationException : EsentUsageException
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x000138E6 File Offset: 0x00011AE6
		public EsentInvalidOperationException() : base("Invalid operation", JET_err.InvalidOperation)
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000138F8 File Offset: 0x00011AF8
		private EsentInvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
