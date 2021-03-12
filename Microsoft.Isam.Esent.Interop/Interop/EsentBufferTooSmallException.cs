using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000148 RID: 328
	[Serializable]
	public sealed class EsentBufferTooSmallException : EsentStateException
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00012472 File Offset: 0x00010672
		public EsentBufferTooSmallException() : base("Buffer is too small", JET_err.BufferTooSmall)
		{
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00012484 File Offset: 0x00010684
		private EsentBufferTooSmallException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
