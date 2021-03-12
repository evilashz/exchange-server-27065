using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000202 RID: 514
	[Serializable]
	public sealed class EsentLogCorruptedException : EsentCorruptionException
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x000138CA File Offset: 0x00011ACA
		public EsentLogCorruptedException() : base("Logs could not be interpreted", JET_err.LogCorrupted)
		{
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000138DC File Offset: 0x00011ADC
		private EsentLogCorruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
