using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public sealed class EsentIndexNotFoundException : EsentStateException
	{
		// Token: 0x06000997 RID: 2455 RVA: 0x000131CA File Offset: 0x000113CA
		public EsentIndexNotFoundException() : base("No such index", JET_err.IndexNotFound)
		{
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000131DC File Offset: 0x000113DC
		private EsentIndexNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
