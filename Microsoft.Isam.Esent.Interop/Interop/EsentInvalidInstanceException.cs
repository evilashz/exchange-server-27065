using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	public sealed class EsentInvalidInstanceException : EsentUsageException
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00012A3E File Offset: 0x00010C3E
		public EsentInvalidInstanceException() : base("Invalid instance handle", JET_err.InvalidInstance)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00012A50 File Offset: 0x00010C50
		private EsentInvalidInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
