using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200010E RID: 270
	[Serializable]
	public sealed class EsentMissingFileToBackupException : EsentInconsistentException
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00011E1A File Offset: 0x0001001A
		public EsentMissingFileToBackupException() : base("Some log or patch files are missing during backup", JET_err.MissingFileToBackup)
		{
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00011E2C File Offset: 0x0001002C
		private EsentMissingFileToBackupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
