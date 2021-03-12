using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public sealed class EsentIndexInvalidDefException : EsentUsageException
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x00013202 File Offset: 0x00011402
		public EsentIndexInvalidDefException() : base("Illegal index definition", JET_err.IndexInvalidDef)
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00013214 File Offset: 0x00011414
		private EsentIndexInvalidDefException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
