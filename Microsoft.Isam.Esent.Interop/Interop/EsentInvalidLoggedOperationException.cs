using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public sealed class EsentInvalidLoggedOperationException : EsentObsoleteException
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x000117A6 File Offset: 0x0000F9A6
		public EsentInvalidLoggedOperationException() : base("Logged operation cannot be redone", JET_err.InvalidLoggedOperation)
		{
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private EsentInvalidLoggedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
