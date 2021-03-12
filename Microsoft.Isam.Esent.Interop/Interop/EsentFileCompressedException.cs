using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000221 RID: 545
	[Serializable]
	public sealed class EsentFileCompressedException : EsentUsageException
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x00013C2E File Offset: 0x00011E2E
		public EsentFileCompressedException() : base("read/write access is not supported on compressed files", JET_err.FileCompressed)
		{
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00013C40 File Offset: 0x00011E40
		private EsentFileCompressedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
