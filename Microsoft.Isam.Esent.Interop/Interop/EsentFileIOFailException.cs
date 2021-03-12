using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public sealed class EsentFileIOFailException : EsentObsoleteException
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00013C12 File Offset: 0x00011E12
		public EsentFileIOFailException() : base("instructs the JET_ABORTRETRYFAILCALLBACK caller to fail the specified I/O", JET_err.FileIOFail)
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00013C24 File Offset: 0x00011E24
		private EsentFileIOFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
