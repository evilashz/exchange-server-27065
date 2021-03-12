using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000168 RID: 360
	[Serializable]
	public sealed class EsentSystemParamsAlreadySetException : EsentStateException
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x000127F2 File Offset: 0x000109F2
		public EsentSystemParamsAlreadySetException() : base("Global system parameters have already been set", JET_err.SystemParamsAlreadySet)
		{
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00012804 File Offset: 0x00010A04
		private EsentSystemParamsAlreadySetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
