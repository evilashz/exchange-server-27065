using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public sealed class EsentInvalidBackupSequenceException : EsentUsageException
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x00011982 File Offset: 0x0000FB82
		public EsentInvalidBackupSequenceException() : base("Backup call out of sequence", JET_err.InvalidBackupSequence)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00011994 File Offset: 0x0000FB94
		private EsentInvalidBackupSequenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
