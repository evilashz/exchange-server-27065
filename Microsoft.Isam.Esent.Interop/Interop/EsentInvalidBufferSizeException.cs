using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200014E RID: 334
	[Serializable]
	public sealed class EsentInvalidBufferSizeException : EsentStateException
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0001251A File Offset: 0x0001071A
		public EsentInvalidBufferSizeException() : base("Data buffer doesn't match column size", JET_err.InvalidBufferSize)
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001252C File Offset: 0x0001072C
		private EsentInvalidBufferSizeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
