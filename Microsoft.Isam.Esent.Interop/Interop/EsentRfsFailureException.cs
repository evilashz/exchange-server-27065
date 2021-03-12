using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000B5 RID: 181
	[Serializable]
	public sealed class EsentRfsFailureException : EsentObsoleteException
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x00011479 File Offset: 0x0000F679
		public EsentRfsFailureException() : base("Resource Failure Simulator failure", JET_err.RfsFailure)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00011488 File Offset: 0x0000F688
		private EsentRfsFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
