using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	public sealed class EsentFileIOBeyondEOFException : EsentCorruptionException
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x00013BBE File Offset: 0x00011DBE
		public EsentFileIOBeyondEOFException() : base("a read was issued to a location beyond EOF (writes will expand the file)", JET_err.FileIOBeyondEOF)
		{
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00013BD0 File Offset: 0x00011DD0
		private EsentFileIOBeyondEOFException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
