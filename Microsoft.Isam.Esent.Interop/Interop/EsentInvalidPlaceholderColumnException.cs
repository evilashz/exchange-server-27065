using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public sealed class EsentInvalidPlaceholderColumnException : EsentUsageException
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00013646 File Offset: 0x00011846
		public EsentInvalidPlaceholderColumnException() : base("Tried to convert column to a primary index placeholder, but column doesn't meet necessary criteria", JET_err.InvalidPlaceholderColumn)
		{
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00013658 File Offset: 0x00011858
		private EsentInvalidPlaceholderColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
