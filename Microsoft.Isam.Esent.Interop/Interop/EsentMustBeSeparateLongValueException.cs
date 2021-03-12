using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000D1 RID: 209
	[Serializable]
	public sealed class EsentMustBeSeparateLongValueException : EsentUsageException
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0001176E File Offset: 0x0000F96E
		public EsentMustBeSeparateLongValueException() : base("Can only preread long value columns that can be separate, e.g. not size constrained so that they are fixed or variable columns", JET_err.MustBeSeparateLongValue)
		{
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00011780 File Offset: 0x0000F980
		private EsentMustBeSeparateLongValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
