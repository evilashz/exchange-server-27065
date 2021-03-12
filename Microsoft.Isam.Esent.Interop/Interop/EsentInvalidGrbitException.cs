using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000129 RID: 297
	[Serializable]
	public sealed class EsentInvalidGrbitException : EsentUsageException
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0001210E File Offset: 0x0001030E
		public EsentInvalidGrbitException() : base("Invalid flags parameter", JET_err.InvalidGrbit)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00012120 File Offset: 0x00010320
		private EsentInvalidGrbitException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
