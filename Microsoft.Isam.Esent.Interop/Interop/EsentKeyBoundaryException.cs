using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C1 RID: 193
	[Serializable]
	public sealed class EsentKeyBoundaryException : EsentObsoleteException
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x000115AE File Offset: 0x0000F7AE
		public EsentKeyBoundaryException() : base("Reached Key Boundary", JET_err.KeyBoundary)
		{
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000115C0 File Offset: 0x0000F7C0
		private EsentKeyBoundaryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
