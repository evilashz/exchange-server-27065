using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public sealed class EsentTableDuplicateException : EsentStateException
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x00012F0E File Offset: 0x0001110E
		public EsentTableDuplicateException() : base("Table already exists", JET_err.TableDuplicate)
		{
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00012F20 File Offset: 0x00011120
		private EsentTableDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
