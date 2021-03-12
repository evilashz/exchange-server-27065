using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public sealed class EsentObjectDuplicateException : EsentObsoleteException
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x0001300A File Offset: 0x0001120A
		public EsentObjectDuplicateException() : base("Table or object name in use", JET_err.ObjectDuplicate)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001301C File Offset: 0x0001121C
		private EsentObjectDuplicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
