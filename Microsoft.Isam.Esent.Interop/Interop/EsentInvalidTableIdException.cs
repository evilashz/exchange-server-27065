using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public sealed class EsentInvalidTableIdException : EsentUsageException
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00012F9A File Offset: 0x0001119A
		public EsentInvalidTableIdException() : base("Invalid table id", JET_err.InvalidTableId)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00012FAC File Offset: 0x000111AC
		private EsentInvalidTableIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
