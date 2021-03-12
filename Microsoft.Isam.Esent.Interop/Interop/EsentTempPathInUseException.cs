using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200016B RID: 363
	[Serializable]
	public sealed class EsentTempPathInUseException : EsentUsageException
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00012846 File Offset: 0x00010A46
		public EsentTempPathInUseException() : base("Temp path already used by another database instance", JET_err.TempPathInUse)
		{
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00012858 File Offset: 0x00010A58
		private EsentTempPathInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
