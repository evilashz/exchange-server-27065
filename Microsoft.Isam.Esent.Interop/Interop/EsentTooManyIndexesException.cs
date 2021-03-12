using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public sealed class EsentTooManyIndexesException : EsentUsageException
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00012242 File Offset: 0x00010442
		public EsentTooManyIndexesException() : base("Too many indexes", JET_err.TooManyIndexes)
		{
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00012254 File Offset: 0x00010454
		private EsentTooManyIndexesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
