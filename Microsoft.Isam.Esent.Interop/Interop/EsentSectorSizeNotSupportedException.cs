using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public sealed class EsentSectorSizeNotSupportedException : EsentFatalException
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x00011F4E File Offset: 0x0001014E
		public EsentSectorSizeNotSupportedException() : base("The physical sector size reported by the disk subsystem, is unsupported by ESE for a specific file type.", JET_err.SectorSizeNotSupported)
		{
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00011F60 File Offset: 0x00010160
		private EsentSectorSizeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
