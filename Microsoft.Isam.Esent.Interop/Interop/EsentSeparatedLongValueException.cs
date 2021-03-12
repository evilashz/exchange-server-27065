using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D0 RID: 208
	[Serializable]
	public sealed class EsentSeparatedLongValueException : EsentStateException
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x00011752 File Offset: 0x0000F952
		public EsentSeparatedLongValueException() : base("Operation not supported on separated long-value", JET_err.SeparatedLongValue)
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00011764 File Offset: 0x0000F964
		private EsentSeparatedLongValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
