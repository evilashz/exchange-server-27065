using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public sealed class EsentLanguageNotSupportedException : EsentObsoleteException
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0001377A File Offset: 0x0001197A
		public EsentLanguageNotSupportedException() : base("Windows installation does not support language", JET_err.LanguageNotSupported)
		{
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001378C File Offset: 0x0001198C
		private EsentLanguageNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
