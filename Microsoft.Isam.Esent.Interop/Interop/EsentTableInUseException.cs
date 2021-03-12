using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public sealed class EsentTableInUseException : EsentStateException
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x00012F2A File Offset: 0x0001112A
		public EsentTableInUseException() : base("Table is in use, cannot lock", JET_err.TableInUse)
		{
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00012F3C File Offset: 0x0001113C
		private EsentTableInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
