using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public sealed class EsentPatchFileMissingException : EsentObsoleteException
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00011B26 File Offset: 0x0000FD26
		public EsentPatchFileMissingException() : base("Hard restore detected that patch file is missing from backup set", JET_err.PatchFileMissing)
		{
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00011B38 File Offset: 0x0000FD38
		private EsentPatchFileMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
