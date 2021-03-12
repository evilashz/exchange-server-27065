using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public sealed class EsentMissingPatchPageException : EsentObsoleteException
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00011AD2 File Offset: 0x0000FCD2
		public EsentMissingPatchPageException() : base("Patch file page not found during recovery", JET_err.MissingPatchPage)
		{
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		private EsentMissingPatchPageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
