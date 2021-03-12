using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public sealed class EsentBadPatchPageException : EsentObsoleteException
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x00011AEE File Offset: 0x0000FCEE
		public EsentBadPatchPageException() : base("Patch file page is not valid", JET_err.BadPatchPage)
		{
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00011B00 File Offset: 0x0000FD00
		private EsentBadPatchPageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
