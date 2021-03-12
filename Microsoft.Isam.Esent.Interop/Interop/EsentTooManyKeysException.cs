using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000135 RID: 309
	[Serializable]
	public sealed class EsentTooManyKeysException : EsentUsageException
	{
		// Token: 0x0600087D RID: 2173 RVA: 0x0001225E File Offset: 0x0001045E
		public EsentTooManyKeysException() : base("Too many columns in an index", JET_err.TooManyKeys)
		{
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00012270 File Offset: 0x00010470
		private EsentTooManyKeysException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
