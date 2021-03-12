using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020001ED RID: 493
	[Serializable]
	public sealed class EsentRecordNotFoundException : EsentStateException
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0001367E File Offset: 0x0001187E
		public EsentRecordNotFoundException() : base("The key was not found", JET_err.RecordNotFound)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00013690 File Offset: 0x00011890
		private EsentRecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
