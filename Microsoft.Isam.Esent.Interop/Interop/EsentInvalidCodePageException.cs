using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public sealed class EsentInvalidCodePageException : EsentUsageException
	{
		// Token: 0x060008C3 RID: 2243 RVA: 0x00012632 File Offset: 0x00010832
		public EsentInvalidCodePageException() : base("Invalid or unknown code page", JET_err.InvalidCodePage)
		{
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00012644 File Offset: 0x00010844
		private EsentInvalidCodePageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
