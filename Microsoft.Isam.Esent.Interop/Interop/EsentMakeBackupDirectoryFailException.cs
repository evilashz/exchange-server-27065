using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public sealed class EsentMakeBackupDirectoryFailException : EsentIOException
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x000119D6 File Offset: 0x0000FBD6
		public EsentMakeBackupDirectoryFailException() : base("Could not make backup temp directory", JET_err.MakeBackupDirectoryFail)
		{
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000119E8 File Offset: 0x0000FBE8
		private EsentMakeBackupDirectoryFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
