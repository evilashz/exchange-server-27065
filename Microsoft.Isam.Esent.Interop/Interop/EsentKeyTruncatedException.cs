using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public sealed class EsentKeyTruncatedException : EsentStateException
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x000116C6 File Offset: 0x0000F8C6
		public EsentKeyTruncatedException() : base("key truncated on index that disallows key truncation", JET_err.KeyTruncated)
		{
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000116D8 File Offset: 0x0000F8D8
		private EsentKeyTruncatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
