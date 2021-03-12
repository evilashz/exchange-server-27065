using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public sealed class EsentSystemPathInUseException : EsentUsageException
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0001280E File Offset: 0x00010A0E
		public EsentSystemPathInUseException() : base("System path already used by another database instance", JET_err.SystemPathInUse)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00012820 File Offset: 0x00010A20
		private EsentSystemPathInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
