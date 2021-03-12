using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000181 RID: 385
	[Serializable]
	public sealed class EsentFileSystemCorruptionException : EsentCorruptionException
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x00012AAE File Offset: 0x00010CAE
		public EsentFileSystemCorruptionException() : base("File system operation failed with an error indicating the file system is corrupt.", JET_err.FileSystemCorruption)
		{
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00012AC0 File Offset: 0x00010CC0
		private EsentFileSystemCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
