using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000C0 RID: 192
	[Serializable]
	public sealed class EsentPageBoundaryException : EsentObsoleteException
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00011592 File Offset: 0x0000F792
		public EsentPageBoundaryException() : base("Reached Page Boundary", JET_err.PageBoundary)
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000115A4 File Offset: 0x0000F7A4
		private EsentPageBoundaryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
