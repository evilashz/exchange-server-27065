using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020000E6 RID: 230
	[Serializable]
	public sealed class EsentDeleteBackupFileFailException : EsentIOException
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x000119BA File Offset: 0x0000FBBA
		public EsentDeleteBackupFileFailException() : base("Could not delete backup file", JET_err.DeleteBackupFileFail)
		{
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000119CC File Offset: 0x0000FBCC
		private EsentDeleteBackupFileFailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
