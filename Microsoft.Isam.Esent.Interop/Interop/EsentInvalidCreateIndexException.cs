using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C5 RID: 453
	[Serializable]
	public sealed class EsentInvalidCreateIndexException : EsentUsageException
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x0001321E File Offset: 0x0001141E
		public EsentInvalidCreateIndexException() : base("Invalid create index description", JET_err.InvalidCreateIndex)
		{
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00013230 File Offset: 0x00011430
		private EsentInvalidCreateIndexException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
