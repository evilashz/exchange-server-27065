using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public sealed class EsentRfsNotArmedException : EsentObsoleteException
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x00011492 File Offset: 0x0000F692
		public EsentRfsNotArmedException() : base("Resource Failure Simulator not initialized", JET_err.RfsNotArmed)
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000114A1 File Offset: 0x0000F6A1
		private EsentRfsNotArmedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
