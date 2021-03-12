using System;
using System.Runtime.Serialization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	public sealed class EsentFileAccessDeniedException : EsentIOException
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0001241E File Offset: 0x0001061E
		public EsentFileAccessDeniedException() : base("Cannot access file, the file is locked or in use", JET_err.FileAccessDenied)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00012430 File Offset: 0x00010630
		private EsentFileAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
