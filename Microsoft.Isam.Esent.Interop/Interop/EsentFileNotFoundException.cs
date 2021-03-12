using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001FF RID: 511
	[Serializable]
	public sealed class EsentFileNotFoundException : EsentStateException
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00013876 File Offset: 0x00011A76
		public EsentFileNotFoundException() : base("File not found", JET_err.FileNotFound)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00013888 File Offset: 0x00011A88
		private EsentFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
