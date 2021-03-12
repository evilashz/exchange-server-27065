using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	public sealed class EsentBackupNotAllowedYetException : EsentStateException
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x0001199E File Offset: 0x0000FB9E
		public EsentBackupNotAllowedYetException() : base("Cannot do backup now", JET_err.BackupNotAllowedYet)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000119B0 File Offset: 0x0000FBB0
		private EsentBackupNotAllowedYetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
