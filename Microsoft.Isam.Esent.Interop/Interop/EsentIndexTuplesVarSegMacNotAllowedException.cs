using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001D0 RID: 464
	[Serializable]
	public sealed class EsentIndexTuplesVarSegMacNotAllowedException : EsentUsageException
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00013352 File Offset: 0x00011552
		public EsentIndexTuplesVarSegMacNotAllowedException() : base("tuple index does not allow setting cbVarSegMac", JET_err.IndexTuplesVarSegMacNotAllowed)
		{
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00013364 File Offset: 0x00011564
		private EsentIndexTuplesVarSegMacNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
