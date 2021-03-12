using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public sealed class EsentFileIOSparseException : EsentObsoleteException
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x00013BA2 File Offset: 0x00011DA2
		public EsentFileIOSparseException() : base("an I/O was issued to a location that was sparse", JET_err.FileIOSparse)
		{
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00013BB4 File Offset: 0x00011DB4
		private EsentFileIOSparseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
