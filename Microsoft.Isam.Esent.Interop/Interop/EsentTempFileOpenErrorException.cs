using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	public sealed class EsentTempFileOpenErrorException : EsentObsoleteException
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x00013806 File Offset: 0x00011A06
		public EsentTempFileOpenErrorException() : base("Temp file could not be opened", JET_err.TempFileOpenError)
		{
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00013818 File Offset: 0x00011A18
		private EsentTempFileOpenErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
